using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Racon_Xamarin_New.Droid.Custom_Renderer;
using Android.Graphics;
using Racon_Xamarin_New.Droid.Custom_Renderers;
using Android.Views;
using static Android.Locations.GpsStatus;

[assembly: ExportRenderer(typeof(Label), typeof(FontFamilyLabelRenderer_Droid))]
namespace Racon_Xamarin_New.Droid.Custom_Renderer
{
    public class FontFamilyLabelRenderer_Droid : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

                Control.Typeface = font;
            }
        }

        

    }
}