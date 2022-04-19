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
        private int start { get; set; }
        private int end { get; set; }
        public SearchServicesPage(DataSalon data)
        {
            InitializeComponent();
            SearchServicesViewModels ServicesViewModel = new SearchServicesViewModels(data);
            BindingContext = ServicesViewModel;
            selectedList = new ObservableCollection<DataSalon>();
            accesstoken_barbar = data.AccessToken_Barbar;
            NameSolan = data.NameSalon;
            start = data.StartTime;
            end = data.EndTime;

        }
        private ObservableCollection<DataSalon> selectedList;


        private void checkbox_CheckChanged(object sender, EventArgs e)

        {

            var checkbox = (Plugin.InputKit.Shared.Controls.CheckBox)sender;


            var ob = checkbox.BindingContext as DataSalon;

            if (ob != null)
            {

                AddOrUpdatetheResult(ob, checkbox);

            }

        }

        private async Task AddOrUpdatetheResult(DataSalon ob, Plugin.InputKit.Shared.Controls.CheckBox checkbox)
        {
            if (checkbox.IsChecked)
            {


                selectedList.Add(ob);

            }
            if (!checkbox.IsChecked)
            {
                selectedList.Remove(ob);
            }
        }


        private void Button_ClickedAsync(object sender, EventArgs e)
        {

            Navigation.PushModalAsync(new SearchTimePage(selectedList, accesstoken_barbar, NameSolan, start, end));

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }

}