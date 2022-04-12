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
    public partial class ProfileSettingsPage : ContentPage
    {
        public ProfileSettingsPage()
        {
            InitializeComponent();
        }
        public async void aboutbutton_Clicked()
        {
            await Navigation.PushModalAsync(new ProfileAboutPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}