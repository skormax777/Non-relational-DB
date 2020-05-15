using BLL;
using DAL;
using Entity;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF.Windows;

namespace WPF.UserControls
{
    public partial class AboutMe : UserControl
    {
        UserBLL _userBLL;
        PostDAL _postDAL;
        PostBLL _postBLL;
        User _user;
        Post _current_post;
        List<Post> _posts;
        int _index_of_post = 0;
        bool _is_any_posts = false;
        bool _tempLike = false;

        public AboutMe()
        {
            InitializeComponent();

            _userBLL = new UserBLL();
            _postDAL = new PostDAL();
            _postBLL = new PostBLL();

            _user = _userBLL.GetUser();
            info.Content = _user.FirstName + "  " + _user.LastName + "\nLogin:   " + _user.Login + "\nActive:   now";

            _current_post = new Post();
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;

            Refresh();
        }

        private void Like(object sender, RoutedEventArgs e)
        {
            if (_posts.Count > 0)
            {
                if (_tempLike == false)
                {
                    btnLike.BorderBrush = Brushes.White;
                    _tempLike = true;
                    _postDAL.AddLike(_userBLL.LoginRead(), _current_post.Id);
                }
                else
                {
                    btnLike.BorderBrush = Brushes.Transparent;
                    _tempLike = false;
                    _postDAL.DismissLike(_userBLL.LoginRead(), _current_post.Id);
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
            _posts = _postDAL.SelectAllPosts(_userBLL.GetUserId());
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
            _posts = _postDAL.SelectAllPosts(_userBLL.GetUserId());
            if (_is_any_posts)
            {
                if (_index_of_post + 1 < _posts.Count)
                {
                    _index_of_post++;
                    Refresh();

                    if(_index_of_post + 1 == _posts.Count)
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

        private void AddPost(object sender, RoutedEventArgs e)
        {
            AddPost window = new AddPost()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.Show();
        }

        private void EditPost(object sender, RoutedEventArgs e)
        {
            EditPost window = new EditPost(_current_post.Id);
            window.Show();
        }

        private void DeletePost(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Are you sure to delete this post?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch(message)
            {
                case MessageBoxResult.Yes:
                    {
                        bool isSuccess = _postBLL.DeletePost(_current_post.Id);
                        if (isSuccess)
                        {
                            MessageBox.Show("Successfully deleted!");
                        }
                        else
                        {
                            MessageBox.Show("Sorry, we can't do it...");
                        }
                        break;
                    }                    
                case MessageBoxResult.No:
                    break;                
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
            People main = new People(_userBLL.GetFollowing(_userBLL.LoginRead()))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        private void Followers(object sender, RoutedEventArgs e)
        {
            People main = new People(_userBLL.GetFollowers(_userBLL.LoginRead()))
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            main.ShowDialog();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            _posts = _postDAL.SelectAllPosts(_userBLL.GetUserId());
            if (_posts != null && _posts.Count > 0)
            {
                btnLike.IsEnabled = true;
                btnComment.IsEnabled = true;
                btnComments.IsEnabled = true;
                btnLikers.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnNext.IsEnabled = true;

                _current_post = _posts[_index_of_post];
                Header.Content = "Posts in your Timeline:   " + _posts.Count;
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
                Header.Content = "No posts in your Timeline";
                Timeline.Content = "";
                date.Content = "";
                btnLike.IsEnabled = false;
                btnComment.IsEnabled = false;
                btnComments.IsEnabled = false;
                btnLikers.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }
    }
}
