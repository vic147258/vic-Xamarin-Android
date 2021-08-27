using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using android_test_2.Models;

namespace android_test_2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.首頁, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.首頁:
                        MenuPages.Add(id, new NavigationPage(new Session_ListPage()));
                        break;
                    case (int)MenuItemType.關於:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.商品:
                        MenuPages.Add(id, new NavigationPage(new Merchandises_ListPage()));
                        break;
                    case (int)MenuItemType.日結算:
                        MenuPages.Add(id, new NavigationPage(new Day_of_economic()));
                        break;
                    case (int)MenuItemType.日商品:
                        MenuPages.Add(id, new NavigationPage(new Day_of_item()));
                        break;
                    case (int)MenuItemType.匯出入:
                        MenuPages.Add(id, new NavigationPage(new inoutfile()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}