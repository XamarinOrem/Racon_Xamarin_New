using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Repository;
using Racon_Xamarin_New.ViewModel;
using Racon_Xamarin_New.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Racon_Xamarin_New.CustomControls
{
    public partial class MainView : ContentPage
    {
        StackLayout verticalLayout;
        List<Products> _list = new List<Products>();
        StackLayout upperLayout;
        int categoriesID;

        static int catId;


        public static bool isBindAgain = false;
        public static bool IsPull = false;

        public static string checkStatus = string.Empty;
        ScrollView _scroll = new ScrollView();

        public MainView()
        {
            InitializeComponent();



            Title = "Menu";



            _scroll.Orientation = ScrollOrientation.Horizontal;
            //_scroll.IsClippedToBounds = true;

            upperLayout = new StackLayout();
            upperLayout.Orientation = StackOrientation.Horizontal;
            upperLayout.Spacing = 15;
            upperLayout.Padding = new Thickness(15, 0, 15, 0);
            upperLayout.BackgroundColor = Color.Transparent;
            //upperLayout.IsClippedToBounds = true;
            upperLayout.HeightRequest = 60;
            //upperLayout.Margin = new Thickness(0, 20, 0, 0);


            //mainLayout.IsClippedToBounds = true;
            BindUpperData();


          
            //mainLayout.Children.Add(ProductsList);

        }

        public async void BindUpperData()
        {
            try
                {
                    if (!CommonLib.checkconnection())

                    {

                        RacoonAlertPopup.textmsg = "Check your internet connection.";

                        await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                        return;

                    }


                  //  await App.Current.MainPage.Navigation.PushPopupAsync(new LoadPopup());





                    var result = await CommonLib.CategoryList(CommonLib.ws_MainUrl + "AccountApi/GetCategories?id=1" + "");
                    if (result != null)
                    {
                     //   LoadPopup.CloseAllPopup();
                        

                        for (int i = 0; i < result.categories.Count; i++)
                        {
                            Label _catLabel = new Label();
                            categoriesID = result.categories[0].PCID;
                            _catLabel.Text = result.categories[i].PCName;
                            _catLabel.HorizontalOptions = LayoutOptions.Center;
                            _catLabel.FontFamily = "Kokila Bold";
                            _catLabel.StyleId = "Kokila Bold";
                            _catLabel.FontSize = 25;
                            _catLabel.FontAttributes = FontAttributes.Bold;
                            _catLabel.TextColor = Color.Black;
                             _catLabel.VerticalOptions = LayoutOptions.Center;

                            BoxView _box = new BoxView();
                            _box.HorizontalOptions = LayoutOptions.FillAndExpand;
                            _box.BackgroundColor = Color.Black;
                            _box.HeightRequest = 2;

                            verticalLayout = new StackLayout();
                            verticalLayout.Orientation = StackOrientation.Vertical;
                            if (Device.RuntimePlatform == Device.iOS)
                            {
                            verticalLayout.HorizontalOptions = LayoutOptions.CenterAndExpand;
                            verticalLayout.Spacing = 8;

                            }
                       
                            verticalLayout.Margin = new Thickness(0, 5, 0, 0);
                            verticalLayout.ClassId = result.categories[i].PCID.ToString();
                            verticalLayout.Children.Add(_catLabel);
                            verticalLayout.Children.Add(_box);

                            TapGestureRecognizer _tap = new TapGestureRecognizer();
                            verticalLayout.GestureRecognizers.Add(_tap);
                            _tap.Tapped += _tap_Tapped;
                            verticalLayout.BindingContext = i;
                            upperLayout.Children.Add(verticalLayout);
                            
                            //upperLayout.Children.Insert(0, firstLayout);

                        }


                        for (int i = 0; i < upperLayout.Children.Count(); i++)
                        {
                            if (i != 0)
                            {
                                var child = upperLayout.Children[i] as StackLayout;
                                child.Children[1].IsVisible = false;
                                var lbl = child.Children[0] as Label;
                                lbl.TextColor = Color.FromHex("#727376");



                            }
                        }

                    }

                    else
                    {
                       // LoadPopup.CloseAllPopup();
                       // RacoonAlertPopup.textmsg = result.msg;
                     //   await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                    }
                _scroll.Content = upperLayout;

                MainLayout.Children.Insert(0,_scroll);
                binddata();

                }
                catch (Exception ex)
                {
                   // LoadPopup.CloseAllPopup();
                    await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

                }
                finally
                {

                }

          
        }

        private void _tap_Tapped(object sender, EventArgs e)
        {
            var child = sender as StackLayout;
            var catId = Convert.ToInt32(child.ClassId);
            child.Children[1].IsVisible = true;
            var lbl = child.Children[0] as Label;
            lbl.TextColor = Color.Black;
            int dddd1 = (int)child.BindingContext;
            for (int i = 0; i < upperLayout.Children.Count(); i++)
            {
                if (i != dddd1)
                {
                    var child1 = upperLayout.Children[i] as StackLayout;
                    child1.Children[1].IsVisible = false;
                    var lbl1 = child1.Children[0] as Label;
                    lbl1.TextColor = Color.FromHex("#727376");
                }
            }

            BindingContext =new MainViewModel(catId);


        }

        void Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            ProductsList.SelectedItem = null;
        }


        protected async override void OnAppearing()
        {

            base.OnAppearing();

           binddata();


        }



        void binddata()
        {
            IsPull = true;
            BindingContext = BindingContext = new MainViewModel(categoriesID);
            IsPull = false;
        }


        private void list_refreshing(object sender, EventArgs e)
        {
            binddata();

            ProductsList.EndRefresh();
        }


        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selecteditem = ProductsList.SelectedItem as NewsItems;
            ProductsList.SelectedItem = null;

        }



    }


}
