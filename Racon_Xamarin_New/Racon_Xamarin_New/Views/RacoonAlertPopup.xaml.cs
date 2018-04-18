using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class RacoonAlertPopup : PopupPage
    {

        public static string textmsg = "";
        public RacoonAlertPopup()
        {
            try
            {
                InitializeComponent();

                magText.Text = textmsg;
            }
            catch(Exception ex)
            {
                
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                YesButton.WidthRequest = 80;

            }
        }

        
        private async void okButton_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);
        }
        public static async void CloseAllPopup()
        {

            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);

        }
    }
}