using Npgsql;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var connection = new NpgsqlConnection(Constring);
            connection.Open();
            FrameLocation.Content = new AuthFrame 
            { 
                Connection = connection,
                Location = FrameLocation 
            };
        }

        private const string Constring
            = "Server = 10.14.206.28; Port=5432; Database=user5; User Id=user5; Password=Lu%5%4e4";
    }
}