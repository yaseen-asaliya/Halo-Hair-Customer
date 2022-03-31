using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using customer_app.Services;
using customer_app.Models;

namespace customer_app.ViewModels
{
    public class SearchServicesViewModels
    {
        FireBaseHaloHair firebase;
        public ObservableCollection<DataServicesModel> Services { get; set; }

        public SearchServicesViewModels()
        {
            firebase = new FireBaseHaloHair();
            Services = new ObservableCollection<DataServicesModel>();
            Services = firebase.getServices();
        }
    }
}
