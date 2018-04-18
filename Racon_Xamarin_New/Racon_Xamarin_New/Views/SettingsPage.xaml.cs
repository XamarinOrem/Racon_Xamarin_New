using Plugin.FirebasePushNotification;
using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Repository;
using Racon_Xamarin_New.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Racon_Xamarin_New
{
    public partial class SettingsPage : ContentPage
    {
        string instaUrl, fbUrl;

        public static string instaURL = "";

        public static string fbURL = "";
        string PageName;

        public static int notificationSetting;
        public SettingsPage(string pageName)
        {
            InitializeComponent();

            PageName = pageName;

            Title = "Settings";



            //Notification toggle setting 

            notificationSwich.IsToggled = false;
            CrossFirebasePushNotification.Current.UnsubscribeAll();


            FirstTimeBindData();






            if (PageName == "LoginPage" || PageName == "SignUpPage" || PageName == "LoginPage")
            {

                LogoutLabel.IsVisible = false;

                notificationLabel.IsVisible = false;

                notificationSwich.IsVisible = false;

            }
            else
            {
                loggedInUser userData = new loggedInUser();

                var settingData = App.Database.GetLoginUser(out userData);


                if (userData.notficationEnabled == "false")
                {
                    notificationSwich.IsToggled = false;
                    CrossFirebasePushNotification.Current.UnsubscribeAll();

                }
                else if (userData.notficationEnabled == null)
                {
                    notificationSwich.IsToggled = false;
                    CrossFirebasePushNotification.Current.UnsubscribeAll();
                }
                else
                {

                    notificationSwich.IsToggled = true;
                    CrossFirebasePushNotification.Current.Subscribe("general");

                }





                LogoutLabel.IsVisible = true;

                notificationLabel.IsVisible = true;

                notificationSwich.IsVisible = true;



            }




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

                    fbUrl = result.company.FacebookLink;
                    instaUrl = result.company.InstagramLink;

                    emailTxt.Text = "Email: " + result.company.Email;
                    adrressTxt.Text = "Address: " + result.company.Address;

                    phoneTxt.Text = "Tlf: " + result.company.Tlf;


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

        private async void notficationClicked(object sender, EventArgs e)
        {
            loggedInUser userData = new loggedInUser();
            var settingData = App.Database.GetLoginUser(out userData);

            if (notificationSwich.IsToggled == true)
            {
                notificationSwich.IsToggled = true;
                userData.notficationEnabled = "true";
                CrossFirebasePushNotification.Current.Subscribe("general");

            }
            else

            {
                // notificationSwich.IsToggled = false;
                notificationSwich.IsToggled = false;
                userData.notficationEnabled = "false";
                CrossFirebasePushNotification.Current.UnsubscribeAll();

            }










            App.Database.SaveLoggedUser(userData);


        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        // socialTapped


        void fb_Tapped(object sender, EventArgs e)
        {

            var urlStore = Device.OnPlatform(fbUrl, fbUrl, "");
            Device.OpenUri(new Uri(urlStore));
        }




        void insta_Tapped(object sender, EventArgs e)
        {

            var urlStore = Device.OnPlatform(instaUrl, instaUrl, "");

            Device.OpenUri(new Uri(urlStore));
        }


        private async void LogOut_Clicked(object sender, EventArgs e)
        {


            var ans = await DisplayAlert("", "Are you sure you want to logout?", "Yes", "No");
            if (ans == true)
            {
                Models.LoginDetails.userId = "";
                App.Database.ClearLoginDetails();
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
        }



    }
}
