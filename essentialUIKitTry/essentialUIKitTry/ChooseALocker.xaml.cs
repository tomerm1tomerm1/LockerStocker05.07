using essentialUIKitTry.Views;
using essentialUIKitTry;
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
        public List<Locker>[] lockerRows = {new List<Locker>(), new List<Locker>(), new List<Locker>(), new List<Locker>() };

        public ChooseALocker()
        {
           //just in case so you can call this code several times np..
            InitializeComponent();
            myList = new List<Locker>();
            SetLockerList();
        }


        Button getBtnForLocker(Locker locker)
        {
            int btnTimingFontSize=10;
            int btn_width = 60;
            int btn_height = 80;
            Button tmp_btn = new Button()
            {
                Text = "L" + locker.Id, 
                StyleId = "" + locker.Id,
                WidthRequest = btn_width,
                HeightRequest = btn_height
            };
            if ((!locker.available) && (locker.user_key == App.m_myUserKey))
            {
                tmp_btn.BackgroundColor = Color.LightSteelBlue;
                tmp_btn.Text = AzureApi.GetRemainingTime(locker);
                tmp_btn.FontSize = btnTimingFontSize;
            }
            else if (locker.available)
            {
                tmp_btn.BackgroundColor = Color.Green;
            }
            else
            {
                tmp_btn.BackgroundColor = Color.Red;
            }
            return tmp_btn;
        }

        async void SetLockerList()
        {
            int numOfRows = 4;
            int lockersInRow = 5;
            ButtonsRow1.Children.Clear();
            ButtonsRow2.Children.Clear();
            ButtonsRow3.Children.Clear();
            ButtonsRow4.Children.Clear();
            
            for(int rowIdx = 0; rowIdx < numOfRows; rowIdx++)
            {
                for (int lockerInRowIdx = 0; lockerInRowIdx < lockersInRow; lockerInRowIdx++)
                {
                    Locker tmpLocker = await AzureApi.GetLocker(rowIdx * lockersInRow + lockerInRowIdx + 1);
                    lockerRows[rowIdx].Add(tmpLocker);
                }
            }
            for(int idxInRow = 0; idxInRow < lockersInRow; idxInRow++)
            {
                Button btn1 = getBtnForLocker(lockerRows[0][idxInRow]);
                Button btn2 = getBtnForLocker(lockerRows[1][idxInRow]);
                Button btn3 = getBtnForLocker(lockerRows[2][idxInRow]);
                Button btn4 = getBtnForLocker(lockerRows[3][idxInRow]);

                btn1.Clicked += Locker_ClickedAsync;
                btn2.Clicked += Locker_ClickedAsync;
                btn3.Clicked += Locker_ClickedAsync;
                btn4.Clicked += Locker_ClickedAsync;
                ButtonsRow1.Children.Add(btn1);
                ButtonsRow2.Children.Add(btn2);
                ButtonsRow3.Children.Add(btn3);
                ButtonsRow4.Children.Add(btn4);
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