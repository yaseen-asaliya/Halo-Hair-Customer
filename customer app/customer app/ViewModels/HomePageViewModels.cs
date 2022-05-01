using customer_app.Models;
using customer_app.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace customer_app.ViewModels
{
    public class HomePageViewModels : BaseViewModel
    {

        FireBaseHaloHair firebase;
        public string NameCustomer { get; set; }
        public ObservableCollection<OfferModel> OfferImages { get; set; }
        public ObservableCollection<OfferModel> Offer { get; set; }
        public ObservableCollection<OfferModel> myOffers { get; set; }
        public HomePageViewModels()
        {
            GetName();
            OfferImages = new ObservableCollection<OfferModel>();
            firebase = new FireBaseHaloHair();
            Offer = new ObservableCollection<OfferModel>();
            myOffers = new ObservableCollection<OfferModel>();
            myOffers = firebase.GetAllOfferImgs();
            myOffers.CollectionChanged += filltedservices;

        }

        private void filltedservices(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OfferModel filltedsrevices = e.NewItems[0] as OfferModel;

                Offer.Add(filltedsrevices);

            }

        }

        private async Task GetName()
        {
            try
            {
                var name = await SecureStorage.GetAsync("NameUser");
                NameCustomer = name;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

