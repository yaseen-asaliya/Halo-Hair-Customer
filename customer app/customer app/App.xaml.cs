using customer_app.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace customer_app
{
    public partial class App : Application
    {
        IAuth auths;
        public App()
        {
            InitializeComponent();
            auths = DependencyService.Get<IAuth>();

            if (auths.IsSigIn())
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }



        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
