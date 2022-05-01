using customer_app.Models;
using customer_app.Services;
using customer_app.Views;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class ProfileAboutViewModels : BaseViewModel
    {
        FireBaseHaloHair _firebase;
        private ObservableCollection<ProfilePageModel> _profile;
        private ObservableCollection<ProfilePageModel> _myProfile;
        public string Location { get; set; }
        private static string _accessToken { get; set; }
        public ICommand BackPage { get; }
        public ICommand LogOut { get; }
        public ObservableCollection<ProfilePageModel> Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ProfilePageModel> Myprofile
        {
            get
            {
                return _myProfile;
            }
            set
            {
                _myProfile = value;
                OnPropertyChanged();
            }
        }

        public ProfileAboutViewModels()
        {
            accessToken();
            _firebase = new FireBaseHaloHair();
            Myprofile = new ObservableCollection<ProfilePageModel>();
            Profile = new ObservableCollection<ProfilePageModel>();
            Profile = _firebase.ProfilePage();
            Profile.CollectionChanged += serviceschanged;
            LogOut = new Command(PerformLogOut);
            BackPage = new Command(backPage);
        }
        private async Task accessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                _accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }        
        private async void backPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private void serviceschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ProfilePageModel profilePageModel = e.NewItems[0] as ProfilePageModel;
                Console.WriteLine(e.NewItems[0]);
                Console.WriteLine(e.NewItems[0].GetType());
                if (profilePageModel.AccessToken_User == _accessToken)
                {
                    Myprofile.Add(profilePageModel);
                    SecureStorage.SetAsync("NameUser", profilePageModel.PersonName.ToString());
                }
            }
        }
        private async void PerformLogOut()
        {
            var auth = DependencyService.Resolve<IAuth>();
            auth.IsSigOut();
            //await Xamarin.Forms.Shell.Current.GoToAsync("//LoginPage");
            await Application.Current.MainPage.DisplayAlert("Logout", "you are logout", "ok");
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
    }
}
