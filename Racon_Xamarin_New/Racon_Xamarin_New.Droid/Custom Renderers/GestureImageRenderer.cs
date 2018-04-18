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
using Xamarin.Forms.Platform.Android;
using Racon_Xamarin_New.CustomControls;
using Xamarin.Forms;
using Racon_Xamarin_New.Droid.Custom_Renderers;

[assembly: ExportRenderer(typeof(CustomImage), typeof(GestureImageRenderer))]
namespace Racon_Xamarin_New.Droid.Custom_Renderers
{
    public class GestureImageRenderer:ImageRenderer
    {
        private readonly CustomGestureListener _listener;
        private readonly GestureDetector _detector;

        public GestureImageRenderer()
        {
            _listener = new CustomGestureListener();
            _detector = new GestureDetector(_listener);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
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
            CustomImage _gi = (CustomImage)this.Element;
            _gi.OnSwipeLeft();
        }

        void HandleOnSwipeRight(object sender, EventArgs e)
        {
            CustomImage _gi = (CustomImage)this.Element;
            _gi.OnSwipeRight();
        }

        void HandleOnSwipeTop(object sender, EventArgs e)
        {
            CustomImage _gi = (CustomImage)this.Element;
            _gi.OnSwipeTop();
        }

        void HandleOnSwipeDown(object sender, EventArgs e)
        {
            CustomImage _gi = (CustomImage)this.Element;
            _gi.OnSwipeDown();
        }
    }
}