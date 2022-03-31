using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using customer_app.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace customer_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalonInDetailsPage : ContentPage
    {
        public SalonInDetailsPage(DataSalon dataSalon)
        {
            InitializeComponent();
            GetInfoSalon(dataSalon);
        }

        public void GetInfoSalon(DataSalon dataSalon)
        {
            NameSalon.Text = dataSalon.SalonName;
            EndTime.Text = dataSalon.EndTime;
            StartTime.Text = dataSalon.StartTime;
        }
    }
}