using Android.App;
using Android.Support.V7.App;

namespace Racon_Xamarin_New.Droid
{
    [Activity(Label = "Racoon", Icon = "@drawable/logo", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}