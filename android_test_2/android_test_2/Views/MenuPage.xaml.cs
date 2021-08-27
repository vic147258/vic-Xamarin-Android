using android_test_2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace android_test_2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                //選單上顯示的文字
                new HomeMenuItem {Id = MenuItemType.首頁, Title="物販" },
                new HomeMenuItem {Id = MenuItemType.商品, Title="商品清單" },
                new HomeMenuItem {Id = MenuItemType.關於, Title="關於" },
                new HomeMenuItem {Id = MenuItemType.日結算, Title="帳目結算" },
                new HomeMenuItem {Id = MenuItemType.日商品, Title="出貨狀況" },
                new HomeMenuItem {Id = MenuItemType.匯出入, Title="備份" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}