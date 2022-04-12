using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace customer_app.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarbersList : ContentView
    {
        public BarbersList()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty NameSalon = BindableProperty.Create(nameof(Name_Salon), typeof(string), typeof(BarbersList), string.Empty);
        public string Name_Salon
        {
            get => (string)GetValue(NameSalon);


            set => SetValue(NameSalon, value);
        }

        public static readonly BindableProperty Salon_ = BindableProperty.Create(nameof(Salon), typeof(string), typeof(BarbersList), string.Empty);
        public string Salon
        {
            get => (string)GetValue(Salon_);


            set => SetValue(Salon_, value);
        }


        public static readonly BindableProperty Location_ = BindableProperty.Create(nameof(Location), typeof(string), typeof(BarbersList), string.Empty);
        public string Location
        {
            get => (string)GetValue(Location_);


            set => SetValue(Location_, value);
        }

        public static readonly BindableProperty startTime = BindableProperty.Create(nameof(StartTime), typeof(string), typeof(BarbersList), string.Empty);
        public string StartTime
        {
            get => (string)GetValue(startTime);


            set => SetValue(startTime, value);
        }

        public static readonly BindableProperty endTime = BindableProperty.Create(nameof(EndTime), typeof(string), typeof(BarbersList), string.Empty);
        public string EndTime
        {
            get => (string)GetValue(endTime);


            set => SetValue(endTime, value);
        }
    }
}
