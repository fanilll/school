using Microsoft.Win32;
using School.Components;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace School.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        private Service service;
        public AddEditServicePage(Service _service)
        {
            InitializeComponent();
            service = _service;
            this.DataContext = service;
            PhotoList.Items.Clear();
            PhotoList.ItemsSource = App.db.ServicePhoto.Where(x => x.ServiceID == service.ID).ToList();
            if (service.ID != 0)
            {
                StackPanelPhoto.Visibility = Visibility.Visible;
            }

        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (service.DurationInSeconds < 0 || service.DurationInSeconds > 14900)
                error.AppendLine("Время услуги не может превышать 4 часа!");

            if (service.ID == 0)
            {
                if (App.db.Service.Any(x => x.Title == service.Title))
                {
                    error.AppendLine("Такая услуга уже существует!");
                    MessageBox.Show(error.ToString());
                }
                else
                {
                    App.db.Service.Add(service);
                    StackPanelPhoto.Visibility = Visibility.Visible;
                }
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            App.db.SaveChanges();

            MessageBox.Show("Сохранено!");

            //navigation.NextPage(new PageComponent("Список услуг", new ListPage()));
        }

        private void AddImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jepg|*.jepg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                service.MainImage = File.ReadAllBytes(openFile.FileName);
                MainImage.Source = new BitmapImage(new Uri(openFile.FileName));
            }

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jepg|*.jepg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                App.db.ServicePhoto.Add(new ServicePhoto()
                {
                    ServiceID = service.ID,
                    PhotoByte = File.ReadAllBytes(openFile.FileName)
                });
                App.db.SaveChanges();
                PhotoList.ItemsSource = App.db.ServicePhoto.Where(x => x.ServiceID == service.ID).ToList();
            }

        }

        private void DeleteImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectPhoto = PhotoList.SelectedItem as ServicePhoto;
            if (selectPhoto != null)
            {
                App.db.ServicePhoto.Remove(selectPhoto);
                App.db.SaveChanges();
                PhotoList.ItemsSource = App.db.ServicePhoto.Where(x => x.ServiceID == service.ID).ToList();
            }
            else
            {
                MessageBox.Show("Ничего не выбрано!");
            }

        }
    }
}
