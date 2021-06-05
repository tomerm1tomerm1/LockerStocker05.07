using essentialUIKitTry.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace essentialUIKitTry
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sign_Up_Main_Page : ContentPage
    {
        public Sign_Up_Main_Page()
        {
            InitializeComponent();
        }
        void Clicked_SignUp_Admin(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SimpleSignUpPage_Admin());
        }
        void Clicked_SignUp_Customer(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SimpleSignUpPage());
        }
    }
}