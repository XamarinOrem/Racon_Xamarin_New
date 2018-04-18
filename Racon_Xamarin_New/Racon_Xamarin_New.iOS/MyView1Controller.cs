using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using CoreGraphics;
using CoreLocation;
using Foundation;
using UIKit;
using System.Globalization;
using Rg.Plugins.Popup.Extensions;
using System.Linq;
using Google.Maps;
using Racon_Xamarin_New.Controls;

namespace Racon_Xamarin_New.iOS
{
    public partial class MyView1Controller : UIViewController
    {

        public MyView1Controller() : base("MyView1Controller", null)
        {
        }
        public static UIView ParentView;
        public static string searchParameter { get; set; }
        public static string searchText { get; set; }
        public object DependencyService { get; private set; }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupMap();
        }



        MapView GoogleMapView;
        #region Web Api Call
        public async void GetAlertUsers()
        {
            double lat = 0;
            double lng = 0;
            try
            {
                lat=RacoonMap.pinLatitude ;
                lng = RacoonMap.pinLongitutde;
                Xamarin.Forms.Maps.Position position = new Xamarin.Forms.Maps.Position(lat, lng);


                UIImage icon = UIImage.FromFile("pin.png");

                var xamMarker = new Marker()
                {
                    Title = RacoonMap.pinLabel,
                    Position = new CLLocationCoordinate2D(lat, lng),
                    AppearAnimation = MarkerAnimation.Pop,
                    Icon = icon,
                    Snippet = RacoonMap.pinAddress,
                };
                xamMarker.Map = GoogleMapView;

                GoogleMapView.Animate(CameraUpdate.SetTarget(new
                                                             CLLocationCoordinate2D(RacoonMap.pinLatitude,
                                                                                    RacoonMap.pinLongitutde)));
                GoogleMapView.InfoTapped += GoogleMapView_InfoTapped;

                GoogleMapView.SelectedMarker = xamMarker;
                GoogleMapView.TappedMarker = (map, marker) =>
                {
                    GoogleMapView.MarkerInfoWindow = new GMSInfoFor(markerInfoWindow);
                    return false;
                };

               

            }
            catch
            {
            }
            finally
            {

            }

        }

        #endregion

        private async void SetupMap()
        {
            if (GoogleMapView != null)
                GoogleMapView.RemoveFromSuperview();
            try
            {

                /*
                    if (position != null)
                    {
                        _centerLatitude = position.Latitude.ToString();
                        _centerLongitude = position.Longitude.ToString();
                    }
                */
            }
            catch (Exception ex)
            {
            }

            if (!string.IsNullOrEmpty(RacoonMap.pinLongitutde.ToString())
                && !string.IsNullOrEmpty(RacoonMap.pinLatitude.ToString()))
            {
                var camera = new CameraPosition(
                    new CLLocationCoordinate2D(RacoonMap.pinLatitude, RacoonMap.pinLongitutde),
                                                10, 30, 0);
                GoogleMapView = MapView.FromCamera(RectangleF.Empty, camera);

                //Add button to zoom to location
                GoogleMapView.Settings.MyLocationButton = false;
                GoogleMapView.MyLocationEnabled = false;



                UIView topView = new UIView();
                topView.BackgroundColor = UIColor.White;
                var topViewframe = topView.Frame;
                topViewframe.X = 0;
                topViewframe.Y = 0;
                topViewframe.Width = (nfloat)App.ScreenWidth;
                topViewframe.Height = 70;
                topView.Frame = topViewframe;

                UIButton btn = new UIButton();
                var btnFrame = btn.Frame;
                btn.Frame = topViewframe;
                btn.TouchUpInside += Btn_TouchUpInside;

                UIImageView img = new UIImageView();
                img.Image = UIImage.FromFile("back.png");
                var imgFrame = img.Frame;
                imgFrame.Width = 25;
                imgFrame.Height = 25;
                imgFrame.X = 17;
                imgFrame.Y = 23;
                img.Frame = imgFrame;

                UILabel lblTitle = new UILabel();
                lblTitle.Text = "Location";
                lblTitle.Font = UIFont.FromName("Helvetica-Bold", 15f);
                lblTitle.TextColor = UIColor.Black;
                var lblTitleFrame = lblTitle.Frame;
                lblTitleFrame.Width = 120;
                lblTitleFrame.Height = 50;
                lblTitleFrame.X = (nfloat)App.ScreenWidth / 2 - 55;
                lblTitleFrame.Y = 15;
                lblTitle.Frame = lblTitleFrame;


                topView.Add(img);
                topView.Add(lblTitle);

                topView.Add(btn);
                View.Add(topView);

                var frame = GoogleMapView.Frame;
                frame.X = 0;
                frame.Y = 60;
                frame.Width = (nfloat)App.ScreenWidth;
                frame.Height = (nfloat)App.ScreenHeight - 40;
                GoogleMapView.Frame = frame;
                View.Add(GoogleMapView);

                GetAlertUsers();
            }
        }

        private void Btn_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true, null);
        }

        UIView markerInfoWindow(UIView view, Marker marker)
        {

            var data = marker.Snippet.Split('\n');
            var lines = data.Length;
            if(data.Length >0)
            {
                lines = data.Length + 1;
            }
            else{
                lines = 1; 
            }
            UIView view1 = new UIView();
            view1.BackgroundColor = UIColor.White;

            var view1Frame = view1.Frame;
            view1Frame.Width = 210;
            view1Frame.Height = 29*lines;
            view1.Frame = view1Frame;

            UILabel lblTitle = new UILabel();
            lblTitle.Text = marker.Title;
            //lblTitle.Font = UIFont.FromName("Helvetica", 15f);
            lblTitle.TextColor = UIColor.Black;
            var lblTitleFrame = lblTitle.Frame;
            lblTitleFrame.Width = 210;
            lblTitleFrame.Height = 20;
            lblTitleFrame.X = 5;
            lblTitleFrame.Y = 5;
            lblTitle.Frame = lblTitleFrame;
            view1.Add(lblTitle);

            UILabel lblSnipt = new UILabel();
            lblSnipt.Text = marker.Snippet;
            lblSnipt.LineBreakMode = UILineBreakMode.WordWrap;
            lblSnipt.Lines = lines;
            //lblSnipt.Font = UIFont.FromName("Helvetica", 15f);
            lblSnipt.TextColor = UIColor.Black;
            var lblSniptFrame = lblSnipt.Frame;
            lblSniptFrame.Width = 210;
            lblSniptFrame.Height = 20*lines;
            lblSniptFrame.X = 5;
            lblSniptFrame.Y = 30;
            lblSnipt.Frame = lblSniptFrame;
            view1.Add(lblSnipt);



            return view1;
        }


        private void GoogleMapView_InfoTapped(object sender, GMSMarkerEventEventArgs e)
        {

        }
    }
}