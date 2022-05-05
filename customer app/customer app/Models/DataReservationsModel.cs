using System;
using System.Collections.Generic;
using System.Text;

namespace customer_app.Models
{
    public class DataReservationsModel
    {
        public int Price { get; set; }
        public string ListOfService { get; set; }
        public string SalonName { get; set; }
        public string TimeSelected { get; set; }
        public string CustomerAccessToken { get; set; }
        public int ReservationsId { get; set; }
        public string DateSelected { get; set; }
        public int HistoryId { get; set; }

    }
}
