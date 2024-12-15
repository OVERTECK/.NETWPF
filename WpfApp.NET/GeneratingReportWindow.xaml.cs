using ClosedXML.Excel;
using Microsoft.Win32;
using System.Windows;

namespace WpfApp.NET
{
    public partial class GeneratingReportWindow : Window
    {
        private static GeneratingReportWindow _context = null!;

        public static GeneratingReportWindow GetContext()
        {
            if (_context == null)
            {
                _context = new GeneratingReportWindow();

                return _context;
            }

            return _context;
        }


        public GeneratingReportWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new SaveFileDialog();

            fileDialog.DefaultExt = ".xlsx";
            fileDialog.Filter = "Excel document (*.xlsx) | *xlsx";

            var result = fileDialog.ShowDialog();

            if (result == true)
            {
                textBlockPath.Text = fileDialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var wb = new XLWorkbook();

                var sh = wb.Worksheets.Add("Products");

                var searchedProducts = Db1Context.GetContext().Products.ToList();

                var countSearchedProducts = searchedProducts.Count();

                for (int i = 0; i < countSearchedProducts; i++)
                {
                    sh.Cell(i + 1, 1).SetValue(searchedProducts[i].Idproduct);
                    sh.Cell(i + 1, 2).SetValue(searchedProducts[i].Title);
                    sh.Cell(i + 1, 3).SetValue(searchedProducts[i].Categories.Title);
                    sh.Cell(i + 1, 4).SetValue(searchedProducts[i].TitlePath);
                }

                wb.SaveAs(textBlockPath.Text);

                MessageBox.Show("Данные успешно экспортированны.", MessageBoxImage.Information.ToString(), MessageBoxButton.OK);

                this.Close();

            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
