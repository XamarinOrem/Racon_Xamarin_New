using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Racon_Xamarin_New.Controls
{
    public class CustomPinNew : Pin
    {
        public string Url { get; set; }
    }
    public class CustomMapNew : Map
    {
        public List<CustomPinNew> CustomPins { get; set; }
    }


    public class RacoonMap :Map
    {
        public static double pinLongitutde { get; set; }

        public static double pinLatitude { get; set; }


        public static string pinLabel { get; set; }

        public static string pinAddress { get; set; }

        public List<CustomPin> CustomPins { get; set; }
       
    }
    public class CustomPin :Pin
    {
        
        public string Url { get; set; }
       
    }
}