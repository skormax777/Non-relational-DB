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

namespace WPF.Windows
{
    public partial class Registration : Window
    {
        UserBLL _userBLL;
        public Registration()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != "" && lastName.Text != "" && login.Text != "")
            {
                if (_userBLL.CheckUserIdentity(login.Text))
                {
                    if (pwd.Password.ToString() == pwd2.Password.ToString() && pwd.Password.ToString() != "")
                    {
                        try
                        {
                            _userBLL.AddUser(firstName.Text, lastName.Text, login.Text, pwd.Password.ToString());
                            _userBLL.LoginWrite(login.Text);
                            MainWindow main = new MainWindow()
                            {
                                WindowStartupLocation = WindowStartupLocation.CenterScreen
                            };
                            main.Show();
                            this.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Error!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid password!");
                    }
                }
                else
                {
                    MessageBox.Show("This login already used!");
                }

            }
            else
            {
                MessageBox.Show("Empty fields!");
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            Login main = new Login();
            main.Show();
            this.Close();
        }
    }
}
