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
    /// Логика взаимодействия для RequestView.xaml
    /// </summary>
    public partial class RequestView : UserControl
    {
        public RequestView()
        {
            InitializeComponent();
            saveButton = new SaveRequestButton() { Request = this };
        }

        public int Number { get; set; }

        public DateTime CreationDate { get; set; }

        public string? Car { get; set; }

        public string? Description { get; set; }

        public string? Creator { get; set; }

        public string? State { get; set; }

        public void Refresh()
        {
            NumberForm.Text = $"№{Number}";
            CreationDateForm.Text = CreationDate.ToString();
            CarForm.Text = Car;
            DescriptionForm.Text = Description;
            CreatorForm.Text = Creator;
            StateForm.Text = State;
            HideSaveButton();
        }

        public NpgsqlConnection? Connection { get; set; }

        private bool isChanged = false;

        public void Save()
        {
            HideSaveButton();
            try
            {
                var cmd = Connection!.CreateCommand();
                cmd.CommandText = "update request set car = 2, description = @description, creator = 2, state = 2 where number = @number";
                cmd.Parameters.AddWithValue("@description", DescriptionForm.Text!);
                cmd.Parameters.AddWithValue("@number", Number);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Заявка сохранена!");
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Возможно Вы не заполнили поля.\n\nДетали:\n{ex.Message}");
            }
        }

        public void HideSaveButton()
        {
            isChanged = false;
            Additions.Children.Remove(saveButton);
        }

        private void OnChanged(object sender, TextChangedEventArgs e)
        {
            if (!isChanged) 
            {
                isChanged = true;
                Additions.Children.Add(saveButton);
            }
        }

        private readonly SaveRequestButton saveButton;
    }
}
