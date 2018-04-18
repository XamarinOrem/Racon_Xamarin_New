using System;
using Racon_Xamarin_New.Droid;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Net.Mobile;
using FFImageLoading.Forms.Droid;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using Android.Content;
using Xamarin;
using Refractored.XamForms.PullToRefresh.Droid;

namespace Racon_Xamarin_New.Droid
{
    [Activity(Label = "Racoon", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (width - 0.5f) / density;
            App.ScreenHeight = (height - 0.5f) / density;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            FormsMaps.Init(this, bundle);
            CachedImageRenderer.Init(true);
            PullToRefreshLayoutRenderer.Init();

            LoadApplication(new App());
            FirebasePushNotificationManager.ProcessIntent(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(intent);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

