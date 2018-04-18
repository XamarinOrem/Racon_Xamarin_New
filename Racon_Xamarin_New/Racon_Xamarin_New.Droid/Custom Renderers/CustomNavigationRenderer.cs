using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Racon_Xamarin_New.Droid.Custom_Renderers;
using Android.Graphics;
using Racon_Xamarin_New.CustomControls;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]
namespace Racon_Xamarin_New.Droid.Custom_Renderers
{
    public class CustomNavigationRenderer : NavigationPageRenderer
    {
        private Android.Support.V7.Widget.Toolbar toolbar;

        public override void OnViewAdded(Android.Views.View child)
        {
            try
            {
                base.OnViewAdded(child);
                if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
                {
                    toolbar = (Android.Support.V7.Widget.Toolbar)child;
                    toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
                }
            }
            catch { }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            try
            {
                var view = e.Child.GetType();
                if (e.Child.GetType() == typeof(Android.Widget.TextView))
                {
                    var textView = (Android.Widget.TextView)e.Child;

                    var spaceFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "Kokila Bold.ttf");
                    textView.Typeface = spaceFont;
                    toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
                }
            }
            catch
            {

            }
        }
    }
}