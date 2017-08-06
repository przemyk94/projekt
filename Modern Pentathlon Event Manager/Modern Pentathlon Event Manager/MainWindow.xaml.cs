using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Modern_Pentathlon_Event_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static long _event;
        public static long _competition = 1;
        public MainWindow()
        {
            InitializeComponent();

        }


        /*
         * Pobranie ID zaznaczonej konkurencji. 
         */ 
        public long idAddCompetition()
        {
            dynamic currentItem = competitionDataGrid.SelectedItem;
            long idCompetition = currentItem.id;
            return idCompetition;
        }
        public long idDeleteCompetition()
        {
            try { 
            dynamic currentItem = eventCompetitionDataGrid.SelectedItem;
            long idCompetition = currentItem.CompetitionID;
            return idCompetition;
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                return 0;
            }

        }

        /*
        * Obsluga przycisku -> dodanie nowej konkurencji do wybranych zawodow.
        */
        private void addCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            eventCompetitionUpdateList.addCompetitionToEvent(idAddCompetition(), MainWindow._event);
            competitionDataGrid.ItemsSource = null;
            eventCompetitionDataGrid.ItemsSource = null; 
            eventCompetitionUpdateList.showEventCompetition(eventCompetitionDataGrid, _event);
            eventCompetitionUpdateList.showCompetitionList(competitionDataGrid, _event);
        }
        /*
        * Obsluga przycisku <- usuniecie zaznaczonej konkurencji z wybranych zawodow.
        */
        private void deleteCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            eventCompetitionUpdateList.deleteCompetitionFromEvent(idDeleteCompetition(), MainWindow._event);
            competitionDataGrid.ItemsSource = null;
            eventCompetitionDataGrid.ItemsSource = null;
            eventCompetitionUpdateList.showEventCompetition(eventCompetitionDataGrid, _event);
            eventCompetitionUpdateList.showCompetitionList(competitionDataGrid, _event);
            //  long idParticipant = idDeleteParticipant();
            // long idEvent = MainWindow._event;

        }


        /*
         *Funkcja do wyswietlania listy zawodnikow nalezacych do danych zawodow.  
         */
        static public void fillParticipantList(ComboBox eventNameComboBox, DataGrid participantListDataGrid)
        {
            //Wyczyszczenie poprzedniej listy zawodnikow. 
            try
            {
                participantListDataGrid.ItemsSource = null;
            }
            catch (Exception f)
            {
                Console.WriteLine(f);
                // Provide for exceptions.
            }
            _event = long.Parse(eventNameComboBox.SelectedValuePath);
            eventParticipantUpdateList.showEventParticipants(participantListDataGrid);
        }

        /*
         * Obsluga przycisku do dodawania elementow do bazydanych (gorny panel) 
         */
        private void AddBrowseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddBrowseWindow winAddBrowse = new AddBrowseWindow();
            winAddBrowse.Show();
        }

        /*
         * Obsluga przycisku - wyswietlenie okna odpowiedzialnego za generowanie list startowych.
         */ 
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_event != 0)
            {
                startOrderWindow winStartOrder = new startOrderWindow();
                winStartOrder.Show();
            }
            else
            {
                MessageBox.Show("Please choose event");
            }
        }

        /*
         * Kod automatycznie wygenerowany przez Visual Studio - obsluga Data Sources. 
         */
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Modern_Pentathlon_Event_Manager._event_managerDataSet _event_managerDataSet = ((Modern_Pentathlon_Event_Manager._event_managerDataSet)(this.FindResource("_event_managerDataSet")));
            // Load data into the table Events. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.EventsTableAdapter _event_managerDataSetEventsTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.EventsTableAdapter();
            _event_managerDataSetEventsTableAdapter.Fill(_event_managerDataSet.Events);
            System.Windows.Data.CollectionViewSource eventsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("eventsViewSource")));
            eventsViewSource.View.MoveCurrentToFirst();
            // Load data into the table EventParticipants. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.EventParticipantsTableAdapter _event_managerDataSetEventParticipantsTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.EventParticipantsTableAdapter();
            _event_managerDataSetEventParticipantsTableAdapter.Fill(_event_managerDataSet.EventParticipants);
            System.Windows.Data.CollectionViewSource eventsEventParticipantsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("eventsEventParticipantsViewSource")));
            eventsEventParticipantsViewSource.View.MoveCurrentToFirst();
        }

        /*
         * Obsluga przycisku - wyswietlanie zawodnikow nalezacych do wybranych zawodow
         */
        private void chooseEventButton_Click(object sender, RoutedEventArgs e)
        {

            fillParticipantList(eventNameComboBox, participantsDataGrid);
            eventCompetitionUpdateList.showEventCompetition(eventCompetitionDataGrid, _event);
            eventCompetitionUpdateList.showCompetitionList(competitionDataGrid, _event);
        }

        /*
         * Obsluga przycisku - wyswietlenie okna odpowiadajacego za dodawanie i usuwanie zawodnikow wybranych zawodow
         */
        private void addDeleteParticipants_Click(object sender, RoutedEventArgs e)
        {
            if (_event != 0)
            {
                addDeleteParticipantsWindow winAddDeleteParticipants = new addDeleteParticipantsWindow();
                winAddDeleteParticipants.Show();
            }
            else
            {
                MessageBox.Show("Please choose event");
            }
        }

        /*
         * Obsluga przycisku - generowanie nowej listy startowej.
         */
        private void newStartOrderButton_Click(object sender, RoutedEventArgs e)
        {
            startOrder.newSwimmingStartOrder(1);
            startOrder.newSwimmingStartOrder(2);
        }
         /*
         * Obsluga przycisku - przypisywanie kolejnosci konkurencji. 
         */
        private void higherOrderButton_Click(object sender, RoutedEventArgs e)
        {
            eventCompetitionUpdateList.changeOrder(idDeleteCompetition(), _event, 1);
            eventCompetitionUpdateList.showEventCompetition(eventCompetitionDataGrid, _event);
        }

        private void lowerOrderButton_Click(object sender, RoutedEventArgs e)
        {
            eventCompetitionUpdateList.changeOrder(idDeleteCompetition(), _event, -1);
            eventCompetitionUpdateList.showEventCompetition(eventCompetitionDataGrid, _event);
        }

        private void participantsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
