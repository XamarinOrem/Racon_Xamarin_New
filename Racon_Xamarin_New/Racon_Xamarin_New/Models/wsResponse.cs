using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racon_Xamarin_New.Repository
{

    //wsLogIn
    public class wsLogInCompany
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string WifiInfo { get; set; }
        public string Tlf { get; set; }
        public string InstagramLink { get; set; }
        public string FacebookLink { get; set; }
        public bool CIsActive { get; set; }
        public DateTime CCreatedDate { get; set; }
        public string OpeningHours { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int CID { get; set; }
    }

    public class wsLogInUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public object ConfirmPassword { get; set; }
        public DateTime LastLogin { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public int MainUserGrp { get; set; }
        public int TotalCoffeeCups { get; set; }
        public object Barcode { get; set; }
        public object BarcodeImage { get; set; }
        public int CID { get; set; }
        public DateTime UserCreateDate { get; set; }
        public DateTime UserLastUpdate { get; set; }
        public bool IsActive { get; set; }
        public int DeviceId { get; set; }
        public object DeviceToken { get; set; }
        public bool RememberMe { get; set; }
        public string BarcodeImageUrl { get; set; }
        public wsLogInCompany Company { get; set; }
    }

    public class wsLogIn
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public wsLogInUser User { get; set; }
    }


    /// <summary>
    /// wsRegistration Model
    /// </summary>
    /// 


    public class Company
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string WifiInfo { get; set; }
        public string Tlf { get; set; }
        public string InstagramLink { get; set; }
        public string FacebookLink { get; set; }
        public bool CIsActive { get; set; }
        public DateTime CCreatedDate { get; set; }
        public string OpeningHours { get; set; }
        public int CID { get; set; }
    }

    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MainUserGrp { get; set; }
        public int TotalCoffeeCups { get; set; }
        public string Barcode { get; set; }
        public string BarcodeImage { get; set; }
        public int CID { get; set; }
        public DateTime UserCreateDate { get; set; }
        public DateTime UserLastUpdate { get; set; }
        public bool IsActive { get; set; }
        public int DeviceId { get; set; }
        public object DeviceToken { get; set; }
        public int UserID { get; set; }
        public object ConfirmPassword { get; set; }
        public string Name { get; set; }
        public bool RememberMe { get; set; }
        public string BarcodeImageUrl { get; set; }
        public Company Company { get; set; }
    }

    public class wsRegistration
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public User User { get; set; }
    }


    // wsForgetPassword

    public class wsForgetPassword
    {
        public int Status { get; set; }
        public string msg { get; set; }
    }


    //wsEventList

    public class News
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public int TypeID { get; set; }
        public string SubTitle { get; set; }
        public string Body { get; set; }
        public string Picture { get; set; }
        public string Place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string URL { get; set; }
        public bool IsActive { get; set; }
        public int MainUserGrp { get; set; }
        public bool IsSubGrp { get; set; }
        public object SubUserGroup { get; set; }
        public bool IsToUser { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public object TypeName { get; set; }
        public object MainUserGrpName { get; set; }
        public string PictureUrl { get; set; }
    }

    public class wsEventList
    {
        public List<News> events { get; set; }
        public int Count { get; set; }
    }


    //wsNewsList

    public class wsNews
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Body { get; set; }
        public object AttachmentPDF { get; set; }
        public object Picture { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateValidFrom { get; set; }
        public DateTime DateValidTo { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public string Summary { get; set; }
        public bool IsActive { get; set; }
        public int MainUserGrp { get; set; }
        public bool IsSubGrp { get; set; }
        public object SubUserGroup { get; set; }
        public bool IsToUser { get; set; }
        public object UserID { get; set; }
        public int NewsID { get; set; }
        public object MainUserGrpName { get; set; }
        public string PictureUrl { get; set; }
        public string AttachmentPDFUrl { get; set; }

    }

    public class wsNewsList
    {
        public List<wsNews> news { get; set; }

        public int Count { get; set; }
    }


    //wsMenuUpperCategoriesList

    public class MenuUpperCategory
    {
        public string PCName { get; set; }
        public DateTime PCCreatedDate { get; set; }
        public bool PCIsActive { get; set; }
        public int CID { get; set; }
        public string PCIcon { get; set; }
        public int PCID { get; set; }
        public string IconUrl { get; set; }
    }

    public class wsMenuUpperCategoriesList
    {
        public List<MenuUpperCategory> categories { get; set; }


    }

    //wsCategoryProductList

    public class WSProduct
    {
        public string PName { get; set; }
        public string PDescription { get; set; }
        public string PPicture { get; set; }
        public bool PIsActive { get; set; }
        public DateTime PCreatedDate { get; set; }
        public int PCID { get; set; }
        public double PPriceIncVAT { get; set; }
        public int PID { get; set; }
        public object CategoryName { get; set; }
        public string PictureUrl { get; set; }
    }

    public class wsCategoryProductList
    {
        public List<WSProduct> products { get; set; }

        public int Count { get; set; }
    }

    //wsBarcode

    public class wsBarcode
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public bool isFree { get; set; }
    }


    //wsFirsttimeCompanydata

    public class wsFirsttimeCompany
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string WifiInfo { get; set; }
        public string Tlf { get; set; }
        public string InstagramLink { get; set; }
        public string FacebookLink { get; set; }
        public bool CIsActive { get; set; }
        public DateTime CCreatedDate { get; set; }
        public string OpeningHours { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int CID { get; set; }
    }

    public class wsFirsttimeCompanydata
    {
        public wsFirsttimeCompany company { get; set; }
    }

    //wsUpdateDeviceInformation

    public class wsUpdateDeviceInformationCompany
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string WifiInfo { get; set; }
        public string Tlf { get; set; }
        public string InstagramLink { get; set; }
        public string FacebookLink { get; set; }
        public bool CIsActive { get; set; }
        public DateTime CCreatedDate { get; set; }
        public string OpeningHours { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int CID { get; set; }
    }

    public class wsUpdateDeviceInformationUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MainUserGrp { get; set; }
        public int TotalCoffeeCups { get; set; }
        public string Barcode { get; set; }
        public string BarcodeImage { get; set; }
        public int CID { get; set; }
        public DateTime UserCreateDate { get; set; }
        public DateTime UserLastUpdate { get; set; }
        public bool IsActive { get; set; }
        public int DeviceId { get; set; }
        public object DeviceToken { get; set; }
        public int UserID { get; set; }
        public object ConfirmPassword { get; set; }
        public string Name { get; set; }
        public bool RememberMe { get; set; }
        public string BarcodeImageUrl { get; set; }
        public wsUpdateDeviceInformationCompany Company { get; set; }
        public object subGroups { get; set; }

    }

    public class wsUpdateDeviceInformation
    {
        public int Status { get; set; }
        public string msg { get; set; }
        public int newsCount { get; set; }
        public int eventCount { get; set; }
        public wsUpdateDeviceInformationUser user { get; set; }
    }


}
