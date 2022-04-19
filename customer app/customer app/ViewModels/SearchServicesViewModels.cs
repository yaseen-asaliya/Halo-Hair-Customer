using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using customer_app.Services;
using customer_app.Models;
using System.Collections.Specialized;
using System.ComponentModel;
using Xamarin.Forms;
using customer_app.Views;
using System.Windows.Input;

namespace customer_app.ViewModels
{
    public class SearchServicesViewModels : BaseViewModel
    {
        FireBaseHaloHair firebase;


        public ObservableCollection<DataSalon> services { get; set; }


        public ObservableCollection<DataSalon> Services
        {
            get { return services; }
            set
            {
                services = value;
                OnPropertyChanged();

            }
        }
        private string accesstoken_barbar { get; set; }
        public SearchServicesViewModels(DataSalon data)
        {
            accesstoken_barbar = data.AccessToken_Barbar;
            Console.WriteLine("The Access tokenBarabar " + accesstoken_barbar);
            firebase = new FireBaseHaloHair();
            FilltedServices = new ObservableCollection<DataSalon>();
            Services = new ObservableCollection<DataSalon>();
            Services = firebase.getServices();
            Services.CollectionChanged += filltedservices;


        }




        private ObservableCollection<DataSalon> filltedServices;
        public ObservableCollection<DataSalon> FilltedServices
        {
            get
            {
                return filltedServices;
            }
            set
            {
                filltedServices = value;
                OnPropertyChanged();
            }
        }


        public ICommand NextPage { get; }

        private void filltedservices(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                DataSalon filltedsrevices = e.NewItems[0] as DataSalon;
                Console.WriteLine(e.NewItems[0].GetType());
                if (filltedsrevices.AccessToken_Barbar == accesstoken_barbar)
                {

                    FilltedServices.Add(filltedsrevices);
                }
            }

        }





    }
}
