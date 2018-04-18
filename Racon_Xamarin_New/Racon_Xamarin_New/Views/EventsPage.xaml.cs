using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Racon_Xamarin_New.Views
{
    public partial class EventsPage : ContentPage
    {
        static Random rnd = new Random();
        List<Events> _list = new List<Events>();


        public static bool isBindAgain = false;
        public static bool IsPull = false;

        public static string checkStatus = string.Empty;
        public EventsPage()
        {
            InitializeComponent();

            Title = "Events";




        }

        public static string GetRandamColor()
        {
            //var randomColor = Xamarin.Forms.Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            //string hex = randomColor.R.ToString("X2") + randomColor.G.ToString("X2") + randomColor.B.ToString("X2");
            //return hex;
            Random rnd = new Random();
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return hexOutput;
        }

        void Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            EventsList.SelectedItem = null;
        }


        protected async override void OnAppearing()
        {

            base.OnAppearing();
            binddata();
           

        }


        void binddata()
        {
            IsPull = true;
            BindingContext = new EventsPageViewModel();
            IsPull = false;
        }


        private void list_refreshing(object sender, EventArgs e)
        {
            binddata();

            EventsList.EndRefresh();
        }


        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selecteditem = EventsList.SelectedItem as EventListModel;
            EventsList.SelectedItem = null;
            Navigation.PushAsync(new EventDetailPage(e.Item as EventListModel));

        }




    }
}
