using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace customer_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileAboutPage : ContentPage
    {
        public ProfileAboutPage()
        {
            InitializeComponent();
        }

        private void aboutbutton_Clicked(object sender, EventArgs e)
        {
            BoxAbout.IsVisible = true;
            aboutview.IsVisible = true;
            Settingsview.IsVisible = false;
            BoxSettings.IsVisible = false;
        }

        private void settingsbutton_Clicked(object sender, EventArgs e)
        {
            BoxSettings.IsVisible = true;
            Settingsview.IsVisible = true;
            aboutview.IsVisible = false;
            BoxAbout.IsVisible = false;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProfileSettingsPage());
        }
    }
}