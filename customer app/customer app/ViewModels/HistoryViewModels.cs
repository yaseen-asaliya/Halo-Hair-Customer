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
        public ICommand BackPage { get; }
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
        private async Task accessToken()
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

            History.CollectionChanged += servicesChanged;
            DeleteCommand = new Command(onDeleteTapped);
            DeleteAppointmentCommand = new Command(onDeleteAppointment);
            BackPage = new Command(backPage);
        }
        private async void backPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private void servicesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                DataReservationsModel services = e.NewItems[0] as DataReservationsModel;
                Console.WriteLine(e.NewItems[0]);
                Console.WriteLine(e.NewItems[0].GetType());
                if (services.AccessToken_User == _accessToken)
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
        private async void onDeleteTapped(object obj)
        {
            DataReservationsModel HistroyModel = (DataReservationsModel)obj;
            await _firebase.DeleteHistory(HistroyModel);
        }
        private async void onDeleteAppointment(object obj)
        {
            DataReservationsModel AppointmentDelete = (DataReservationsModel)obj;
            //   await firebase.onDeleteAppointment(AppointmentDelete.ID_Reservations);


        }

    }
}
