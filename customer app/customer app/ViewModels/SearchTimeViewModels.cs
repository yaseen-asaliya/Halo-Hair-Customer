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
        FireBaseHaloHair Firebase;
        private ObservableCollection<DataSalon> selectedList;

        private ObservableCollection<string> listservices { get; set; }
        public ObservableCollection<AppointmentmModel> times { get; set; }
        public SearchTimeViewModels()
        {
        }
        private string liststring { get; set; }
        private string nameSoaln { get; set; }
        private string accesstoken { get; set; }
        private AppointmentmModel timeObj { get; set; }
        public ICommand BackPage { get; }

        public SearchTimeViewModels(ObservableCollection<DataSalon> selectedList, string accesstoken_barbar, string NamSoaln, string start, string end)
        {
            Firebase = new FireBaseHaloHair();
            FillterTime = new ObservableCollection<TimeModel>();
            filtertimemodel = new ObservableCollection<TimeModel>();
            filtertimemodel = Firebase.GetDTimeSalon();
            filtertimemodel.CollectionChanged += Favorite_CollectionChanged;


            this.selectedList = selectedList;
            foreach (DataSalon item in selectedList)
            {
                liststring += item.Service_Name.ToString() + Environment.NewLine;

            }
            nameSoaln = NamSoaln;
            accesstoken = accesstoken_barbar;

            listservices = new ObservableCollection<string>();

            foreach (DataSalon item in selectedList)
            {
                listservices.Add(item.Service_Name);

            }
            times = new ObservableCollection<AppointmentmModel>();




            appointment = new Command(async () => await AddTime(CalendarSelectedDate, liststring, selectedTime, accesstoken_barbar, nameSoaln, isAvabile, id));
            TimesCommand = new Command(onTime);

            BackPage = new Command(Back_Page);

        }
        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private void print(ObservableCollection<TimeModel> fillterTime)
        {
            string text = "";
            int x = 0;
            bool isavabile = false;


            foreach (var item in fillterTime)
            {
                if (item.AccessToken_Barbar == accesstoken)
                {

                    foreach (var time in item.Time)
                    {
                        timeObj = new AppointmentmModel();
                        text = item.Time[x].Item1.ToString();
                        isavabile = item.Time[x].Item2;
                        if (!isavabile)
                        {
                            timeObj.time = text;
                            times.Add(timeObj);
                            x = x + 1;
                        }


                    }
                }
                x = 0;
            }

        }

        private async Task AddTime(string calendarSelectedDate, string liststring, string selectedTime, string accesstoken_barbar, string nameSoaln, bool isAvabile, int id)
        {
            string Today = DateTime.Now.Date.ToString("d");
            CalendarSelectedDate = Today;
            if (calendarSelectedDate != null && liststring != null && selectedTime != null && accesstoken_barbar != null)
            {
                await Firebase.AddTime(Today, liststring, selectedTime, accesstoken_barbar, nameSoaln, isAvabile, id);
                await Application.Current.MainPage.DisplayAlert("successful", "Appointment in : " + nameSoaln + "\n on Time : " + selectedTime, "ok");
                await Xamarin.Forms.Shell.Current.GoToAsync("//HomePage");

            }
            else
                await Application.Current.MainPage.DisplayAlert("Failed", "Please fill in all the data", "ok");

        }

        public string CalendarSelectedDate { get; set; }

        public ICommand DateSelectedCommand { get; }

        public ICommand appointment { get; }





        public ICommand TimesCommand { get; }
        private string selectedTime { get; set; }





        private ObservableCollection<TimeModel> filtertimemodel { get; set; }
        private ObservableCollection<TimeModel> filtertime;
        public ObservableCollection<TimeModel> FillterTime
        {
            get
            {
                return filtertime;
            }
            set
            {
                filtertime = value;
                OnPropertyChanged();

            }

        }




        private void Favorite_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems[0] != null)
                {
                    FillterTime.Add((TimeModel)e.NewItems[0]);
                    OnPropertyChanged();
                    print(FillterTime);
                }
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                FillterTime.Remove((TimeModel)e.OldItems[0]);
                OnPropertyChanged();
            }

        }


        private bool isAvabile { get; set; }
        private int id { get; set; }
        private async void onTime(object obj)
        {
            AppointmentmModel appointmentmModel = (AppointmentmModel)obj;
            TimeModel timeModel;
            var time = appointmentmModel.time;
            if (time == null)
            {
                return;
            }
            else
            {
                selectedTime = time.ToString();
                if (isAvabile == appointmentmModel.isAvabile)
                {
                    isAvabile = true;
                    id = appointmentmModel.id;
                }

            }

        }
    }


}

