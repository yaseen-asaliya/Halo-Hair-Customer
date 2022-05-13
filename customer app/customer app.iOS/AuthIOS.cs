using customer_app.iOS;
using Firebase.Auth;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthIOS))]
namespace customer_app.iOS
{
    public class AuthIOS:IAuth
    {
       
            public bool IsSigIn()
            {
                var user = Auth.DefaultInstance.CurrentUser;
                return user != null;
            }

            public bool IsSigOut()
            {
                try
                {
                    _ = Auth.DefaultInstance.SignOut(out NSError error);
                    return error == null;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public async Task<string> LoginWithEmailAndPassword(string email, string password)
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }

            public async Task<string> SignUpWithEmailAndPassword(string email, string password)
            {
                try
                {
                    var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
                    var changeRequest = user.User.ProfileChangeRequest();

                    return await user.User.GetIdTokenAsync();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        public async Task ResetPassword(string Email)
        {
            await Auth.DefaultInstance.SendPasswordResetAsync(Email);
        }


    }
}