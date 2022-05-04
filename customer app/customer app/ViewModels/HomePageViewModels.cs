using customer_app.Models;
using customer_app.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using customer_app.Views;
namespace customer_app.ViewModels
{
    public class HomePageViewModels : BaseViewModel
    {

        FireBaseHaloHair firebase;
        public string NameCustomer { get; set; }
        public ObservableCollection<OfferModel> OfferImages { get; set; }
        public ObservableCollection<OfferModel> Offer { get; set; }
        public ObservableCollection<OfferModel> myOffers { get; set; }
        public ICommand SearchBarberPage { get; }
        public HomePageViewModels()
        {
            GetName();
            OfferImages = new ObservableCollection<OfferModel>();
            firebase = new FireBaseHaloHair();
            Offer = new ObservableCollection<OfferModel>();
            myOffers = new ObservableCollection<OfferModel>();
            myOffers = firebase.GetAllOfferImgs();
            myOffers.CollectionChanged += filltedservices;
            SearchBarberPage = new Command(onSearchBarberPage);
        }
        private async void onSearchBarberPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SearchBarberPage());
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

