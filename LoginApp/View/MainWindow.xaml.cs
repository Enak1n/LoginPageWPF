﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using LoginApp.Model;

namespace LoginApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBaseContext _db = new DataBaseContext();

        public MainWindow()
        {
            InitializeComponent();
            Database.SetInitializer<DataBaseContext>(new DropCreateDatabaseIfModelChanges<DataBaseContext>());
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string emailCondition = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
            string? username = UsernameTextBox.Text;
            string? password = PasswordTextBox.Password;
            string? repeatedPassword = RepeatPasswordTextBox.Password;
            string? email = EmailTextBox.Text;

            if (username == null || username.Length < 4)
            {
                MarkInvalid(UsernameTextBox);
            }
            else if (password == null || password.Length < 10)
            {
                MarkInvalid(PasswordTextBox);
            }
            else if (password != repeatedPassword)
            {
                MarkInvalid(PasswordTextBox);
            }
            else if (!Regex.IsMatch(email, emailCondition) || email == null)
            {
                MarkInvalid(EmailTextBox);
            }
            else
            {
                User user = new User(username, password, email);
                User userFromDb;
                using (DataBaseContext context = new DataBaseContext())
                {
                    userFromDb = context.Users.Where(b => b.username == username || b.email == email).FirstOrDefault();
                }

                if (userFromDb != null )
                {
                    MessageBox.Show("User is already created!");
                }
                else
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    ResetTextBoxes();

                    UserPageWindow userPageWindow = new UserPageWindow();
                    userPageWindow.Show();
                    Hide();
                }
            }
        }

        private void ResetTextBoxes()
        {
            UsernameTextBox.ToolTip = string.Empty;
            PasswordTextBox.ToolTip = string.Empty;
            RepeatPasswordTextBox.ToolTip = string.Empty;
            UsernameTextBox.ToolTip = string.Empty;

            UsernameTextBox.Background = Brushes.Transparent;
            PasswordTextBox.Background = Brushes.Transparent;
            RepeatPasswordTextBox.Background = Brushes.Transparent;
            EmailTextBox.Background = Brushes.Transparent;
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Hide();
        }

        private void MarkInvalid(Control control)
        {
            control.ToolTip = "Неккоректное значение!";
            control.Background = Brushes.LightPink;
        }
    }
}
