using essentialUIKitTry.ViewModels;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace essentialUIKitTry.Views
{
    /// <summary>
    /// Page to show the something went wrong
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsideALockerImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsideALockerImage" /> class.
        /// </summary>
        public InsideALockerImage()
        {
            this.InitializeComponent();
            this.BindingContext = InsideALockerImageViewModel.BindingContext;
        }
        void Back_To_Locker_Profile(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}