﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using customer_app.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace customer_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegisterPage());
        }
    }
}