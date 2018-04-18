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

    public partial class NewsItemdetailView : ContentPage
    {
        public NewsItemdetailView(NewsItems  newsitemData )
        {
            InitializeComponent();

            this.Title = "Details";

            ImageNews.Source = newsitemData.ImageUrl;

            NewsTitle.Text = newsitemData.Title;

            DescriptionTitle.Text = newsitemData.Description;


        }
    }



}