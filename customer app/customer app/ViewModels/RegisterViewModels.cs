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
        private IAuth _auth;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string ConfirmPassword { get; set; }
        public ICommand SigUpCommad { get; }

        public RegisterViewModels()
        {
            _auth = DependencyService.Get<IAuth>();
            _firebase = new FireBaseHaloHair();
            SigUpCommad = new Command(async () => await SignUp(Email, Password));
        }
        private async void AddUser(string CustomerAccessToken)
        {
            AuthenticationModel addUser = new AuthenticationModel();
            {
                addUser.CustomerName = Name;
                addUser.Phone = Phone;
                addUser.CustomerAccessToken = CustomerAccessToken;
            }
            await _firebase.AddNewUser(addUser);
        }
        private async Task SignUp(string email, string password)
        {

            if (Password == ConfirmPassword)
            {
                string acccessToken = await _auth.SignUpWithEmailAndPassword(email, password);
                if (null != acccessToken)
                {
                    AddUser(acccessToken);
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