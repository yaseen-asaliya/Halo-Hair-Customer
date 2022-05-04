using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace customer_app.Models
{
    public class TimeModel
    {
        public ObservableCollection<(string, bool)> Time { get; set; }
        public string time { get; set; }
        public string AccessToken_Barbar { get; set; }
        public string Item1 { get; set; }
        public bool Item2 { get; set; }
    }
}
