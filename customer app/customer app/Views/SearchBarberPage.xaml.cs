using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using customer_app.ViewModels;
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
            BindingContext = new SearchBarberViewModels();
        }

        private void SearchBar_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var _container = BindingContext as SearchBarberViewModels;
            SearchBarber.BatchBegin();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                SearchBarber.ItemsSource = _container.Salon;
            else
                SearchBarber.ItemsSource = _container.Salon.Where(i => i.SalonName.Contains(e.NewTextValue));


        }
    }

}