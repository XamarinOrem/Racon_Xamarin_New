using Racon_Xamarin_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Racon_Xamarin_New.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntializerPage : ContentPage
    {
        public IntializerPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.BackgroundImage = "ic_bg.png";
            }
            else
            {
                this.BackgroundImage = "ic_bg.png";
            }

            Device.BeginInvokeOnMainThread(() => {
                    GetMainPage();
                });

           



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
                            App.Current.MainPage = new NavigationPage(new HomePage());
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
                            App.Current.MainPage = new NavigationPage(new CompanyLogin());
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
                    Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

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
    }
}