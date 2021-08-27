using android_test_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace android_test_2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Session_ListPage : ContentPage
    {
        public Session_ListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Session_of_show> sos = await App.Database.Get_Session_of_shows_Async();
            sos.ForEach(x => x.show_description = x.show_day.ToString("yyyy年MM月dd日") + " " + x.location + " " + (x.remarks == "" || x.remarks == null ? "" : "(" + x.remarks + ")"));
            listView.ItemsSource = sos;
        }

        async void OnSessionAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Session_EditPage
            {
                BindingContext = new Session_of_show() { show_day = DateTime.UtcNow },
                Title = "新增場次"
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Order_ListPage
                {
                    BindingContext = e.SelectedItem as Session_of_show,
                    Title = (e.SelectedItem as Session_of_show).name_of_show + ""
                }) ;
            }

            /*
            //單點會進入其中的清單，不是直接修改了
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Session_EditPage
                {
                    BindingContext = e.SelectedItem as Session_of_show ,
                    Title = "修改場次內容"
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
                await Navigation.PushAsync(new Session_EditPage
                {
                    BindingContext = mi.CommandParameter as Session_of_show,
                    Title = "修改場次內容"
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
            bool answer = await DisplayAlert("警告", "確定刪除這筆資料中的所有訊息嗎", "刪除", "取消");
            

            if (mi.CommandParameter != null && answer)
            {
                await App.Database.Delete_Session_of_show_Async(mi.CommandParameter as Session_of_show);
                //await Navigation.PopAsync();
                listView.ItemsSource = await App.Database.Get_Session_of_shows_Async();
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



    }
}