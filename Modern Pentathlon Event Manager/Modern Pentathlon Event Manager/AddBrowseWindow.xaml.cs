using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Modern_Pentathlon_Event_Manager
{
    /// <summary>
    /// Interaction logic for AddBrowseWindow.xaml
    /// </summary>
    public partial class AddBrowseWindow : Window
    {
        
        int currentYear = DateTime.Now.Year; //aktualna data
                                            
        public AddBrowseWindow()
        {
            InitializeComponent();
            browseDataBase.showParticipants(participantDataGrid);
            browseDataBase.showEvents(eventsDataGrid);
            //browseDataBase.showCompetitions(competitionsDataGrid);
            browseDataBase.showClubs(clubsDataGrid);
        }
        /*
         * Wygenerowane autoamtycznie przez Data Sources
         */
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Modern_Pentathlon_Event_Manager._event_managerDataSet _event_managerDataSet = ((Modern_Pentathlon_Event_Manager._event_managerDataSet)(this.FindResource("_event_managerDataSet")));
            // Load data into the table Participant. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.ParticipantTableAdapter _event_managerDataSetParticipantTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.ParticipantTableAdapter();
            _event_managerDataSetParticipantTableAdapter.Fill(_event_managerDataSet.Participant);
            System.Windows.Data.CollectionViewSource participantViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("participantViewSource")));
            participantViewSource.View.MoveCurrentToFirst();
            // Load data into the table Events. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.EventsTableAdapter _event_managerDataSetEventsTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.EventsTableAdapter();
            _event_managerDataSetEventsTableAdapter.Fill(_event_managerDataSet.Events);
            System.Windows.Data.CollectionViewSource eventsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("eventsViewSource")));
            eventsViewSource.View.MoveCurrentToFirst();
            // Load data by setting the CollectionViewSource.Source property:


            // Load data into the table Sex. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.SexTableAdapter _event_managerDataSetSexTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.SexTableAdapter();
            _event_managerDataSetSexTableAdapter.Fill(_event_managerDataSet.Sex);
            System.Windows.Data.CollectionViewSource sexViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("sexViewSource")));
            sexViewSource.View.MoveCurrentToFirst();
            // Load data into the table Club. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.ClubTableAdapter _event_managerDataSetClubTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.ClubTableAdapter();
            _event_managerDataSetClubTableAdapter.Fill(_event_managerDataSet.Club);
            System.Windows.Data.CollectionViewSource clubViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clubViewSource")));
            clubViewSource.View.MoveCurrentToFirst();
            // Load data into the table Nation. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.NationTableAdapter _event_managerDataSetNationTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.NationTableAdapter();
            _event_managerDataSetNationTableAdapter.Fill(_event_managerDataSet.Nation);
            System.Windows.Data.CollectionViewSource nationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("nationViewSource")));
            nationViewSource.View.MoveCurrentToFirst();
            // Load data into the table Competitions. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.CompetitionsTableAdapter _event_managerDataSetCompetitionsTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.CompetitionsTableAdapter();
            _event_managerDataSetCompetitionsTableAdapter.Fill(_event_managerDataSet.Competitions);
            System.Windows.Data.CollectionViewSource competitionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("competitionsViewSource")));
            competitionsViewSource.View.MoveCurrentToFirst();
            // Load data into the table Regions. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.RegionsTableAdapter _event_managerDataSetRegionsTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.RegionsTableAdapter();
            _event_managerDataSetRegionsTableAdapter.Fill(_event_managerDataSet.Regions);
            System.Windows.Data.CollectionViewSource regionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("regionsViewSource")));
            regionsViewSource.View.MoveCurrentToFirst();
        }

        /*
         * Obsluga przycisku - dodawanie nowych zawodnikow. 
         */
        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// Wywolanie funkcji odpowiedzalnej za zapisywanie 
                /// nowych participantow do bazy danych
                addToDB.addParticipant(ParticipantSurnameTextBox,
                ParticipantNameTextBox,
                ParticipantCityTextBox,
                ParticipantBirthYearTextBox,
                ParticipantBirthMonthTextBox,
                ParticipantBirthDayTextBox,
                sexNameComboBox,
                clubNameComboBox,
                nationNameComboBox,
                ParticipantRegTimeMinTextBox,
                ParticipantRegTimeSecTextBox,
                ParticipantRegTimeCentTextBox);
                //Wyswietlenie na nowo listy zawodnikow. 
                browseDataBase.showParticipants(participantDataGrid);
            }
            catch (Exception ex)
            {
                string fail = string.Format("Problem : \n {0}", ex.Message);
                MessageBox.Show(fail, "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// Wywolanie funkcji odpowiedzalnej za zapisywanie 
                /// nowych participantow do bazy danych
                addToDB.addEvent(EventNameTextBox,
                EventPlaceTextBox,
                EventDateYearTextBox,
                EventDateMonthTextBox,
                EventDateDayTextBox,
                EventNationComboBox,
                EventRegionComboBox);
                //Wyswietlenie na nowo listy zawodnikow. 
                browseDataBase.showEvents(eventsDataGrid);
            }
            catch (Exception ex)
            {
                string fail = string.Format("Problem : \n {0}", ex.Message);
                MessageBox.Show(fail, "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EventRegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EventDateDayTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EventNationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddClubButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// Wywolanie funkcji odpowiedzalnej za zapisywanie 
                /// nowych participantow do bazy danych
                addToDB.addClub(ClubNameTextBox,
                ClubRegionComboBox);
                //Wyswietlenie na nowo listy zawodnikow. 
                browseDataBase.showClubs(clubsDataGrid);
            }
            catch (Exception ex)
            {
                string fail = string.Format("Problem : \n {0}", ex.Message);
                MessageBox.Show(fail, "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClubRegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void eventsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ParticipantBirthYearTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}