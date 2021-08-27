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
    public partial class Day_of_item : ContentPage
    {
        public Day_of_item()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Session_of_show> items = await App.Database.Get_Session_of_shows_Async();
            Picker1.ItemsSource = items;

            Picker1.SelectedIndexChanged += Picker_SelectedIndexChanged;

            listView1.ItemsSource = null;

        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            show_content();
        }

        async void show_content()
        {
            Session_of_show sos = (Picker1.SelectedItem as Session_of_show);
            show_name.Text = "(未選擇)";
            if (sos == null)
                return;

            List<Orders> od = await App.Database.Get_Orders_Async();
            od = od.Where(x => x.session_id == sos.session_id).ToList();

            List<Order_detail> od_d = await App.Database.Get_Order_details_Async();
            od_d = od_d.Where(x => check_in_array(x.order_id, od)).ToList();

            //var res2 = od_d
            //    .GroupBy(x => x.merchandise_id, x => x.product_type)
            //   .Select(g => new { PA = g.Key, Sum = g.Sum(f => f.) });


            List<IGrouping<int, Order_detail>> result = od_d.GroupBy(x => x.merchandise_id).ToList();

            result = result.OrderBy(x => x.Key).ToList(); //排序

            List<Item_of_day> iod = new List<Item_of_day>();

            foreach (IGrouping<int, Order_detail> group in result)
            {
                String tttname = (await App.Database.Get_Merchandise_Async(group.Key)).product_name;

                List<IGrouping<int, Order_detail>> result2 = group.GroupBy(x => x.product_type).ToList();
                result2 = result2.OrderBy(x => x.Key).ToList(); //排序

                foreach (IGrouping<int, Order_detail> group2 in result2)
                {
                    String ttttype = chang_show_description(group2.Key);
                    int tttquantity = 0;
                    
                    foreach (Order_detail qqqqqqqqqqqqqqqqqqqqqqqqqqq in group2)
                    {
                        tttquantity += qqqqqqqqqqqqqqqqqqqqqqqqqqq.quantity;
                    }

                    iod.Add(new Item_of_day { product_name = tttname, product_type = ttttype, all_quantity = tttquantity });
                }
            }


            show_name.Text = sos.name_of_show;
            listView1.ItemsSource = iod;

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

            //if (the_type != 0) ccc += "\n";


            return ccc;
        }


        Boolean check_in_array(int id, List<Orders> od)
        {
            int ccount = od.Where(x => x.order_id == id).Count();

            if (ccount == 0)
                return false;
            else
                return true;
        }

    }
}