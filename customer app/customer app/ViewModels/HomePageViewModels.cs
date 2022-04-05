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
    public class HomePageViewModels : BaseViewModel
    {
        FireBaseHaloHair firebase;
        public ObservableCollection<DataOfferModel> Offer;

        public HomePageViewModels()
        {
            firebase = new FireBaseHaloHair();
            Offer = new ObservableCollection<DataOfferModel>();
            Offer = firebase.GetDataOffers();
        }
    }


}
