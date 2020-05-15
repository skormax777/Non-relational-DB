using BLL;
using MongoDB.Bson;
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
    /// Interaction logic for EditPost.xaml
    /// </summary>
    public partial class EditPost : Window
    {
        ObjectId _postId;
        PostBLL _postBLL;
        public EditPost(ObjectId postId)
        {
            InitializeComponent();
            _postId = postId;
            _postBLL = new PostBLL();
            Text.Text = _postBLL.GetPost(_postId).Text;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _postBLL.EditPost(Text.Text, _postId);
            this.Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
