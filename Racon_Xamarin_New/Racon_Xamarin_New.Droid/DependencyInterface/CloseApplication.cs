﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Racon_Xamarin_New.DependencyInterface;
using Xamarin.Forms;
using Racon_Xamarin_New.Droid.DependencyInterface;

[assembly: Dependency(typeof(CloseApplication))]

namespace Racon_Xamarin_New.Droid.DependencyInterface
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }


}