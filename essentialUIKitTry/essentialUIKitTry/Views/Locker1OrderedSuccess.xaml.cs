﻿using essentialUIKitTry.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace essentialUIKitTry.Views
{
    /// <summary>
    /// Page to show the payment success.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Locker1OrderedSuccess : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Locker1OrderedSuccess" /> class.
        /// </summary>
        int lockerId;
        public Locker1OrderedSuccess(int lockerId)
        {
            this.InitializeComponent();
            this.BindingContext = PaymentViewModel.BindingContext;
            this.lockerId = lockerId;
            lbl1.Text = "LOCKER ID: " + lockerId;
            lbl2.Text = "You have successfully ordered locker " +  lockerId + ". Enjoy!";
        }
        void OrderProfileClicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new LockerProfilePage(this.lockerId));
        }
        void Back_To_Menu_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ChooseALocker());
        }
    }
}