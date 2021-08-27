using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace android_test_2.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "姬Revolt";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://www.facebook.com/himeRevolt/")));
        }

        public ICommand OpenWebCommand { get; }
    }
}