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
    /// <summary>
    /// Interaction logic for People.xaml
    /// </summary>
    public partial class People : Window
    {
        public People(List<string> people)
        {
            InitializeComponent();
            if (people != null && people.Count > 0)
            {
                foreach (var p in people)
                {
                    lbxPeople.Items.Add(p);
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
