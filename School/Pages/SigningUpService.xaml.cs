using School.Components;
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

namespace School.Pages
{
    /// <summary>
    /// Логика взаимодействия для SigningUpService.xaml
    /// </summary>
    public partial class SigningUpService : Page
    {
        Service service;
        public SigningUpService(Service _service)
        {
            InitializeComponent();
            service = _service;
            this.DataContext = service;
            ClientCb.ItemsSource = App.db.Client.ToList();
            ClientCb.DisplayMemberPath = "FullName";
            DateDb.DisplayDateStart = DateTime.Now;
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if(DateDb.SelectedDate != null && !string.IsNullOrWhiteSpace(TimeTb.Text) && ClientCb.SelectedItem != null)
            {
                var selFateTime = $"{DateDb.SelectedDate.Value.ToString("yyyy-MM-dd")} {TimeTb.Text}";
                if (DateTime.TryParse(selFateTime, out DateTime result))
                {
                    if (DateTime.Now < result)
                    {
                        var selectClient = ClientCb.SelectedItem as Client;
                        App.db.ClientService.Add(new ClientService()
                        {
                            ClientID = selectClient.ID,
                            ServiceID = service.ID,
                            StartTime = result
                        });
                        MessageBox.Show("Запись добавлена");
                        App.db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Невозможно записать на прошлое время!");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат времени!");
                }
            }
            else
            {
                MessageBox.Show("Не выбрана время!");
            }

            
        }
    }
}

