using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using customer_app.Models;
using customer_app.Views;
namespace customer_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBarberPage : ContentPage
    {
        public SearchBarberPage()
        {
            InitializeComponent();
        }
        public async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var solan = args.Item as DataSalon;
            if (solan != null)
            {
                await Navigation.PushModalAsync(new SalonInDetailsPage(solan));
                SearchBarber.SelectedItem = null;
            }
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
                     
        }
    }
}