using System.Windows;
using System.Windows.Controls;
using WpfApp.NET;
using Microsoft.EntityFrameworkCore;

namespace Pract_3
{
    public partial class TableWindow : Window
    {
        public TableWindow()
        {
            InitializeComponent();

            fillListBox(listView);
        }

        public static void fillListBox(ListView listView)
        {
            try
            {
                var searchedProducts = Db1Context.GetContext()
                    .Products
                    .AsNoTracking()
                    .ToList();

                foreach (var item in searchedProducts)
                {
                    if (item.TitlePath != null)
                        item.TitlePath = @"\Resources\" + item.TitlePath;
                }

                listView.ItemsSource = searchedProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void addElementBtnClick(object sender, RoutedEventArgs e)
        {
            var addElementWindow = new AddElement(listView);

            addElementWindow.Owner = this;

            addElementWindow.ShowDialog();
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = listView.SelectedItems.Cast<Product>().ToList();

                var answerDelete = MessageBox.Show("Вы действительно хотите удалить выделенные элементы?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                switch (answerDelete)
                {
                    case MessageBoxResult.Yes:

                        Db1Context.GetContext().Products.RemoveRange(selectedItems);
                        Db1Context.GetContext().SaveChanges();

                        break;

                    case MessageBoxResult.No:
                        return;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            fillListBox(listView);
        }

        private void ComboBox_Initialized(object sender, EventArgs e)
        {
            try
            {
                comboBoxFilter.SelectedIndex = 0;

                var categList = new List<String> { "Не фильтровать" };

                var searchedCategories = Db1Context.GetContext()
                    .Categories
                    .OrderByDescending(c => c.Title)
                    .Select(c => c.Title)
                    .ToList();

                comboBoxFilter.ItemsSource = categList.Union(searchedCategories);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } 

        private void comboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectString = comboBoxFilter.SelectedValue.ToString();

            if (listView is null)
                return;

            if (comboBoxFilter.SelectedIndex == 0)
            {
                fillListBox(listView);

                return;
            }

            try
            {
                //myConnection.Open();

                //var command = new MySqlCommand("SELECT categories.title, product.title, title_path " +
                //                               "FROM db_1.product " +
                //                               "INNER JOIN db_1.categories " +
                //                               "ON product.categories_id = categories.id " +
                //                               "WHERE categories.title = @selectString", myConnection);

                //command.Parameters.AddWithValue("@selectString", selectString);

                //var categList = new List<Product> { };

                //using (MySqlDataReader reader = command.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        string categoriesTitle = Convert.ToString(reader[0]);

                //        string title = Convert.ToString(reader[1]);

                //        string titlePath = @"D:\Projects\C#\Tasks\WpfApp1\Pract_3\Resources\" + Convert.ToString(reader[2]);

                //        categList.Add(new Product() { title = title, categoriesTitle = categoriesTitle, titlePath = titlePath});
                //    }
                //}

                var CategoryId = Db1Context.GetContext().Categories.Where(c => c.Title == selectString).Select(c => c.Id).First();

                var searchedProducts = Db1Context.GetContext()
                    .Products
                    .Where(c => c.CategoriesId == CategoryId)
                    .Select(c => new Product
                    {
                        Title = c.Title,
                        TitlePath = "/Resources/" + c.TitlePath,
                        CategoriesId = c.CategoriesId
                    }).ToList();


                listView.ItemsSource = searchedProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
