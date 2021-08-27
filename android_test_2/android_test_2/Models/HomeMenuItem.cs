using System;
using System.Collections.Generic;
using System.Text;

namespace android_test_2.Models
{
    public enum MenuItemType
    {
        首頁,
        商品,
        關於,
        日結算,
        日商品,
        匯出入
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
