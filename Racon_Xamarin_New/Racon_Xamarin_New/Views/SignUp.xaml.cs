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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Racon_Xamarin_New.Views
{
   
    public partial class SignUp : ContentPage
    {
        string firstName, lastName, userName, emailID, password;

        string FbLink, InstaLink;

        string PageName = "SignUpPage";
        public SignUp(string fbLink, string instaLink)
        {
            InitializeComponent();



            Title = "Sign Up";

            this.BackgroundImage = "ic_bg.png";


            if (Device.RuntimePlatform == Device.iOS)
            {
                //weatherGridcolumZero.Width = GridLength.Auto;

                //weatherGridcolumlast.Width = GridLength.Auto;

                FirstName.PlaceholderColor = Color.Black;

                LastName.PlaceholderColor = Color.Black;

                Email.PlaceholderColor = Color.Black;

                Password.PlaceholderColor = Color.Black;

                UserName.PlaceholderColor = Color.Black;

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
            
            FirstName.Text = string.Empty;
            
            LastName.Text = string.Empty;
            
            UserName.Text = string.Empty;

            Email.Text = string.Empty;

            Password.Text = string.Empty;

            FbLink = fbLink;
            InstaLink = instaLink;


            temperatuerData tempData = new temperatuerData();

            if (App.Database.GetTempData(out tempData))
            {

                lblTempValue.Text = tempData.temprature;
                //lblTempUnit.Text = " " + tempData.tempUnit;

                imgWeather.Source = tempData.weatherImgDB;


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


               

               firstName = FirstName.Text;

                lastName = LastName.Text;

                userName = UserName.Text;
                emailID = Email.Text;

                password = Password.Text;

                string postData = "firstname=" + firstName + "&lastname=" + lastName + "&username=" + userName + "&email=" + emailID + "&password=" + password + "&CID=1" + "";



                var result = await CommonLib.RegisterUser(CommonLib.ws_MainUrl + "AccountApi/Registration?" + postData);
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


        async void  SignupBtn_Clicked(object sender, EventArgs e)
        {
            //Validation Part
            string msg = string.Empty;

             firstName =FirstName.Text ;

            lastName= LastName.Text ;

            userName =  UserName.Text;

            emailID = Email.Text ;

            password = Password.Text ;







            if (string.IsNullOrEmpty(firstName))
            {
                msg += "Enter your First name!" + Environment.NewLine;
            }

         

            if (string.IsNullOrEmpty(lastName))
            {
                msg += "Enter your Last name!" + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(userName))
            {
                msg += "Enter your User name!" + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(emailID))
            {
                msg += "Enter your Email!" + Environment.NewLine;
            }

            else
            {
                if (!ValidateEmail(emailID.Trim()))
                {
                    msg += emailID + " is Invalid Email Address!" + Environment.NewLine;
                }
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

        //Validate Email

        private bool ValidateEmail(string email)
        {
            string msg = string.Empty;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
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
    }
}
