using customer_app.Models;
using customer_app.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace customer_app.ViewModels
{
    public class HistoryViewModels
    {
        FireBaseHaloHair firebase;
        public ObservableCollection<DataReservationsModel> History { get; set; }

        public HistoryViewModels()
        {
            firebase = new FireBaseHaloHair();
            History = new ObservableCollection<DataReservationsModel>();
            History = firebase.GetDataReservation();
        }
    }
}
