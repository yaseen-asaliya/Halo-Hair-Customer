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
        private string language;
        private bool isVisibleAbout;
        private bool isVisibleSettings;
        public string Action { get; set; }
        public string Location { get; set; }
        private static string _accessToken { get; set; }
        public ICommand BackButton { get; }
        public ICommand LogOut { get; }
        public ICommand ProfileAboutPage { get; }
        public ICommand EditPhoneCommand { get; }
        public ICommand EditNameCommand { get; }
        public ICommand Aboutbutton { get; }
        public ICommand Settingsbutton { get; }
        public ICommand TapLanguageButton { get; }
        
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
            AccessToken();
            _firebase = new FireBaseHaloHair();
            Myprofile = new ObservableCollection<ProfilePageModel>();
            Profile = new ObservableCollection<ProfilePageModel>();
            Profile = _firebase.ProfilePage();
            Profile.CollectionChanged += Serviceschanged;
            LogOut = new Command(PerformLogOut);
            BackButton = new Command(BackPage);
            ProfileAboutPage = new Command(OnProfileAboutPage);
            EditNameCommand = new Command(OnEditNameCommand);
            EditPhoneCommand = new Command(OnEditPhoneCommand);
            LogOut = new Command(PerformLogOut);
            Aboutbutton = new Command(About);
            Settingsbutton = new Command(Setting);
            TapLanguageButton = new Command(TapLanguage);
            Language = "English";
            IsVisibleAbout = true;
            IsVisibleSettings = false;
        }
        private async void AccessToken()
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
        private void Setting(object obj)
        {
            IsVisibleAbout = false;
            IsVisibleSettings = true;
        }
        private void About(object obj)
        {
            IsVisibleSettings = false;
            IsVisibleAbout = true;
        }
        private async void TapLanguage(object obj)
        {
            Action = await Application.Current.MainPage.DisplayActionSheet("Select Language", "Cancel", null, "English", "Arabic");
            if (Action == "Cancel")
            {
                return;
            }
            Language = Action;
        }
        private async void OnEditNameCommand(object obj)
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("Edit Name", "New Name");
            if (result != null)
            {
                Myprofile[0].CustomerName = result;
                await _firebase.UpdatePerson(Myprofile[0]);

            }
        }
        private async void OnEditPhoneCommand(object sender)
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("Edit Phone", "New Phone");
            if (result != null)
            {
                Myprofile[0].Phone = result;
                await _firebase.UpdatePerson(Myprofile[0]);
            }
            }
        private async void OnProfileAboutPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ProfileAboutPage());
        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private void Serviceschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ProfilePageModel profilePageModel = e.NewItems[0] as ProfilePageModel;
                if (profilePageModel.CustomerAccessToken == _accessToken)
                {
                    Myprofile.Remove(profilePageModel);
                    Myprofile.Add(profilePageModel);
                    SecureStorage.SetAsync("NameUser", profilePageModel.CustomerName.ToString());
                }
            }
        }
        private async void PerformLogOut()
        {
            var oauthToken = SecureStorage.Remove("oauth_token");
            var auth = DependencyService.Resolve<IAuth>();
            auth.IsSigOut();
            await Application.Current.MainPage.DisplayAlert("Logout", "you are logout", "ok");
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
    }
}