using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racon_Xamarin_New.Models
{
    public class CommonLib
    {

        public static string BaseUrl = "http://175.176.184.119:8025/api/AccountApi/";
        public static bool checkconnection()
        {
            var con = CrossConnectivity.Current.IsConnected;
            return con == true ? true : false;
        }
    }
}
