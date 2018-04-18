using System;
using System.ComponentModel;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using Racon_Xamarin_New.Droid.Custom_Renderers;
using Racon_Xamarin_New.Controls;
using Android.Content;
using System.Collections.Generic;
using Android.Widget;

[assembly: ExportRenderer(typeof(RacoonMap), typeof(CustomMapRenderer))]

namespace Racon_Xamarin_New.Droid.Custom_Renderers
{
    public class CustomMapRenderer:  MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        bool isDrawn;
        List<CustomPin> customPins;


        public CustomMapRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);


            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (RacoonMap)e.NewElement;
                customPins = formsMap.CustomPins;
                Control.GetMapAsync(this);
            }

        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                NativeMap.SetPadding(0, 0, 0, 900);

                NativeMap.MyLocationEnabled = true;
                NativeMap.UiSettings.ZoomControlsEnabled = false;
                NativeMap.UiSettings.CompassEnabled = false;
                NativeMap.UiSettings.MyLocationButtonEnabled = true;
                NativeMap.BuildingsEnabled = false;

                isDrawn = true;
            }
        }




        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }
        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
            return marker;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
              //  throw new Exception("Custom pin not found");
            }

            if (!string.IsNullOrWhiteSpace(customPin.Url))
            {
                var url = Android.Net.Uri.Parse(customPin.Url);
                var intent = new Intent(Intent.ActionView, url);
                intent.AddFlags(ActivityFlags.NewTask);
                Android.App.Application.Context.StartActivity(intent);
            }
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

              
                
                 view = inflater.Inflate(Resource.Layout.XamarinMapInfoWindow, null);
                

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }


        CustomPin GetCustomPin(Marker annotation)
        {
            try
            {
                var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
                foreach (var pin in customPins)
                {
                    if (pin.Position == position)
                    {
                        return pin;
                    }
                }
            }
            catch (Exception ex)
            {

               // throw;
            }
          
            return null;
        }
    }
}