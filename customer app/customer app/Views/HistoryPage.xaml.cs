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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

       
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchServicesPage());
        }
        private void loadBarbers(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchBarberPage());
        }
        private void LoadTime(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchTimePage());
        }
        
    }
}