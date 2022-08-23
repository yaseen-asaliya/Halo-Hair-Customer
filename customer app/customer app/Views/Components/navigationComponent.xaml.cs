using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public static readonly BindableProperty ChangeisVisible = BindableProperty.Create(nameof(isVisible), typeof(string), typeof(navigationComponent), string.Empty);

        public string isVisible
        {
            get => (string)GetValue(ChangeisVisible);


            set => SetValue(ChangeisVisible, value);
        }
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ButtonComponent), default);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonComponent), default(ICommand));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}