using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using customer_app.Models;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace customer_app.Services
{
    public class FireBaseHaloHair
    {
        FirebaseClient firebaseClient;
        public FireBaseHaloHair()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");
            AccessToken();
        }
        public ObservableCollection<DataSalon> getServices()
        {
            var servicesData = firebaseClient.Child("Services").AsObservable<DataSalon>().AsObservableCollection();

            return servicesData;
        }
        public ObservableCollection<DataSalon> GetDataSalon()
        {
            var dataSalons = firebaseClient.Child("ScheduleTime").AsObservable<DataSalon>().AsObservableCollection();

            return dataSalons;
        }

        private string PersonName { get; set; }
        public async Task AddTime(string calendarSelectedDate, string liststring, string selectedTime, string accesstoken_barbar, string nameSoaln)
        {
            AppointmentmModel scheduleTimeModel = new AppointmentmModel();
            {

                scheduleTimeModel.DateSelected = calendarSelectedDate;
                scheduleTimeModel.ListOfService = liststring;
                scheduleTimeModel.time = selectedTime;
                scheduleTimeModel.PersonName = PersonName;
                scheduleTimeModel.NameSolan = nameSoaln;
                scheduleTimeModel.AccessToken_Barbar = accesstoken_barbar;
                scheduleTimeModel.AccessToken_User = AccessToken_User;
                scheduleTimeModel.ID_History = 1;
            }

            await firebaseClient.Child("ReservationsRequest").PostAsync(scheduleTimeModel);
            await firebaseClient.Child("History").PostAsync(scheduleTimeModel);

        }
        private string AccessToken_User { get; set; }
        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                var oname = await SecureStorage.GetAsync("NameUser");
                AccessToken_User = oauthToken;
                PersonName = oname;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ObservableCollection<DataReservationsModel> GetDataReservation()
        {
            var dataReservation = firebaseClient.Child("History").AsObservable<DataReservationsModel>().AsObservableCollection();

            return dataReservation;
        }
        public async Task AddNewUser(string name, long phone, string ulr, string location)
        {
            try
            {
                Console.WriteLine(ulr.ToString());
                AuthenticationModel addUser = new AuthenticationModel();
                {
                    addUser.PersonName = name;
                    addUser.Phone = phone;
                    addUser.AccessToken_User = ulr;
                    addUser.location = location;

                }
                await firebaseClient.Child("Users_Customer").PostAsync(addUser);
                await Application.Current.MainPage.DisplayAlert("Successful", "Register User", "ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Register User Try agin " + ex.Message, "ok");

            }


        }
        public ObservableCollection<AuthenticationModel> GetAuthentications()
        {
            var dataprofile = firebaseClient.Child("Authentication").AsObservable<AuthenticationModel>().AsObservableCollection();

            return dataprofile;
        }

        public async Task DeleteHistory(DataReservationsModel control)
        {
            var todelete = (await firebaseClient.Child("History").OnceAsync<DataReservationsModel>())
                    .FirstOrDefault(item => item.Object.ID_History == control.ID_History);
            await firebaseClient.Child("History").Child(todelete.Key).DeleteAsync();
        }

        public ObservableCollection<ProfilePageModel> ProfilePage()
        {
            var Users_Customer = firebaseClient.Child("Users_Customer").AsObservable<ProfilePageModel>().AsObservableCollection();


            return Users_Customer;
        }

    }
}
