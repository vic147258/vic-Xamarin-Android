using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace android_test_2.Models
{
    public class Order_detail
    {
        [PrimaryKey]
        public int order_detail_id { get; set; }   //訂單細節編號
        public int order_id { get; set; }   //哪筆訂單
        public int merchandise_id { get; set; }   //哪個商品
        public int product_type { get; set; }   //基本上就是4個人選了哪幾個
        public int quantity { get; set; }   //買了幾個
        public int final_amount { get; set; }   //成交金額
        public Boolean is_take_pic { get; set; }  //是否拍好照了(False就要顯示提示)
        public string remarks { get; set; }



        public String product_name { get; set; }   //拿來顯示品名用的
        public String show_description { get; set; }   //拿來顯說明用的()
        public String show_description2 { get; set; }   //拿來顯是否拍照用的()
    }
}
