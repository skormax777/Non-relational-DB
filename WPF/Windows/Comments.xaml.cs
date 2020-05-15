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
    /// Interaction logic for WhoCommented.xaml
    /// </summary>
    public partial class Comments : Window
    {
        public Comments(List<string> comments)
        {
            InitializeComponent();
            if (comments != null && comments.Count > 0)
            {
                foreach (var c in comments)
                {
                    lbxComments.Items.Add(c);
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
