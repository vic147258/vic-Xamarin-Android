using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using android_test_2.Models;
using System.IO;
using Xamarin.Essentials;
using Plugin.FilePicker;
using Syncfusion.XlsIO;
using System.Collections.Generic;
using Syncfusion.Compression;
using System.Reflection;
using android_test_2.Data;

namespace android_test_2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class inoutfile : ContentPage
    {
        public inoutfile()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 匯出的按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void click_export(object sender, EventArgs e)
        {

            bool answer = await DisplayAlert("提示", "匯出需要等一下喔\n可以等嗎?", "可以", "取消");

            if (!answer)
                return;


            ExcelEngine excelEngine = new ExcelEngine();
            excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;
            IWorkbook workbook = excelEngine.Excel.Workbooks.Create(4);  //不包含總表

            IWorksheet worksheet1 = workbook.Worksheets[0];
            IWorksheet worksheet2 = workbook.Worksheets[1];
            IWorksheet worksheet3 = workbook.Worksheets[2];
            IWorksheet worksheet4 = workbook.Worksheets[3];
            //IWorksheet worksheet5 = workbook.Worksheets[4];

            #region Session_of_show
            List<Session_of_show> Revoltitem1 = await App.Database.Get_Session_of_shows_Async();
            worksheet1.Name = "場次表";
            worksheet1.Range["A1"].Text = "索引";
            worksheet1.Range["B1"].Text = "場次名稱";
            worksheet1.Range["C1"].Text = "地點";
            worksheet1.Range["D1"].Text = "日期";
            worksheet1.Range["E1"].Text = "備註";
            for (int i = 0; i < Revoltitem1.Count; i++)
            {
                worksheet1.Range["A" + (i + 2).ToString()].Value = Revoltitem1[i].session_id.ToString();
                worksheet1.Range["B" + (i + 2).ToString()].Text = Revoltitem1[i].name_of_show != null ? Revoltitem1[i].name_of_show.ToString() : "";
                worksheet1.Range["C" + (i + 2).ToString()].Text = Revoltitem1[i].location != null ? Revoltitem1[i].location.ToString() : "";
                worksheet1.Range["D" + (i + 2).ToString()].Text = Revoltitem1[i].show_day.ToString();
                worksheet1.Range["E" + (i + 2).ToString()].Text = Revoltitem1[i].remarks != null ? Revoltitem1[i].remarks.ToString() : "";
            }
            #endregion
            
            #region Merchandises
            List<Merchandises> Revoltitem2 = await App.Database.Get_Merchandises_Async();
            worksheet2.Name = "商品表";
            worksheet2.Range["A1"].Text = "索引";
            worksheet2.Range["B1"].Text = "商品名稱";
            worksheet2.Range["C1"].Text = "定價";
            worksheet2.Range["D1"].Text = "排序";
            worksheet2.Range["E1"].Text = "顯示於選單";
            worksheet2.Range["F1"].Text = "現場拍攝";
            worksheet2.Range["G1"].Text = "備註";
            for (int i = 0; i < Revoltitem2.Count; i++)
            {
                worksheet2.Range["A" + (i + 2).ToString()].Value = Revoltitem2[i].merchandise_id.ToString();
                worksheet2.Range["B" + (i + 2).ToString()].Text = Revoltitem2[i].product_name != null ? Revoltitem2[i].product_name.ToString() : "";
                worksheet2.Range["C" + (i + 2).ToString()].Value = Revoltitem2[i].price.ToString();
                worksheet2.Range["D" + (i + 2).ToString()].Value = Revoltitem2[i].sort.ToString();
                worksheet2.Range["E" + (i + 2).ToString()].Boolean = Revoltitem2[i].is_to_show;
                worksheet2.Range["F" + (i + 2).ToString()].Boolean = Revoltitem2[i].on_site_photo;
                worksheet2.Range["G" + (i + 2).ToString()].Text = Revoltitem2[i].remarks != null ? Revoltitem2[i].remarks.ToString() : "";
            }
            #endregion

            #region Orders
            List<Orders> Revoltitem3 = await App.Database.Get_Orders_Async();
            worksheet3.Name = "訂單表";
            worksheet3.Range["A1"].Text = "索引";
            worksheet3.Range["B1"].Text = "場次ID";
            worksheet3.Range["C1"].Text = "暱稱";
            worksheet3.Range["D1"].Text = "是否付款";
            worksheet3.Range["E1"].Text = "是否取件";
            worksheet3.Range["F1"].Text = "備註";
            for (int i = 0; i < Revoltitem3.Count; i++)
            {
                worksheet3.Range["A" + (i + 2).ToString()].Value = Revoltitem3[i].order_id.ToString();
                worksheet3.Range["B" + (i + 2).ToString()].Value = Revoltitem3[i].session_id.ToString();
                worksheet3.Range["C" + (i + 2).ToString()].Text = Revoltitem3[i].nickname != null ? Revoltitem3[i].nickname.ToString() : "";
                worksheet3.Range["D" + (i + 2).ToString()].Boolean = Revoltitem3[i].ispay;
                worksheet3.Range["E" + (i + 2).ToString()].Boolean = Revoltitem3[i].istake;
                worksheet3.Range["F" + (i + 2).ToString()].Text = Revoltitem3[i].remarks != null ? Revoltitem3[i].remarks.ToString() : "";
            }
            #endregion

            #region Order_detail
            List<Order_detail> Revoltitem4 = await App.Database.Get_Order_details_Async();
            worksheet4.Name = "訂單清單";
            worksheet4.Range["A1"].Text = "索引";
            worksheet4.Range["B1"].Text = "訂單ID";
            worksheet4.Range["C1"].Text = "商品ID";
            worksheet4.Range["D1"].Text = "4人狀態";
            worksheet4.Range["E1"].Text = "數量";
            worksheet4.Range["F1"].Text = "成交金額";
            worksheet4.Range["G1"].Text = "已拍攝";
            worksheet4.Range["H1"].Text = "備註";
            for (int i = 0; i < Revoltitem4.Count; i++)
            {
                worksheet4.Range["A" + (i + 2).ToString()].Value = Revoltitem4[i].order_detail_id.ToString();
                worksheet4.Range["B" + (i + 2).ToString()].Value = Revoltitem4[i].order_id.ToString();
                worksheet4.Range["C" + (i + 2).ToString()].Value = Revoltitem4[i].merchandise_id.ToString();
                worksheet4.Range["D" + (i + 2).ToString()].Value = Revoltitem4[i].product_type.ToString();
                worksheet4.Range["E" + (i + 2).ToString()].Value = Revoltitem4[i].quantity.ToString();
                worksheet4.Range["F" + (i + 2).ToString()].Value = Revoltitem4[i].final_amount.ToString();
                worksheet4.Range["G" + (i + 2).ToString()].Boolean = Revoltitem4[i].is_take_pic;
                worksheet4.Range["H" + (i + 2).ToString()].Text = Revoltitem4[i].remarks != null ? Revoltitem4[i].remarks.ToString() : "";
            }
            #endregion

            #region 自訂表單     
            /*
            worksheet5.Name = "總表";
            worksheet5.Range["A1"].Text = "指定場次";
            worksheet5.Range["B1"].Value = "場次名稱";
            worksheet5.Range["A2"].Value = "1";
            worksheet5.Range["B2"].Formula = "=VLOOKUP(A2,場次表!A2:B31,2,FALSE)";
            worksheet5.Range["B2"].ColumnWidth = 1;
            */
            #endregion


            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);

            workbook.Close();

            //Save the stream as a file in the device and invoke it for viewing
            //Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("GettingStared.xlsx", "application/msexcel", stream);


            await SaveAndShareFileAsync("姬Revolt(" + DateTime.Now.ToString("MMddHHmmss") + ").xlsx", stream);
            //await DisplayAlert("提示", "匯出 " + bbbb + " 完成", "確定");
        }





        /// <summary>
        /// 匯入的按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void click_inport(object sender, EventArgs e)
        {
            Plugin.FilePicker.Abstractions.FileData pickedFile = await CrossFilePicker.Current.PickFile();

            if (pickedFile != null && pickedFile.FileName.EndsWith("xlsx", StringComparison.OrdinalIgnoreCase))
            {

                #region 檔案放入工作表 workbook 跟 worksheet1~4
                //存到暫存

                var File_Path = Path.Combine(FileSystem.CacheDirectory, pickedFile.FileName);

                MemoryStream msmsmsms = new MemoryStream();
                pickedFile.GetStream().CopyTo(msmsmsms);
                if (File.Exists(File_Path))
                    File.Delete(File_Path);
                File.WriteAllBytes(File_Path, msmsmsms.ToArray());

                //Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView(pickedFile.FileName, "application/msexcel", msmsmsms);

                //await DisplayAlert("提示", "長度：" + msmsmsms.Length.ToString() + "", "確定");

                //讀到物件

                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;

                /*
                Assembly assembly = typeof(App).GetTypeInfo().Assembly;
                Stream excelStream = assembly.GetManifestResourceStream(pickedFile.FileName);
                */

                FileStream fs = new FileStream(File_Path, FileMode.Open, FileAccess.Read);
                //StreamReader r = new StreamReader(fs);
                //MemoryStream excelStream =new MemoryStream() ; // assembly.GetManifestResourceStream(File_Path);
                //fs.CopyTo(excelStream);

                fs.Position = 0;
                //excelStream.Position = 0;
                IWorkbook workbook = application.Workbooks.Open(fs, ExcelOpenType.Automatic, ExcelParseOptions.Default);

                IWorksheet worksheet1 = workbook.Worksheets[0];
                IWorksheet worksheet2 = workbook.Worksheets[1];
                IWorksheet worksheet3 = workbook.Worksheets[2];
                IWorksheet worksheet4 = workbook.Worksheets[3];
                #endregion

                #region 清除資料庫
                if (!(await DisplayAlert("警告", "這會刪除所有現有資料哦~\n真的可以嗎?", "可以", "取消"))) return;
                App.Database.re_database();
                #endregion

                #region Session_of_show
                int i = 2;
                while (true)
                {
                    if (worksheet1.Range["A" + i.ToString()].Value == "") break;
                    Session_of_show sos = new Session_of_show();
                    sos.session_id = int.Parse(worksheet1.Range["A" + i.ToString()].Value);
                    sos.name_of_show = worksheet1.Range["B" + i.ToString()].Text;
                    sos.location = worksheet1.Range["C" + i.ToString()].Text;
                    sos.show_day = worksheet1.Range["D" + i.ToString()].DateTime;
                    sos.remarks = worksheet1.Range["E" + i.ToString()].Text;
                    await App.Database.Insert_Session_of_show_Async(sos);
                    //await App.Database.Save_Session_of_show_Async(sos);
                    i++;
                }
                #endregion

                //await DisplayAlert("提示", "場次完成", "確定");

                #region Merchandises
                i = 2;
                while (true)
                {
                    
                    if (worksheet2.Range["A" + i.ToString()].Value == "") break;
                    Merchandises mds = new Merchandises();
                    mds.merchandise_id = int.Parse(worksheet2.Range["A" + i.ToString()].Value);
                    mds.product_name = worksheet2.Range["B" + i.ToString()].Text;
                    mds.price =  int.Parse(worksheet2.Range["C" + i.ToString()].Value);
                    mds.sort = int.Parse(worksheet2.Range["D" + i.ToString()].Value);
                    mds.is_to_show = worksheet2.Range["E" + i.ToString()].Boolean;
                    mds.on_site_photo = worksheet2.Range["F" + i.ToString()].Boolean;
                    mds.remarks = worksheet2.Range["G" + i.ToString()].Text;
                    await App.Database.Insert_Merchandise_Async(mds);
                    //await App.Database.Save_Merchandise_Async(mds);
                    i++;
                }
                #endregion

                //await DisplayAlert("提示", "商品完成", "確定");

                #region Orders
                i = 2;
                while (true)
                {
                    if (worksheet3.Range["A" + i.ToString()].Value == "") break;
                    Orders ord = new Orders();
                    ord.order_id = int.Parse(worksheet3.Range["A" + i.ToString()].Value);
                    ord.session_id = int.Parse(worksheet3.Range["B" + i.ToString()].Value);
                    ord.nickname = worksheet3.Range["C" + i.ToString()].Text;
                    //ord.ispay = Convert.ToBoolean(worksheet3.Range["D" + i.ToString()].Text);
                    //ord.istake = Convert.ToBoolean(worksheet3.Range["E" + i.ToString()].Text);
                    ord.ispay = worksheet3.Range["D" + i.ToString()].Boolean;
                    ord.istake = worksheet3.Range["E" + i.ToString()].Boolean;
                    ord.remarks = worksheet3.Range["F" + i.ToString()].Text;
                    await App.Database.Insert_Order_Async(ord);
                    //await App.Database.Save_Order_Async(ord);
                    i++;
                }
                #endregion

                //await DisplayAlert("提示", "訂單完成", "確定");

                #region Order_detail
                i = 2;
                while (true)
                {
                    if (worksheet4.Range["A" + i.ToString()].Value == "") break;
                    Order_detail ord_d = new Order_detail();
                    ord_d.order_detail_id = int.Parse(worksheet4.Range["a" + i.ToString()].Value);
                    ord_d.order_id = int.Parse(worksheet4.Range["B" + i.ToString()].Value);
                    ord_d.merchandise_id = int.Parse(worksheet4.Range["C" + i.ToString()].Value);
                    ord_d.product_type = int.Parse(worksheet4.Range["D" + i.ToString()].Value);
                    ord_d.quantity = int.Parse(worksheet4.Range["E" + i.ToString()].Value);
                    ord_d.final_amount = int.Parse(worksheet4.Range["F" + i.ToString()].Value);
                    ord_d.is_take_pic = worksheet4.Range["G" + i.ToString()].Boolean;
                    ord_d.remarks = worksheet4.Range["H" + i.ToString()].Text;
                    await App.Database.Insert_Order_detail_Async(ord_d);
                    //await App.Database.Save_Order_detail_Async(ord_d);
                    i++;
                }
                #endregion


                
                await DisplayAlert("提示", "匯入完成~", "確定");

            }
            else
                await DisplayAlert("提示", "匯入失敗", "確定");

            /*
            //這個只能讀本地端檔案
            String templateFileName = "count.txt";
            using (var stream = await FileSystem.OpenAppPackageFileAsync(templateFileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var fileContents = await reader.ReadToEndAsync();
                }
            }*/
        }


        /// <summary>
        /// 存檔分享的副程式
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SaveAndShareFileAsync(String the_filname, MemoryStream context)
        {
            //存到暫存
            var File_Path = Path.Combine(FileSystem.CacheDirectory, the_filname);
            if (File.Exists(File_Path))
                File.Delete(File_Path);
            File.WriteAllBytes(File_Path, context.ToArray());
            
            
            //分享
            ExperimentalFeatures.Enable("ShareFileRequest_Experimental");
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = the_filname,
                File = new ShareFile(File_Path)
            });
            
        }

        /// <summary>
        /// 存檔分享的副程式(純文字)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SaveAndShareFileAsync(String the_filname, String context)
        {
            //存到暫存
           
            var file = Path.Combine(FileSystem.CacheDirectory, the_filname);
            File.WriteAllText(file, context);

            //分享
            ExperimentalFeatures.Enable("ShareFileRequest_Experimental");
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = the_filname,
                File = new ShareFile(file)
            });
        }



        public async Task<String> ReadLocalFileAsync()
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "count.txt");

            if (backingFile == null || !File.Exists(backingFile))
            {
                return "";
            }

            String context = "";
            using (var reader = new StreamReader(backingFile, true))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    //if (int.TryParse(line, out var newcount))
                    {
                        context += (int.Parse(line) + 999).ToString() + "\n";
                    }
                }
            }

            return context;
        }


    }
}