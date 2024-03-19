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
using task5.Model;

namespace task5.Forms
{
    /// <summary>
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        WaiterPanel waiter;
        User user;
        Order order = new Order();
        public CreateOrder(WaiterPanel wp, User u)
        {
            this.waiter = wp;
            this.user = u;
            order.UserUserid = user.IdUsers;
            order.DateTime = DateTime.Now;
            order.Table = 1;
            order.StatusStatusid = 1;

            DataContext = order;

            InitializeComponent();
            lvDish.ItemsSource = EfModel.init().OderDishes.Where(p => p.OrderId == order.IdOrder).ToList();
        }

        public void save()
        {
            order.Details = tbDetails.Text;
            order.Table = Convert.ToInt32(tbTable.Text);
            EfModel.init().Add(order);
            EfModel.Save();
            EfModel.init().Entry(order).Reload();
            MessageBox.Show(order.Details + "/" + tbDetails.Text + " " + order.Table + "/" + tbTable.Text);
            
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            save();
            waiter.IsEnabled = true;
        }

        public void update()
        {
            IEnumerable<OderDish> dishes = EfModel.init().OderDishes.Where(p => p.OrderId == order.IdOrder);
            lvDish.ItemsSource = dishes.ToList();


            if (order != null)
            {
                decimal orderprice = 0;
                List<OderDish> list1 = EfModel.init().OderDishes.Where(p => p.OrderId == order.IdOrder).ToList();
                foreach (var item in list1)
                {
                    orderprice += item.Dish.Price;
                }
                tbOrderPrice.Text = "Стоимость заказа: " + orderprice.ToString() + "руб.";
            }
        }


        private void tbDetails_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {
            if(order.IdOrder == 0)
            {
                MessageBox.Show("Для добавления блюд создайте заказ");
                return;
            }
            addOrderDish addOrderDish = new addOrderDish(order, this);
            addOrderDish.Show();
            this.IsEnabled = false;
        }

        private void tbTable_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTable.Text = String.Concat(tbTable.Text.Where(Char.IsDigit));
        }

        private void tbTable_SelectionChanged(object sender, RoutedEventArgs e)
        {
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                save();
                saveButton.IsEnabled = false;
            } catch(Exception ex)
            {
                MessageBox.Show("Заполните информацию о заказе");
            }
        }
    }
}
