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

        public SearchTimePage(ObservableCollection<DataSalon> selectedList, string accesstoken_barbar, string NameSolan, int start, int end)
        {
            InitializeComponent();
            SelectedList = selectedList;
            SearchTimeViewModels searchTimeViewModels = new SearchTimeViewModels(SelectedList, accesstoken_barbar, NameSolan, start, end);
            BindingContext = searchTimeViewModels;
        }

        public ObservableCollection<DataSalon> SelectedList { get; }
    }
}
