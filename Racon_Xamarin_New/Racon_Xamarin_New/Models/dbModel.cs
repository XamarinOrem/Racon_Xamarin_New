using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racon_Xamarin_New.Models
{
    public class loggedInUser
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string userId { get; set; }


        public string UserName { get; set; }

        public string UserCreateDate { get; set; }

        public string BarcodeImageUrl { get; set; }

        public string InstagramLink { get; set; }

        public string FacebookLink { get; set; }

        public string WifiInfo { get; set; }

        public string TotalCoffeeCups { get; set; }

        public string UserGroup { get; set; }

        public string temprature { get; set; }

        public string weatherImg { get; set; }
        public string Address { get; set; }
        public string OpeningHours { get; set; }

        public double lat { get; set; }
        public double lng { get; set; }

        public string notficationEnabled { get; set; }
        //

    }

    public class temperatuerData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string temprature { get; set; }

        public string weatherImgDB { get; set; }

        public string tempUnit { get; set; }

    }

        public class LoginDetails
    {
        public static string userId { get; set; }


   }


   




}
