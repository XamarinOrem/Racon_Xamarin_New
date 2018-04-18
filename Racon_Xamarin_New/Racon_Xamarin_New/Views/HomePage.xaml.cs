using Plugin.Geolocator;
using Racon_Xamarin_New.Controls;
using Racon_Xamarin_New.CustomControls;
using Racon_Xamarin_New.DependencyInterface;
using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Repository;
using Racon_Xamarin_New.ViewModel;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Racon_Xamarin_New.Views
{
    public partial class HomePage : ContentPage
    {
      //  wsLogIn userData;

        string TotalNoOfCoffee;
        string location, mapAddress;
        double locationlat, locationlng;
        string instaUrl, fbUrl;

        string PageName ="";

      
        public HomePage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                //weatherGridcolumZero.Width = GridLength.Auto;

                //weatherGridcolumlast.Width = GridLength.Auto;

                InstragramTxt.Text = "See Us On Insta";

                FbText.Text = "Like Us On fb";

                settingImg.HeightRequest = 50;

                settingImg.WidthRequest = 50;

            }

            else
            {

                weatherGridcolumZero.Width = 50;

                weatherGridcolumlast.Width = 50;

                InstragramTxt.Text = "See Us On Instagram";

            }

            
            loggedInUser user = new loggedInUser();

            if (App.Database.GetLoginUser(out user))
            {
                LoginDetails.userId = user.userId;

                TotalNoOfCoffee =  user.TotalCoffeeCups;

                loggedUserName.Text = user.UserName;

                registeredOnWhichDate.Text = user.UserCreateDate;


                barCodeImage.Source = user.BarcodeImageUrl;





                passwordText.Text = Convert.ToString(user.WifiInfo);

                



            }

            temperatuerData tempData = new temperatuerData();

            if (App.Database.GetTempData(out tempData))
            {

               // lblTemp.Text = tempData.temprature+""+tempData.tempUnit;

              //  imgWeather.Source = tempData.weatherImgDB;


            }

           NavigationPage.SetHasNavigationBar(this, false);

            this.BackgroundImage = "ic_bg.png";

            var width = App.ScreenWidth;

            if (width < 350)
            {
                FbText.FontSize = 13;
                InstragramTxt.FontSize = 13;
            }

            this.Opacity = 0.9;

            
           
            BindingContext = new HomePageViewModel(this);
           // UpdateControls();

            string url = "http://www.yr.no/place/Norway/M%C3%B8re_og_Romsdal/%C3%85lesund/%C3%85lesund/forecast_hour_by_hour.xml";

            GetXML(url);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateControls();
           // BindingContext = new HomePageViewModel(this);
          
        }
        public void UpdateControls()
        {
            updateDeviceToken();
        }
        public async void updateDeviceToken()
        {
            try
            {

                int deviceid = 0;
                if (string.IsNullOrEmpty(App.deviceToken))
                {
                    App.deviceToken = Plugin.FirebasePushNotification.CrossFirebasePushNotification.Current.Token;
                }
                if (Device.RuntimePlatform == "Android")
                {
                    deviceid = 1;
                }
                else if (Device.RuntimePlatform == "iOS")
                {
                    deviceid = 2;
                }

                if (!CommonLib.checkconnection())
                {
                    RacoonAlertPopup.textmsg = "Check your internet connection.";
                    await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                    return;

                }
              //  await App.Current.MainPage.Navigation.PushPopupAsync(new LoadPopup());


                string postData = "";

                var result = await CommonLib.UpdateDeviceInformation(CommonLib.ws_MainUrl + "AccountApi/UpdateDeviceInformation?" + "userId=" + LoginDetails.userId + "&deviceid=" + deviceid + "&devicetoken=" + App.deviceToken, postData);


                if (result.Status == 1)
                {
                 //   LoadPopup.CloseAllPopup();

                    newsButton.Text = Convert.ToString(result.newsCount);

                    eventButton.Text = Convert.ToString(result.eventCount);
                

                    TotalNoOfCoffee = Convert.ToString(result.user.TotalCoffeeCups);

                    passwordText.Text = result.user.Company.WifiInfo;

                    fbUrl = result.user.Company.FacebookLink;
                    instaUrl = result.user.Company.InstagramLink;


                    location = result.user.Company.Address;

                    mapAddress = result.user.Company.OpeningHours;
                    location = result.user.Company.Address;

                    locationlat = result.user.Company.lat;
                    locationlng = result.user.Company.lng;

                }
                else
                {
                  //  LoadPopup.CloseAllPopup();
                }
                cupVisiblity();
            }
            catch (Exception ex)
            {

               // LoadPopup.CloseAllPopup();
            }
        }


        void cupVisiblity()
        {
            if (TotalNoOfCoffee == "0" || string.IsNullOrEmpty(TotalNoOfCoffee))
            {

                firstCup.Source = "ic_coffee_empty.png";
                secondCup.Source = "ic_coffee_empty.png";
                thirdCup.Source = "ic_coffee_empty.png";
                fourthCup.Source = "ic_coffee_empty.png";
                fivthCup.Source = "ic_coffee_empty.png";
                sixthCup.Source = "ic_coffee_empty.png";
                seventhCup.Source = "ic_coffee_empty.png";
                eightCup.Source = "ic_coffee_empty.png";
                ninethCup.Source = "ic_coffee_empty.png";
                tenthCup.Source = "ic_coffee_empty_free.png";;

            }

            if (TotalNoOfCoffee == "1")
            {

                firstCup.Source = "ic_coffee_full.png";
                secondCup.Source = "ic_coffee_empty.png";
                thirdCup.Source = "ic_coffee_empty.png";
                fourthCup.Source = "ic_coffee_empty.png";
                fivthCup.Source = "ic_coffee_empty.png";
                sixthCup.Source = "ic_coffee_empty.png";
                seventhCup.Source = "ic_coffee_empty.png";
                eightCup.Source = "ic_coffee_empty.png";
                ninethCup.Source = "ic_coffee_empty.png";
                tenthCup.Source = "ic_coffee_empty_free.png";;

            }

            if (TotalNoOfCoffee == "2")
            {

                firstCup.Source = "ic_coffee_full.png";
                secondCup.Source = "ic_coffee_full.png";
                thirdCup.Source = "ic_coffee_empty.png";
                fourthCup.Source = "ic_coffee_empty.png";
                fivthCup.Source = "ic_coffee_empty.png";
                sixthCup.Source = "ic_coffee_empty.png";
                seventhCup.Source = "ic_coffee_empty.png";
                eightCup.Source = "ic_coffee_empty.png";
                ninethCup.Source = "ic_coffee_empty.png";
                tenthCup.Source = "ic_coffee_empty_free.png";;

            }

            if (TotalNoOfCoffee == "3")
            {

                firstCup.Source = "ic_coffee_full.png";
                secondCup.Source = "ic_coffee_full.png";
                thirdCup.Source = "ic_coffee_full.png";
                fourthCup.Source = "ic_coffee_empty.png";
                fivthCup.Source = "ic_coffee_empty.png";
                sixthCup.Source = "ic_coffee_empty.png";
                seventhCup.Source = "ic_coffee_empty.png";
                eightCup.Source = "ic_coffee_empty.png";
                ninethCup.Source = "ic_coffee_empty.png";
                tenthCup.Source = "ic_coffee_empty_free.png";;


            }
            if (TotalNoOfCoffee == "4")
            {

                firstCup.Source = "ic_coffee_full.png";
                secondCup.Source = "ic_coffee_full.png";
                thirdCup.Source = "ic_coffee_full.png";
                fourthCup.Source = "ic_coffee_full.png";
                fivthCup.Source = "ic_coffee_empty.png";
                sixthCup.Source = "ic_coffee_empty.png";
                seventhCup.Source = "ic_coffee_empty.png";
                eightCup.Source = "ic_coffee_empty.png";
                ninethCup.Source = "ic_coffee_empty.png";
                tenthCup.Source = "ic_coffee_empty_free.png";;


            }
            if (TotalNoOfCoffee == "5")
            {

                firstCup.Source = "ic_coffee_full.png";
                secondCup.Source = "ic_coffee_full.png";
                thirdCup.Source = "ic_coffee_full.png";
                fourthCup.Source = "ic_coffee_full.png";
                fivthCup.Source = "ic_coffee_full.png";
                sixthCup.Source = "ic_coffee_empty.png";
                seventhCup.Source = "ic_coffee_empty.png";
                eightCup.Source = "ic_coffee_empty.png";
                ninethCup.Source = "ic_coffee_empty.png";
                tenthCup.Source = "ic_coffee_empty_free.png";;

                if (TotalNoOfCoffee == "6")
                {

                    firstCup.Source = "ic_coffee_full.png";
                    secondCup.Source = "ic_coffee_full.png";
                    thirdCup.Source = "ic_coffee_full.png";
                    fourthCup.Source = "ic_coffee_full.png";
                    fivthCup.Source = "ic_coffee_full.png";
                    sixthCup.Source = "ic_coffee_full.png";
                    seventhCup.Source = "ic_coffee_empty.png";
                    eightCup.Source = "ic_coffee_empty.png";
                    ninethCup.Source = "ic_coffee_empty.png";
                    tenthCup.Source = "ic_coffee_empty_free.png";;

                    if (TotalNoOfCoffee == "7")
                    {

                        firstCup.Source = "ic_coffee_full.png";
                        secondCup.Source = "ic_coffee_full.png";
                        thirdCup.Source = "ic_coffee_full.png";
                        fourthCup.Source = "ic_coffee_full.png";
                        fivthCup.Source = "ic_coffee_full.png";
                        sixthCup.Source = "ic_coffee_full.png";
                        seventhCup.Source = "ic_coffee_full.png";
                        eightCup.Source = "ic_coffee_empty.png";
                        ninethCup.Source = "ic_coffee_empty.png";
                        tenthCup.Source = "ic_coffee_empty_free.png";;

                    }
                    if (TotalNoOfCoffee == "8")
                    {

                        firstCup.Source = "ic_coffee_full.png";
                        secondCup.Source = "ic_coffee_full.png";
                        thirdCup.Source = "ic_coffee_full.png";
                        fourthCup.Source = "ic_coffee_full.png";
                        fivthCup.Source = "ic_coffee_full.png";
                        sixthCup.Source = "ic_coffee_full.png";
                        seventhCup.Source = "ic_coffee_full.png";
                        eightCup.Source = "ic_coffee_full.png";
                        ninethCup.Source = "ic_coffee_empty.png";
                        tenthCup.Source = "ic_coffee_empty_free.png";;

                        if (TotalNoOfCoffee == "9")
                        {

                            firstCup.Source = "ic_coffee_full.png";
                            secondCup.Source = "ic_coffee_full.png";
                            thirdCup.Source = "ic_coffee_full.png";
                            fourthCup.Source = "ic_coffee_full.png";
                            fivthCup.Source = "ic_coffee_full.png";
                            sixthCup.Source = "ic_coffee_full.png";
                            seventhCup.Source = "ic_coffee_full.png";
                            eightCup.Source = "ic_coffee_full.png";
                            ninethCup.Source = "ic_coffee_full.png";
                            tenthCup.Source = "ic_coffee_empty_free.png";;

                            freeCupVisiblityText.IsVisible = true;

                        }

                        if (TotalNoOfCoffee == "10")
                        {


                            firstCup.Source = "ic_coffee_full.png";
                            secondCup.Source = "ic_coffee_full.png";
                            thirdCup.Source = "ic_coffee_full.png";
                            fourthCup.Source = "ic_coffee_full.png";
                            fivthCup.Source = "ic_coffee_full.png";
                            sixthCup.Source = "ic_coffee_full.png";
                            seventhCup.Source = "ic_coffee_full.png";
                            eightCup.Source = "ic_coffee_full.png";
                            ninethCup.Source = "ic_coffee_full.png";
                            tenthCup.Source = "ic_coffee_full.png";



                        }
                    }
                }

            }

        }




        async  void map_Tapped(object sender, EventArgs e)
        {
            try
            {
                RacoonMap.pinLabel = location;

                RacoonMap.pinAddress = mapAddress;

                RacoonMap.pinLatitude = locationlat;
                RacoonMap.pinLongitutde = locationlng;

                if (Device.RuntimePlatform == "iOS")
                {
                    DependencyService.Get<IShowMapView>().map();
                }
                else
                {
                    
                    await Navigation.PushAsync(new MyPage(locationlat,
                                                          locationlng, location, mapAddress));

                }
            }
            catch (Exception ex)
            {

            }
        }
        async void FbText_Clicked(object sender, EventArgs e)
        {
            if (fbUrl == null)
            {
                RacoonAlertPopup.textmsg = "Check your internet connection.";

                await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                return;
            }
            else
            {
                var urlStore = Device.OnPlatform(fbUrl, fbUrl, "");
                Device.OpenUri(new Uri(urlStore));
            }


        }




        async void InstragramTxt_Clicked(object sender, EventArgs e)
        {
            if (instaUrl == null)
            {
                RacoonAlertPopup.textmsg = "Check your internet connection.";

                await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                return;
            }
            else
            {
                var urlStore = Device.OnPlatform(instaUrl, instaUrl, "");
                Device.OpenUri(new Uri(urlStore));
            }

        }




        void Settings_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(PageName));
        }

        void Events_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EventsPage());
        }

        void News_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewsPage());
        }

        void Menu_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainView());
        }


        protected override bool OnBackButtonPressed()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:


                    break;
                case Device.Android:
                    DependencyService.Get<ICloseApplication>().closeApplication();

                    break;

            }



            return base.OnBackButtonPressed();
        }



        //temperature Binding

        string tempValue = string.Empty;
        string tempUnit = string.Empty;
        string symbol = string.Empty;
        string symbolImage = string.Empty;


        public async void GetXML(string url)
        {
            try
            {
                Uri geturi = new Uri(url);
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();
                XDocument doc = XDocument.Parse(response);
                foreach (XElement item in doc.Root.Descendants())
                {
                    if (item.Name.LocalName.ToLower().ToString() == "tabular")
                    {
                        foreach (var innr1 in item.Elements())
                        {
                            if (innr1.Name.LocalName.ToLower().ToString() == "time")
                            {
                                foreach (var innr2 in innr1.Elements())
                                {
                                    if (innr2.Name.LocalName.ToLower().ToString() == "symbol")
                                    {
                                        symbol = innr2.FirstAttribute.Value;
                                    }
                                    if (innr2.Name.LocalName.ToLower().ToString() == "temperature")
                                    {
                                        tempUnit = innr2.FirstAttribute.Value;
                                        tempValue = innr2.LastAttribute.Value;


                                       // lblTemp.Text = tempValue;
                                        //lblTempUnit.Text = tempUnit;

                                        lblTempValue.Text=tempValue;


                                        goto there;
                                    }
                                }
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
            }
            there:
            string url1 = "https://api.met.no/weatherapi/weathericon/1.1/available";

            GetXML1(url1, symbol);
        }

        public async void GetXML1(string url, string symbol)
        {
            try
            {
                Uri geturi = new Uri(url);
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();
                XDocument doc = XDocument.Parse(response);
                bool isMatched = false;
                foreach (XElement item in doc.Root.Descendants())
                {
                    if (item.Name.LocalName.ToLower().ToString() == "query")
                    {
                        foreach (var innr1 in item.Elements())
                        {
                            if (innr1.Name.LocalName.ToLower().ToString() == "parameter")
                            {
                                foreach (var innr2 in innr1.Elements())
                                {
                                    if (innr2.Name.LocalName.ToLower().ToString() == "value")
                                    {
                                        if (innr2.Value == symbol)
                                        {
                                            isMatched = true;
                                        }
                                    }
                                }
                            }
                            if (isMatched)
                            {
                                if (innr1.Name.LocalName.ToLower().ToString() == "uri")
                                {
                                    symbolImage = innr1.Value;





                                    goto there;
                                }
                            }

                        }

                    }
                }
                there:
                {
                    imgWeather.Source = symbolImage;
                }
            }
            catch (Exception ex)
            {
            }

        }

    }
}
