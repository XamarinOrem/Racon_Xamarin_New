using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Racon_Xamarin_New.CustomControls
{
    public class CustomLayout : StackLayout
    {
        public CustomLayout()
        { }
        public event EventHandler SwipeDown;
        public event EventHandler SwipeTop;
        public event EventHandler SwipeLeft;
        public event EventHandler SwipeRight;

        public void OnSwipeDown()
        {
            if (SwipeDown != null)
                SwipeDown(this, null);
        }

        public void OnSwipeTop()
        {
            if (SwipeTop != null)
                SwipeTop(this, null);
        }

        public void OnSwipeLeft()
        {
            if (SwipeLeft != null)
                SwipeLeft(this, null);
        }

        public void OnSwipeRight()
        {
            if (SwipeRight != null)
                SwipeRight(this, null);
        }
    }

    public class CustomGrid : Grid
    {
        public CustomGrid()
        { }
        public event EventHandler SwipeDown;
        public event EventHandler SwipeTop;
        public event EventHandler SwipeLeft;
        public event EventHandler SwipeRight;

        public void OnSwipeDown()
        {
            if (SwipeDown != null)
                SwipeDown(this, null);
        }

        public void OnSwipeTop()
        {
            if (SwipeTop != null)
                SwipeTop(this, null);
        }

        public void OnSwipeLeft()
        {
            if (SwipeLeft != null)
                SwipeLeft(this, null);
        }

        public void OnSwipeRight()
        {
            if (SwipeRight != null)
                SwipeRight(this, null);
        }
    }

    public class CustomImage : Image
    {
        public CustomImage()
        { }
        public event EventHandler SwipeDown;
        public event EventHandler SwipeTop;
        public event EventHandler SwipeLeft;
        public event EventHandler SwipeRight;

        public void OnSwipeDown()
        {
            if (SwipeDown != null)
                SwipeDown(this, null);
        }

        public void OnSwipeTop()
        {
            if (SwipeTop != null)
                SwipeTop(this, null);
        }

        public void OnSwipeLeft()
        {
            if (SwipeLeft != null)
                SwipeLeft(this, null);
        }

        public void OnSwipeRight()
        {
            if (SwipeRight != null)
                SwipeRight(this, null);
        }
    }
}
