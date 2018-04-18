using Racon_Xamarin_New.Models;
using Racon_Xamarin_New.Repository;
using Racon_Xamarin_New.Views;
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
    public class NewsPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int totalcount = 0;
        public bool IsFirstHit = false;
        public int getEventCount = 0;
        public int pageindex = 1;
        public int pageSize = 5;
        public bool HitinProcess = false;

        public string labelStatus, starButtonStatus;

        public InfiniteScrollCollection<NewsItems> Items { get;  }



       

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

       
        public NewsPageViewModel()
        {
            Items = new InfiniteScrollCollection<NewsItems>
            {
                OnLoadMore = async () =>
                {
                    var items = new InfiniteScrollCollection<NewsItems>();

                    if (totalcount > getEventCount && getEventCount!=0 || IsFirstHit == false)
                    {
                        if (!HitinProcess)
                        {
                            HitinProcess = true;
                            IsLoadingMore = true;
                            var response = await CommonLib.NewsList(CommonLib.ws_MainUrl + "AccountApi/GetNews?" + "userId=" + LoginDetails.userId + "&pageIndex=" + pageindex + "&pageSize=" + pageSize + "");
                                
                            if (response != null && response.news.Count != 0 )
                            {
                               


                                
                                    pageindex++;
                                    IsFirstHit = true;
                                    totalcount = response.Count;
                                    getEventCount = response.news.Count;
                                    items = GetItems(true, response.news);
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


        InfiniteScrollCollection<NewsItems> GetItems(bool clearList, List<wsNews> newsItemList)
        {
            InfiniteScrollCollection<NewsItems> items;
            if (clearList || Items == null)
            {
                items = new InfiniteScrollCollection<NewsItems>();
            }
            else
            {
                items = new InfiniteScrollCollection<NewsItems>(Items);
            }



            foreach (var newsItem in newsItemList)
            {



                items.Add(new NewsItems
                {


                    Title = UppercaseFirst(newsItem.Title),
                    Description = UppercaseFirst(newsItem.SubTitle),

                    ImageUrl = newsItem.PictureUrl





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
