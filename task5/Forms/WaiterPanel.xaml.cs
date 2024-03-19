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
using Microsoft.EntityFrameworkCore;
using task5.Model;

namespace task5.Forms
{
    /// <summary>
    /// Логика взаимодействия для WaiterPanel.xaml
    /// </summary>
    public partial class WaiterPanel : Window
    {
        User user;
        public WaiterPanel(User u)
        {
            this.user = u;
            DataContext = user;
            InitializeComponent();


            cbSort.Items.Add("По убыванию цены");
            cbSort.Items.Add("По возрастанию цены");

            List<Category> categories = EfModel.init().Categories.ToList();
            categories.Insert(0, new Category { Name = "Все" });
            cbFiltr.ItemsSource = categories;
            cbFiltr.SelectedIndex = 0;

            update();
        }

        public void update()
        {
            IEnumerable<Dish> dishes = EfModel.init().Dishes.Include(p => p.CategoryNavigation)
                .Where(p => p.Name.Contains(tbSearch.Text));

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    dishes = dishes.OrderByDescending(p => p.Price);
                    break;
                case 1:
                    dishes = dishes.OrderBy(p => p.Price);
                    break;
            }

            if(cbFiltr.SelectedIndex > 0)
            {
                dishes = dishes.Where(p => p.Category == (cbFiltr.SelectedItem as Category).IdCategory);
            }


            DishList.ItemsSource = dishes.ToList();
        }

        private void createOrderButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrder createOrder = new CreateOrder(this, user);
            createOrder.Show();
            this.IsEnabled = false;
        }

        private void authPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void cbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }
    }
}
