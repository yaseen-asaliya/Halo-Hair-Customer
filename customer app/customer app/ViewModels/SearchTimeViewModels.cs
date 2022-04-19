using customer_app.Models;
using customer_app.Services;
using customer_app.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class SearchTimeViewModels
    {
        FireBaseHaloHair Firebase;
        private ObservableCollection<DataSalon> selectedList;

        private ObservableCollection<string> listservices { get; set; }
        public SearchTimeViewModels()
        {
        }
        private string liststring { get; set; }
        private int starttime { get; set; }
        private string nameSoaln { get; set; }
        private int endtime { get; set; }
        public SearchTimeViewModels(ObservableCollection<DataSalon> selectedList, string accesstoken_barbar, string NamSoaln, int start, int end)
        {
            this.selectedList = selectedList;
            foreach (DataSalon item in selectedList)
            {
                liststring += item.Service_Name.ToString() + Environment.NewLine;

            }
            nameSoaln = NamSoaln;
            starttime = start;
            endtime = end;

            listservices = new ObservableCollection<string>();



            foreach (DataSalon item in selectedList)
            {
                listservices.Add(item.Service_Name);

            }

            SearchTime();

            Firebase = new FireBaseHaloHair();
            appointment = new Command(async () => await AddTime(CalendarSelectedDate, liststring, selectedTime, accesstoken_barbar, nameSoaln));
            TimesCommand = new Command(onTime);
        }

        private async Task AddTime(string calendarSelectedDate, string liststring, string selectedTime, string accesstoken_barbar, string nameSoaln)
        {
            if (calendarSelectedDate != null && liststring != null && selectedTime != null && accesstoken_barbar != null)
            {
                await Firebase.AddTime(CalendarSelectedDate, liststring, selectedTime, accesstoken_barbar, nameSoaln);
                await Application.Current.MainPage.DisplayAlert("successful", "Appointment in : " + nameSoaln + "\n on Time : " + selectedTime, "ok");
                await Xamarin.Forms.Shell.Current.GoToAsync("//HomePage");

            }
            else
                await Application.Current.MainPage.DisplayAlert("Failed", "Please fill in all the data", "ok");

        }

        public string CalendarSelectedDate { get; set; }

        public ICommand DateSelectedCommand { get; }

        public ICommand appointment { get; }

        public ObservableCollection<AppointmentmModel> times { get; set; }

        public void SearchTime()
        {
            times = new ObservableCollection<AppointmentmModel>();

            int start = starttime;
            int end = endtime;
            AppointmentmModel temp = new AppointmentmModel();
            for (double i = start; i <= end; i += 0.5)
            {
                if (i - (int)i == 0.5)
                {
                    if (i > 12)
                    {
                        temp.time = ((i - 0.5) - 12).ToString() + ":30 PM";
                    }
                    else
                    {
                        temp.time = (i - 0.5).ToString() + ":30 AM";
                    }
                }
                else
                {
                    if (i > 12)
                    {
                        temp.time = (i - 12).ToString() + ":00 PM";
                    }
                    else
                    {
                        temp.time = i.ToString() + ":00 AM";
                    }
                }
                AppointmentmModel timeObj = new AppointmentmModel();
                timeObj.time = temp.time.ToString();
                times.Add(timeObj);
            }
        }
        
        public ICommand TimesCommand { get; }
        private string selectedTime { get; set; }

        private async void onTime(object obj)
        {
            AppointmentmModel appointmentmModel = (AppointmentmModel)obj;
            var time = appointmentmModel.time;
            if (time == null)
            {
                return;
            }
            else
            {
                selectedTime = time.ToString();
            }

            
        }


    }

}

