using Xamarin.Forms;
using Racon_Xamarin_New.Droid.Custom_Renderer;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;

[assembly: ExportRenderer(typeof(Entry), typeof(FontFamilyEntryRenderer_Droid))]
namespace Racon_Xamarin_New.Droid.Custom_Renderer
{
    public class FontFamilyEntryRenderer_Droid:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

                Control.Typeface = font;
            }
            if (Control != null)
            {
                GradientDrawable customBG = new GradientDrawable();
                customBG.SetColor(Android.Graphics.Color.Transparent);
                //customBG.SetCornerRadius(3);
                int borderWidth = 2;
                customBG.SetStroke(borderWidth, Android.Graphics.Color.White);
                this.Control.SetBackground(customBG);
                this.Control.Gravity = GravityFlags.CenterVertical;
            }
        }
    }
}