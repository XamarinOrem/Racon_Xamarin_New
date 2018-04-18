using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Racon_Xamarin_New.Models
{
    public class EventListModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }
        public string SubTitle { get; set; }

        public string Day { get; set; }

        public string Date { get; set; }

        public string Year { get; set; }


        public string Place { get; set; }

        public string Color { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
