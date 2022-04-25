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
        struct time
        {
            public int hour;
            public int minute;
        }
        struct booked
        {
            public string appointment;//12:39 am
            public bool isBooked; //true
        }
        time getTimeSplitedAsInt(string time)
        {
            time temp = new time();
            string[] tempTime = time.Split(':');
            temp.hour = Int32.Parse(tempTime[0]);
            temp.minute = Int32.Parse(tempTime[1]);
            return temp;
        }
        string getTimeAsString(int time)
        {
            int hours = time / 60;
            int minutes = time % 60;
            string Time = "";
            if (hours > 12)
            {
                if (minutes < 10)
                {
                    Time = (hours - 12) + ":0" + minutes + " PM";

                }
                else
                {
                    Time = (hours - 12) + ":" + minutes + " PM";
                }
            }
            else
            {
                if (minutes < 10)
                {
                    Time = hours + ":0" + minutes + " AM";
                }
                else
                {
                    Time = hours + ":" + minutes + " AM";
                }
            }
            return Time;

        }
        public void SearchTime()
        {
            times = new ObservableCollection<AppointmentmModel>();
            AppointmentmModel temp = new AppointmentmModel();

            //  time startTime = getTimeSplitedAsInt(starttime);
            //  time endTime = getTimeSplitedAsInt(endtime);
            time startTime = getTimeSplitedAsInt("10:35");
            time endTime = getTimeSplitedAsInt("15:34");
            int start = (startTime.hour * 60) + startTime.minute;
            int end = (endTime.hour * 60) + endTime.minute;
            List<booked> ListOfTimes = new List<booked>();

            for (int i = start; i < end; i += 30)
            {
                booked tempTime = new booked();
                tempTime.appointment = getTimeAsString(i);
                tempTime.isBooked = false;
                ListOfTimes.Add(tempTime);

                for(int ii = 0; ii < ListOfTimes.Count; ii++)
                {
                    Console.WriteLine(ListOfTimes[ii].appointment + "status : "+ ListOfTimes[ii].isBooked);
                }
               /* AppointmentmModel timeObj = new AppointmentmModel();
                timeObj.time = temp.time.ToString();
                times.Add(timeObj);*/
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

