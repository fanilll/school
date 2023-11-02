using School.Components;
using School.Components.PartialClass;
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
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ListPage()
        {
            InitializeComponent();

            if (App.isAdmin == false)
            {
                AddBut.Visibility = Visibility.Collapsed;
            }
            Refresh();
        }
        private void Refresh()
        {
            IEnumerable<Service> servicesListSort = App.db.Service;
            if (SortCb.SelectedIndex != 0)
            {
                if (SortCb.SelectedIndex == 1)
                {
                    servicesListSort = servicesListSort.OrderBy(x => x.CostDiscount);
                }
                else if (SortCb.SelectedIndex == 2)
                {
                    servicesListSort = servicesListSort.OrderByDescending(x => x.CostDiscount);
                }
            }
            if (DiscountFilterCb.SelectedIndex != 0)
            {
                if (DiscountFilterCb.SelectedIndex == 1)
                {
                    servicesListSort = servicesListSort.Where(x => x.Discount >= 0 && x.Discount < 5);
                }
                if (DiscountFilterCb.SelectedIndex == 2)
                {
                    servicesListSort = servicesListSort.Where(x => x.Discount >= 5 && x.Discount < 15);
                }
                if (DiscountFilterCb.SelectedIndex == 3)
                {
                    servicesListSort = servicesListSort.Where(x => x.Discount >= 15 && x.Discount < 30);
                }
                if (DiscountFilterCb.SelectedIndex == 4)
                {
                    servicesListSort = servicesListSort.Where(x => x.Discount >= 30 && x.Discount < 70);
                }
                if (DiscountFilterCb.SelectedIndex == 5)
                {
                    servicesListSort = servicesListSort.Where(x => x.Discount >= 70 && x.Discount < 100);
                }
            }
            if (SearchTb.Text != null)
            {
                servicesListSort = servicesListSort.Where(x => x.Title.ToLower().Contains
                (SearchTb.Text.ToLower()) || x.Description.ToLower().Contains
                (SearchTb.Text.ToLower()));
            }
            ServiceWP.Children.Clear();
            foreach (var service in servicesListSort)
            {
                ServiceWP.Children.Add(new ServiceUserControl(service));
            }
            CountDataTb.Text = servicesListSort.Count() + " из " + App.db.Service.Count();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void DiscountFilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            navigation.NextPage(new PageComponent("Добавление услуги",
                new AddEditServicePage(new Service())));
        }
    }
}
