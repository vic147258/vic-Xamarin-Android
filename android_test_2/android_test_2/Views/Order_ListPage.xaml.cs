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
    public partial class Order_ListPage : ContentPage
    {
        public Order_ListPage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            read_to_listView();

            //listView.ItemsSource = (await App.Database.Get_Orders_Async()).FindAll(x => x.session_id == ((Session_of_show)BindingContext).session_id);
            //listView.ItemsSource = await App.Database.Get_Orders_Async();
        }

        async void read_to_listView()
        {
            //await DisplayAlert("讀表一次", "讀表一次", "OK");


            //listView = null;
            //listView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "listView");
            

            List<Orders> aaa = (await App.Database.Get_Orders_Async()).FindAll(x => x.session_id == ((Session_of_show)BindingContext).session_id);
            List<Order_detail> bbb = await App.Database.Get_Order_details_Async();
            aaa.ForEach(x => x.count_of_detail = bbb.FindAll(y => y.order_id == x.order_id).Count);
            
            aaa.ForEach(x => x.show_state = (x.ispay ? "" : "(未付款) ") + "" + (x.istake ? "" : "(未取貨) ") + "" + (bbb.FindAll(y => y.order_id == x.order_id && y.is_take_pic == false).Count == 0 ? "": "( " + bbb.FindAll(y => y.order_id == x.order_id && y.is_take_pic == false).Count + " 項未拍攝) "));
            aaa.ForEach(x => x.show_state2 = (x.remarks == "" || x.remarks == null ? "" : "(" + x.remarks + ")"));

            listView.ItemsSource = aaa;
            
            
        }

        async void OnSessionAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Order_EditPage
            {
                BindingContext = new Orders() { session_id = ((Session_of_show)BindingContext).session_id },
                //the_Session_id = ((Orders)BindingContext).session_id,
                Title = "新增粉絲"
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Detail_listPage
                {
                    BindingContext = e.SelectedItem as Orders,
                    Title = (e.SelectedItem as Orders).nickname + " 的清單"
                });
            }

            /*
            //傳送至直接編輯頁面
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Order_EditPage
                {
                    BindingContext = e.SelectedItem as Orders,
                    Title = "修改訂單"
                });
            }*/
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
                await Navigation.PushAsync(new Order_EditPage
                {
                    BindingContext = mi.CommandParameter as Orders,
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
            Orders ooo = mi.CommandParameter as Orders;

            //DisplayAlert("提示", mi.CommandParameter + "恩恩", "OK");
            bool answer = await DisplayAlert("警告", "確定刪除 [" + ooo.nickname + "] 的所有訊息嗎?", "刪除", "取消");

            if (answer)
            {
                await App.Database.Delete_Order_Async(mi.CommandParameter as Orders);

                //await Navigation.PopAsync();
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


        /// <summary>
        /// 長按-取款付款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Onhold_pay_and_take(object sender, EventArgs e)
        {
            var mi = ((ToolbarItem)sender) ;
            Orders ooo = mi.CommandParameter as Orders;

            if (mi.Text.Equals("付款"))
            {
                //bool answer = await DisplayAlert(ooo.nickname + "付款狀態", "確定將 [" + ooo.nickname + "] 的付款狀態\n設為 [" + (ooo.ispay ? "未付款":"已付款") + "] 嗎?", "確定", "取消");
                //if (answer)
                {
                    ooo.ispay = !ooo.ispay;
                    await App.Database.Save_Order_Async(ooo);
                    //await Task.Delay(300);
                    read_to_listView();
                    //listView.ScrollTo(ooo, ScrollToPosition.Center, true);
                    
                }

            }
            else if (mi.Text.Equals("取貨"))
            {
                //bool answer = await DisplayAlert(ooo.nickname + "取貨狀態", "確定將 [" + ooo.nickname + "] 的付款狀態\n設為 [" + (ooo.istake ? "未取貨" : "已取貨") + "] 嗎?", "確定", "取消");
                //if (answer)
                {
                    ooo.istake = !ooo.istake;
                    await App.Database.Save_Order_Async(ooo);
                    //await Task.Delay(300);
                    read_to_listView();
                    //listView.ScrollTo(ooo, ScrollToPosition.Center, true);
                    
                }
            }

        }

    }
}