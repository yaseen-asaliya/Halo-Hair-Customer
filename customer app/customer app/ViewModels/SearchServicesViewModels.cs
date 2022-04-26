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
        public ICommand BackPage { get; }

        public SearchServicesViewModels(DataSalon data)
        {
            accesstoken_barbar = data.AccessToken_Barbar;
            Console.WriteLine("The Access tokenBarabar " + accesstoken_barbar);
            firebase = new FireBaseHaloHair();
            FilltedServices = new ObservableCollection<DataSalon>();
            Services = new ObservableCollection<DataSalon>();
            Services = firebase.getServices();
            Services.CollectionChanged += filltedservices;
            // CheckBox = new Command(checkbox_CheckChanged);
            BackPage = new Command(Back_Page);

        }
        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
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


        private int count = 0;
        public ICommand CheckBox { get; }

        private void checkbox_CheckChanged(object sender)

        {

            var checkbox = (Plugin.InputKit.Shared.Controls.CheckBox)sender;


            var ob = checkbox.BindingContext as DataSalon;

            if (ob != null)
            {
                count += ob.Prices;
                // AddOrUpdatetheResult(ob, checkbox);

            }

        }

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
