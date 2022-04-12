using customer_app.Models;
using customer_app.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace customer_app.ViewModels
{
    public class ProfileAboutViewModels
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public string NameSalon { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string location { get; set; }


        FireBaseHaloHair firebase;
        public ObservableCollection<Authentication> Profile { get; set; }

        public ProfileAboutViewModels()
        {
            firebase = new FireBaseHaloHair();
            Profile = new ObservableCollection<Authentication>();
            Profile = firebase.GetAuthentications();
        }
    }
}
