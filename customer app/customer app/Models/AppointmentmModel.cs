using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace customer_app.Models
{
    public class AppointmentmModel
    {
        public string DateSelected { get; set; }
        public string services { get; set; }
        public string AccessToken_Barbar { get; set; }

        public string AccessToken_User { get; set; }
        public int ID_History { get; set; }


        public string NameSolan { get; set; }
        public string time { get; set; }
        public string ListOfService { get; set; }
        public string PersonName { get; set; }
        public ObservableCollection<string> ListService { get; set; }
    }
}
