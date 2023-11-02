using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace School.Components
{
    public partial class Service
    {
        public decimal CostDiscount
        {
            get
            {
                if (Discount == 0)
                    return Cost;
                else
                    return (Cost - (Cost * (decimal)Discount / 100));
            }
        }


        public string costTimeStr
        {
            get
            {
                if (Discount == 0)
                {
                    return $"{Cost} рублей за {DurationInSeconds / 60} минут";
                }
                else
                {
                    return $" {CostDiscount:0} рублей за {DurationInSeconds / 60} минут";
                }
            }

        }
        public Visibility GetVisibility
        {
            get
            {
                if (Discount == 0)
                {
                    return Visibility.Collapsed;
                }

                else
                {
                    return Visibility.Visible;
                }
            }
        }
        public string DiscountStr
        {
            get
            {
                if (Discount == 0)
                {
                    return "";
                }
                else
                {
                    return $"*скидка {Discount}%";
                }
            }
        }
        public Brush ColorDiscount
        {
            get
            {
                if (Discount == 0)
                {
                    return new SolidColorBrush(Colors.White);
                }
                else return new SolidColorBrush(Colors.LightGreen);
            }
        }
    }
}
