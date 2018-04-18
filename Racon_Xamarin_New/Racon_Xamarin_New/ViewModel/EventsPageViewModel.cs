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
   public class EventsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int totalcount = 0;
        public bool IsFirstHit = false;
        public int getEventCount = 0;
        public int pageindex = 1;
        public int pageSize = 5;
        public bool HitinProcess = false;

        public string labelStatus, starButtonStatus;

        public InfiniteScrollCollection<EventListModel> Items { get; set; }

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


        public EventsPageViewModel()
        {
            Items = new InfiniteScrollCollection<EventListModel>
            {
                OnLoadMore = async () =>
                {
                    var items = new InfiniteScrollCollection<EventListModel>();
                    if (totalcount > getEventCount && getEventCount != 0 || IsFirstHit == false)
                    {

                        if (!HitinProcess)
                        {
                            HitinProcess = true;
                            IsLoadingMore = true;

                            var response = await CommonLib.EventList(CommonLib.ws_MainUrl + "AccountApi/GetEvents?" + "userId=" +
                                LoginDetails.userId+"&pageIndex="+pageindex+ "&pageSize="+pageSize + "");

                            if (response != null && response.events.Count != 0)
                            {

                                   

                                        pageindex++;
                                        IsFirstHit = true;
                                        totalcount = response.Count ;
                                        getEventCount = response.events.Count;
                                        items = GetItems(true,response.events);
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


        InfiniteScrollCollection<EventListModel> GetItems(bool clearList, List<News> eventItemList)
        {
            InfiniteScrollCollection<EventListModel> items;
            if (clearList || Items == null)
            {
                items = new InfiniteScrollCollection<EventListModel>();
            }
            else
            {
                items = new InfiniteScrollCollection<EventListModel>(Items);
            }



            foreach (var eventItem in eventItemList)
            {



                items.Add(new EventListModel
                {


                    Title = UppercaseFirst(eventItem.Title),
                    SubTitle = UppercaseFirst(eventItem.SubTitle),
                    Place = UppercaseFirst(eventItem.Place),

                    Day = eventItem.StartDate.ToString("dddd"),

                    Date = eventItem.StartDate.ToString("dd MMMM"),

                    Year = eventItem.StartDate.ToString("yyyy"),

                    Color =  "#" + GetRandamColor()





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


        public static string GetRandamColor()
        {
            //var randomColor = Xamarin.Forms.Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            //string hex = randomColor.R.ToString("X2") + randomColor.G.ToString("X2") + randomColor.B.ToString("X2");
            //return hex;
            Random rnd = new Random();
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return hexOutput;
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
