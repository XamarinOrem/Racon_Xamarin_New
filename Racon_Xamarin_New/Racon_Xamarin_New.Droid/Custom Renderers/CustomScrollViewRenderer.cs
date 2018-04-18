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
using System.ComponentModel;
using Racon_Xamarin_New.Droid.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ScrollView), typeof(CustomScrollViewRenderer))]
namespace Racon_Xamarin_New.Droid.Custom_Renderers
{
    public class CustomScrollViewRenderer:ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            try
            {
                base.OnElementChanged(e);

                if (e.OldElement != null || this.Element == null)
                {
                    return;
                }

                if (e.OldElement != null)
                {
                    e.OldElement.PropertyChanged -= OnElementPropetyChanged;
                }

                e.NewElement.PropertyChanged += OnElementPropetyChanged;
            }
            catch { }
        }

        private void OnElementPropetyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (ChildCount > 0)
                {
                    GetChildAt(0).HorizontalScrollBarEnabled = false;
                }
            }
            catch { }
        }
    }
}