using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using android_test_2.Services;
using android_test_2.Views;
using android_test_2.Data;
using System.IO;

namespace android_test_2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        static himerevolt_Database database;

        public static himerevolt_Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new himerevolt_Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "himerevolt.db3"));
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
