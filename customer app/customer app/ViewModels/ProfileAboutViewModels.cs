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

        public string Location { get; set; }
        private static string _accessToken { get; set; }
        public ICommand BackPage { get; }
        public ICommand LogOut { get; }
        public ICommand ProfileAboutPage { get; }
        public ICommand EditPhoneCommand { get; }
        public ICommand EditNameCommand { get; }


        public ObservableCollection<ProfilePageModel> Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ProfilePageModel> _myProfile;
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
        private async Task AccessToken()
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

        public ProfileAboutViewModels()
        {
            AccessToken();
            _firebase = new FireBaseHaloHair();
            Myprofile = new ObservableCollection<ProfilePageModel>();
            Profile = new ObservableCollection<ProfilePageModel>();
            Profile = _firebase.ProfilePage();
            Profile.CollectionChanged += serviceschanged;
            LogOut = new Command(PerformLogOut);
            BackPage = new Command(backPage);
            ProfileAboutPage = new Command(onProfileAboutPage);
            EditNameCommand = new Command(onEditNameCommand);
            EditPhoneCommand = new Command(onEditPhoneCommand);
            LogOut = new Command(PerformLogOut);
        }

        private async void onEditNameCommand(object obj)
        {



            string result = await App.Current.MainPage.DisplayPromptAsync("Edit Name", "New Name");
            if (result != null)
            {
                ProfilePageModel profilePage = new ProfilePageModel();
                {
                    profilePage.AccessToken_User = _accessToken;
                    //  profilePage.PersonName = PersonName;
                    profilePage.Phone = Phone;
                    profilePage.PersonName = result;
                    //    profilePage.location = location;

                }
                _firebase.UpdatePerson(profilePage);
            }
        }

        private async void onEditPhoneCommand(object sender)
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("Edit Phone", "New Phone");
            if (result != null)
            {
                ProfilePageModel profilePage = new ProfilePageModel();
                {
                    profilePage.AccessToken_User = _accessToken;
                    //profilePage.NameSalon = NameSalon;
                    profilePage.Phone = result;
                    profilePage.PersonName = PersonName;
                    //   profilePage.location = location;

                }
                _firebase.UpdatePerson(profilePage);
            }

        }






















        private async void onProfileAboutPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ProfileAboutPage());
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
        private string PersonName { get; set; }
        //    private string NameSalon { get; set; }
        private string Phone { get; set; }
        private string location { get; set; }

    }
}
