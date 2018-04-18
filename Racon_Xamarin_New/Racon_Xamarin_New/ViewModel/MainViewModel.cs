using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace Racon_Xamarin_New.ViewModel
{
   public  class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int totalcount = 0;
        public bool IsFirstHit = false;
        public int getEventCount = 0;
        public int pageindex = 1;
        public int pageSize = 5;
        public bool HitinProcess = false;

        public string labelStatus, starButtonStatus;

        public InfiniteScrollCollection<Products> Items { get; }

        public bool _isLoadingMore;
        public bool IsLoadingMore
        {
            get
            {
                return _isLoadingMore;
            }
            set
            {
                _isLoadingMore = value;
                OnPropertyChanged(nameof(IsLoadingMore));
            }
        }


        public MainViewModel( int categoriesId)
        {
            Items = new InfiniteScrollCollection<Products>
            {
                OnLoadMore = async () =>
                {
                    var items = new InfiniteScrollCollection<Products>();
                    if (totalcount > getEventCount && getEventCount != 0 || IsFirstHit == false || totalcount ==0)
                    {

                        if (!HitinProcess)
                        {
                            HitinProcess = true;
                            IsLoadingMore = true;

                            var response = await CommonLib.CategoryProductList(CommonLib.ws_MainUrl + "AccountApi/GetProducts?id="+ categoriesId+ "&pageIndex="+pageindex+"&pageSize= "+pageSize+"");

                            if (response != null && response.products.Count != 0 && response.Count!=0)
                            {

                               


                                    pageindex++;
                                    IsFirstHit = true;
                                    totalcount = response.Count;
                                    getEventCount = response.products.Count;
                                    items = GetItems(true, response.products);
                                    IsLoadingMore = false;
                                    HitinProcess = false;


                            }
                            else
                            {
                                getEventCount = 0;
                                HitinProcess = false;
                                IsLoadingMore = false;

                            }
                        }
                    }
                    //Call your Web API next items page.
                    //if (!ProductCategories.IsPull)
                    //    await Task.Delay(1200);

                    return items;
                }
            };
            Items.LoadMoreAsync();
        }


        InfiniteScrollCollection<Products> GetItems(bool clearList, List<WSProduct> productItemList)
        {
            InfiniteScrollCollection<Products> items;
            if (clearList || Items == null)
            {
                items = new InfiniteScrollCollection<Products>();
            }
            else
            {
                items = new InfiniteScrollCollection<Products>(Items);
            }



            foreach (var productItem in productItemList)
            {



                items.Add(new Products
                {


                    Title = UppercaseFirst(productItem.PName),
                    Description = UppercaseFirst(productItem.PDescription),

                    ImageUrl = productItem.PictureUrl,

                    Price = Convert.ToString( productItem.PPriceIncVAT)+ " kr",





                });



            }

            return items;
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }




        public void CollectionDidChange(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}
