using customer_app.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace customer_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchTimePage : ContentPage
    {
        public SearchTimePage()
        {
            InitializeComponent();
            
            int start = 8;
            int end = 9;
            times time = new times();
            var myList = new List<times>();
            for(double i = start; i <= end; i+=0.5)
            {
                if (i - (int)i == 0.5)
                {
                    if (i > 12)
                    {
                        time.Title = ((i-0.5) - 12).ToString() + ":30 PM";
                    }
                    else
                    {
                        time.Title = (i - 0.5).ToString() + ":30 AM";
                    }
                }
                else
                {
                    if (i > 12)
                    {
                        time.Title = (i - 12).ToString() + ":00 PM";
                    }
                    else
                    {
                        time.Title = i.ToString() + ":00 AM";
                    }
                }
                times temp = new times();
                temp.Title = time.Title.ToString();
                myList.Add(temp);
            }            
            MyButtons.Children.Clear(); //just in case so you can call this code several times np..         
            foreach (var item in myList)
            {
                var btn = new Button()
                {
                    Text = item.Title, //Whatever prop you wonna put as title;
                    StyleId = item.Title //use a property from event as id to be passed to handler
                    };
                btn.BackgroundColor = Color.Purple;
                btn.ScaleX = 1;
                btn.ScaleY = 1;
                btn.Clicked += OnDynamicBtnClicked;
                MyButtons.Children.Add(btn);
            }
        }
        private void OnDynamicBtnClicked(object sender, EventArgs e)
        {

            var myBtn = sender as Button;
            // who called me?
            var myId = myBtn.StyleId; //this was set during dynamic creation
            
            //do you stuff upon is
            switch (myId)
            {
                case "1": //or other value you might have set as id
                          //todo
                    break;
                default:
                    //todo
                    break;
            }

        }

    }

    public class times
    {
        public string Title { get; set; }
    }
}