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
    /// Логика взаимодействия для addOrderDish.xaml
    /// </summary>
    public partial class addOrderDish : Window
    {
        OderDish orderdish = new OderDish();
        CreateOrder createOrder;
        public addOrderDish(Order order, CreateOrder co)
        {
            orderdish.OrderId = order.IdOrder;
            createOrder = co;
            InitializeComponent();

            List<Dish> dishes = EfModel.init().Dishes.ToList();
            cbDish.ItemsSource = dishes;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            orderdish.Amount = Convert.ToInt32(tbAmount.Text);

            EfModel.init().OderDishes.Add(orderdish);
            bool success = EfModel.Save();
            if (success)
                MessageBox.Show("Блюдо добавлено к заказу");
            else
                MessageBox.Show("Ошибка при добавлении блюда");
        }

        private void cbDish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            orderdish.DishId = (cbDish.SelectedItem as Dish).Iddish;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            createOrder.IsEnabled = true;
            createOrder.update();
        }

        private void tbAmount_TextChanged(object sender, TextChangedEventArgs e)
        {

            tbAmount.Text = String.Concat(tbAmount.Text.Where(Char.IsDigit));
        }
    }
}
