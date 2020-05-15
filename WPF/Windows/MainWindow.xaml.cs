using BLL;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.UserControls;

namespace WPF.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MoveCursorMenu(int index)
        {
        
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            if (ListViewMenu.IsLoaded)
            {
                MoveCursorMenu(index);

            }

            switch (index)
            {
                case 0:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new TimeLine());
                    break;
                case 1:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new AboutMe());
                    break;
                case 2:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new Settings());
                    break;
                default:
                    break;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            UserBLL friend = new UserBLL();
            if (friend.IsUserExist(txtLogin.Text))
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new AboutFriend(txtLogin.Text));
            }
            else
            {
                MessageBox.Show("User isn't exist!");
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            UserBLL user = new UserBLL();
            user.UpdateByDateTime();
            Login window = new Login()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.Show();
            this.Close();
        }
    }
}
