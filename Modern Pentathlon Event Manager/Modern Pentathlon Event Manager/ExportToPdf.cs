using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Modern_Pentathlon_Event_Manager
{
    public class ExportToPdf : MainWindow
    {
        public static void ToPdf(DataGrid grid, string name)
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), name + ".pdf"), FileMode.Create));

            document.Open();


            PdfPTable table = new PdfPTable(grid.Columns.Count);
            foreach (DataGridColumn column in grid.Columns)
            {
                table.AddCell(new Phrase(column.Header.ToString()));

            }
            table.HeaderRows = 1;
            System.Collections.IEnumerable itemsSource = grid.ItemsSource as System.Collections.IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        System.Windows.Controls.Primitives.DataGridCellsPresenter presenter = FindVisualChild<System.Windows.Controls.Primitives.DataGridCellsPresenter>(row);
                        for (int i = 0; i < grid.Columns.Count; ++i)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt != null)
                            {
                                table.AddCell(new Phrase(txt.Text));
                            }
                        }
                    }
                }
                iTextSharp.text.Paragraph firstpara = new iTextSharp.text.Paragraph(name);
                document.Add(firstpara);
                document.Add(table);
                document.Close();
            }
        }


        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
