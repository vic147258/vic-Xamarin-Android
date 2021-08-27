using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace android_test_2.Models
{
    public class Item_of_day
    {

        [PrimaryKey, AutoIncrement]
        public int order_detail_id { get; set; }   //沒錄用流水號
        public String product_name { get; set; }   //商品名稱
        public String product_type { get; set; }     //基本上就是4個人選了哪幾個
        public int all_quantity { get; set; }     //總數


    }
}
