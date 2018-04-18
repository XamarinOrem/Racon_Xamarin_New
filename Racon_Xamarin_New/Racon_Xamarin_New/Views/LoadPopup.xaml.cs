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
    public partial class LoadPopup : PopupPage
    {
        public LoadPopup()
        {
            InitializeComponent();



        }
            

        public static async void CloseAllPopup()
        {
            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);
        }


    }
}