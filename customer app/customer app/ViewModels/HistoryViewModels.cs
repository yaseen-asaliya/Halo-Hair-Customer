using customer_app.Models;
using customer_app.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class HistoryViewModels : BaseViewModel
    {
        private FireBaseHaloHair _firebase;
        private ObservableCollection<DataReservationsModel> _history { get; set; }
        private ObservableCollection<DataReservationsModel> _filltedHistory;
        public ICommand DeleteCommand { get; }
        public ICommand DeleteAppointmentCommand { get; }
        public ICommand BackButton { get; }
        private static string _accessToken { get; set; }
        public ObservableCollection<DataReservationsModel> FilltedHistory
        {
            get
            {
                return _filltedHistory;
            }
            set
            {
                _filltedHistory = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DataReservationsModel> History
        {
            get { return _history; }
            set
            {
                _history = value;
                OnPropertyChanged();
            }
        }
        private async void accessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                _accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public HistoryViewModels()
        {
            accessToken();
            _firebase = new FireBaseHaloHair();
            History = new ObservableCollection<DataReservationsModel>();
            History = _firebase.GetDataReservation();
            FilltedHistory = new ObservableCollection<DataReservationsModel>();

            History.CollectionChanged += ServicesChanged;
            DeleteCommand = new Command(OnDeleteTapped);
            BackButton = new Command(BackPage);
        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private void ServicesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                DataReservationsModel services = e.NewItems[0] as DataReservationsModel;

                if (services.CustomerAccessToken == _accessToken)
                {
                    FilltedHistory.Add(services);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                DataReservationsModel services = e.OldItems[0] as DataReservationsModel;
                FilltedHistory.Remove(services);
            }
        }
        private async void OnDeleteTapped(object obj)
        {
            DataReservationsModel HistroyModel = (DataReservationsModel)obj;
            var res = await App.Current.MainPage.DisplayAlert("Are you sure that want to delete?", $"Appointment will delete only from history, call barber to cancel ", "Yes", "Cancel");
            if (res)
            {
                await _firebase.DeleteHistory(HistroyModel);
            }

        }

    }
}