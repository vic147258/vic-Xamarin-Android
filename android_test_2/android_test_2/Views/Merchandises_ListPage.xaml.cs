using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using android_test_2.Models;

namespace android_test_2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Merchandises_ListPage : ContentPage
    {
        public Merchandises_ListPage()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Merchandises> ssss = await App.Database.Get_Merchandises_Async();

            ssss = ssss.OrderBy(x => x.sort).ToList();


            ssss.ForEach(x => x.show_description = "單價 " + x.price.ToString() + " 元 " + (x.remarks == "" || x.remarks == null ? "" : "(" + x.remarks + ")"));

            ssss.ForEach(x => x.show_description2 = (x.on_site_photo ? "(現場拍照商品)" : "") +(!x.is_to_show ? " (選單不顯示)" : ""));

            listView.ItemsSource = ssss;
        }

        async void OnSessionAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Merchandises_EditPage
            {
                BindingContext = new Merchandises() { is_to_show = true },
                Title = "新增商品"
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Merchandises_EditPage
                {
                    BindingContext = e.SelectedItem as Merchandises,
                    Title = "修改商品"
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
                await Navigation.PushAsync(new Merchandises_EditPage
                {
                    BindingContext = mi.CommandParameter as Merchandises,
                    Title = "修改商品"
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
            //Session_of_show sos = (Session_of_show)mi.CommandParameter;

            //DisplayAlert("提示", mi.CommandParameter + "恩恩", "OK");
            //bool answer = await DisplayAlert("警告", "確定筆資料中的所有訊息嗎", "刪除", "取消");
            bool answer = await DisplayAlert("警告", "對不起\n不給你刪除", "保留", "取消");
            /*
            if (mi.CommandParameter != null && answer)
            {
                await App.Database.Delete_Merchandise_Async(mi.CommandParameter as Merchandises);
                //await Navigation.PopAsync();
                listView.ItemsSource = await App.Database.Get_Merchandises_Async();
            }
            */
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




    }
}