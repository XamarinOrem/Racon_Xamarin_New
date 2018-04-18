using Racon_Xamarin_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Racon_Xamarin_New.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : ContentPage
    {
        
        
        public EventDetailPage(EventListModel eventData )
        {
            InitializeComponent();
            this.Title = "Details";

            DayText.Text = eventData.Day;
            DateText.Text = eventData.Date;
            YearText.Text = eventData.Year;

            TitleText.Text = eventData.Title;

            PlaceText.Text = eventData.Place;


            SubTitleText.Text= eventData.SubTitle;

           

            boxViewVertical.BackgroundColor = Color.FromHex(eventData.Color);


        }


      

      
    }
}