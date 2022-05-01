using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace customer_app.ViewModels
{
    public class HomePageViewModels
    {
        public string NameCustomer { get; set; }
        public HomePageViewModels()
        {
            getName();
        }
        private async Task getName()
        {
            try
            {
                NameCustomer = await SecureStorage.GetAsync("NameUser");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
