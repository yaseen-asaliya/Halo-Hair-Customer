using customer_app.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class ResetPasswordNewPasswordViewModels
    {
        public ResetPasswordNewPasswordViewModels()
        {
            SendNewPassword = new Command(OnResetPassword);
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string email;
        public ICommand SendNewPassword { get; }


        private async void OnResetPassword()
        {
            var auth = DependencyService.Resolve<IAuth>();
            auth.ResetPassword(email);


            await Application.Current.MainPage.DisplayAlert("Reset Password", "please check your email ", "ok");
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());



        }
    }
}
