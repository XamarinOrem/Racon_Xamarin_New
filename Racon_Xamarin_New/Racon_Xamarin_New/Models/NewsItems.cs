using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Racon_Xamarin_New.Models
{
    public class NewsItems 
    {
       // public event PropertyChangedEventHandler PropertyChanged;
        public string ImageUrl { get; set;}
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsVisible { get; set; }

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
