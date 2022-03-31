using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using customer_app.Models;
using customer_app.Services;

namespace customer_app.ViewModels
{
    public class SearchBarberViewModels : BaseViewModel
    {
        FireBaseHaloHair firebase;

        public ObservableCollection<DataSalon> Salon { get; set; }

        public SearchBarberViewModels()
        {
            firebase = new FireBaseHaloHair();
            Salon = new ObservableCollection<DataSalon>();
            Salon = firebase.GetDataSalon();
        }
    }
}
