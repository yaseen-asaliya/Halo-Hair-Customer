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
            BindingContext = ServicesViewModel;
            selectedList = new ObservableCollection<DataSalon>();
            accesstoken_barbar = data.AccessToken_Barbar;
            NameSolan = data.NameSalon;
            start = data.StartTime;
            end = data.EndTime;
            BindingContext = new SearchServicesViewModels(data);

        }
        private ObservableCollection<DataSalon> selectedList;

        private int count = 0;

        private void checkbox_CheckChanged(object sender, EventArgs e)

        {

            var checkbox = (Plugin.InputKit.Shared.Controls.CheckBox)sender;


            var ob = checkbox.BindingContext as DataSalon;

            if (ob != null)
            {
                count += ob.Prices;
                AddOrUpdatetheResult(ob, checkbox);

            }

        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
        typeof(ICommand),
          typeof(SearchServicesPage),
         default(ICommand));
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
      typeof(object),
      typeof(SearchServicesPage),
      default);
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
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

        private void SearchBar_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var _container = BindingContext as SearchServicesViewModels;
            SearchServices.BatchBegin();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                SearchServices.ItemsSource = _container.FilltedServices;
            else
                SearchServices.ItemsSource = _container.FilltedServices.Where(i => i.Service_Name.Contains(e.NewTextValue));


        }
        private void Button_ClickedAsync(object sender, EventArgs e)
        {

            Navigation.PushModalAsync(new SearchTimePage(selectedList, accesstoken_barbar, NameSolan, start, end));

        }


    }

}