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
    public partial class NewsPage : ContentPage
    {
       

        public static bool isBindAgain = false;
        public static bool IsPull = false;

        public static string checkStatus = string.Empty;
        public NewsPage()
        {
            InitializeComponent();

            Title = "News";

            binddata();


        }

      

        void Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            NewsList.SelectedItem = null;
        }


        protected async override void OnAppearing()
        {

            base.OnAppearing();
            binddata();


        }

        void binddata()
        {
            IsPull = true;
            BindingContext = new NewsPageViewModel();
            IsPull = false;
        }


        private void list_refreshing(object sender, EventArgs e)
        {
            binddata();

            NewsList.EndRefresh();
        }


        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selecteditem = NewsList.SelectedItem as NewsItems;
            NewsList.SelectedItem = null;
            Navigation.PushAsync(new NewsItemdetailView(e.Item as NewsItems));
            //Navigation.PushAsync(new NewsItemdetailView());
        }





    }
}
