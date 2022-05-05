using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using customer_app.Models;
using Xamarin.Forms;
using customer_app.ViewModels;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace customer_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchServicesPage : ContentPage
    {
        private string accesstoken_barbar { get; set; }
        private string NameSolan { get; set; }
        private string start { get; set; }
        private string end { get; set; }
        public SearchServicesPage(DataSalon data)
        {
            InitializeComponent();
            SearchServicesViewModels ServicesViewModel = new SearchServicesViewModels(data);
            BindingContext = new SearchServicesViewModels(data);

        }     
   

        private void SearchBar_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var _container = BindingContext as SearchServicesViewModels;
            SearchServices.BatchBegin();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                SearchServices.ItemsSource = _container.FilltedServices;
            else
                SearchServices.ItemsSource = _container.FilltedServices.Where(i => i.ServiceName.Contains(e.NewTextValue));


        }
       
    }

}