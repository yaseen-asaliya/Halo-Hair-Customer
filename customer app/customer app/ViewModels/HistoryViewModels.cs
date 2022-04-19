using customer_app.Models;
using customer_app.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class HistoryViewModels : BaseViewModel
    {
        FireBaseHaloHair firebase;
        public ObservableCollection<DataReservationsModel> history { get; set; }


        public ObservableCollection<DataReservationsModel> History
        {
            get { return history; }
            set
            {
                history = value;
                OnPropertyChanged();

            }
        }



        private ObservableCollection<DataReservationsModel> filltedhistory;
        public ObservableCollection<DataReservationsModel> FilltedHistory
        {
            get
            {
                return filltedhistory;
            }
            set
            {
                filltedhistory = value;
                OnPropertyChanged();
            }
        }

        private static string accessToken { get; set; }

        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ICommand DeleteCommand { get; }
        public HistoryViewModels()
        {
            AccessToken();
            firebase = new FireBaseHaloHair();
            History = new ObservableCollection<DataReservationsModel>();
            History = firebase.GetDataReservation();
            FilltedHistory = new ObservableCollection<DataReservationsModel>();

            History.CollectionChanged += serviceschanged;
            DeleteCommand = new Command(onDeleteTapped);

        }

        private void serviceschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                DataReservationsModel services = e.NewItems[0] as DataReservationsModel;
                Console.WriteLine(e.NewItems[0]);
                Console.WriteLine(e.NewItems[0].GetType());
                if (services.AccessToken_User == accessToken)
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
            await firebase.DeleteHistory(HistroyModel);




        }


    }
}
