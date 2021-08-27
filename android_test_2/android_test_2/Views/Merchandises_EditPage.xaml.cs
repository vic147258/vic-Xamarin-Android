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
    public partial class Merchandises_EditPage : ContentPage
    {
        public Merchandises_EditPage()
        {
            InitializeComponent();
        }


        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Merchandises)BindingContext;
            //note.show_day = DateTime.UtcNow;

            await App.Database.Save_Merchandise_Async(note);
            await Task.Delay(300);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("警告", "對不起\n不給你刪除", "保留", "取消");
            /*
            var note = (Merchandises)BindingContext;
            await App.Database.Delete_Merchandise_Async(note);
            await Navigation.PopAsync();
            */
        }

    }
}