using School.Components.PartialClass;
using School.Pages;
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

namespace School
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            navigation.mainWindow = this;
            //var path = @"C:\Users\fhusn\Desktop\";
            //foreach (var item in App.db.Service.ToArray())
            //{
            //    var fullPath = path + item.MainImagePath;
            //    item.MainImage = File.ReadAllBytes(fullPath);
            //}
            //App.db.SaveChanges();
            navigation.NextPage(new PageComponent("Авторизация",
                new Autoris()));
        }

        private void BackBut_Click(object sender, RoutedEventArgs e)
        {
            navigation.BackPage();

        }


        private void ExitBut_Click(object sender, RoutedEventArgs e)
        {
            navigation.ClearHistory();
            navigation.NextPage(new PageComponent("Авторизация",
                new Autoris()));
        }
    }
}
