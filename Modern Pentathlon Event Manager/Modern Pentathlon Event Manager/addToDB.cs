using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Modern_Pentathlon_Event_Manager
{
    //Klasa do dodawania nowych zawodnikow do bazydanych.
    class addToDB
    {
        
        static public void addParticipant(
            TextBox ParticipantSurnameTextBox,
            TextBox ParticipantNameTextBox,
            TextBox ParticipantCityTextBox,
            TextBox ParticipantBirthYearTextBox,
            TextBox ParticipantBirthMonthTextBox,
            TextBox ParticipantBirthDayTextBox,
            ComboBox sexNameComboBox,
            ComboBox clubNameComboBox,
            ComboBox nationNameComboBox,
            TextBox ParticipantRegTimeMinTextBox,
            TextBox ParticipantRegTimeSecTextBox,
            TextBox ParticipantRegTimeCentTextBox)
        {
            // Konwersja danych znajdujacych sie w polach wypelnianych przez uzytkownika. 
            int _BirthYear = int.Parse(ParticipantBirthYearTextBox.Text);
            int _BirthMonth = int.Parse(ParticipantBirthMonthTextBox.Text);
            int _BirthDay = int.Parse(ParticipantBirthDayTextBox.Text);
            long _sex = long.Parse(sexNameComboBox.SelectedValuePath.ToString());
            long _clubName = long.Parse(clubNameComboBox.SelectedValuePath.ToString());
            long _nationName = long.Parse(nationNameComboBox.SelectedValuePath.ToString());
            string _nationName1 = nationNameComboBox.Text;
            string _clubName1 = clubNameComboBox.Text;
            string _sex1 = sexNameComboBox.Text;
            string _regTime = participantRegTime(ParticipantRegTimeMinTextBox, ParticipantRegTimeSecTextBox, ParticipantRegTimeCentTextBox);

            //Sprawdzenie poprawnosci wprowadzaonych danych przez uzytkownika. 
            if (checkIsDataCorrect(_BirthDay, _BirthMonth, _BirthYear, _regTime)) {

                // Zapisanie wartosci do bazy danych. 
                    using (var db = new appDBEntities())
                    {
                        Participant newItem = new Participant
                        {
                            Surname = ParticipantSurnameTextBox.Text,
                            Name = ParticipantNameTextBox.Text,
                            BirthDay = _BirthYear,
                            BirthMonth = _BirthMonth,
                            BirthYear = _BirthDay,
                            City = ParticipantCityTextBox.Text,
                            Sex = _sex,
                            Club = _clubName,
                            Nationality = _nationName,
                            RegTime = _regTime
                        };
                            //Obsluga bazy danych.
                            db.Participant.Add(newItem);
                            db.SaveChanges();
                            db.Dispose();

                    //Wyswietlenie komunikatu o poprawnosci wprowadzonych danych.
                    string inf = string.Format("Participant added :\n{0} {1}\n\nSex: {2},  Birth: {3}-{4}-{5},\nCity: {6},  Club: {7}, Nation: {8}\nEntry Time: {9}",
                    newItem.Surname, newItem.Name, _sex1,
                    newItem.BirthDay, newItem.BirthMonth, newItem.BirthYear,
                    newItem.City, _clubName1, _nationName1, _regTime);
                    MessageBox.Show(inf, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Czyszczenie pol wypelnianych przez uzytkownika.
                    ParticipantBirthYearTextBox.Text = "YYYY";
                    ParticipantBirthMonthTextBox.Text = "MM";
                    ParticipantBirthDayTextBox.Text = "DD";

                    sexNameComboBox.Text = null;
                    clubNameComboBox.Text = null;
                    nationNameComboBox.Text = null;
                    ParticipantNameTextBox.Text = null;
                    ParticipantSurnameTextBox.Text = null;
                    ParticipantRegTimeMinTextBox.Text = "mm";
                    ParticipantRegTimeSecTextBox.Text = "ss";
                    ParticipantRegTimeCentTextBox.Text = "cccc";
                    ParticipantCityTextBox.Text = null;
                }//end using
            }//end if
            else
            {
                MessageBox.Show("Write a correct birth date.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            } //end else
        }//end funkcji addParticipant

        static bool checkIsDataCorrect(int BirthDay, int BirthMonth, int BirthYear, string RegTime)
        {
            int currentYear = DateTime.Now.Year;

            ///
            /// UWAGA TUTAJ WARTO ZROBIC FUNKCJE KTORA BEDZIE OGARNIALA CZY UZYTKOWNIK 
            /// NIE WPISZE DATY UR ZAWODNIKA NP. 30.02.2017 (DD,MM,RRRR) LUB 
            /// WPISZE ZE KTOS URODZIL SIE 31 W MIESIACU KTORY MA 30 DNI 
            /// 

            if (BirthDay > 0 && BirthMonth > 0 && BirthMonth <= 12 && BirthYear > 1900 && BirthYear <= currentYear &&
                (BirthMonth == 1 && BirthDay <= 31) ||
            (BirthMonth == 2 && BirthDay <= 29) ||
            (BirthMonth == 3 && BirthDay <= 31) ||
            (BirthMonth == 4 && BirthDay <= 30) ||
            (BirthMonth == 5 && BirthDay <= 31) ||
            (BirthMonth == 6 && BirthDay <= 30) ||
            (BirthMonth == 7 && BirthDay <= 31) ||
            (BirthMonth == 8 && BirthDay <= 31) ||
            (BirthMonth == 9 && BirthDay <= 30) ||
            (BirthMonth == 10 && BirthDay <= 31) ||
            (BirthMonth == 11 && BirthDay <= 30) ||
            (BirthMonth == 12 && BirthDay <= 31))
            {
                return true;
            }
            return false;
        }
        static bool checkIsDataCorrectFuture(int BirthDay, int BirthMonth, int BirthYear, string RegTime)
        {
            int currentYear = DateTime.Now.Year;

            if (BirthDay > 0 && BirthMonth > 0 && BirthMonth <= 12 && BirthYear > 2015  &&
                (BirthMonth == 1 && BirthDay <= 31) ||
            (BirthMonth == 2 && BirthDay <= 29) ||
            (BirthMonth == 3 && BirthDay <= 31) ||
            (BirthMonth == 4 && BirthDay <= 30) ||
            (BirthMonth == 5 && BirthDay <= 31) ||
            (BirthMonth == 6 && BirthDay <= 30) ||
            (BirthMonth == 7 && BirthDay <= 31) ||
            (BirthMonth == 8 && BirthDay <= 31) ||
            (BirthMonth == 9 && BirthDay <= 30) ||
            (BirthMonth == 10 && BirthDay <= 31) ||
            (BirthMonth == 11 && BirthDay <= 30) ||
            (BirthMonth == 12 && BirthDay <= 31))
            {
                return true;
            }
            return false;
        }

        static string participantRegTime(TextBox Minutes, TextBox Seconds, TextBox Centesimal)
        {
            string min = Minutes.Text;
            string sec = Seconds.Text;
            string cent = Centesimal.Text;
            string RegTime = string.Format("{0}:{1}.{2}", min, sec, cent);
            return RegTime;
        }
        static string DateFormat(TextBox Days, TextBox Months, TextBox Years)
        {
            string y = Years.Text;
            string m = Months.Text;
            string d =Days.Text;
            string DataFormat = string.Format("{0}.{1}.{2}", y, m, d);
            return DataFormat;
        }
        public static void dbSaveChanges(appDBEntities db) { 
         try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
        }
        ///EVENT
        static public void addEvent(
            TextBox EventNameTextBox,
            TextBox EventPlaceTextBox,
            TextBox EventDateYearTextBox,
            TextBox EventDateMonthTextBox,
            TextBox EventDateDayTextBox,
            ComboBox EventNationComboBox,
            ComboBox EventRegionComboBox)
        {
            // Wypełnienie comboboxa
            //fillComboBoxes.fillRegionComboBox(EventRegionComboBox, MainWindow._event);
            // Konwersja danych znajdujacych sie w polach wypelnianych przez uzytkownika. 
            int _EventDateYear = int.Parse(EventDateYearTextBox.Text);
            int _EventDateMonth = int.Parse(EventDateMonthTextBox.Text);
            int _EventDateDay = int.Parse(EventDateDayTextBox.Text);
            long _NationName = long.Parse(EventNationComboBox.SelectedValuePath.ToString());
            string _NationName1 = EventNationComboBox.Text;
            // long _RegionName = long.Parse(RegionNameComboBox.SelectedValuePath.ToString());
            string _EventName = EventNameTextBox.Text;
            string _EventPlace = EventPlaceTextBox.Text;
            long _EventRegion = long.Parse(EventRegionComboBox.SelectedValuePath.ToString());
            string _EventRegion1 = EventRegionComboBox.Text;
            string _Date = DateFormat(EventDateYearTextBox, EventDateMonthTextBox, EventDateDayTextBox);
            //Sprawdzenie poprawnosci wprowadzaonych danych przez uzytkownika. 
            if (checkIsDataCorrectFuture(_EventDateDay, _EventDateMonth, _EventDateYear, _Date))
            {

                // Zapisanie wartosci do bazy danych. 
                using (var db = new appDBEntities())
                {

                    Events newItem = new Events
                    {
                        EventName = EventNameTextBox.Text,
                        EventPlace = EventPlaceTextBox.Text,
                        EventDate = _Date,
                        EventNation = _NationName,
                        EventRegion = _EventRegion                                                                    
                    };
                    try { 
                    //Obsluga bazy danych.
                    db.Events.Add(newItem);
                    db.SaveChanges();
                    db.Dispose();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        MessageBox.Show(string.Format("Error - {0}", e), "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //Wyswietlenie komunikatu o poprawnosci wprowadzonych danych.
                    string inf = string.Format("Event added :\n{0}\nPlace: {1}\nDate: {2}\nNation: {3}\nRegion: {4}",
                    newItem.EventName, newItem.EventPlace, newItem.EventDate,
                    _NationName1, _EventRegion1);
                    MessageBox.Show(inf, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Czyszczenie pol wypelnianych przez uzytkownika.
                    EventDateYearTextBox.Text = "YYYY";
                    EventDateMonthTextBox.Text = "MM";
                    EventDateDayTextBox.Text = "DD";

                    EventNationComboBox.Text = null;
                    EventNameTextBox.Text = null;
                    EventPlaceTextBox.Text = null;
                    EventRegionComboBox.Text = null;
                   
                }//end using
            }//end if
            else
            {
                MessageBox.Show("Write a correct date.", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            } //end else
        }//end funkcji addEvent





        /// EVENTS
        ///CLUBS
        static public void addClub(
            TextBox ClubNameTextBox,
            ComboBox ClubRegionComboBox)
        {
            
            // Konwersja danych znajdujacych sie w polach wypelnianych przez uzytkownika. 
                       
            string _ClubName = ClubNameTextBox.Text;
            
            long _ClubRegion = long.Parse(ClubRegionComboBox.SelectedValuePath.ToString());
            string _ClubRegion1 = ClubRegionComboBox.Text;
            
                // Zapisanie wartosci do bazy danych. 
                using (var db = new appDBEntities())
                {

                    Club newItem = new Club
                    {
                        ClubName = ClubNameTextBox.Text,
                        
                        ClubRegion = _ClubRegion
                    };
                    try
                    {
                        //Obsluga bazy danych.
                        db.Club.Add(newItem);
                        db.SaveChanges();
                        db.Dispose();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        MessageBox.Show(string.Format("Error - {0}", e), "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //Wyswietlenie komunikatu o poprawnosci wprowadzonych danych.
                    string inf = string.Format("Club added :\n{0}\nRegion: {1}",
                    newItem.ClubName,_ClubRegion1);
                    MessageBox.Show(inf, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    //Czyszczenie pol wypelnianych przez uzytkownika.
                    
                    ClubNameTextBox.Text = null;
                   
                    ClubRegionComboBox.Text = null;

                }//end using
           
        }//end funkcji addClub
    }//end classToDB
}//end namespace


