using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Modern_Pentathlon_Event_Manager
{
    class eventCompetitionUpdateList
    {
        public static void showCompetitionList(DataGrid competitionDataGrid, long _event)
        {
            try
            {
                competitionDataGrid.ItemsSource = null;
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                // Provide for exceptions.
            }

            using (var dt = new appDBEntities())
            {
                var query = from c in dt.Competitions
                            where !(from o in dt.EventCompetitions
                                    where o.EventID == MainWindow._event
                                    select o.CompetitionID).Contains(c.id)
                            select new { c.CompetitionName, c.id };
                var data = query.ToList();
                competitionDataGrid.ItemsSource = data;
                //  foreach (object str in data)
                //    eventParticipantsDataGrid.Items.Add(str);
                dt.Dispose();
            }
        }

        public static void showEventCompetition(DataGrid eventCompetitionDataGrid, long _event)
        {
            try
            {
                eventCompetitionDataGrid.ItemsSource = null;
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                // Provide for exceptions.
            }
            using (var dt = new appDBEntities())
            {
                var query = from c in dt.EventCompetitions
                            join t2 in dt.Competitions on c.CompetitionID equals t2.id
                            where c.EventID == MainWindow._event
                            orderby c.Order //descending
                            select new { t2.CompetitionName, c.CompetitionID, c.Order };
                var data = query.ToList();
                eventCompetitionDataGrid.ItemsSource = data;
                dt.Dispose();
            }
        }

        public static void addCompetitionToEvent(long idCompetition, long idEvent)
        {
            using (var db = new appDBEntities())
            {
                EventCompetitions newItem = new EventCompetitions
                {
                    EventID = idEvent,
                    CompetitionID = idCompetition,
                };
                try
                {
                    db.EventCompetitions.Add(newItem);
                    db.SaveChanges();
                    db.Dispose();
                 //   MessageBox.Show(string.Format("Adding participant with ID = {0}  to EventID = {1}", idParticipant, idEvent));
                }
                catch (Exception f)
                {
                    Console.WriteLine(f);
                    // Provide for exceptions.
                }
            }
        }

        /*
        * Usuwa zaznaczona konkurencje z danej imprezy.
        */
        public static void deleteCompetitionFromEvent(long idCompetition, long idEvent)
        {
            var db = new appDBEntities();
           
            var deleteCompetitionInEvent =
                from EventCompetitions in db.EventCompetitions
                where EventCompetitions.CompetitionID == idCompetition && EventCompetitions.EventID == idEvent
                select EventCompetitions;

            foreach (var EventParticipants in deleteCompetitionInEvent)
            {
                db.EventCompetitions.Remove(EventParticipants);
            }
            try
            {
                db.SaveChanges();
              //  MessageBox.Show(string.Format("Deleting participant with ID = {0}  from EventID = {1}", idParticipant, idEvent));

            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                // Provide for exceptions.
            }
        }

        /*
        * Obsluga dodawania kolejnosci. 
        */
        public static void changeOrder(long idCompetition, long idEvent, int points)
        {
            var db = new appDBEntities();

            if (idCompetition == 0)
            {
                MessageBox.Show(string.Format("Error: idCompetition =  {0},  idEvent = {1}", idCompetition, idEvent));
            }
            else { 
            EventCompetitions c = (from EventCompetitions in db.EventCompetitions
                          where EventCompetitions.CompetitionID == idCompetition && EventCompetitions.EventID == idEvent
                          select EventCompetitions).First();
            if(c.Order == null)
            {
                c.Order = 0;
                c.Order = c.Order + points;
            }
            else { 
            c.Order = c.Order + points;
            }
            try
            {
                db.SaveChanges();
                //  MessageBox.Show(string.Format("Deleting participant with ID = {0}  from EventID = {1}", idParticipant, idEvent));

            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                // Provide for exceptions.
            }
            }
        }



    }
}
