using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using customer_app.Models;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace customer_app.Services
{
    public class FireBaseHaloHair
    {
        FirebaseClient firebaseClient;
        public FireBaseHaloHair()
        {
            firebaseClient = new FirebaseClient("https://halo-hair-676ed-default-rtdb.firebaseio.com");
        }
        public ObservableCollection<DataServicesModel> getServices()
        {
            var servicesData = firebaseClient.Child("Services").AsObservable<DataServicesModel>().AsObservableCollection();

            return servicesData;
        }
        public ObservableCollection<DataSalon> GetDataSalon()
        {
            var dataSalons = firebaseClient.Child("ScheduleTime").AsObservable<DataSalon>().AsObservableCollection();

            return dataSalons;
        }

        public async Task AddNewUser(string name, long phone, string ulr, string location)
        {
            Console.WriteLine(ulr.ToString());
            AuthenticationModel addUser = new AuthenticationModel();
            {
                addUser.Name = name;
                addUser.Phone = phone;
                addUser.ulr = ulr;
                addUser.location = location;


            }
            await firebaseClient.Child("Users_Customer").PostAsync(addUser);

        }
    }
}
