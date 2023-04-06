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

namespace LoginApp
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string? username = UsernameTextBox.Text;
            string? password = PasswordTextBox.Password;

            if (username == null || username.Length < 4)
            {
                UsernameTextBox.ToolTip = "Неккоректное значение!";
                UsernameTextBox.Background = Brushes.LightPink;
            }
            else if (password == null || password.Length < 10)
            {
                PasswordTextBox.ToolTip = "Неккоректное значение!";
                PasswordTextBox.Background = Brushes.LightPink;
            }
            else
            {
                ResetTextBoxes();

                User authUser = null;

                using (DataBaseContext context = new DataBaseContext())
                {
                    authUser = context.Users.Where(b => b.username == username && b.password == password).FirstOrDefault();
                }

                if(authUser != null)
                {
                    UserPageWindow userPageWindow = new UserPageWindow();
                    userPageWindow.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("User in not found!");
                }
            }
        }

        private void ResetTextBoxes()
        {
            UsernameTextBox.ToolTip = string.Empty;
            PasswordTextBox.ToolTip = string.Empty;

            UsernameTextBox.Background = Brushes.Transparent;
            PasswordTextBox.Background = Brushes.Transparent;
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
    }
}
