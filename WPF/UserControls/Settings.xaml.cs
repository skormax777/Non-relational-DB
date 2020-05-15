﻿using BLL;
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

namespace WPF.UserControls
{
    public partial class Settings : UserControl
    {
        UserBLL _userBLL;
        public Settings()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            var user = _userBLL.GetUser();
            firstName.Text = user.FirstName;
            lastName.Text = user.LastName;
            login.Text = user.Login;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text != "" && lastName.Text != "" && login.Text != "")
            {
                if (_userBLL.CheckUserIdentity(login.Text) || login.Text == _userBLL.LoginRead())
                {
                    if (_userBLL.UpdateByField(_userBLL.LoginRead(), "LastName", lastName.Text)
                        & _userBLL.UpdateByField(_userBLL.LoginRead(), "FirstName", firstName.Text)
                        & _userBLL.UpdateByField(_userBLL.LoginRead(), "Login", login.Text))
                    {
                        _userBLL.LoginWrite(login.Text);
                        MessageBox.Show("Successfully updated!");
                    }
                    else
                    {
                        MessageBox.Show("Error!");
                    }
                }
                else
                {
                    MessageBox.Show("This login already used!");
                }
            }
            else
            {
                MessageBox.Show("Empty data fields!");
            }
        }

        private void Save_Pwd_Click(object sender, RoutedEventArgs e)
        {
            if (pwd.Password == pwd2.Password)
            {
                bool temp = _userBLL.UpdatePassword(old_pwd.Password, pwd.Password);
                if (temp == true)
                {
                    MessageBox.Show("Password successfully changed!");
                }
                else
                {
                    MessageBox.Show("Error! Please, input correct old password.");
                }
            }
            else
            {
                MessageBox.Show("Please, repeat new password correctly.");
            }
        }
    }
}
