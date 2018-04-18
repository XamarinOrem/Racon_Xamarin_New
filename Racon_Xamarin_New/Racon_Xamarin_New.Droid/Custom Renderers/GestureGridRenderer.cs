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
using Xamarin.Forms;
using Racon_Xamarin_New.Droid.Custom_Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomGrid), typeof(GestureGridRenderer))]
namespace Racon_Xamarin_New.Droid.Custom_Renderers
{
    public class GestureGridRenderer : VisualElementRenderer<Grid>
    {
        private readonly CustomGestureListener _listener;
        private readonly GestureDetector _detector;

        public GestureGridRenderer()
        {
            _listener = new CustomGestureListener();
            _detector = new GestureDetector(_listener);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Grid> e)
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
            CustomGrid _gi = (CustomGrid)this.Element;
            _gi.OnSwipeLeft();
        }

        void HandleOnSwipeRight(object sender, EventArgs e)
        {
            CustomGrid _gi = (CustomGrid)this.Element;
            _gi.OnSwipeRight();
        }

        void HandleOnSwipeTop(object sender, EventArgs e)
        {
            CustomGrid _gi = (CustomGrid)this.Element;
            _gi.OnSwipeTop();
        }

        void HandleOnSwipeDown(object sender, EventArgs e)
        {
            CustomGrid _gi = (CustomGrid)this.Element;
            _gi.OnSwipeDown();
        }
    }

}