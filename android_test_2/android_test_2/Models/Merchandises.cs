using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace android_test_2.Models
{
    public class Merchandises
    {


        [PrimaryKey]
        public int merchandise_id { get; set; }  //產品ID
        public string product_name { get; set; }  //產品名稱
        public int price { get; set; }      //定價
        public int sort { get; set; }      //排序
        public Boolean is_to_show { get; set; }      //現場拍照(決定新增商品時的預設值)
        public Boolean on_site_photo { get; set; }      //現場拍照(決定新增商品時的預設值)
        public string remarks { get; set; }


        public String show_description { get; set; }   //拿來顯說明用的()
        public String show_description2 { get; set; }   //拿來顯說明用的()
    }
}
