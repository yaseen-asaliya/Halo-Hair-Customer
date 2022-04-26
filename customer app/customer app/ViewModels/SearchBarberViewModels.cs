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
        FireBaseHaloHair firebase;

        public ObservableCollection<DataSalon> Salon { get; set; }

        public ICommand ShowBarbarCommand { get; }
        public ICommand BackPage { get; }

        public SearchBarberViewModels()
        {
            firebase = new FireBaseHaloHair();
            Salon = new ObservableCollection<DataSalon>();
            Salon = firebase.GetDataSalon();
            ShowBarbarCommand = new Command(onShowServices);
            BackPage = new Command(Back_Page);

        }
        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void onShowServices(object obj)
        {
            DataSalon serviceModel = (DataSalon)obj;
            await Application.Current.MainPage.Navigation.PushModalAsync(new SearchServicesPage(serviceModel));


        }





    }
}
