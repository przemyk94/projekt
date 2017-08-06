using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Pentathlon_Event_Manager
{
    class fillComboBoxes
    {
        /*
         * Wypełnienie ComboBoxa konkurencjami przypisanymi do danych zawodow
         */
        static public void fillCompetitionComboBox(System.Windows.Controls.ComboBox comboBox, long _event)
        {
            using (var db = new appDBEntities())
            {
                var query = from c in db.EventCompetitions
                            join com in db.Competitions  on c.CompetitionID equals com.id
                            where c.EventID == _event
                            orderby c.Order //descending
                            select com.CompetitionName;
                var data = query.ToList();
                foreach (string str in data)
                {
                    comboBox.Items.Add(str);
                } 
                db.Dispose();
            }
        }
        //static public void fillRegionComboBox(System.Windows.Controls.ComboBox comboBox, long _event)
        //{
        //    using (var db = new appDBEntities())
        //    {
        //        var query = from c in db.Regions
                            
        //                    select c.RegionName;
        //        var data = query.ToList();
        //        foreach (string str in data)
        //        {
        //            comboBox.Items.Add(str);
        //        }
        //        db.Dispose();
        //    }
        //}
    }
}
