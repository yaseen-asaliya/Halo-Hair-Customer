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
        private string _email;
        public ICommand BackButton { get; }
        public ICommand SendNewPassword { get; }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public ResetPasswordNewPasswordViewModels()
        {
            SendNewPassword = new Command(OnResetPassword);
            BackButton = new Command(BackPage);
        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }     
        private async void OnResetPassword()
        {
            var auth = DependencyService.Resolve<IAuth>();
            auth.ResetPassword(_email);
            await Application.Current.MainPage.DisplayAlert("Reset Password", "please check your email ", "ok");
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
    }
}
