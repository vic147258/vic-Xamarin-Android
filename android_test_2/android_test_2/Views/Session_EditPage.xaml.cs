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
    public partial class Session_EditPage : ContentPage
    {
        public int edit_type { get; set; }
        public Session_EditPage()
        {
            InitializeComponent();            
        }

       

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Session_of_show)BindingContext;
            note.show_day = DateTime.UtcNow;
            await App.Database.Save_Session_of_show_Async(note);
            //await DisplayAlert("提示", "存檔完成~", "確定");
            await Task.Delay(300);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Session_of_show)BindingContext;
            await App.Database.Delete_Session_of_show_Async(note);
            await Navigation.PopAsync();
        }
    }
}