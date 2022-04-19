using System;
using System.Collections.Generic;
using System.Text;

namespace customer_app.Models
{
    public class DataReservationsModel
    {
        public int Price { get; set; }
        public string ListOfService { get; set; }
        public string NameSolan { get; set; }
        public string time { get; set; }
        public string AccessToken_User { get; set; }

        public int ID_History { get; set; }

    }
}
