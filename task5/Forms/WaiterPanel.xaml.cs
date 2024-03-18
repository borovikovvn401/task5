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
            update();
        }

        public void update()
        {
            IEnumerable<Dish> dishes = EfModel.init().Dishes.Include(p => p.CategoryNavigation);

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
    }
}
