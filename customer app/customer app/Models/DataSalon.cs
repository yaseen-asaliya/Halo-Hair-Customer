using System;
using System.Collections.Generic;
using System.Text;

namespace customer_app.Models
{
    public class DataSalon
    {

        public string Location { get; set; }
        public bool IsChecked { get; set; }
        public string EndTime { get; set; }
        public string StartTime { get; set; }
        public string SalonName { get; set; }
        public string ServiceName { get; set; }
        public int Price { get; set; }
        public int TimeNeed { get; set; }
        public string Deseription { get; set; }
        public string BarberAccessToken { get; set; }

    }
}
