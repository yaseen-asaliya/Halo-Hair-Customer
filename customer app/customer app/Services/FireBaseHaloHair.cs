using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using customer_app.Models;

namespace customer_app.Services
{
    public class FireBaseHaloHair
    {
        FirebaseClient firebaseClient;
        public FireBaseHaloHair()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");
        }
        public ObservableCollection<DataServicesModel> getServices()
        {
            var servicesData = firebaseClient.Child("Services").AsObservable<DataServicesModel>().AsObservableCollection();

            return servicesData;
        }
        public ObservableCollection<DataSalon> GetDataSalon()
        {
            var dataSalons = firebaseClient.Child("ScheduleTime").AsObservable<DataSalon>().AsObservableCollection();

            return dataSalons;
        }

        public ObservableCollection<DataOfferModel> GetDataOffers()
        {
            var dataOffers = firebaseClient.Child("Offer").AsObservable<DataOfferModel>().AsObservableCollection();

            return dataOffers;
        }
    }
}
