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

namespace School.Pages
{
    /// <summary>
    /// Логика взаимодействия для EntryPage.xaml
    /// </summary>
    public partial class EntryPage : Page
    {
        public EntryPage()
        {
            InitializeComponent();
            var endDate = DateTime.Today.AddDays(2).Date;
            EntryListP.ItemsSource = App.db.ClientService.Where(x => x.StartTime >= DateTime.Today && x.StartTime < endDate).OrderBy(x=> x.StartTime).ToList();
           // EntryListP.ItemsSource = App.db.ClientService.ToList();
        }
    }
}
