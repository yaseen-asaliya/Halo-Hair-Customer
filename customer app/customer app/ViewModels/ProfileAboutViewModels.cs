using customer_app.Models;
using customer_app.Services;
using customer_app.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace customer_app.ViewModels
{
    public class ProfileAboutViewModels : BaseViewModel
    {
        FireBaseHaloHair firebase;


        private ObservableCollection<ProfilePageModel> profile;

        public ObservableCollection<ProfilePageModel> Profile
        {
            get { return profile; }
            set
            {
                profile = value;
                OnPropertyChanged();

            }
        }



        private ObservableCollection<ProfilePageModel> myprofile;
        public ObservableCollection<ProfilePageModel> Myprofile
        {
            get
            {
                return myprofile;
            }
            set
            {
                myprofile = value;
                OnPropertyChanged();
            }
        }



        private static string accessToken { get; set; }

        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ProfileAboutViewModels()
        {
            AccessToken();
            firebase = new FireBaseHaloHair();
            Myprofile = new ObservableCollection<ProfilePageModel>();
            Profile = new ObservableCollection<ProfilePageModel>();
            Profile = firebase.ProfilePage();
            Profile.CollectionChanged += serviceschanged;
            LogOut = new Command(PerformLogOut);


        }
        public string location { get; set; }
        private void serviceschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ProfilePageModel profilePageModel = e.NewItems[0] as ProfilePageModel;
                Console.WriteLine(e.NewItems[0]);
                Console.WriteLine(e.NewItems[0].GetType());
                if (profilePageModel.AccessToken_User == accessToken)
                {

                    Myprofile.Add(profilePageModel);
                    SecureStorage.SetAsync("NameUser", profilePageModel.PersonName.ToString());


                }
            }

        }



        public ICommand LogOut { get; }


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
