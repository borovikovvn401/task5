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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        User user;
        public AdminPanel(User u)
        {
            this.user = u;
            DataContext = user;
            InitializeComponent();
            update();
        }

        public void update()
        {
            IEnumerable<Dish> dishes = EfModel.init().Dishes.Include(p => p.CategoryNavigation);

            DishList.ItemsSource = dishes.ToList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addDish addDish = new addDish(this, new Dish());
            addDish.Show();
            this.IsEnabled = false;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            addDish addDish = new addDish(this, DishList.SelectedItem as Dish);
            addDish.Show();
            this.IsEnabled = false;
        }

        private void orderListButton_Click(object sender, RoutedEventArgs e)
        {
            OrderList orderList = new OrderList();
            orderList.Show();
        }

        private void authPage_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
