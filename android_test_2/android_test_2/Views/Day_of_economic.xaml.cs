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
    public partial class Day_of_economic : ContentPage
    {
        public Day_of_economic()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Session_of_show> items = await App.Database.Get_Session_of_shows_Async();
            List<Merchandises> items2 = await App.Database.Get_Merchandises_Async();
            items2 = items2.Where(x => x.is_to_show == true).ToList();
            items2 = items2.OrderBy(x => x.sort).ToList();
            Picker1.ItemsSource = items;
            Picker2.ItemsSource = items2;

            Picker1.SelectedIndexChanged += Picker_SelectedIndexChanged;
            Picker2.SelectedIndexChanged += Picker_SelectedIndexChanged;

            show_name.Text = "-";
            Dividend_item.Text = "-";
            total_amount.Text = "-";
            hime_Kurumi.Text = "-";
            hime_Mika.Text = "-";
            hime_Youta.Text = "-";
            hime_Guagua.Text = "-";

        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            show_content();
        }

        async void show_content()
        {
            Session_of_show sos = (Picker1.SelectedItem as Session_of_show);
            Merchandises mds = (Picker2.SelectedItem as Merchandises);
            if (sos == null)
                return;

            int special_item_id = -999999;

            int total_money = 0;
            double Kurumi = 0;
            double Mika = 0;
            double Youta = 0;
            double Guagua = 0;
            double denominator = 0; //分母

            if (mds != null)
                special_item_id = mds.merchandise_id;
            else
            {
                Dividend_item.Text = "-";
                hime_Kurumi.Text = "-";
                hime_Mika.Text = "-";
                hime_Youta.Text = "-";
                hime_Guagua.Text = "-";
            }
            List<Orders> od = await App.Database.Get_Orders_Async();
            od = od.Where(x => x.session_id == sos.session_id).ToList();

            for (int i = 0; i < od.Count; i++)
            {
                if (od[i].ispay)
                {
                    List<Order_detail> od_d = await App.Database.Get_Order_details_Async();
                    od_d = od_d.Where(x => x.order_id == od[i].order_id).ToList();
                    for (int j = 0; j < od_d.Count; j++)
                    {
                        total_money += od_d[j].final_amount * od_d[j].quantity;
                        if (od_d[j].merchandise_id == special_item_id)
                        {
                            denominator = himerevolt_tools.BitCount(od_d[j].product_type);
                            double this_dividend = (od_d[j].final_amount * od_d[j].quantity) / denominator;
                            if ((od_d[j].product_type & 1) == 1) Kurumi += this_dividend;
                            if ((od_d[j].product_type & 2) == 2) Mika += this_dividend;
                            if ((od_d[j].product_type & 4) == 4) Youta += this_dividend;
                            if ((od_d[j].product_type & 8) == 8) Guagua += this_dividend;
                        }
                    }
                }
            }


            show_name.Text = sos.name_of_show;
            total_amount.Text = total_money.ToString();
            if (special_item_id != -999999)
            {
                Dividend_item.Text = mds.product_name;
                hime_Kurumi.Text = Kurumi.ToString("0.##");
                hime_Mika.Text = Mika.ToString("0.##");
                hime_Youta.Text = Youta.ToString("0.##");
                hime_Guagua.Text = Guagua.ToString("0.##");
            }
        }


    }
}