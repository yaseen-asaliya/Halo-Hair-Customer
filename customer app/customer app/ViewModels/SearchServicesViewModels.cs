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
        private FireBaseHaloHair _firebase;
        private ObservableCollection<DataSalon> _filltedServices;
        int count = 0;
        public ObservableCollection<DataSalon> services { get; set; }
        private string _barberAccesstoken { get; set; }
        public ICommand BackButton { get; }
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
            _barberAccesstoken = data.BarberAccessToken;
            _firebase = new FireBaseHaloHair();
            FilltedServices = new ObservableCollection<DataSalon>();
            Services = new ObservableCollection<DataSalon>();
            Services = _firebase.getServices();
            Services.CollectionChanged += FillerServices;
            BackButton = new Command(BackPage);
        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        } 
        private void CheckboxCheckChanged(object sender)
        {
            var checkbox = (Plugin.InputKit.Shared.Controls.CheckBox)sender;
            var ob = checkbox.BindingContext as DataSalon;
            if (ob != null)
            {
                count += ob.Price;
            }
        }
        private void FillerServices(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                DataSalon filltedsrevices = e.NewItems[0] as DataSalon;
                Console.WriteLine(e.NewItems[0].GetType());
                if (filltedsrevices.BarberAccessToken == _barberAccesstoken)
                {
                    FilltedServices.Add(filltedsrevices);
                }
            }
        }
    }
}
