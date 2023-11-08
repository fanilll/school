using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Components
{
    public partial class ClientService
    {
        public string TimeStart
        {
            get
            {
                var time = StartTime - DateTime.Now;
                int hours = (int)time.TotalMinutes/60;
                int minutes = (int)time.TotalMinutes % 60;
                return $"{hours}:{minutes}";
            }
        }
        public TimeSpan MyTimeStart {
            get
            {
                return StartTime - DateTime.Now;
            }
        }
        public string ColorStr
        {
            get
            {
                if (MyTimeStart.TotalMinutes <60)
                {
                    return "#FF0000";
                }
                else
                {
                    return "#000000";
                }
               
            }
        }
    }
}
