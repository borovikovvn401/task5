using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using task5.Model;

namespace task5.Forms
{
    /// <summary>
    /// Логика взаимодействия для addDish.xaml
    /// </summary>
    public partial class addDish : Window
    {
        AdminPanel adminPanel;
        Dish dish;
        public addDish(AdminPanel ap, Dish d)
        {
            this .adminPanel = ap;
            this.dish = d;
            DataContext = dish;
            InitializeComponent();

            List<Category> categories = EfModel.init().Categories.ToList();
            cbCategories.ItemsSource = categories;

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (dish.Iddish == 0)
                EfModel.init().Add(dish);
            EfModel.Save();

            if (dish.Iddish != 0)
                EfModel.init().Entry(dish).Reload();
            adminPanel.update();

            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            adminPanel.IsEnabled = true;
        }

        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog { Filter = "Jpeg files|*.jpg|All Files|*.*" };
            if (fileDialog.ShowDialog() == true)
            {
                dish.Image= File.ReadAllBytes(fileDialog.FileName);
            }
        }
    }
}
