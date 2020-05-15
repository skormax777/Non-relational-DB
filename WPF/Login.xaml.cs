using BLL;
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
using WPF.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        UserBLL _userBLL;
        public Login()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void LOGIN(object sender, RoutedEventArgs e)
        {
            if (_userBLL.CheckPassword(txtLogin.Text, Password.Password.ToString()))
            {
                _userBLL.LoginWrite(txtLogin.Text);
                MainWindow main = new MainWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid password!");
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Registration main = new Registration()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.Show();
            this.Close();
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
