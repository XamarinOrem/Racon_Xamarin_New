
using Plugin.FirebasePushNotification;
using Racon_Xamarin_New.CustomControls;
using Racon_Xamarin_New.Data;
using Racon_Xamarin_New.DependencyInterface;
using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Racon_Xamarin_New
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public static double ScreenHeight;
        public static double ScreenWidth;

        public static string deviceToken = string.Empty;
        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
             
                    GetMainPage();
                
            }
            else{
                MainPage = new NavigationPage(new IntializerPage());

            }

            Current.Resources = new ResourceDictionary();
            Current.Resources.Add("UlycesColor", Color.FromRgb(121, 248, 81));
            var navigationStyle = new Style(typeof(NavigationPage));
            var barTextColorSetter = new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.Black };
            var barBackgroundColorSetter = new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.White };

            navigationStyle.Setters.Add(barTextColorSetter);
            navigationStyle.Setters.Add(barBackgroundColorSetter);

            Current.Resources.Add(navigationStyle);
        }



        void GetMainPage()
        {



            loggedInUser user = new loggedInUser();

            try
            {



                if (App.Database.GetLoginUser(out user))
                {
                    LoginDetails.userId = user.userId;
                    if (user.UserGroup == "2")
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            MainPage = new NavigationPage(new HomePage());
                           // App.Current.MainPage = new NavigationPage(new HomePage());
                        }
                        else
                        {
                            Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                        }
                    }

                    else
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            MainPage = new NavigationPage(new CompanyLogin());
                            //App.Current.MainPage = new NavigationPage(new CompanyLogin());
                        }
                        else
                        {
                            
                            Application.Current.MainPage.Navigation.PushAsync(new CompanyLogin());
                        }

                    }



                    //Task.Run(async () =>
                    //{
                    //    await Task.Delay(300);
                    //    Device.BeginInvokeOnMainThread(() =>
                    //    {
                    //        Application.Current.MainPage = new NavigationPage(new BottomPageView());
                    //    });
                    //});


                }
                else
                {

                    MainPage = new NavigationPage(new LoginPage());
                 //   Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

                    //Task.Run(async () =>
                    //{
                    //    await Task.Delay(300);
                    //    Device.BeginInvokeOnMainThread(() =>
                    //    {

                    //        Application.Current.MainPage = new NavigationPage(new Views.LogInPage());
                    //    });
                    //});

                }

            }
            catch (Exception ex)
            {


            }
            finally
            {


            }

        }



        private static DBracon database;
        public static DBracon Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new DBracon(DependencyService.Get<IFileHelper>().GetLocalFilePath("DBracon.db3"));
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return database;
            }
        }



        /// <summary>
        /// FCM push notifications initialize code
        /// </summary>
        protected override void OnStart()
        {
            loggedInUser user = new loggedInUser();

            if (App.Database.GetLoginUser(out user))
            {

                if (user.notficationEnabled == "True")
                {
                    CrossFirebasePushNotification.Current.Subscribe("general");
                }
                else if (user.notficationEnabled == null)
                {

                    CrossFirebasePushNotification.Current.UnsubscribeAll();
                }

                else
                {
                    CrossFirebasePushNotification.Current.UnsubscribeAll();

                }
            }
            else
            {
                CrossFirebasePushNotification.Current.UnsubscribeAll();

            }
            // Handle when your app starts
            
          //  CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                if (Device.RuntimePlatform == "Android")
                {
                    App.deviceToken = p.Token;
                }
                System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            };

            var aa = CrossFirebasePushNotification.Current.Token;
            if (Device.RuntimePlatform == "iOS")
            {
                App.deviceToken = aa;
            }
            System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Received");
                    if (p.Data.ContainsKey("body"))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            // mPage.Message = $"{p.Data["body"]}";
                        });

                    }
                }
                catch (Exception ex)
                {

                }

            };

         


        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
