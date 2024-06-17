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
    /// Логика взаимодействия для RequestMenu.xaml
    /// </summary>
    public partial class RequestMenu : UserControl
    {
        public RequestMenu()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            Requests.Children.Clear();
            using var cmd = Connection!.CreateCommand();
            cmd.CommandText =
"""
select request.number, creation_date, car_label.model, description, account.login, request_state.name
from request
join car_label on car_label.number = request.car
join account on account.number = public.request.creator
join request_state on request_state.number = request.state
""";
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                var view = new RequestView
                {
                    Number = reader.GetInt32(0),
                    CreationDate = reader.GetDateTime(1),
                    Car = reader.GetString(2),
                    Description = reader.GetString(3),
                    Creator = reader.GetString(4),
                    State = reader.GetString(5),
                    Connection = Connection
                };
                view.Refresh();
                Requests.Children.Add(view);
            }
        }

        public NpgsqlConnection? Connection { get; set; }
    }
}
