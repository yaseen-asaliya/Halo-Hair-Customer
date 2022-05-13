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
            var TimeSalons = _firebaseClient.Child("Worktime").AsObservable<TimeModel>().AsObservableCollection();
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
                scheduleTimeModel.TimeSelected = selectedTime;
                scheduleTimeModel.CustomerName = PersonName;
                scheduleTimeModel.SalonName = nameSoaln;
                scheduleTimeModel.BarberAccessToken = accesstokenbarbar;
                scheduleTimeModel.CustomerAccessToken = userAccessToken;
                scheduleTimeModel.HistoryId = ID;
                scheduleTimeModel.ReservationsId = ID;
                scheduleTimeModel.Id = id;
            }
            await Update(id, selectedTime, accesstokenbarbar);

            await _firebaseClient.Child("ReservationsRequest").PostAsync(scheduleTimeModel);
            await _firebaseClient.Child("History").PostAsync(scheduleTimeModel);

        }
        public async Task Update(int Id, string selectedTime, string Accesstoken)
        {

            TimeModel timeModel = new TimeModel();
            {
                timeModel.Item1 = selectedTime;
                timeModel.Item2 = true;
            }


            var toUpdateChild = (await _firebaseClient.Child("Worktime").OnceAsync<TimeModel>())
                   .FirstOrDefault(item => item.Object.Time[Id].Item1 == selectedTime && item.Object.BarberAccessToken == Accesstoken);

            await _firebaseClient
                  .Child($"Worktime")
                  .Child(toUpdateChild.Key)
                  .Child($"Time/{Id}")
                  .PutAsync(timeModel);                        
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
        public async Task AddNewUser(AuthenticationModel addUser)
        {
            await _firebaseClient.Child("Customers").PostAsync(addUser);
        }
        public async Task OnDeleteAppointment(int Id_Appointmentm)
        {
            var todelete = (await _firebaseClient.Child("ReservationsRequest").OnceAsync<DataReservationsModel>())
                                .FirstOrDefault(item => item.Object.ReservationsId == Id_Appointmentm);
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
                    .FirstOrDefault(item => item.Object.HistoryId == control.HistoryId);
            await _firebaseClient.Child("History").Child(todelete.Key).DeleteAsync();
        }
        public ObservableCollection<ProfilePageModel> ProfilePage()
        {
            var Users_Customer = _firebaseClient.Child("Customers").AsObservable<ProfilePageModel>().AsObservableCollection();


            return Users_Customer;
        }
        public async Task UpdatePerson(ProfilePageModel profilePageModel)
        {
            var todelete = (await _firebaseClient.Child("Customers").OnceAsync<ProfilePageModel>())
                   .FirstOrDefault(item => item.Object.CustomerAccessToken == userAccessToken);
            try
            {
                await _firebaseClient
                     .Child("Customers")
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