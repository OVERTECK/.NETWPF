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
                    .ToList();

                foreach (var item in searchedProducts)
                {
                    if (item.TitlePath == null && item.Image == null)
                    {
                        item.TitlePath = @"\Resources\no_images.png";
                    }
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
                ComboBoxFilter.SelectedIndex = 0;

                var defaultCategories = new List<Category> { new Category { Title = "Не фильтровать" }};

                var searchedCategories = Db1Context.GetContext()
                    .Categories
                    .OrderByDescending(c => c.Title)
                    .ToList();

                ComboBoxFilter.ItemsSource = defaultCategories.Union(searchedCategories);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } 

        private void comboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView is null)
                return;

            if (ComboBoxFilter.SelectedIndex == 0)
            {
                fillListBox(listView);

                return;
            }

            try
            {
                var selectedCategories = ComboBoxFilter.SelectedItem as Category;
                
                var searchedProducts = Db1Context.GetContext()
                    .Products
                    .Where(c => c.CategoriesId == selectedCategories.Id)
                    .ToList();

                foreach (var item in searchedProducts)
                {
                    if (item.TitlePath == null && item.Image == null)
                    {
                        item.TitlePath = @"\Resources\no_images.png";
                    }
                }

                listView.ItemsSource = searchedProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var generatingReportWindow = new GeneratingReportWindow();

            generatingReportWindow.Owner = this;

            generatingReportWindow.ShowDialog();
        }
    }
}
