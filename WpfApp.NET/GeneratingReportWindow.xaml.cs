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
                if (textBlockPath.Text == "")
                {
                    MessageBox.Show("Указажите папку для сохранения!");

                    return;
                }

                var wb = new XLWorkbook();

                var sh = wb.Worksheets.Add("Products");

                var searchedProducts = Db1Context.GetContext().Products.ToList();

                var countSearchedProducts = searchedProducts.Count();

                int index = 1;

                foreach (var product in searchedProducts)
                {
                    sh.Cell(index, 1).SetValue(product.Idproduct);
                    sh.Cell(index, 2).SetValue(product.Title);
                    sh.Cell(index, 3).SetValue(product.Categories.Title);
                    sh.Cell(index, 4).SetValue(product.TitlePath);

                    index += 1;
                }

                wb.SaveAs(textBlockPath.Text);

                MessageBox.Show("Данные успешно экспортированны.", MessageBoxImage.Information.ToString(), MessageBoxButton.OK);

                this.Close();

            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
