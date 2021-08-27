using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace android_test_2.Models
{
    class himerevolt_tools
    {

        public async void out_to_excel(String the_filname, String[][] data)
        {

            ExcelEngine excelEngine = new ExcelEngine();
            excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;
            IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);
            IWorksheet worksheet1 = workbook.Worksheets[0];


            worksheet1.Range["A1"].Text = "索引";



            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);
            workbook.Close();

            //存到暫存
            var File_Path = Path.Combine(FileSystem.CacheDirectory, the_filname);
            File.WriteAllBytes(File_Path, stream.ToArray());


            //分享
            ExperimentalFeatures.Enable("ShareFileRequest_Experimental");
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = the_filname,
                File = new ShareFile(File_Path)
            });

        }

        /// <summary>
        /// 計算 1 的個數
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int BitCount(int n)
        {
            Int64 tmp = n - ((n >> 1) & 3681400539) - ((n >> 2) & 1227133513);
            return Convert.ToInt32(((tmp + (tmp >> 3)) & 3340530119) % 63);
        }


    }
}
