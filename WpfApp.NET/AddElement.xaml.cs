using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp.NET;

namespace Pract_3
{
    public partial class AddElement : Window
    {
        private ListView _listView;

        public AddElement(ListView listView)
        {
            InitializeComponent();

            this._listView = listView;

            var searchedCategories = Db1Context.GetContext()
                .Categories
                .OrderByDescending(c => c.Title)
                .Select(c => c.Title)
                .ToList();

            comboBox.ItemsSource = searchedCategories;
        }

        private void sendRequestBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                string categoriesTitle = comboBox.Text.Trim();

                string productTitle = tb_2.Text.Trim();

                if (categoriesTitle == "")
                    throw new ArgumentException("Введите название категории!");

                if (productTitle == "")
                    throw new ArgumentException("Введите название продукта!");

                var categoriesId = Db1Context.GetContext()
                    .Categories
                    .Where(c => c.Title == categoriesTitle)
                    .Select(c => c.Id)
                    .First();

                Db1Context.GetContext()
                    .Products
                    .Add(new Product { Title = productTitle, CategoriesId = categoriesId });

                Db1Context.GetContext().SaveChanges();

                TableWindow.fillListBox(this._listView);

                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
