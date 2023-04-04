using System;
using System.Collections.Generic;
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

            System.Data.SQLite.SQLiteFactory.Instance.CreateConnection();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string emailCondition = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)";
            string? username = UsernameTextBox.Text;
            string? password = PasswordTextBox.Password;
            string? repeatedPassword = RepeatPasswordTextBox.Password;
            string? email = EmailTextBox.Text;

            if(username == null || username.Length < 4)
            {
                UsernameTextBox.ToolTip = "Неккоректное значение!";
                UsernameTextBox.Background = Brushes.LightPink;
            }
            else if(password == null || password.Length < 10)
            {
                PasswordTextBox.ToolTip = "Неккоректное значение!";
                PasswordTextBox.Background = Brushes.LightPink;
            }
            else if (password != repeatedPassword)
            {
                RepeatPasswordTextBox.ToolTip = "Пароли не совпадают!";
                RepeatPasswordTextBox.Background = Brushes.LightPink;
            }
            else if(!Regex.IsMatch(email, emailCondition) || email == null)
            {
                EmailTextBox.ToolTip = "Email не соответствует шаблону!";
                EmailTextBox.Background = Brushes.LightPink;
            }
            else
            {
                User user = new User(username, password, email);

                _db.Users.Add(user);
                _db.SaveChanges();
                ResetTextBoxes();
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
    }
}
