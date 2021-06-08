using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using CounterFunctions;
using System;
using essentialUIKitTry;

namespace essentialUIKitTry.Views
{
    /// <summary>
    /// Page to show chat profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LockerProfilePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LockerProfilePage" /> class.
        /// </summary>

        Locker locker;
        public LockerProfilePage(int lockerId)
        {
            this.InitializeComponent();
            SetLocker(lockerId);
        }
        async void SetLocker(int lockerId)
        {
            string status = "not set";
            this.locker = await AzureApi.GetLocker(lockerId);
            LockerIdLbl.Text = "Locker Id: " + lockerId;


            string timeRemainingStr = AzureApi.GetRemainingTime(locker); 
            TimeRemainingLbl.Text = timeRemainingStr;


            if (this.locker.locked) status = "Locked";
            else status = "Unlocked";
            StatusLbl.Text = "Status: " + status;
        }
        void Navigate_To_Photo(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new InsideALockerImage());
        }
    }
}