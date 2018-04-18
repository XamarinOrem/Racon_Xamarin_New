using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Racon_Xamarin_New.CustomControls;
using Xamarin.Forms;
using Racon_Xamarin_New.Droid.Custom_Renderers;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(CustomLayout), typeof(GestureLayoutRenderer))]
namespace Racon_Xamarin_New.Droid.Custom_Renderers
{
    public class GestureLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        private readonly CustomGestureListener _listener;
        private readonly GestureDetector _detector;
        public GestureLayoutRenderer()
        {
            _listener = new CustomGestureListener();
            _detector = new GestureDetector(_listener);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
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
            CustomLayout _gi = (CustomLayout)this.Element;
            _gi.OnSwipeLeft();
        }

        void HandleOnSwipeRight(object sender, EventArgs e)
        {
            CustomLayout _gi = (CustomLayout)this.Element;
            _gi.OnSwipeRight();
        }

        void HandleOnSwipeTop(object sender, EventArgs e)
        {
            CustomLayout _gi = (CustomLayout)this.Element;
            _gi.OnSwipeTop();
        }

        void HandleOnSwipeDown(object sender, EventArgs e)
        {
            CustomLayout _gi = (CustomLayout)this.Element;
            _gi.OnSwipeDown();
        }
    }
}