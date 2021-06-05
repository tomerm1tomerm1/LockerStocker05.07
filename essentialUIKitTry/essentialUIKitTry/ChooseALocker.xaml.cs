using essentialUIKitTry.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterFunctions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;






namespace essentialUIKitTry
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseALocker : ContentPage
    {

     public List<Locker> myList;
      private  Locker locker1;
        private Locker locker2;
        private Locker locker3; //your list here

        public ChooseALocker()
        {
           //just in case so you can call this code several times np..
            InitializeComponent();
            myList = new List<Locker>();
            SetLockerList();

            /*foreach (var item in myList)
            {
                Button btn = new Button()
                {
                    Text = "Locker " + item.Id, //Whatever prop you wonna put as title;
                     StyleId = "" + item.Id //use a property from event as id to be passed to handler
                };
                if ( item.available)
                {
                    btn.BackgroundColor = Color.Green; 
                } else
                {
                    btn.BackgroundColor = Color.Red; 
                }
                btn.Clicked += Locker1_ClickedAsync;
                MyButtons.Children.Add(btn);
            }*/
        }


        async void SetLockerList()
        {
            MyButtons.Children.Clear();
            
            myList = new List<Locker>();
            locker1 = await AzureApi.GetLocker(1);
            locker2 = await AzureApi.GetLocker(2);
            locker3 = await AzureApi.GetLocker(3);
           //your list here
            myList.Add(locker1);
            myList.Add(locker2);
            myList.Add(locker3);
            foreach (var item in myList)
            {
                Button btn = new Button()
                {
                    Text = "Locker " + item.Id, //Whatever prop you wonna put as title;
                    StyleId = "" + item.Id //use a property from event as id to be passed to handler
                };
                if (item.available)
                {
                    btn.BackgroundColor = Color.Green;
                }
                else
                {
                    btn.BackgroundColor = Color.Red;
                }
                btn.Clicked += Locker_ClickedAsync;
                MyButtons.Children.Add(btn);
            }
        }

        async void Locker_ClickedAsync(object sender, System.EventArgs e)
        {
            int locker_id= int.Parse((sender as Button).StyleId);
            var available = await AzureApi.IsAvailableAsync(locker_id);

            if (available)
            {
                AzureApi.SetOccupy(locker_id, "userKey");
                await Navigation.PushAsync(new Locker1OrderedSuccess(locker_id));
            }
            else
            {
                await Navigation.PushAsync(new Locker2OrderFailed(""+locker_id));  //FIXME: should be failed
            }
            SetLockerList();
        }
        async void Locker2_Clicked(object sender, System.EventArgs e)
        {
            int locker_id= int.Parse((sender as Button).StyleId);
            var available = await AzureApi.IsAvailableAsync(2);

            if (available)
            {
                AzureApi.SetOccupy(2, "userKey");
                await Navigation.PushAsync(new Locker1OrderedSuccess(locker_id));
            }
            else
            {
                await Navigation.PushAsync(new Locker2OrderFailed("2"));  //FIXME: should be failed
            }
        }
        async void Locker3_Clicked(object sender, System.EventArgs e)
        {
            var available = await AzureApi.IsAvailableAsync(3);
            int locker_id= int.Parse((sender as Button).StyleId);

            if (available)
            {
                AzureApi.SetOccupy(3, "userKey");
                await Navigation.PushAsync(new Locker1OrderedSuccess(locker_id));
            }
            else
            {
                await Navigation.PushAsync(new Locker2OrderFailed("3"));  //FIXME: should be failed
            }
        }
    }
}