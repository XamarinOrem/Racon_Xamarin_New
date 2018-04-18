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
    public partial class CompanyLogin : ContentPage
    {
        string instaUrl, fbUrl;

        string PageName = "";

        string location, mapAddress;
        double lat, lng;

        public static string EventButtonText = string.Empty;
        public static string NewsButtonText = string.Empty;
        public CompanyLogin()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
               // weatherGridcolumZero.Width = GridLength.Auto;

                //weatherGridcolumlast.Width = GridLength.Auto;

                InstragramTxt.Text="See Us On Insta";
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

            NavigationPage.SetHasNavigationBar(this, false);

            this.BackgroundImage = "ic_bg.png";

            var width = App.ScreenWidth;

            if (width < 350)
            {
                FbText.FontSize = 13;
                InstragramTxt.FontSize = 13;
            }

            this.Opacity = 0.9;


         


            BindingContext = new CompanyViewModel(this);
          //  UpdateControls();

            string url = "http://www.yr.no/place/Norway/M%C3%B8re_og_Romsdal/%C3%85lesund/%C3%85lesund/forecast_hour_by_hour.xml";

            GetXML(url);
        }



        public void UpdateControls()
        {
            updateDeviceToken();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateControls();

           
        }

        /// <summary>
        /// update device token everytime on app start
        /// </summary>
        public async void updateDeviceToken()
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

            string postData = "";

            var result = await CommonLib.UpdateDeviceInformation(CommonLib.ws_MainUrl + "AccountApi/UpdateDeviceInformation?" + "userId=" + LoginDetails.userId + "&deviceid=" + deviceid + "&devicetoken=" + App.deviceToken, postData);

            try
            {

                if (result.Status == 1)
                {

                    newsButton.Text = Convert.ToString(result.newsCount);

                    eventButton.Text = Convert.ToString(result.eventCount);


                    fbUrl = result.user.Company.FacebookLink;
                    instaUrl = result.user.Company.InstagramLink;


                    location = result.user.Company.Address;

                    mapAddress = result.user.Company.OpeningHours;
                    lat = result.user.Company.lat;
                    lng = result.user.Company.lng;

                }

                else
                {

                }

            }
            catch (Exception ex)
            {


            }



        }


        async  void map_Tapped(object sender, EventArgs e)
        {
            try
            {
                RacoonMap.pinLabel = location;

                RacoonMap.pinAddress = mapAddress;

                RacoonMap.pinLatitude = lat;
                RacoonMap.pinLongitutde = lng;

                if (Device.RuntimePlatform == "iOS")
                {
                    DependencyService.Get<IShowMapView>().map();
                }
                else
                {
                    
                    await Navigation.PushAsync(new MyPage(lat,
                                                          lng, 
                                                          location,
                                                          mapAddress));
                }
            }
            catch (Exception ex)
            {

            }
        }

        async void fb_Tapped(object sender, EventArgs e)
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




        async void insta_Tapped(object sender, EventArgs e)
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



        //Scan Clicked Portion
        void Scan_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new ScanPage());
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



        void Settings_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(PageName));
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

                                        lblTempValue.Text = tempValue;




                                       



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
