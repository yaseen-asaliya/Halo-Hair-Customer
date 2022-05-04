using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using customer_app.Services;

namespace customer_app.ViewModels
{
    public class RegisterViewModels
    {
        FireBaseHaloHair _firebase;
        IAuth auth;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Location { get; set; }
        public ICommand SigUpCommad { get; }

        public RegisterViewModels()
        {
            auth = DependencyService.Get<IAuth>();
            _firebase = new FireBaseHaloHair();
            SigUpCommad = new Command(async () => await signUp(Email, Password));
        }
        private async void addUser(string name, long phone, string url, string location)
        {
            await _firebase.AddNewUser(name, phone, url, location);
        }
        private async Task signUp(string email, string password)
        {
            try
            {
                string url = await auth.SignUpWithEmailAndPassword(email, password);

                if (null != url)
                {
                    addUser(Name, Phone, url, Location);
                    await Application.Current.MainPage.DisplayAlert("Successful", "Register User", "ok");
                }

                else if (url == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Email already exists", "ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Register User Try agin " + ex.Message, "ok");

            }
        }
    }
}
