using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace customer_app.ViewModels
{
    public class HomePageViewModels
    {
        public string NameCustomer { get; set; }

        public HomePageViewModels()
        {
            GetName();

        }
        private async Task GetName()
        {
            try
            {
                var name = await SecureStorage.GetAsync("NameUser");
                NameCustomer = name;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
