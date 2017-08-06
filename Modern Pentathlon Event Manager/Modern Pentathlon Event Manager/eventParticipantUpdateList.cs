using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Modern_Pentathlon_Event_Manager
{
    class eventParticipantUpdateList
    {
        /*
         * Wyswietlanie listy uczestnikow przypisanych do aktualnie wybranych zawodow.  
        */
        public static void showEventParticipants(DataGrid eventParticipantsDataGrid)
        {
            using (var dt = new appDBEntities())
            {
                var query = from c in dt.EventParticipants
                            join t2 in dt.Participant on c.ParticipantID equals t2.id
                            join t3 in dt.Sex on t2.Sex equals t3.id
                            join t4 in dt.Club on t2.Club equals t4.id
                            where c.EventID == MainWindow._event
                            select new { c.ParticipantID, t2.Surname, t2.Name, t3.SexName, t2.City, t2.BirthYear, t4.ClubName, t2.RegTime };
                var data = query.ToList();
                eventParticipantsDataGrid.ItemsSource = data;
                //  foreach (object str in data)
                //    eventParticipantsDataGrid.Items.Add(str);
                dt.Dispose();
            }
        }

        /*
        * Wyswietlanie listy uczestnikow przypisanych do aktualnie wybranych zawodow.  
        */
        public static void showParticipants(DataGrid participantDataGrid)
        {
            using (var dt = new appDBEntities())
            {
                var query = from c in dt.Participant
                            join t2 in dt.Club on c.Club equals t2.id
                            join t3 in dt.Sex on c.Sex equals t3.id
                            join t4 in dt.Nation on c.Nationality equals t4.id
                           // where c.EventID == MainWindow._event
                            select new { c.id, c.Surname, c.Name, t3.SexName, c.BirthYear, t2.ClubName, c.City, c.RegTime, t4.NationName };
                var data = query.ToList();
                participantDataGrid.ItemsSource = data;
                //  foreach (object str in data)
                //    eventParticipantsDataGrid.Items.Add(str);
                dt.Dispose();
            }
        }

        /*
        * Przypisanie zaznaczonego zawodnika do aktualnie wybranych zawodow. 
        */

        public static void addParticipantToEvent(long idParticipant, long idEvent)
        {
            using (var db = new appDBEntities())
            {
                EventParticipants newItem = new EventParticipants
                {
                    EventID = idEvent,
                    ParticipantID = idParticipant,
                };
                try
                {
                    db.EventParticipants.Add(newItem);
                    db.SaveChanges();
                    db.Dispose();
                    MessageBox.Show(string.Format("Adding participant with ID = {0}  to EventID = {1}", idParticipant, idEvent));
                }
                catch (Exception f)
                {
                    Console.WriteLine(f);
                    // Provide for exceptions.
                }
            }
        }


        /*
        * Usuwa zawodnika z danej imprezy. Usuwany jest zawodnik ktory zostal zaznaczony w prawym oknie AddDeleteParticipantsWindow.
        */
        public static void deleteParticipantFromEvent(long idParticipant, long idEvent)
        {
            var db = new appDBEntities();

            var deleteParticipantInEvent =
                from EventParticipants in db.EventParticipants
                where EventParticipants.ParticipantID == idParticipant && EventParticipants.EventID == idEvent
                select EventParticipants;

            foreach (var EventParticipants in deleteParticipantInEvent)
            {
                db.EventParticipants.Remove(EventParticipants);
            }
            try
            {
                db.SaveChanges();
                MessageBox.Show(string.Format("Deleting participant with ID = {0}  from EventID = {1}", idParticipant, idEvent));

            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                // Provide for exceptions.
            }
        }

    }
}
