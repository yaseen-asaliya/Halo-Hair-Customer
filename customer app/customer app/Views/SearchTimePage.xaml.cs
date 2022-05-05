using customer_app.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using customer_app.ViewModels;
using System.Collections.ObjectModel;
using customer_app.Models;
using customer_app.ViewModels;
namespace customer_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchTimePage : ContentPage
    {
        public SearchTimePage(ObservableCollection<DataSalon> selectedList, string BarbarAccesstoken, string NameSolan, string start, string end)
        {
            InitializeComponent();
            SearchTimeViewModels searchTimeViewModels = new SearchTimeViewModels(selectedList, BarbarAccesstoken, NameSolan, start, end);
            BindingContext = searchTimeViewModels;
        }
    }
}
