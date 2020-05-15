using BLL;
using DAL;
using Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Windows;

namespace WPF.UserControls
{
    /// <summary>
    /// Interaction logic for AboutFriend.xaml
    /// </summary>
    public partial class AboutFriend : UserControl
    {
        string _friendLogin;
        UserBLL _userBLL;
        UserDAL _userDAL;
        PostBLL _postBLL;
        User _user;
        Post _current_post;
        List<Post> _posts;
        int _index_of_post = 0;
        bool _is_any_posts = false;
        bool _tempLike = false;

        public AboutFriend(string friendLogin)
        {
            InitializeComponent();

            _friendLogin = friendLogin;
            _userBLL = new UserBLL();
            _userDAL = new UserDAL();
            _postBLL = new PostBLL();

            _user = _userBLL.GetUser(_friendLogin);
            info.Content = _user.FirstName + "  " + _user.LastName + "\nLogin:   " + _user.Login + "\nActive:\n      " + _user.LastLogin;
            if (_userBLL.IsUserFollowing(_userBLL.LoginRead(), _friendLogin))
            {
                btnFollow.BorderBrush = Brushes.Green;
            }
            else
                btnFollow.BorderBrush = Brushes.Transparent;

            _current_post = new Post();
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;

            Refresh();
        }
        private void Follow_Click(object sender, RoutedEventArgs e)
        {
            if (!_userBLL.IsUserFollowing(_userBLL.LoginRead(), _friendLogin))
            {
                _userDAL.AddFollowing(_userBLL.LoginRead(), _friendLogin);
                _userDAL.AddFollowers(_friendLogin, _userBLL.LoginRead());
                btnFollow.BorderBrush = Brushes.Green;
            }
            else
            {
                _userDAL.Unfollow(_userBLL.LoginRead(), _friendLogin);
                btnFollow.BorderBrush = Brushes.White;
            }
        }

        private void Like(object sender, RoutedEventArgs e)
        {
            if (_posts.Count > 0)
            {
                if (_tempLike == false)
                {
                    btnLike.BorderBrush = Brushes.White;
                    _tempLike = true;
                    _postBLL.AddLike(_userBLL.LoginRead(), _current_post.Id);
                }
                else
                {
                    btnLike.BorderBrush = Brushes.Transparent;
                    _tempLike = false;
                    _postBLL.DismissLike(_userBLL.LoginRead(), _current_post.Id);
                }
                btnLike.Content = "Likes: " + _postBLL.GetNumOfLikes(_current_post.Id).ToString();
            }
            else
            {
                btnLike.BorderBrush = Brushes.Transparent;
                _tempLike = false;
            }
        }

        private void Prev(object sender, RoutedEventArgs e)
        {
            _posts = _postBLL.GetAllPosts(_friendLogin);
            if (_is_any_posts)
            {
                if (_index_of_post > 0)
                {
                    _index_of_post--;
                    Refresh();

                    if (_index_of_post == 0)
                    {
                        btnPrev.IsEnabled = false;
                        btnNext.IsEnabled = true;
                    }
                    else
                    {
                        btnPrev.IsEnabled = true;
                    }
                }
            }
        }

        private void Comment(object sender, RoutedEventArgs e)
        {
            AddComment main = new AddComment(_current_post.Id)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.Show();
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            _posts = _postBLL.GetAllPosts(_friendLogin);
            if (_is_any_posts)
            {
                if (_index_of_post + 1 < _posts.Count)
                {
                    _index_of_post++;
                    Refresh();

                    if (_index_of_post + 1 == _posts.Count)
                    {
                        btnNext.IsEnabled = false;
                        btnPrev.IsEnabled = true;
                    }
                    else
                    {
                        btnNext.IsEnabled = true;
                        btnPrev.IsEnabled = true;
                    }
                }
            }
        }

        private void WhoLiked(object sender, RoutedEventArgs e)
        {
            People main = new People(_postBLL.GetWhoLiked(_current_post.Id))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.Show();
        }

        private void WhoCommented(object sender, RoutedEventArgs e)
        {
            Comments main = new Comments(_postBLL.GetWhoCommented(_current_post.Id))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        private void Following(object sender, RoutedEventArgs e)
        {
            People main = new People(_userBLL.GetFollowing(_friendLogin))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        private void Followers(object sender, RoutedEventArgs e)
        {
            People main = new People(_userBLL.GetFollowers(_friendLogin))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        public void Refresh()
        {
            _posts = _postBLL.GetAllPosts(_friendLogin);
            if (_posts != null && _posts.Count > 0)
            {
                btnLike.IsEnabled = true;
                btnComment.IsEnabled = true;
                btnComments.IsEnabled = true;
                btnLikers.IsEnabled = true;
                btnNext.IsEnabled = true;

                _current_post = _posts[_index_of_post];
                Header.Content = "Posts in " + _user.FirstName + "'s Timeline:   " + _posts.Count;
                Timeline.Content = _current_post.Text;
                date.Content = "Posted on:  " + _current_post.DateTime;
                _is_any_posts = true;

                if (_posts.Count == 1 || _index_of_post == _posts.Count - 1) btnNext.IsEnabled = false;

                if (_postBLL.DidUserLikePost(_userBLL.LoginRead(), _current_post.Id))
                {
                    btnLike.BorderBrush = Brushes.YellowGreen;
                    _tempLike = true;
                }
                else
                {
                    btnLike.BorderBrush = Brushes.Transparent;
                    _tempLike = false;
                }
                btnLike.Content = "Likes: " + _postBLL.GetNumOfLikes(_current_post.Id).ToString();
            }
            else
            {
                Header.Content = "No posts in " + _user.FirstName + "'s Timeline";
                Timeline.Content = "";
                btnLike.IsEnabled = false;
                btnComment.IsEnabled = false;
                btnComments.IsEnabled = false;
                btnLikers.IsEnabled = false;
            }
        }
    }
}
