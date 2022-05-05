using customer_app.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class LoginViewModels :BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string _email;
        private string _password;
        IAuth auth;
        public Command SubmitCommand { get; }
        public ICommand ResetPasswordCommad { get; }
        public ICommand RegisterPage { get; }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public LoginViewModels()
        {
            auth = DependencyService.Get<IAuth>();
            SubmitCommand = new Command(async () => await SignIn(_email, _password));
            ResetPasswordCommad = new Command(OnForgetPassword);
            RegisterPage = new Command(OnRegisterPage);
        }
        private async void OnRegisterPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
        }
        private async void OnForgetPassword()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ResetPasswordNewPasswordPage());
        }
        async Task SignIn(string _email, string _password)
        {

            IsBusy = true;
            if (_email != null && _password != null)
            {

                string token = await auth.LoginWithEmailAndPassword(_email, _password);

                if (token != string.Empty)
                {

                    try
                    {

                        await SecureStorage.SetAsync("oauth_token", token);
                        IsBusy = false;
                        App.Current.MainPage = new AppShell();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                IsBusy = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Email And Password is Empty", "ok");
                IsBusy = false;

            }
        }     
        

    }
}
