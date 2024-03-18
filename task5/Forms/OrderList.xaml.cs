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
    /// Логика взаимодействия для OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        public OrderList()
        {
            InitializeComponent();
            update();
        }

        public void update()
        {

            DataGridContext.ItemsSource = EfModel.init().Orders.Include(p => p.StatusStatus).Include(p => p.UserUser).ToList();
        }

        private void cbStatus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as ComboBox).ItemsSource = EfModel.init().Statuses.ToList();

            Order order;
        }

        private void DishList_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridContext.SelectedItems.Count == 1)
            {
                Order order = DataGridContext.SelectedItem as Order;
                if(order != null)
                {
                    StringBuilder sb = new StringBuilder();
                    List<OderDish> list1 = EfModel.init().OderDishes.Where(p => p.OrderId == order.IdOrder).ToList();
                    foreach (var item in list1)
                    {
                        sb.Append(item.Dish.Name + " x" + item.Amount + "\n");

                    }
                    MessageBox.Show(sb.ToString());
                }
            }
        }
    }
}
