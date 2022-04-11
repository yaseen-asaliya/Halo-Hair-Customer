using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace customer_app.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class navigationComponent : ContentView
    {
        public navigationComponent()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty ChangeTitle = BindableProperty.Create(nameof(Titles), typeof(string), typeof(navigationComponent), string.Empty);

        public string Titles
        {
            get => (string)GetValue(ChangeTitle);
            set => SetValue(ChangeTitle, value);
        }
    }
}