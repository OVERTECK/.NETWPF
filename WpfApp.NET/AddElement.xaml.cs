using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
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

                var pathToFile = TextBlockOpenFileDialog.Text;

                var binaryImage = new byte[0];

                if (pathToFile != "")
                {
                    binaryImage = File.ReadAllBytes(pathToFile);
                } else
                {
                    binaryImage = null;
                }


                Db1Context.GetContext()
                    .Products
                    .Add(new Product
                    {
                        Title = productTitle,
                        CategoriesId = categoriesId,
                        TitlePath = null,
                        Image = binaryImage
                    });

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Image file (*.png; *.jpeg; *.jpg) | *.png;*.jpeg;*.jpg";

            if (fileDialog.ShowDialog() == true)
            {
                var fileName = fileDialog.FileName;

                TextBlockOpenFileDialog.Text = fileName;
            }
        }
    }
}