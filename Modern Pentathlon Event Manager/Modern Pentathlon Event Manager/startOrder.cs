using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Modern_Pentathlon_Event_Manager.generateStartOrders;

namespace Modern_Pentathlon_Event_Manager
{
    class startOrder
    {
  
        /*
         * Funkcja czysci poprzednia liste startowa. 
         */
        public static void clearStartOrder(int gender)
        {
            var db = new appDBEntities();
            var deleteStartOrder =
                from StartOrders in db.StartOrders
                join t2 in db.Participant on StartOrders.FKParticipant equals t2.id
                where StartOrders.FKevent == MainWindow._event && StartOrders.FKcompetition == MainWindow._competition
                && t2.Sex == gender
                select StartOrders;

            foreach (var StartOrders in deleteStartOrder)
            {
                db.StartOrders.Remove(StartOrders);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
            }
        }

        /*
         * Funkcja wypelnia liste startowa zawodnikami przypisanymi do danych zawodow. 
         */
        public static void fillStartOrder(int gender)
        {
            using (var dt = new appDBEntities())
            {
                var query = from c in dt.EventParticipants
                            join t2 in dt.Participant on c.ParticipantID equals t2.id
                            join t3 in dt.Sex on t2.Sex equals t3.id
                            join t4 in dt.Club on t2.Club equals t4.id
                            where c.EventID == MainWindow._event && t2.Sex == gender
                            select new { c.id, c.ParticipantID, t2.Surname, t2.Name, t3.SexName, t2.BirthYear, t4.ClubName, t2.RegTime };

                /* TO DO LIST
                 * POBIERZ LISTE ZAWODNIKOW Z WYBRANYCH ZAWODOW
                 * DODAJ ZAWODNIKOW DO TABELI STARTORDERS
                 * WYPELNIJ SERIE/TORY STARTOWE WG WZORU
                 */
                foreach (var a in query)
                {
                    StartOrders newItem = new StartOrders
                    {
                        FKevent = MainWindow._event,
                        FKeventParticipant = a.id,
                        FKcompetition = MainWindow._competition,
                        FKParticipant = a.ParticipantID,
                        EntryTime = a.RegTime
                    };
                    //Dodaj
                    dt.StartOrders.Add(newItem);
                }
                try
                {
                    dt.SaveChanges();
                    //      MessageBox.Show(string.Format("Deleting participant with ID = {0}  from EventID = {1}", idParticipant, idEvent));
                }
                catch (Exception f)
                {
                    Console.WriteLine(f);
                    // Provide for exceptions.
                }


            }
        }


        /*
         * Funkcja wypelnia liste startowa zawodnikami przypisanymi do danych zawodow. 
         */
        public static void swimmingGenerateStartOrder(int gender)
        {
            /*
             * Pobieramy liste zawodnikow bioracych udzial w zawodach z podzialem na plec - mezczyzni lub kobiety
             */ 
            using (var db = new appDBEntities())
            {
                var query = from c in db.StartOrders
                            join participant in db.Participant on c.FKParticipant equals participant.id
                            join sex in db.Sex on participant.Sex equals sex.id
                            where c.FKevent == MainWindow._event &&
                            c.FKcompetition == MainWindow._competition &&
                            participant.Sex == gender
                            orderby c.EntryTime
                            // where c.EventID == MainWindow._event
                            select c;
                            /*new {participant.Surname, participant.Name, sex.SexName, participant.BirthYear,
                            c.Number, c.Heat, c.Track, c.EntryTime};*/
                int count_participants = query.Count(); //liczba uczestnikow 
                int count_lanes = 6; //liczba torow 
                int[] heats_table; //tablica przechowujaca liczbe osob startujacej w danej serii
                CountHeats countHeats = new CountHeats(count_participants, count_lanes); //policz liczbe serii
                heats_table = countHeats.heats(); //policz liczbe osob w kazdej serii
                int count_heats = heats_table.Count(); //zmienna przechowujaca liczbe serii
                int[] lanes = { 3, 4, 2, 5, 1, 6}; //kolejnosc rozdawania torow dla uczestnikow
                int lane_num = 0;
                int number = 1; //zmienna do okreslania numeru zawodnika
                /*
                 * Petla przypisujaca kazdemu zawodnikowi numer, tor i serie w ktorej plynie.
                 */
                foreach (var c in query) 
                {
                    c.Number = number;
                    if (heats_table[count_heats - 1] != 0)
                    {
                        if (heats_table[count_heats - 1] > 0)
                        {
                            c.Heat = count_heats;
                            c.Track = lanes[lane_num];
                            lane_num++;
                            heats_table[count_heats - 1]--;
                        }
                    }
                    else
                    {
                        lane_num = 0;
                        count_heats = count_heats - 1;
                        c.Heat = count_heats;
                        c.Track = lanes[lane_num];
                        heats_table[count_heats - 1]--;
                        lane_num++;
                    }
                    
                    number++;
                }
                /* zapisanie zmian do bazy danych */
                addToDB.dbSaveChanges(db);
            }
        }

        public static void newSwimmingStartOrder(int gender)
        {
            clearStartOrder(gender);
            fillStartOrder(gender);
            swimmingGenerateStartOrder(gender);
        }
        /*
        * Wyswietlanie list startowych dla danej konkurencji. 
        */
        public static void showStartOrder(DataGrid dataGrid, int gender, int competition)
        {
            browseDataBase.cleanDataGrid(dataGrid);
            try
            {
                using (var db = new appDBEntities())
                {
                    var query = from c in db.StartOrders
                                join participant in db.Participant on c.FKParticipant equals participant.id
                                join sex in db.Sex on participant.Sex equals sex.id
                                join club in db.Club on participant.Club equals club.id
                                where c.FKevent == MainWindow._event &&
                                c.FKcompetition == competition &&
                                participant.Sex == gender
                                // where c.EventID == MainWindow._event
                                select new
                                {
                                    participant.Surname,
                                    participant.Name,
                                    sex.SexName,
                                    participant.BirthYear,
                                    club.ClubName,
                                    c.Heat,
                                    c.Track,
                                    c.EntryTime
                                };
                    var query1 = query.OrderBy(c => c.Heat).ThenBy(c => c.Track);
                    var data = query1.ToList();
                    dataGrid.ItemsSource = data;
                    db.Dispose();
                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                // Provide for exceptions.
            }

        }

        /*
         * Pobranie ID wybranej konkurencji - wymagane do dalszych funkcji. 
         */ 
        public static int idSelctedCompetition(ComboBox competitionComboBox)
        {
            try
            {
                dynamic currentItem = competitionComboBox.SelectedItem;
                string lookingFor = currentItem;
                using (var db = new appDBEntities())
                {
                    var query = (from c in db.Competitions
                                 where c.CompetitionName == lookingFor
                                 select c.id).FirstOrDefault();
                    return int.Parse(query.ToString());
                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                return 0;
            }
        }


        public static int checkPreviousCompetitionScoresDone(int Event, int Order)
        {
            try
            {
                using (var db = new appDBEntities())
                {
                    var query = (from c in db.EventCompetitions
                                 where c.EventID == Event && c.Order == Order - 1
                                 join comp in db.Competitions on c.CompetitionID equals comp.id
                                 select c.ScoresDone).FirstOrDefault();
                    return int.Parse(query.ToString());
                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                return 0;
            }
        }

        /*
         * Pobiera dane dotyczace kolejnosci w ktorej bedzie rozgrywana wybrana konkurencja. 
         */
        public static int orderCompetition(ComboBox competitionComboBox, int Event, int competition)
        {
            try
            {
                dynamic currentItem = competitionComboBox.SelectedItem;
                string lookingFor = currentItem;
                using (var db = new appDBEntities())
                {
                    var query = (from c in db.EventCompetitions
                                 join comp  in db.Competitions on c.CompetitionID equals comp.id
                                 where comp.CompetitionName == lookingFor
                                 select comp.StartOrder).FirstOrDefault();
                    return int.Parse(query.ToString());
                }
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                return 0;
            }
        }

        /*
         * Główna funkcja obslugujaca tworzenie list startowych. 
         */
        public static void newStartOrder(int Event, string competition, int order, bool scoresDone)
        {
            if(order == 1)
            {
                switch(competition)
                {
                    case "Swimming":
                        newSwimmingStartOrder(1);
                        newSwimmingStartOrder(2);
                        break;
                    case "Running":

                        break;

                    case "Fencing":

                        break;

                    case "Laser-Run":

                        break;

                    case "Shooting":

                        break;

                    case "Riding":

                        break;

                }
            }
            else
            {
                //if order blabla
            }
        }

    }
}
