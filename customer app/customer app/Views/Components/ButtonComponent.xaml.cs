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
    public partial class ButtonComponent : ContentView
    {
        public ButtonComponent()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty Text_NameButton = BindableProperty.Create(nameof(NameButton), typeof(string), typeof(ButtonComponent), string.Empty);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
         typeof(object),
         typeof(ButtonComponent),
         default);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
          typeof(ICommand),
            typeof(ButtonComponent),
           default(ICommand));
        public string NameButton
        {
            get => (string)GetValue(Text_NameButton);


            set => SetValue(Text_NameButton, value);
        }
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
