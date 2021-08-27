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
    public partial class Order_EditPage : ContentPage
    {
        public Order_EditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Orders aaaaa = (Orders)BindingContext;

            checkbox_pay.IsChecked = aaaaa.ispay;
            checkbox_take.IsChecked = aaaaa.istake;

        }


        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Orders)BindingContext;
            note.ispay = checkbox_pay.IsChecked;
            note.istake = checkbox_take.IsChecked;
            await App.Database.Save_Order_Async(note);
            await Task.Delay(300);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Orders)BindingContext;
            await App.Database.Delete_Order_Async(note);

            List<Order_detail> oooddd = (await App.Database.Get_Order_details_Async()).FindAll(x => x.order_id == note.order_id);
            foreach (Order_detail todelete in oooddd)
            {
                await App.Database.Delete_Order_detail_Async(todelete);
            }

            await Navigation.PopAsync();
        }

    }
}