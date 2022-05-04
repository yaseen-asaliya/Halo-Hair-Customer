using Firebase.Database;
using System;
using System.Collections.ObjectModel;
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
        FirebaseClient _firebaseClient;
        Random rnd;
        private string PersonName { get; set; }
        private string userAccessToken { get; set; }
        public FireBaseHaloHair()
        {
            _firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");
            accessToken();
        }

        public ObservableCollection<OfferModel> GetAllOfferImgs()
        {
            return _firebaseClient.Child("Offer").AsObservable<OfferModel>().AsObservableCollection();
        }
        public ObservableCollection<DataSalon> getServices()
        {
            var servicesData = _firebaseClient.Child("Services").AsObservable<DataSalon>().AsObservableCollection();
            return servicesData;
        }
        public ObservableCollection<DataSalon> GetDataSalon()
        {
            var dataSalons = _firebaseClient.Child("ScheduleTime").AsObservable<DataSalon>().AsObservableCollection();
            return dataSalons;
        }
        public ObservableCollection<TimeModel> GetDTimeSalon()
        {
            var TimeSalons = _firebaseClient.Child("TIME").AsObservable<TimeModel>().AsObservableCollection();
            return TimeSalons;
        }
        public async Task AddTime(string calendarSelectedDate, string liststring, string selectedTime, string accesstokenbarbar, string nameSoaln, bool isAvabile, int id)
        {
            rnd = new Random();
            int ID = rnd.Next(1, 256300000);
            AppointmentmModel scheduleTimeModel = new AppointmentmModel();
            {
                scheduleTimeModel.DateSelected = calendarSelectedDate;
                scheduleTimeModel.ListOfService = liststring;
                scheduleTimeModel.time = selectedTime;
                scheduleTimeModel.PersonName = PersonName;
                scheduleTimeModel.NameSolan = nameSoaln;
                scheduleTimeModel.AccessToken_Barbar = accesstokenbarbar;
                scheduleTimeModel.AccessToken_User = userAccessToken;
                scheduleTimeModel.ID_History = ID;
                scheduleTimeModel.ID_Reservations = ID;
                scheduleTimeModel.id = id;
            }
            //   await UpdatePerson(id, selectedTime, accesstokenbarbar);

            await _firebaseClient.Child("ReservationsRequest").PostAsync(scheduleTimeModel);
            await _firebaseClient.Child("History").PostAsync(scheduleTimeModel);

        }
        private async Task accessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                var oname = await SecureStorage.GetAsync("NameUser");
                userAccessToken = oauthToken;
                PersonName = oname;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public ObservableCollection<DataReservationsModel> GetDataReservation()
        {
            var dataReservation = _firebaseClient.Child("History").AsObservable<DataReservationsModel>().AsObservableCollection();

            return dataReservation;
        }
        public async Task AddNewUser(string name, long phone, string url, string location)
        {
            try
            {
                AuthenticationModel addUser = new AuthenticationModel();
                {
                    addUser.PersonName = name;
                    addUser.Phone = phone;
                    addUser.AccessToken_User = url;
                   // addUser.location = location;

                }
                await _firebaseClient.Child("Users_Customer").PostAsync(addUser);
                await Application.Current.MainPage.DisplayAlert("Successful", "Register User", "ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Register User Try agin " + ex.Message, "ok");

            }


        }
        public async Task OnDeleteAppointment(int Id_Appointmentm)
        {
            var todelete = (await _firebaseClient.Child("ReservationsRequest").OnceAsync<DataReservationsModel>())
                                .FirstOrDefault(item => item.Object.ID_Reservations == Id_Appointmentm);
            try
            {
                await _firebaseClient.Child("ReservationsRequest").Child(todelete.Key).DeleteAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Sorry, the barber has accepted the invitation. Please commit to the reservation ", "ok");
            }
        }
        public ObservableCollection<AuthenticationModel> GetAuthentications()
        {
            var dataprofile = _firebaseClient.Child("Authentication").AsObservable<AuthenticationModel>().AsObservableCollection();

            return dataprofile;
        }
        public async Task DeleteHistory(DataReservationsModel control)
        {
            var todelete = (await _firebaseClient.Child("History").OnceAsync<DataReservationsModel>())
                    .FirstOrDefault(item => item.Object.ID_History == control.ID_History);
            await _firebaseClient.Child("History").Child(todelete.Key).DeleteAsync();
        }
        public ObservableCollection<ProfilePageModel> ProfilePage()
        {
            var Users_Customer = _firebaseClient.Child("Users_Customer").AsObservable<ProfilePageModel>().AsObservableCollection();


            return Users_Customer;
        }
        public async Task UpdatePerson(ProfilePageModel profilePageModel)
        {

            var todelete = (await _firebaseClient.Child("Users_Customer").OnceAsync<ProfilePageModel>())
                   .FirstOrDefault(item => item.Object.AccessToken_User == userAccessToken);
            try
            {
                await _firebaseClient
                     .Child($"Users_Customer")
                     .Child(todelete.Key)
                     .PutAsync(profilePageModel);
            }

            catch (Exception ex)
            {
                await Xamarin.Forms.Shell.Current.DisplayAlert("Failed", ex.Message, "ok");
            }
        }







    }
}