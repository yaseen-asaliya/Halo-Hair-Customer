using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using customer_app.Services;
using customer_app.Models;
using customer_app.Views;

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
        public string ConfirmPassword { get; set; }
        public ICommand SigUpCommad { get; }

        public RegisterViewModels()
        {
            auth = DependencyService.Get<IAuth>();
            _firebase = new FireBaseHaloHair();
            SigUpCommad = new Command(async () => await signUp(Email, Password));
        }
        private async void addUser(string AccessToken_User)
        {
            AuthenticationModel addUser = new AuthenticationModel();
            {
                addUser.PersonName = Name;
                addUser.Phone = Phone;
                addUser.AccessToken_User = AccessToken_User;


            }
            await _firebase.AddNewUser(addUser);
        }


        private async Task signUp(string email, string password)
        {

            if (Password == ConfirmPassword)
            {
                string acccessToken = await auth.SignUpWithEmailAndPassword(email, password);
                if (null != acccessToken)
                {
                    addUser(acccessToken);
                    await Application.Current.MainPage.DisplayAlert("Successful", "Register User", "ok");
                    await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Confirm password is incorrect", "ok");
            }
        }


    }
}