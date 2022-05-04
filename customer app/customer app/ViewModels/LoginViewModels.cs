﻿using customer_app.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class LoginViewModels
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string _email;
        private string _password;
        IAuth auth;
        public Command SubmitCommand { get; }
        public ICommand ResetPasswordCommad { get; }
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
        public ICommand RegisterPage { get; }
        public LoginViewModels()
        {
            auth = DependencyService.Get<IAuth>();
            SubmitCommand = new Command(async () => await SignIn(_email, _password));
            ResetPasswordCommad = new Command(onForgetPassword);
            RegisterPage = new Command(onRegisterPage);
        }
        private async void onRegisterPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
        }
        private async void onForgetPassword()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ResetPasswordNewPasswordPage());
        }
        async Task SignIn(string _email, string _password)
        {

            if (_email != null && _password != null)
            {

                string token = await auth.LoginWithEmailAndPassword(_email, _password);

                if (token != string.Empty)
                {

                    try
                    {
                        await SecureStorage.SetAsync("oauth_token", token);
                        App.Current.MainPage = new AppShell();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Email And Password is Empty", "ok");

            }
        }

    }
}
