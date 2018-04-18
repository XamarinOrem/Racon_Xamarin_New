using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading;
using FFImageLoading.Forms.Touch;
using Foundation;
using Google.Maps;
using Plugin.FirebasePushNotification;
using Plugin.FirebasePushNotification.Abstractions;
using Refractored.XamForms.PullToRefresh.iOS;
using UIKit;


namespace Racon_Xamarin_New.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {



        const string MapsApiKey = 
            //"AIzaSyAhFzClJyrf4ofgQTKJZEwrfk_kN3kPufk";// 
            "AIzaSyAGjHi9BZ4B-NauZFpx9Q5DYm-7iEOkQ0A";
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            MapServices.ProvideAPIKey(MapsApiKey);

            global::Xamarin.Forms.Forms.Init();

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            Xamarin.FormsMaps.Init();

            App.ScreenWidth = UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = UIScreen.MainScreen.Bounds.Height;

            CachedImageRenderer.Init();

            PullToRefreshLayoutRenderer.Init();

            LoadApplication(new App());

           // FirebasePushNotificationManager.Initialize(options,true);             FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]         {                 new NotificationUserCategory("message",new List<NotificationUserAction> {                     new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground)                 } ),                 new NotificationUserCategory("request",new List<NotificationUserAction> {                     new NotificationUserAction("Accept","Accept"),                     new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)                 } )          } );              return base.FinishedLaunching(app, options);         }

          public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)         {             #if DEBUG                          FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken, FirebaseTokenType.Sandbox);                                                                 #endif             #if RELEASE                         FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken,FirebaseTokenType.Production);             #endif          }              } } 