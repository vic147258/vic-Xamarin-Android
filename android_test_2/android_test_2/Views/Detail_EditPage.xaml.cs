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
    public partial class Detail_EditPage : ContentPage
    {
        public Detail_EditPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Merchandises> items = await App.Database.Get_Merchandises_Async();
            items = items.Where(x => x.is_to_show == true).ToList();
            items = items.OrderBy(x => x.sort).ToList();
            Picker1.ItemsSource = items;// await App.Database.Get_Merchandises_Async();

           

            Order_detail aaaaa = (Order_detail)BindingContext;


            if ((aaaaa.product_type & 1) == 1) checkbox1.IsChecked = true;
            if ((aaaaa.product_type & 2) == 2) checkbox2.IsChecked = true;
            if ((aaaaa.product_type & 4) == 4) checkbox3.IsChecked = true;
            if ((aaaaa.product_type & 8) == 8) checkbox4.IsChecked = true;


            Picker1.SelectedItem = items.Find(x => x.merchandise_id == aaaaa.merchandise_id);


        }



        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (Picker1.SelectedItem == null)
            {
                await DisplayAlert("未選擇商品", "你真的還沒選商品", "OK");
                return;
            }

            var note = (Order_detail)BindingContext;

            note.merchandise_id = (Picker1.SelectedItem as Merchandises).merchandise_id;

            note.product_type = 0;
            if (checkbox1.IsChecked) note.product_type += (int)Math.Pow(2, 0);
            if (checkbox2.IsChecked) note.product_type += (int)Math.Pow(2, 1);
            if (checkbox3.IsChecked) note.product_type += (int)Math.Pow(2, 2);
            if (checkbox4.IsChecked) note.product_type += (int)Math.Pow(2, 3);

            //note.final_amount = (Picker1.SelectedItem as Merchandises).price;  //直接讀欄位

            await App.Database.Save_Order_detail_Async(note);
            await Task.Delay(300);
            await Navigation.PopAsync();
        }
        private void Picker1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Picker1.SelectedItem != null)
            {
                famount.Text = (Picker1.SelectedItem as Merchandises).price.ToString();
                checkbox5.IsChecked = !(Picker1.SelectedItem as Merchandises).on_site_photo;
            }
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Order_detail)BindingContext;
            await App.Database.Delete_Order_detail_Async(note);
            await Navigation.PopAsync();
        }

        void On_butt_chang_Quantity(object sender, EventArgs e)
        {
            //textbox_Quantity.Text = ((Button)sender).Text;

            if (((Button)sender).Text.Equals("+"))
                textbox_Quantity.Text = (int.Parse(textbox_Quantity.Text) + 1).ToString();
            else
            {
                if (int.Parse(textbox_Quantity.Text) > 1)
                {
                    textbox_Quantity.Text = (int.Parse(textbox_Quantity.Text) - 1).ToString();
                }
            }

        }


    }
}