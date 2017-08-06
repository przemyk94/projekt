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
    /// Interaction logic for startOrderWindow.xaml
    /// </summary>
    public partial class startOrderWindow : Window
    {
        public startOrderWindow()
        {
            InitializeComponent();
            fillComboBoxes.fillCompetitionComboBox(competitionComboBox, MainWindow._event);
        }

        private void newStartOrderButton_Click(object sender, RoutedEventArgs e)
        {
            
            startOrder.newSwimmingStartOrder(1);
            startOrder.newSwimmingStartOrder(2);
        }

        private void printStartOrder_Click(object sender, RoutedEventArgs e)
        {
            int competition = int.Parse(startOrder.idSelctedCompetition(competitionComboBox).ToString());
            ExportToPdf2.ToPdf(2, competition, "Lista Startowa");
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startOrder.showStartOrder(maleDataGrid, 2, int.Parse(startOrder.idSelctedCompetition(competitionComboBox).ToString()));
            startOrder.showStartOrder(femaleDataGrid, 1, int.Parse(startOrder.idSelctedCompetition(competitionComboBox).ToString()));
        }
    }
}
