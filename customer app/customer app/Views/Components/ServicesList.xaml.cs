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
    public partial class ServicesList : ContentView
    {
        public ServicesList()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty ServiceName = BindableProperty.Create(nameof(Service_Name), typeof(string), typeof(ServicesList), string.Empty);
        public string Service_Name
        {
            get => (string)GetValue(ServiceName);


            set => SetValue(ServiceName, value);
        }

        public static readonly BindableProperty Services_ = BindableProperty.Create(nameof(Services), typeof(string), typeof(ServicesList), string.Empty);
        public string Services
        {
            get => (string)GetValue(Services_);


            set => SetValue(Services_, value);
        }

        public static readonly BindableProperty Prices_ = BindableProperty.Create(nameof(Prices), typeof(string), typeof(ServicesList), string.Empty);
        public string Prices
        {
            get => (string)GetValue(Prices_);


            set => SetValue(Prices_, value);
        }

        public static readonly BindableProperty TimeNeeded = BindableProperty.Create(nameof(Time_Needed), typeof(string), typeof(ServicesList), string.Empty);
        public string Time_Needed
        {
            get => (string)GetValue(TimeNeeded);


            set => SetValue(TimeNeeded, value);
        }
    }
}
