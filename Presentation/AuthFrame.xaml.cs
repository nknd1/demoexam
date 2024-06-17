using Npgsql;
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

namespace Presentation
{
    /// <summary>
    /// Логика взаимодействия для AuthFrame.xaml
    /// </summary>
    public partial class AuthFrame : UserControl
    {
        public AuthFrame()
        {
            InitializeComponent();
        }

        private void OnSignInClick(object sender, RoutedEventArgs e)
            => SignIn();

        private void SignIn()
        {
            try
            {
                var cmd = Connection!.CreateCommand();
                cmd.CommandText = "select * from account where login = @login and password_hash = @password_hash";
                cmd.Parameters.AddWithValue("@login", LoginForm.Text);
                cmd.Parameters.AddWithValue("@password_hash", PasswordHash);
                if (cmd.ExecuteScalar() is not null)
                {
                    var requestMenu = new RequestMenu();
                    requestMenu.Connection = Connection;
                    requestMenu.Refresh();
                    Location!.Content = requestMenu;
                    MessageBox.Show($"Добро пожаловать, {LoginForm.Text}!");
                }
                else MessageBox.Show("Неправильный логин или пароль.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возможно Вы не заполнили поля.\n\nДетали:\n{ex.Message}");
            }
        }

        private string PasswordHash
            => NETCore.Encrypt.EncryptProvider.HMACSHA256(PasswordForm.Password, LoginForm.Text);

        // пароль
        // nikita -> 1234
        // maxon -> 12345
        // alexey -> 123456
        // dmitry -> 1234567

        public ContentControl? Location { get; set; }

        public NpgsqlConnection? Connection { get; set; }
    }
}
