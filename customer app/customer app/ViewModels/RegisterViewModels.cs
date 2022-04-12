using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using customer_app.Models;
using customer_app.Services;

namespace customer_app.ViewModels
{
    public class RegisterViewModels
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public long phone { get; set; }
        public string location { get; set; }

        public ICommand SigUpCommad { get; }


        IAuth auth;

        FireBaseHaloHair firebase;

        public RegisterViewModels()
        {
            auth = DependencyService.Get<IAuth>();
            firebase = new FireBaseHaloHair();
            SigUpCommad = new Command(async () => await SignUp(email, password));

        }

        private async void AddUser(string name, long phone, string ulr,string location)
        {
            await firebase.AddNewUser(name, phone, ulr,location);
        }


        private async Task SignUp(string email, string password)
        {

            try
            {
                string ulr = await auth.SignUpWithEmailAndPassword(email, password);
                Console.WriteLine(ulr);

                if (null != ulr)
                {
                    AddUser(name, phone, ulr,location);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("The Exceptions : " + ex);
            }
        }

    }


}
