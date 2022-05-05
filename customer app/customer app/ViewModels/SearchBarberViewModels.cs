using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using customer_app.Models;
using customer_app.Services;
using Xamarin.Forms;
using customer_app.Views;
using System.Collections.Specialized;

namespace customer_app.ViewModels
{
    public class SearchBarberViewModels : BaseViewModel
    {
        FireBaseHaloHair _firebase;
        public ObservableCollection<DataSalon> Salon { get; set; }
        public ICommand ShowBarbarCommand { get; }
        public ICommand BackButton { get; }
        public SearchBarberViewModels()
        {
            _firebase = new FireBaseHaloHair();
            Salon = new ObservableCollection<DataSalon>();
            Salon = _firebase.GetDataSalon();
            ShowBarbarCommand = new Command(OnShowServices);
            BackButton = new Command(BackPage);
        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void OnShowServices(object obj)
        {
            DataSalon serviceModel = (DataSalon)obj;
            await Application.Current.MainPage.Navigation.PushModalAsync(new SearchServicesPage(serviceModel));
        }
    }
}
