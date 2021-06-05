using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace essentialUIKitTry.Views
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleSignUpPage_Admin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSignUpPage_Admin" /> class.
        /// </summary>
        public SimpleSignUpPage_Admin()
        {
            this.InitializeComponent();
        }
        void Clicked_SimpleLoginPage(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}