using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using System.Net.Http;
using Newtonsoft.Json;

namespace Racon_Xamarin_New.Repository
{
    public class CommonLib
    {
       

        public static string ws_MainUrl = "http://175.176.184.119:8025/api/";

        public static bool checkconnection()
        {
            var con = CrossConnectivity.Current.IsConnected;
            return con == true ? true : false;
        }

        

        /// <summary>
        /// This is for Login page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
       public static async Task<wsLogIn> LoginUser(string url)
        {
            wsLogIn objData = new wsLogIn();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsLogIn>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //#wsRegistration
        /// <summary>
        /// this is for registration user
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsRegistration> RegisterUser(string url)
        {
            wsRegistration objData = new wsRegistration();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsRegistration>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //#wsForgetPassword
        /// <summary>
        /// this is for forget password
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsForgetPassword> ForgotPassword(string url, string postData)
        {
            wsForgetPassword objData = new wsForgetPassword();
            try
            {
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    TimeSpan time = new TimeSpan(0, 0, 20);
                    client.Timeout = time;
                    StringContent str = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
                    result = await client.PostAsync(new Uri(url), str);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsForgetPassword>(await result.Content.ReadAsStringAsync());
                }

            }
            catch (Exception ex)
            {


            }
            return objData;
        }



        //wsEventList
        /// <summary>
        /// This is for Event List Page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<wsEventList> EventList(string url)
        {
            wsEventList objData = new wsEventList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsEventList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsNewsList
        /// <summary>
        /// This is for Event List Page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<wsNewsList> NewsList(string url)
        {
            wsNewsList objData = new wsNewsList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsNewsList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {



            }

            return objData;
        }



        //wsMenuUpperCategoriesList
        /// <summary>
        /// This is for Event List Page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<wsMenuUpperCategoriesList> CategoryList(string url)
        {
            wsMenuUpperCategoriesList objData = new wsMenuUpperCategoriesList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsMenuUpperCategoriesList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {



            }

            return objData;
        }


        /// <summary>
        /// This is for MainView Product List
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>

        public static async Task<wsCategoryProductList> CategoryProductList(string url)
        {
            wsCategoryProductList objData = new wsCategoryProductList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsCategoryProductList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {



            }

            return objData;
        }


        //wsBarcode
        /// <summary>
        /// This id for barcode scanner
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>

        public static async Task<wsBarcode> BarcodeScanner(string url, string postData)
        {
            wsBarcode objData = new wsBarcode();
            try
            {
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    TimeSpan time = new TimeSpan(0, 0, 20);
                    client.Timeout = time;
                    StringContent str = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
                    result = await client.PostAsync(new Uri(url), str);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsBarcode>(await result.Content.ReadAsStringAsync());
                }

            }
            catch (Exception ex)
            {


            }
            return objData;
        }


        //wsFirsttimeCompanydata

        public static async Task<wsFirsttimeCompanydata> CompanyListWithoutLogin(string url)
        {
            wsFirsttimeCompanydata objData = new wsFirsttimeCompanydata();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsFirsttimeCompanydata>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {



            }

            return objData;
        }


        //wsUpdateDeviceInformation

        public static async Task<wsUpdateDeviceInformation> UpdateDeviceInformation(string url, string postData)
        {
            wsUpdateDeviceInformation objData = new wsUpdateDeviceInformation();
            try
            {
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    TimeSpan time = new TimeSpan(0, 0, 20);
                    client.Timeout = time;
                    StringContent str = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
                    result = await client.PostAsync(new Uri(url), str);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsUpdateDeviceInformation>(await result.Content.ReadAsStringAsync());
                }

            }
            catch (Exception ex)
            {


            }
            return objData;
        }

    }
}
