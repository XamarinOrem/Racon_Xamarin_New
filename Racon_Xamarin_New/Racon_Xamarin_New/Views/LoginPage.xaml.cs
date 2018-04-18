using Plugin.Geolocator;
using Racon_Xamarin_New.Controls;
using Racon_Xamarin_New.CustomControls;
using Racon_Xamarin_New.DependencyInterface;
using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Repository;
using Racon_Xamarin_New.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Racon_Xamarin_New
{
    public partial class LoginPage : ContentPage
    {
        string emailID, password;

        string fbLink, instaLink;

        string PageName = "LoginPage";

        public static string Location { get; set; }

        public static string locationLabel { get; set; }

        public static double locationlat { get; set; }

        public static double locationlng { get; set; }

        public LoginPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);



            if (Device.RuntimePlatform == Device.iOS)
            {
                scroolLogIn.IsEnabled = true;

                loginLayout.Margin = new Thickness(0,10,0,0);

                threetabbedLayout.Margin = new Thickness(0, 10, 0, 0);

                threetabbedLayout.VerticalOptions = LayoutOptions.FillAndExpand;

                Email.HeightRequest = 40;

                Password.HeightRequest = 40;

                settingImg.HeightRequest = 50;

                settingImg.WidthRequest = 50;


                //imgWeather.HeightRequest = 8;

                //imgWeather.WidthRequest = 8;


                //weatherGridcolumZero.Width = GridLength.Auto;

                //weatherGridcolumlast.Width = GridLength.Auto;
                InstragramTxt.Text = "See Us On Insta";
                FbText.Text = "Like Us On fb";

            }
            else{
                //weatherGridcolumZero.Width = 50;

               // weatherGridcolumlast.Width = 50;

                InstragramTxt.Text = "See Us On Instagram";

            }

            var width = App.ScreenWidth;

            if (width < 350)
            {
                FbText.FontSize = 13;
                InstragramTxt.FontSize = 13;
            }

            this.Opacity = 0.9;

            this.BackgroundImage = "ic_bg.png";

            emailID = Email.Text;

            password = Password.Text;


            string url = "http://www.yr.no/place/Norway/M%C3%B8re_og_Romsdal/%C3%85lesund/%C3%85lesund/forecast_hour_by_hour.xml";

            GetXML(url);

          //  FirstTimeBindData();
        }
        //first time login page bind


        protected override void OnAppearing()
        {
            base.OnAppearing();
            FirstTimeBindData();
        }



        async void FirstTimeBindData()
        {
            try
            {
                if (!CommonLib.checkconnection())
                {

                    RacoonAlertPopup.textmsg = "Check your internet connection.";

                    await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                    return;

                }


                await App.Current.MainPage.Navigation.PushPopupAsync(new LoadPopup());





                var result = await CommonLib.CompanyListWithoutLogin(CommonLib.ws_MainUrl + "AccountApi/GetCompanyDetail?Id=1" + "");
                if (result != null)
                {
                    LoadPopup.CloseAllPopup();
                   
                    fbLink = result.company.FacebookLink;
                    instaLink = result.company.InstagramLink;

                    Location = result.company.Address;

                    locationLabel = result.company.OpeningHours;
                    locationlat = result.company.lat;
                    locationlng = result.company.lng;
                }
                else
                {
                    LoadPopup.CloseAllPopup();
                    RacoonAlertPopup.textmsg = "Servor Error,Please after sometimes!";
                    await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                }

            }
            catch (Exception ex)
            {
                LoadPopup.CloseAllPopup();
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

            }
            finally
            {

            }

        }


        //login binding
        async void Login_Clicked(object sender, EventArgs e)
        {
            string msg = string.Empty;


            emailID = Email.Text;

            password = Password.Text;


            if (string.IsNullOrEmpty(emailID))
            {
                msg += "Enter your Email!" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(password))
            {
                msg += "Enter your Password!" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(msg))
            {

                RacoonAlertPopup.textmsg = msg;
                await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                return;
            }

            BindData();

        }
        string tempValue = string.Empty;
        string tempUnit = string.Empty;
        string symbol = string.Empty;
        string symbolImage = string.Empty;
        async void BindData()
        {
            try
            {
                if (!CommonLib.checkconnection())
                {

                    RacoonAlertPopup.textmsg = "Check your internet connection.";

                    await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                    return;

                }


                await App.Current.MainPage.Navigation.PushPopupAsync(new LoadPopup());





                emailID = Email.Text;

                password = Password.Text;

                string postData = "&email=" + emailID + "&password=" + password + "";



                var result = await CommonLib.LoginUser(CommonLib.ws_MainUrl + "AccountApi/Login?" + postData);
                if (result != null && result.Status != 0)
                {
                    if (result.User.MainUserGrp == 2)
                    {
                        LoadPopup.CloseAllPopup();

                        loggedInUser objUser = new loggedInUser();
                        objUser.UserGroup = Convert.ToString(result.User.MainUserGrp);

                        objUser.userId = Convert.ToString(result.User.UserID);

                        objUser.UserName = result.User.UserName;

                        objUser.WifiInfo = result.User.Company.WifiInfo;

                        objUser.TotalCoffeeCups = Convert.ToString(result.User.TotalCoffeeCups);

                        objUser.UserCreateDate = result.User.UserCreateDate.ToString("MM/dd/yyyy");

                        objUser.BarcodeImageUrl = result.User.BarcodeImageUrl;

                        objUser.InstagramLink = result.User.Company.InstagramLink;

                        objUser.FacebookLink = result.User.Company.FacebookLink;

                        objUser.Address = result.User.Company.Address;
                        objUser.OpeningHours = result.User.Company.OpeningHours;

                        objUser.lat = result.User.Company.lat;
                        objUser.lng = result.User.Company.lng;

                        App.Database.SaveLoggedUser(objUser);

                        LoginDetails.userId = Convert.ToString(result.User.UserID);

                        //RacoonAlertPopup.textmsg = result.msg;

                        //await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                        await Application.Current.MainPage.Navigation.PushAsync(new HomePage());

                    }
                    else
                    {
                        LoadPopup.CloseAllPopup();

                        loggedInUser objUser = new loggedInUser();
                        objUser.UserGroup = Convert.ToString(result.User.MainUserGrp);
                        objUser.userId = Convert.ToString(result.User.UserID);
                        objUser.InstagramLink = result.User.Company.InstagramLink;

                        objUser.FacebookLink = result.User.Company.FacebookLink;

                        App.Database.SaveLoggedUser(objUser);

                        LoginDetails.userId = Convert.ToString(result.User.UserID);




                        //RacoonAlertPopup.textmsg = result.msg;

                        //await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());

                        await Application.Current.MainPage.Navigation.PushAsync(new CompanyLogin());

                    }



                }

                else
                {
                    LoadPopup.CloseAllPopup();
                    RacoonAlertPopup.textmsg = result.msg;
                    await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                }

            }
            catch (Exception ex)
            {
                LoadPopup.CloseAllPopup();
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");

            }
            finally
            {

            }

        }


        // socialTapped


        async void fb_Tapped(object sender, EventArgs e)
        {
            if (fbLink == null)
            {
                RacoonAlertPopup.textmsg = "Check your internet connection.";

                await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                return;
            }
            else
            {
                var urlStore = Device.OnPlatform(fbLink, fbLink, "");
                Device.OpenUri(new Uri(urlStore));
            }


        }




        async void insta_Tapped(object sender, EventArgs e)
        {
            if (instaLink == null)
            {
                RacoonAlertPopup.textmsg = "Check your internet connection.";

                await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                return;
            }
            else
            {
                var urlStore = Device.OnPlatform(instaLink, instaLink, "");
                Device.OpenUri(new Uri(urlStore));
            }

        }




        async void map_Tapped(object sender, EventArgs e)
        {
            try
            {
                RacoonMap.pinLabel = LoginPage.Location;

                RacoonMap.pinAddress = LoginPage.locationLabel;

                RacoonMap.pinLatitude = LoginPage.locationlat;
                RacoonMap.pinLongitutde = LoginPage.locationlng;

                if (Device.RuntimePlatform == "iOS")
                {
                    DependencyService.Get<IShowMapView>().map();
                }
                else
                {
                    
                    RacoonMap.pinLatitude = locationlat;

                    RacoonMap.pinLongitutde = locationlng;

                    RacoonMap.pinLabel = LoginPage.Location;

                    RacoonMap.pinAddress = LoginPage.locationLabel;

                    await Navigation.PushAsync(new MyPage(locationlat,
                                                          locationlng, LoginPage.locationLabel, LoginPage.locationLabel));

                }
            }
            catch (Exception ex )
            {

            }

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

        void Forgot_Password_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgetPassword(fbLink, instaLink));
        }

        void SignUp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUp(fbLink, instaLink));
        }


        temperatuerData tempData = new temperatuerData();

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

                                        lblTempValue.Text = tempValue;
                                        lblTemp.Text = " " + tempUnit;


                                        tempData.temprature = tempValue;

                                        tempData.tempUnit = tempUnit;





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

                                   

                                    tempData.weatherImgDB = innr1.Value;

                                    App.Database.SaveTempData(tempData);

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
