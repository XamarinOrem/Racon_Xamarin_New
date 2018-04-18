using Racon_Xamarin_New.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racon_Xamarin_New.Data
{
    public class DBracon
    {
        /// <summary>
        /// Declaratipn part
        /// </summary>
        readonly SQLiteConnection database;



        /// <summary>
        /// Constructor part
        /// </summary>
        /// <param name="dbPath"></param>
        public DBracon(string dbPath)
        {
            try
            {
                database = new SQLiteConnection(dbPath);
                database.CreateTable<loggedInUser>();
                database.CreateTable<temperatuerData>();
                

            }

            catch (Exception ex)
            {

            }
            
        }



        public bool GetLoginUser(out loggedInUser user)
        {
            user = new loggedInUser();
            bool isLogin = false;
            try
            {
                user = database.Table<loggedInUser>().First();
                if (user != null)
                {
                    isLogin = true;
                }
            }
            catch (Exception ex)
            {
                isLogin = false;
            }
            return isLogin;
        }

        public int SaveLoggedUser(loggedInUser objLoggedUser)
        {
            int status = 0;
            try
            {
                database.DeleteAll<loggedInUser>();
                status = database.Insert(objLoggedUser);
            }
            catch (Exception ex)
            {
                status = 0;
            }
            return status;
        }


             
        public int ClearLoginDetails()
        {
            var status = 0;
            try
            {
                var data = database.Table<loggedInUser>().ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }

            }
            catch (Exception ex)
            {

            }

            return status;
        }



        public bool GetTempData(out temperatuerData tempData)
        {
            tempData = new temperatuerData();
            bool isLogin = false;
            try
            {
                tempData = database.Table<temperatuerData>().First();
                if (tempData != null)
                {
                    isLogin = true;
                }
            }
            catch (Exception ex)
            {
                isLogin = false;
            }
            return isLogin;
        }

        public int SaveTempData(temperatuerData objTempData)
        {
            int status = 0;
            try
            {
                database.DeleteAll<temperatuerData>();
                status = database.Insert(objTempData);
            }
            catch (Exception ex)
            {
                status = 0;
            }
            return status;
        }


    }
}
