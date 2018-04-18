using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Xamarin.Forms;
using Racon_Xamarin_New.Droid.Custom_Renderer;
using Android.Content;
using Racon_Xamarin_New.CustomControls;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomBorderButtonRenderer))]
namespace Racon_Xamarin_New.Droid.Custom_Renderer
{
    public class CustomBorderButtonRenderer:ButtonRenderer
    {
        public CustomBorderButtonRenderer() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Control.SetAllCaps(false);
            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

                Control.Typeface = font;
            }
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }
    }

 
}