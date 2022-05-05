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
        public ICommand Aboutbutton { get; }
        public ICommand Settingsbutton { get; }
        public ICommand TapLanguage { get; }
        public string Action { get; set; }
        private string language;
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
                OnPropertyChanged();
            }
        }
        private bool isVisibleAbout;
        public bool IsVisibleAbout
        {
            get
            {
                return isVisibleAbout;
            }
            set
            {
                isVisibleAbout = value;
                OnPropertyChanged();
            }
        }
        private bool isVisibleSettings;
        public bool IsVisibleSettings
        {
            get
            {
                return isVisibleSettings;
            }
            set
            {
                isVisibleSettings = value;
                OnPropertyChanged();
            }
        }
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
            Aboutbutton = new Command(About_button);
            Settingsbutton = new Command(Settings_button);
            TapLanguage = new Command(Tap_Language);
            Language = "English";
            IsVisibleAbout = true;
            IsVisibleSettings = false;
        }

        private void Settings_button(object obj)
        {
            IsVisibleAbout = false;
            IsVisibleSettings = true;
        }

        private void About_button(object obj)
        {
            IsVisibleSettings = false;
            IsVisibleAbout = true;
        }

        private async void Tap_Language(object obj)
        {
            Action = await Application.Current.MainPage.DisplayActionSheet("Select Language", "Cancel", null, "English", "Arabic");
            if (Action == "Cancel")
            {
                return;
            }
            Language = Action;
        }

        private async void onEditNameCommand(object obj)
        {



            string result = await App.Current.MainPage.DisplayPromptAsync("Edit Name", "New Name");
            if (result != null)
            {
                Myprofile[0].PersonName = result;
                await _firebase.UpdatePerson(Myprofile[0]);

            }
        }

        private async void onEditPhoneCommand(object sender)
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("Edit Phone", "New Phone");
            if (result != null)
            {
                Myprofile[0].Phone = result;
                await _firebase.UpdatePerson(Myprofile[0]);
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
                    Myprofile.Remove(profilePageModel);
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