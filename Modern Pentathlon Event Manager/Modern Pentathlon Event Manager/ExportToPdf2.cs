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
using System.Drawing;

namespace Modern_Pentathlon_Event_Manager
{
    public class ExportToPdf2 : MainWindow
    {
        public static void ToPdf(int gender, int competition, string fileName)
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".pdf"), FileMode.Create));

            document.Open();

            using (var db = new appDBEntities())
            {
                /*
                 *Wygenerowanie danych ktore maja zostac pokazane w pdfie.  
                 */
                var query = from c in db.StartOrders
                            join participant in db.Participant on c.FKParticipant equals participant.id
                            join sex in db.Sex on participant.Sex equals sex.id
                            join club in db.Club on participant.Club equals club.id
                            where c.FKevent == MainWindow._event &&
                            c.FKcompetition == competition &&
                            participant.Sex == gender
                            // where c.EventID == MainWindow._event
                            select new {participant.Surname,
                                participant.Name,
                                sex.SexName,
                                participant.BirthYear,
                                club.ClubName,
                                c.Heat, c.Track,
                                c.EntryTime};
               
                int columns = 8; //liczba kolumn do wygenerowania (?)
                var query1 = query.OrderBy(c => c.Heat).ThenBy(c => c.Track); //query1 jest posortowane wg Serii a nastepnie numerow torow

                //numer najwiekszej i najmniejszej serii - chyba nie korzystam juz z tego xd
                int max_heat = int.Parse(query1.Max(c => c.Heat).ToString());
                int min_heat = int.Parse(query1.Min(c => c.Heat).ToString());
                
                //Tworzenie pdfa 
                PdfPTable table = new PdfPTable(columns); 
                table.WidthPercentage = 80;       //Setting the PDF File width percentage

                //creating 1st cell and show as table title - skopiowalem z neta
                PdfPCell cell = new PdfPCell();
                cell.Colspan = 8;
                cell.Border = 1;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Phrase = new Phrase("Start Order ", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 18, 1, new BaseColor(35,21,24)));
                cell.BackgroundColor = new BaseColor(255,32,12);
                cell.PaddingBottom = 10;

                // adding 1st cell to table 
                table.AddCell(cell);

                /*var headerParagraph = new Paragraph("Raport śąóżćń", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true)));
                 * Wypisanie nazw naszych kolumn - recznie 
                 */
                // creating and adding 3rd,4th,5th,6th cell using constructor and display as a students name and subkects
                table.AddCell(new PdfPCell(new Phrase("Surname", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.BLACK))) { NoWrap = false, HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                table.AddCell(new PdfPCell(new Phrase("Name", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                table.AddCell(new PdfPCell(new Phrase("Gender", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                table.AddCell(new PdfPCell(new Phrase("Birth Year", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                table.AddCell(new PdfPCell(new Phrase("Club", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                table.AddCell(new PdfPCell(new Phrase("Heat", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                table.AddCell(new PdfPCell(new Phrase("Track", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                table.AddCell(new PdfPCell(new Phrase("Entry Time", new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.BLACK))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });

                /*
                 * to sa zmienne pomocnicze do porownania wartosci z poprzedniej i biezacej iteracji. 
                 * 
                 */
                int a = 0;
                int b = 0;
                int d = 0; 
                foreach (var c in query1)
                {
                    b = int.Parse(c.Heat.ToString());
                    d = a - b;
                    a = int.Parse(c.Heat.ToString());
                    /*
                     * Jak roznica pomiedzy liczba biezacej i poprzedniej "serii" rowna sie -1 lub 0 
                     * to program powinien zrobic wiersz na cala szerokosc pliku i wypisac "seria <numer_serii>" 
                     * nie wiem jak zrobic wiersz na cala szerokosc
                     */
                    if(d == 1 || d == -1)
                    {
                        //creating 1st cell and show as table title
                        PdfPCell cell2 = new PdfPCell();
                        cell2.Colspan = 8;
                        cell2.Border = 1;
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell2.Phrase = new Phrase(string.Format("Heat {0}", c.Heat), new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 18, 1, new BaseColor(35, 21, 24)));
                        cell2.BackgroundColor = new BaseColor(255, 32, 12);
                        cell2.PaddingBottom = 10;
                        // adding 1st cell to table
                        table.AddCell(cell2);
                    }
                    /*
                     * Dodanie danych z bazy danych. */
                      
                    table.AddCell(new PdfPCell(new Phrase(c.Surname, new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                    table.AddCell(new PdfPCell(new Phrase(c.Name, new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                    table.AddCell(new PdfPCell(new Phrase(c.SexName, new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                    table.AddCell(new PdfPCell(new Phrase(c.BirthYear.ToString(), new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                    table.AddCell(new PdfPCell(new Phrase(c.ClubName, new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                    table.AddCell(new PdfPCell(new Phrase(c.Heat.ToString(), new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                    table.AddCell(new PdfPCell(new Phrase(c.Track.ToString(), new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                    table.AddCell(new PdfPCell(new Phrase(c.EntryTime, new Font(BaseFont.CreateFont(@"C:\Windows\Fonts\Arial.ttf", BaseFont.CP1250, true), 8, 1, BaseColor.RED))) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5, BackgroundColor = BaseColor.WHITE });
                }
                document.Open();
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
