using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Repository;
using Racon_Xamarin_New.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace Racon_Xamarin_New.ViewModel
{
    public class CompanyViewModel : INotifyPropertyChanged
    {
        Page page;

        public CompanyViewModel(Page page)
        {
            this.page = page;

        }

        bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                    return;

                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }


        ICommand refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;


            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                var page1 = (CompanyLogin)page;
                page1.UpdateControls();
                IsBusy = false;
                return false;
            });
        }


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;


        #endregion
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
