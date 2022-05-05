using customer_app.Models;
using customer_app.Services;
using customer_app.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class SearchTimeViewModels : BaseViewModel
    {
        private FireBaseHaloHair _firebase;
        private ObservableCollection<DataSalon> _selectedList;
        private ObservableCollection<TimeModel> _filtertime;
        private ObservableCollection<TimeModel> filtertimemodel { get; set; }
        private ObservableCollection<string> listservices { get; set; }
        public ObservableCollection<AppointmentmModel> times { get; set; }
        public ICommand BackButton { get; set; }
        private string _liststring { get; set; }
        private string _nameSoaln { get; set; }
        private string _accesstoken { get; set; }
        private AppointmentmModel _timeObj { get; set; }
        public string CalendarSelectedDate { get; set; }
        public ICommand DateSelectedCommand { get; }
        public ICommand Appointment { get; }
        public ICommand TimesCommand { get; }
        private string _selectedTime { get; set; }
        private bool _isAvabile { get; set; }
        private int ID { get; set; }
        public SearchTimeViewModels()
        {

        }
        public SearchTimeViewModels(ObservableCollection<DataSalon> selectedList, string Barberaccesstoken, string NamSoaln, string start, string end)
        {
            string Today = DateTime.Now.Date.ToString("d");
            CalendarSelectedDate = Today;
            _firebase = new FireBaseHaloHair();
            FillterTime = new ObservableCollection<TimeModel>();
            filtertimemodel = new ObservableCollection<TimeModel>();
            filtertimemodel = _firebase.GetDTimeSalon();
            filtertimemodel.CollectionChanged += FavoriteCollectionChanged;

            this._selectedList = selectedList;
            foreach (DataSalon item in selectedList)
            {
                _liststring += item.ServiceName.ToString() + Environment.NewLine;

            }
            _nameSoaln = NamSoaln;
            _accesstoken = Barberaccesstoken;

            listservices = new ObservableCollection<string>();

            foreach (DataSalon item in selectedList)
            {
                listservices.Add(item.ServiceName);

            }
            times = new ObservableCollection<AppointmentmModel>();


            Appointment = new Command(async () => await AddTime(CalendarSelectedDate, _liststring, _selectedTime, Barberaccesstoken, _nameSoaln, _isAvabile, ID));
            TimesCommand = new Command(OnTime);
            BackButton = new Command(BackPage);
        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private void Print(ObservableCollection<TimeModel> fillterTime)
        {
            string text = "";
            int count = 0;
            bool isavabile;


            foreach (var item in fillterTime)
            {
                if (item.BarberAccessToken == _accesstoken)
                {
                    foreach (var time in item.Time)
                    {
                        _timeObj = new AppointmentmModel();
                        text = item.Time[count].Item1.ToString();
                        isavabile = item.Time[count].Item2;
                        if (!isavabile)
                        {
                            _timeObj.TimeSelected = text;
                            _timeObj.Id = count;
                            times.Add(_timeObj);                            
                        }
                        count++;
                    }
                }
                count = 0;
            }
        }
        private async Task AddTime(string calendarSelectedDate, string liststring, string selectedTime, string accesstoken_barbar, string nameSoaln, bool isAvabile, int id)
        {            
            if (calendarSelectedDate != null && liststring != null && selectedTime != null && accesstoken_barbar != null)
            {
                var res = await App.Current.MainPage.DisplayAlert("Booking Appointment", $"Are you sure you want to book an appointment? ", "Yes", "Cancel");
                if (res)
                {
                    await _firebase.AddTime(calendarSelectedDate, liststring, selectedTime, accesstoken_barbar, nameSoaln, isAvabile, id);
                    await Application.Current.MainPage.DisplayAlert("successful", "Appointment Booked\n Salon:" + nameSoaln + "\nDate: " + calendarSelectedDate + "\nTime: " + selectedTime, "ok");
                    await Xamarin.Forms.Shell.Current.GoToAsync("//HomePage");
                }              

            }
            else
                await Application.Current.MainPage.DisplayAlert("Failed", "Please fill in all the data", "ok");
        }        
        public ObservableCollection<TimeModel> FillterTime
        {
            get
            {
                return _filtertime;
            }
            set
            {
                _filtertime = value;
                OnPropertyChanged();

            }

        }
        private void FavoriteCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0] != null)
                {
                    FillterTime.Add((TimeModel)e.NewItems[0]);
                    OnPropertyChanged();
                    Print(FillterTime);
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                FillterTime.Remove((TimeModel)e.OldItems[0]);
                OnPropertyChanged();
            }
        }        
        private async void OnTime(object obj)
        {
            AppointmentmModel appointmentmModel = (AppointmentmModel)obj;
            TimeModel timeModel;
            var time = appointmentmModel.TimeSelected;
            if (time == null)
            {
                return;
            }
            else
            {
                _selectedTime = time.ToString();
                if (_isAvabile == appointmentmModel.IsAvabile)
                {
                    _isAvabile = true;
                    ID = appointmentmModel.Id;

                }

            }

        }
    }
}

