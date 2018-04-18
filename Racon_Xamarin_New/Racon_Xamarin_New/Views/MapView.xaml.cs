using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Racon_Xamarin_New.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Racon_Xamarin_New.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {

        string location, openingLocation;
        Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();


        public MapView(Plugin.Geolocator.Abstractions.Position _position)
        {
            InitializeComponent();
            this.Title = "Location";
            position = _position;

            location = LoginPage.Location;

            openingLocation = LoginPage.locationLabel;

            mapView.HeightRequest = App.ScreenHeight;
            mapView.WidthRequest = App.ScreenWidth;
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();



            // On Droid this wraps behind the other views.
            if (Device.RuntimePlatform == Device.Android)
            {
                Grid.SetRowSpan(mapView, 3);

                if (ToolbarItems.Count > 1)
                {
                    var firstItem = ToolbarItems.FirstOrDefault();
                    ToolbarItems.Remove(firstItem);
                }
            }

            try
            {
                // Set the map to your current location.

                if (position != null)
                {
                    //var pin = new Pin()
                    //{
                    //    Position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),
                    //    Label = location,
                    //    Address =openingLocation
                    //};
                    //mapView.Pins.Add(pin);

                  

                    var pin = new CustomPin
                    {
                        Type = PinType.Place,
                        Position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),
                        Label = location,
                        Address = openingLocation,
                        Id = "Xamarin",
                        Url = "http://xamarin.com/about/"
                    };




                    mapView.CustomPins = new List<CustomPin> { pin };

                    mapView.Pins.Add(pin);


                    mapView.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),
                        Distance.FromMiles(1.0)).WithZoom(5));
                }
            }
            catch
            {
                // No biggie if you don't allow location, only here for show purposes.
            }






        }


    }
}