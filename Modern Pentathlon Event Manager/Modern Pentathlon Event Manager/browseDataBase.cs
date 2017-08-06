using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Modern_Pentathlon_Event_Manager
{
     /*
     *Tutaj Znajduja sie funkcje odpowiedzialne za wyswietlanie danych w oknie "AddBrowseWindow".  
     *Okno sluzy do wprowadzania i edytowania danych znajdujacych sie w bazie danych takich jak:
     * zawodnicy, kluby, regiony, zawody. 
     */
    class browseDataBase
    {
        /*
         * Czyszczenie danych znajdujacych sie w DataGridzie.
         * Uzywac przed odswiezeniem/dodaniem/usunieciem danych w datagridzie. 
         */
        public static void cleanDataGrid(DataGrid DataGrid)
        {
            try
            {
                DataGrid.ItemsSource = null;
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                // Provide for exceptions.
            }
        }

        /*
         * Wyswietlanie list zawodnikow zapisanych w bazie danych. 
         */
        public static void showParticipants(DataGrid participantsDataGrid)
        {
            cleanDataGrid(participantsDataGrid);

            using (var db = new appDBEntities())
            {
                var query = from c in db.Participant
                            join gender in db.Sex on c.Sex equals gender.id
                            join club in db.Club on c.Club equals club.id
                            join nation in db.Nation on c.Nationality equals nation.id
                            select new {c.id, c.Surname, c.Name, gender.SexName, c.City, c.BirthYear, club.ClubName, c.RegTime, nation.NationName };
                var data = query.ToList();
                participantsDataGrid.ItemsSource = data;
                db.Dispose();
            }

        }

        /*
         * Wyswietlanie listy klubow zapisanych w bazie danych. 
         */
        public static void showClubs(DataGrid clubsDataGrid)
        {
            cleanDataGrid(clubsDataGrid);

            using (var db = new appDBEntities())
            {
                var query = from c in db.Club
                            join regions in db.Regions on c.ClubRegion equals regions.id
                            select new { c.id, c.ClubName, regions.RegionName };
                var data = query.ToList();
                clubsDataGrid.ItemsSource = data;
                db.Dispose();
            }

        }
         /*
         * Wyswietlanie listy wydarzen zapisanych w bazie danych. 
         */
        public static void showEvents(DataGrid eventsDataGrid)
        {
            cleanDataGrid(eventsDataGrid);

            using (var db = new appDBEntities())
            {
                var query = from c in db.Events
                            join regions in db.Regions on c.EventRegion equals regions.id
                            join nations in db.Nation on c.EventNation equals nations.id
                            select new { c.id, c.EventName, c.EventPlace, c.EventDate, regions.RegionName, nations.NationName };
                var data = query.ToList();
                eventsDataGrid.ItemsSource = data;
                db.Dispose();
            }

        }
        /*
        * Wyswietlanie listy konkurencji zapisanych w bazie danych. 
        */
        public static void showCompetitions(DataGrid competitionsDataGrid)
        {
            cleanDataGrid(competitionsDataGrid);

            using (var db = new appDBEntities())
            {
                var query = from c in db.Competitions
                            select new { c.id, c.CompetitionName };
                var data = query.ToList();
                competitionsDataGrid.ItemsSource = data;
                db.Dispose();
            }

        }




    }
}
