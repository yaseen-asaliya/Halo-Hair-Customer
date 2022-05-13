using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace customer_app.Models
{
    public class AppointmentmModel
    {
        public string DateSelected { get; set; }
        public string BarberAccessToken { get; set; }
        public string CustomerAccessToken { get; set; }
        public int HistoryId { get; set; }
        public bool IsAvabile { get; set; }
        public string SalonName { get; set; }
        public string TimeSelected { get; set; }
        public int ReservationsId { get; set; }
        public string ListOfService { get; set; }
        public string CustomerName { get; set; }
        public int Id { get; set; }
        public ObservableCollection<string> ListService { get; set; }
    }
}
