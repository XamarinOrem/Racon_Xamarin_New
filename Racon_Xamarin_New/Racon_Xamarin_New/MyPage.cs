using System;
using System.Collections.Generic;
using Racon_Xamarin_New.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Racon_Xamarin_New
{
    public class MyPage : ContentPage
    {
        public MyPage(double Latitude,double Longitude,string Label,string Address)
        {
            var customMap = new CustomMapNew
            {
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight
            };
            var pin = new CustomPinNew
            {
                Type = PinType.Place,
                Position = new Position(Latitude,Longitude),
                Label = Label,
                Address = Address,
                Id = "Xamarin",
                Url = ""
            };

            customMap.CustomPins = new List<CustomPinNew> { pin };
            customMap.Pins.Add(pin);
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(Latitude, Longitude), Distance.FromMiles(1.0)));

            Content = customMap;
        }
    }
}

