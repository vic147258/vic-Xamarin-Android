using android_test_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace android_test_2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail_listPage : ContentPage
    {
        public Detail_listPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            read_to_listView();
        }

        async void read_to_listView()
        {
            List<Order_detail> aaa = (await App.Database.Get_Order_details_Async()).FindAll(x => x.order_id == ((Orders)BindingContext).order_id);
            List<Merchandises> bbb = await App.Database.Get_Merchandises_Async();
            aaa.ForEach(x => x.product_name = bbb.Find(y => y.merchandise_id == x.merchandise_id).product_name);

            aaa.ForEach(x => x.show_description = chang_show_description(x.product_type) + (x.remarks == "" || x.remarks == null ? "" : " (" + x.remarks + ")") + "\n" + "單價 " + x.final_amount + " 元, " + x.quantity + "個");

            aaa.ForEach(x => x.show_description2 = (x.is_take_pic ? "" : "(未拍攝)"));

            int total_amount = 0;
            aaa.ForEach(x => total_amount += x.final_amount * x.quantity);

            aaa.Add(new Order_detail() { order_detail_id = -99, show_description = "總計：" + total_amount.ToString() + " 元", product_name = "共計 " + aaa.Count.ToString() + " 項" });

            listView.ItemsSource = aaa;
        }

        String chang_show_description(int the_type)
        {
            String ccc = "";

            if ((the_type & 1) == 1)
                ccc += "Kurumi";

            if ((the_type & 2) == 2 & ccc != "")
                ccc += ", Mika";
            else if ((the_type & 2) == 2)
                ccc += "Mika";

            if ((the_type & 4) == 4 & ccc != "")
                ccc += ", Youta";
            else if ((the_type & 4) == 4)
                ccc += "Youta";

            if ((the_type & 8) == 8 & ccc != "")
                ccc += ", Guagua";
            else if ((the_type & 8) == 8)
                ccc += "Guagua";

            if (the_type != 0) ccc += "";


            return ccc;
        }

        async void OnSessionAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Detail_EditPage
            {
                BindingContext = new Order_detail() { order_id = ((Orders)BindingContext).order_id ,quantity = 1 },
                Title = "新增項目"
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null && (e.SelectedItem as Order_detail).order_detail_id != -99)
            {
                await Navigation.PushAsync(new Detail_EditPage
                {
                    BindingContext = e.SelectedItem as Order_detail,
                    Title = "修改項目"
                });
            }
        }


        /// <summary>
        /// 長按-進入修改介面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Onhold_edit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //Session_of_show sos = (Session_of_show)mi.CommandParameter;

            //DisplayAlert("提示", mi.CommandParameter + "恩恩", "OK");

            if (mi.CommandParameter != null)
            {
                await Navigation.PushAsync(new Detail_EditPage
                {
                    BindingContext = mi.CommandParameter as Order_detail,
                    Title = "修改訂單"
                });
            }
        }

        /// <summary>
        /// 長按-刪除某筆資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Onhold_delete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Order_detail sos = (Order_detail)mi.CommandParameter;

            //DisplayAlert("提示", mi.CommandParameter + "恩恩", "OK");
            bool answer = await DisplayAlert("警告", "確定刪除 [" + sos.product_name + "] 嗎?", "刪除", "取消");

            if (mi.CommandParameter != null && answer)
            {
                await App.Database.Delete_Order_detail_Async(mi.CommandParameter as Order_detail);
                //await Navigation.PopAsync();  //這個會回到上一頁
                read_to_listView();
            }
        }

        /// <summary>
        /// 長按-輸出成檔案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Onhold_output(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //Session_of_show sos = (Session_of_show)mi.CommandParameter;
            await DisplayAlert("提示", "還沒寫", "OK");
            //bool answer = await DisplayAlert("警告", "確定筆資料中的所有訊息嗎", "刪除", "取消");
        }

        async void Onhold_take_pic(object sender, EventArgs e)
        {
            var mi = ((ToolbarItem)sender);
            Order_detail od = mi.CommandParameter as Order_detail;

            //if (mi.Text.Equals("已拍"))
            {
                //bool answer = await DisplayAlert("拍照確認", "確定將 [" + od.nickname + "] 的付款狀態\n設為 [" + (od.ispay ? "未付款" : "已付款") + "] 嗎?", "確定", "取消");
                //if (answer)
                {
                    od.is_take_pic = !od.is_take_pic;
                    await App.Database.Save_Order_detail_Async(od);
                    read_to_listView();
                }

            }

        }


    }
}