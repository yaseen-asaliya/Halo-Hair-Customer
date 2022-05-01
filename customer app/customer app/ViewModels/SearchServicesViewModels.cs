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
        FireBaseHaloHair _firebase;
        private ObservableCollection<DataSalon> _filltedServices;
        private int _count = 0;
        public ObservableCollection<DataSalon> services { get; set; }
        private string _barbarAccesstoken { get; set; }
        public ICommand BackPage { get; }
        public ICommand CheckBox { get; }
        public ObservableCollection<DataSalon> Services
        {
            get { return services; }
            set
            {
                services = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataSalon> FilltedServices
        {
            get
            {
                return _filltedServices;
            }
            set
            {
                _filltedServices = value;
                OnPropertyChanged();
            }
        }
        
        public SearchServicesViewModels(DataSalon data)
        {
            _barbarAccesstoken = data.AccessToken_Barbar;
            Console.WriteLine("The Access tokenBarabar " + _barbarAccesstoken);
            _firebase = new FireBaseHaloHair();
            FilltedServices = new ObservableCollection<DataSalon>();
            Services = new ObservableCollection<DataSalon>();
            Services = _firebase.getServices();
            Services.CollectionChanged += filltedServices;
            // CheckBox = new Command(checkbox_CheckChanged);
            BackPage = new Command(backPage);
        }
        private async void backPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        } 
        private void checkboxCheckChanged(object sender)
        {
            var checkbox = (Plugin.InputKit.Shared.Controls.CheckBox)sender;
            var ob = checkbox.BindingContext as DataSalon;
            if (ob != null)
            {
                _count += ob.Prices;
                // AddOrUpdatetheResult(ob, checkbox);
            }
        }
        private void filltedServices(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                DataSalon filltedsrevices = e.NewItems[0] as DataSalon;
                Console.WriteLine(e.NewItems[0].GetType());
                if (filltedsrevices.AccessToken_Barbar == _barbarAccesstoken)
                {
                    FilltedServices.Add(filltedsrevices);
                }
            }
        }
    }
}
