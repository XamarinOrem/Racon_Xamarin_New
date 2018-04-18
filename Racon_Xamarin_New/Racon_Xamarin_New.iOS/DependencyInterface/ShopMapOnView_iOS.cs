using AVFoundation;
using Foundation;
using Racon_Xamarin_New.iOS.DependencyInterface;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Racon_Xamarin_New.DependencyInterface;
[assembly: Dependency(typeof(ShopMapOnView_iOS))]
namespace Racon_Xamarin_New.iOS.DependencyInterface
{
    public class ShopMapOnView_iOS : IShowMapView
    {
        public void map()
        {
            MyView1Controller  _ViewController1 = new MyView1Controller();
            try
            {
                UIApplication.SharedApplication.KeyWindow.RootViewController
                             .PresentViewController(_ViewController1,
                   true, null);
            }
            catch (Exception ex)
            {
            }
        }


    }
}

