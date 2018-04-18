using Plugin.Geolocator;
using Racon_Xamarin_New.Controls;
using Racon_Xamarin_New.CustomControls;
using Racon_Xamarin_New.DependencyInterface;
using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Repository;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Racon_Xamarin_New.Views
{
    public partial class ForgetPassword : ContentPage
    {
        string emailID;

        string FbLink, InstaLink;

        string PageName = "LoginPage";
        public ForgetPassword(string fbLink,string instaLink )
        {
            InitializeComponent();

            Title = "Forget Password";

            FbLink = fbLink;
            InstaLink = instaLink;

            this.BackgroundImage = "ic_bg.png";

            try
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    //weatherGridcolumZero.Width = GridLength.Auto;

                    //weatherGridcolumlast.Width = GridLength.Auto;

                    Email.HeightRequest = 40;

                    Email.PlaceholderColor = Color.Black;


                    settingImg.HeightRequest = 50;

                    settingImg.WidthRequest = 50;

                    InstragramTxt.Text = "See Us On Insta";
                    FbText.Text = "Like Us On fb";

                }

                else
                {

                    weatherGridcolumZero.Width = 50;

                    weatherGridcolumlast.Width = 50;

                    InstragramTxt.Text = "See Us On Instagram";

                }

                var width = App.ScreenWidth;

                if (width < 350)
                {
                    FbText.FontSize = 13;
                    InstragramTxt.FontSize = 13;
                }

                this.Opacity = 0.9;
                emailID = Email.Text;


                temperatuerData tempData = new temperatuerData();

                if (App.Database.GetTempData(out tempData))
                {

                    lblTempValue.Text = tempData.temprature;
                  //  lblTempUnit.Text = " " + tempData.tempUnit;

                    imgWeather.Source = tempData.weatherImgDB;


                }

            }
            catch (Exception ex)
            {

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
                    await Navigation.PushAsync(new MyPage(LoginPage.locationlat,
                                                          LoginPage.locationlng, 
                                                          LoginPage.Location,
                                                          LoginPage.locationLabel));

                }
            }
            catch (Exception ex)
            {

            }

        }
        // socialTapped

        async void fb_Tapped(object sender, EventArgs e)
        {
            if (FbLink == null)
            {
                RacoonAlertPopup.textmsg = "Check your internet connection.";

                await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                return;
            }
            else
            {
                var urlStore = Device.OnPlatform(FbLink, FbLink, "");
                Device.OpenUri(new Uri(urlStore));
            }


        }




        async void insta_Tapped(object sender, EventArgs e)
        {
            if (InstaLink == null)
            {
                RacoonAlertPopup.textmsg = "Check your internet connection.";

                await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                return;
            }
            else
            {
                var urlStore = Device.OnPlatform(InstaLink, InstaLink, "");
                Device.OpenUri(new Uri(urlStore));
            }

        }


        async  void sendBtn_Clicked(object sender, EventArgs e)
        {

            string msg = string.Empty;


            emailID = Email.Text;

            


            if (string.IsNullOrEmpty(emailID))
            {
                msg += "Enter your Email!" + Environment.NewLine;
            }

            

            if (!string.IsNullOrEmpty(msg))
            {

                RacoonAlertPopup.textmsg = msg;
                await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                return;
            }

            BindData();
        }


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

                
                string postData = "&email=" + emailID +"";



                var result = await CommonLib.ForgotPassword(CommonLib.ws_MainUrl + "AccountApi/ForgotPassword?"+ postData,"");
                if (result != null && result.Status != 0)
                {
                    LoadPopup.CloseAllPopup();

                    RacoonAlertPopup.textmsg = result.msg;

                    await App.Current.MainPage.Navigation.PushPopupAsync(new RacoonAlertPopup());
                    await Navigation.PushAsync(new LoginPage());

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

        void Settings_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(PageName ));
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
    }
}
