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
using Racon_Xamarin_New.CustomControls;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Racon_Xamarin_New.Droid.Custom_Renderers;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(GestureLabelRenderer))]
namespace Racon_Xamarin_New.Droid.Custom_Renderers
{
    public class GestureLabelRenderer:LabelRenderer
    {
        private readonly CustomGestureListener _listener;
        private readonly GestureDetector _detector;

        public GestureLabelRenderer()
        {
            _listener = new CustomGestureListener();
            _detector = new GestureDetector(_listener);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                this.GenericMotion -= HandleGenericMotion;
                this.Touch -= HandleTouch;
                _listener.OnSwipeLeft -= HandleOnSwipeLeft;
                _listener.OnSwipeRight -= HandleOnSwipeRight;
                _listener.OnSwipeTop -= HandleOnSwipeTop;
                _listener.OnSwipeDown -= HandleOnSwipeDown;
            }

            if (e.OldElement == null)
            {
                this.GenericMotion += HandleGenericMotion;
                this.Touch += HandleTouch;
                _listener.OnSwipeLeft += HandleOnSwipeLeft;
                _listener.OnSwipeRight += HandleOnSwipeRight;
                _listener.OnSwipeTop += HandleOnSwipeTop;
                _listener.OnSwipeDown += HandleOnSwipeDown;
            }

            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

                Control.Typeface = font;
            }
        }

        void HandleTouch(object sender, TouchEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
        }

        void HandleGenericMotion(object sender, GenericMotionEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
        }

        void HandleOnSwipeLeft(object sender, EventArgs e)
        {
            CustomLabel _gi = (CustomLabel)this.Element;
            _gi.OnSwipeLeft();
        }

        void HandleOnSwipeRight(object sender, EventArgs e)
        {
            CustomLabel _gi = (CustomLabel)this.Element;
            _gi.OnSwipeRight();
        }

        void HandleOnSwipeTop(object sender, EventArgs e)
        {
            CustomLabel _gi = (CustomLabel)this.Element;
            _gi.OnSwipeTop();
        }

        void HandleOnSwipeDown(object sender, EventArgs e)
        {
            CustomLabel _gi = (CustomLabel)this.Element;
            _gi.OnSwipeDown();
        }
    }
}