using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using task5.Forms;
using task5.Model;

namespace task5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            if( tbLogin.Text != string.Empty && tbPassowrd.Text != string.Empty )
            {
                User user = EfModel.init().Users.Include(p => p.RoleNavigation).FirstOrDefault(p => p.Login == tbLogin.Text && p.Password == tbPassowrd.Text);

                if( user != null )
                {
                    switch(user.RoleNavigation.Name)
                    {
                        case "Администратор":
                            AdminPanel adminPanel = new AdminPanel(user);
                            adminPanel.Show();
                            this.Close();
                            break;
                        case "Официант":
                            WaiterPanel waiterPanel = new WaiterPanel(user);
                            waiterPanel.Show();
                            this.Close();
                            break;
                    }
                    return;
                }

                MessageBox.Show("Пользователь не найден");
            }
        }
    }
}