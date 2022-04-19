using System;
using System.Collections.Generic;
using System.Text;

namespace customer_app.Models
{
    public class DataSalon
    {
        public int EndTime { get; set; }
        public string Location { get; set; }
        public bool isChecked { get; set; }

        public int StartTime { get; set; }
        public string NameSalon { get; set; }
        public string Service_Name { get; set; }
        public int Prices { get; set; }
        public int Time_Needed { get; set; }
        public string Deseription { get; set; }
        public string AccessToken_Barbar { get; set; }

    }
}
