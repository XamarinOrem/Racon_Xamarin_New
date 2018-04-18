using System;
using MapKit;

namespace Racon_Xamarin_New.iOS
{
    public class CustomMKAnnotationView : MKAnnotationView
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public CustomMKAnnotationView(IMKAnnotation annotation, string id)
         : base(annotation, id)
        {
        }
    }

}
