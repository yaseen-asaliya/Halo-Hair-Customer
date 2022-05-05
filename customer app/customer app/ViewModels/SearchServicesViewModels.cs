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
using System.Threading.Tasks;

namespace customer_app.ViewModels
{
    public class SearchServicesViewModels : BaseViewModel
    {
        private FireBaseHaloHair _firebase;
        private ObservableCollection<DataSalon> _filltedServices;
        private int _count = 0;
        private int _numberOfSelectedItems = 0;
        private string _salonName { get; set; }
        private string _start { get; set; }
        private string _end { get; set; }

        public ObservableCollection<DataSalon> services { get; set; }
        private string _barberAccesstoken { get; set; }
        public ICommand BackButton { get; }
        public ICommand CheckBox { get; }
        public ICommand CheckCommand { get; }
        public ICommand NextButton { get; }
        public ICommand CommandParameter { get; }      
        private ObservableCollection<DataSalon> _selectedList { get; set; }
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
            _selectedList = new ObservableCollection<DataSalon>();
            _barberAccesstoken = data.BarberAccessToken;
            _salonName = data.SalonName;
            _start = data.StartTime;
            _end = data.EndTime;
            BackButton = new Command(BackPage);
            CheckCommand = new Command(CheckboxCheckChanged);
            NextButton = new Command(ButtonClickedAsync);
        }
        private void CheckboxCheckChanged(object sender)
        {

            var checkbox = (Plugin.InputKit.Shared.Controls.CheckBox)sender;
            var ob = checkbox.BindingContext as DataSalon;
            if (ob != null)
            {
                _numberOfSelectedItems += ob.Price;
                AddOrUpdatetheResult(ob, checkbox);

            }
        }
        private void AddOrUpdatetheResult(DataSalon ob, Plugin.InputKit.Shared.Controls.CheckBox checkbox)
        {
            if (checkbox.IsChecked)
            {
                _selectedList.Add(ob);
            }
            if (!checkbox.IsChecked)
            {
                _selectedList.Remove(ob);
            }
        }            
        private async void ButtonClickedAsync(object sender)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SearchTimePage(_selectedList, _barberAccesstoken, _salonName, _start, _end));
        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
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