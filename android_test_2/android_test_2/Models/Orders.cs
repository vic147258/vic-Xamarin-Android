using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace android_test_2.Models
{
    public class Orders
    {
        [PrimaryKey]
        public int order_id { get; set; }   //訂單編號
        public int session_id { get; set; }   //哪一場的訂單
        public string nickname { get; set; }   //訂單買家名子
        public Boolean ispay { get; set; }   //是否付款
        public Boolean istake { get; set; }   //是否取貨


        public string remarks { get; set; }

        public int count_of_detail { get; set; }   //用來計算有幾個商品的
        public string show_state { get; set; }   //用來顯示付款取貨資訊
        public string show_state2 { get; set; }   //用來顯示備註

        /*
        public int count_of_detail;

        public Orders()
        {
            get_count_of_detail();
        }

        async public void get_count_of_detail()
        {
            List<Order_detail> hhhhhhhh = await App.Database.Get_Order_details_Async();
            // List<Order_detail> zzzzzz = hhhhhhhh.FindAll(x => x.order_id == this.order_id).Count;
            count_of_detail = hhhhhhhh.FindAll(x => x.order_id == this.order_id).Count;
        }
        */

    }
}