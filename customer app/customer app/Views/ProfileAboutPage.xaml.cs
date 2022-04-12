using customer_app.Models;
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

        public static object SelectedItem { get; private set; }

        public async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var Profile = args.Item as Authentication;
            if (Profile != null)
            {
                await Navigation.PushModalAsync(new SalonInDetailsPage(Profile));
                ProfileAboutPage.SelectedItem = null;
            }
        }

        private void Frame_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}