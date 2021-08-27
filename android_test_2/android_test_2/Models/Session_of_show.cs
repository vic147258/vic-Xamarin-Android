using System;
using System.Windows.Input;
using System.Xml.Serialization;
using SQLite;
using Xamarin.Forms;

namespace android_test_2.Models
{
    public class Session_of_show
    {
        [PrimaryKey]
        public int session_id { get; set; }
        public string name_of_show { get; set; }
        public string location { get; set; }
        public DateTime show_day { get; set; }
        public string remarks { get; set; }


        public String show_description { get; set; }   //拿來顯說明用的()

    }
}
