using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Modern_Pentathlon_Event_Manager
{
    /// <summary>
    /// Interaction logic for addDeleteParticipantsWindow.xaml
    /// </summary>
    public partial class addDeleteParticipantsWindow : Window
    {
        public addDeleteParticipantsWindow()
        {
            InitializeComponent();
            eventParticipantUpdateList.showEventParticipants(eventParticipantsDataGrid);
            eventParticipantUpdateList.showParticipants(participantDataGrid);
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Modern_Pentathlon_Event_Manager._event_managerDataSet _event_managerDataSet = ((Modern_Pentathlon_Event_Manager._event_managerDataSet)(this.FindResource("_event_managerDataSet")));
            // Load data into the table Participant. You can modify this code as needed.
            Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.ParticipantTableAdapter _event_managerDataSetParticipantTableAdapter = new Modern_Pentathlon_Event_Manager._event_managerDataSetTableAdapters.ParticipantTableAdapter();
            _event_managerDataSetParticipantTableAdapter.Fill(_event_managerDataSet.Participant);
            System.Windows.Data.CollectionViewSource participantViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("participantViewSource")));
            participantViewSource.View.MoveCurrentToFirst();
        }

        /*
        * Pobieranie aktualnie zaznaczonych ID zawodnikow. 
        * idAdd - wartosc z lewego datagridu
        * idDelete - wartosc z prawego datagridu
        */
        public long idAddParticipant()
        {
            dynamic currentItem = participantDataGrid.SelectedItem;
            long idParticipant = currentItem.id;
            return idParticipant;
        }
        public long idDeleteParticipant()
        {
            dynamic currentItem = eventParticipantsDataGrid.SelectedItem;
            long idEmployee = currentItem.ParticipantID;
            return idEmployee;
        }
        /*
        * Obsluga przycisku -> 
        */
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            eventParticipantUpdateList.addParticipantToEvent(idAddParticipant(), MainWindow._event);
            eventParticipantsDataGrid.ItemsSource = null;
            eventParticipantUpdateList.showEventParticipants(eventParticipantsDataGrid);
            // int idParticipant = idAddParticipant();
            // long idEvent = MainWindow._event;
        }
        /*
        * Obsluga przycisku <- 
        */
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            eventParticipantUpdateList.deleteParticipantFromEvent(idDeleteParticipant(), MainWindow._event);
            eventParticipantsDataGrid.ItemsSource = null;
            eventParticipantUpdateList.showEventParticipants(eventParticipantsDataGrid);
            //  long idParticipant = idDeleteParticipant();
            // long idEvent = MainWindow._event;

        }
    }
}


