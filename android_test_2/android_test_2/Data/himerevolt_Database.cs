using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using android_test_2.Models;

namespace android_test_2.Data
{
    public class himerevolt_Database
    {
        readonly SQLiteAsyncConnection _database;

        public himerevolt_Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Session_of_show>().Wait();
            _database.CreateTableAsync<Merchandises>().Wait();
            _database.CreateTableAsync<Orders>().Wait();
            _database.CreateTableAsync<Order_detail>().Wait();
        }

        public void re_database()
        {
            _database.DropTableAsync<Session_of_show>().Wait();
            _database.DropTableAsync<Merchandises>().Wait();
            _database.DropTableAsync<Orders>().Wait();
            _database.DropTableAsync<Order_detail>().Wait();
            _database.CreateTableAsync<Session_of_show>().Wait();
            _database.CreateTableAsync<Merchandises>().Wait();
            _database.CreateTableAsync<Orders>().Wait();
            _database.CreateTableAsync<Order_detail>().Wait();
        }


        #region 表演日的資料表
        public Task<List<Session_of_show>> Get_Session_of_shows_Async()
        {
            return _database.Table<Session_of_show>().ToListAsync();
        }

        public Task<Session_of_show> Get_Session_of_show_Async(int id)
        {
            return _database.Table<Session_of_show>()
                            .Where(i => i.session_id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> Save_Session_of_show_Async(Session_of_show sos)
        {
            if (sos.session_id != 0)
            {
                return _database.UpdateAsync(sos);
            }
            else
            {   //是0的就跑這
                Insert_auto_Session_of_show(sos);
                sos.session_id = -99;
                return _database.UpdateAsync(sos);
            }
        }

        async void Insert_auto_Session_of_show(Session_of_show sos)
        {   //自動 +1 的部分
            Session_of_show sssss = await _database.Table<Session_of_show>().OrderByDescending(x => x.session_id).FirstOrDefaultAsync();

            if (sssss == null)
                sos.session_id = 1;
            else
                sos.session_id = sssss.session_id + 1;
            await _database.InsertAsync(sos);
        }

        public Task<int> Insert_Session_of_show_Async(Session_of_show sos)
        {
            //專們給匯入用的
            return _database.InsertAsync(sos);
        }

        public Task<int> Delete_Session_of_show_Async(Session_of_show note)
        {
            //連帶刪除
            delete_Session_link(note.session_id);
            return _database.DeleteAsync(note);
        }
        async void delete_Session_link(int the_session_id)
        {
            List<Orders> oooddd = (await App.Database.Get_Orders_Async()).FindAll(x => x.session_id == the_session_id);
            foreach (Orders todelete in oooddd)
            {
                await App.Database.Delete_Order_Async(todelete);
            }
        }


        #endregion

        #region 商品的資料表
        public Task<List<Merchandises>> Get_Merchandises_Async()
        {
            return _database.Table<Merchandises>().ToListAsync();
        }

        public Task<Merchandises> Get_Merchandise_Async(int id)
        {
            return _database.Table<Merchandises>()
                            .Where(i => i.merchandise_id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> Save_Merchandise_Async(Merchandises sos)
        {
            if (sos.merchandise_id != 0)
            {
                return _database.UpdateAsync(sos);
            }
            else
            {
                //是0的就跑這
                Insert_auto_Merchandise(sos);
                sos.merchandise_id = -99;
                return _database.UpdateAsync(sos);
            }
        }

        async void Insert_auto_Merchandise(Merchandises sos)
        {   //自動 +1 的部分
            Merchandises sssss = await _database.Table<Merchandises>().OrderByDescending(x => x.merchandise_id).FirstOrDefaultAsync();

            if (sssss == null)
                sos.merchandise_id = 1;
            else
                sos.merchandise_id = sssss.merchandise_id + 1;
            await _database.InsertAsync(sos);
        }

        public Task<int> Insert_Merchandise_Async(Merchandises sos)
        {
            //專們給匯入用的
            return _database.InsertAsync(sos);
        }

        public Task<int> Delete_Merchandise_Async(Merchandises note)
        {
            return _database.DeleteAsync(note);
        }

        #endregion

        #region 訂單的資料表
        public Task<List<Orders>> Get_Orders_Async()
        {
            return _database.Table<Orders>().ToListAsync();
        }

        public Task<Orders> Get_Order_Async(int id)
        {
            return _database.Table<Orders>()
                            .Where(i => i.order_id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> Save_Order_Async(Orders sos)
        {
            if (sos.order_id != 0)
            {
                return _database.UpdateAsync(sos);
            }
            else
            {
                //是0的就跑這
                Insert_auto_Order(sos);
                sos.order_id = -99;
                return _database.UpdateAsync(sos);
            }
        }

        async void Insert_auto_Order(Orders sos)
        {   //自動 +1 的部分
            Orders sssss = await _database.Table<Orders>().OrderByDescending(x => x.order_id).FirstOrDefaultAsync();

            if (sssss == null)
                sos.order_id = 1;
            else
                sos.order_id = sssss.order_id + 1;
            await _database.InsertAsync(sos);
        }

        public Task<int> Insert_Order_Async(Orders sos)
        {
            //專們給匯入用的
            return _database.InsertAsync(sos);
        }

        public Task<int> Delete_Order_Async(Orders note)
        {
            //讀取關聯資料一起刪除
            delete_Order_link(note.order_id);

            return _database.DeleteAsync(note);
        }

        async void delete_Order_link(int the_order_id)
        {
            List<Order_detail> oooddd = (await App.Database.Get_Order_details_Async()).FindAll(x => x.order_id == the_order_id);
            foreach (Order_detail todelete in oooddd)
            {
                await App.Database.Delete_Order_detail_Async(todelete);
            }
        }

        #endregion

        #region 訂單的詳細資料表
        public Task<List<Order_detail>> Get_Order_details_Async()
        {
            return _database.Table<Order_detail>().ToListAsync();
        }

        public Task<Order_detail> Get_Order_detail_Async(int id)
        {
            return _database.Table<Order_detail>()
                            .Where(i => i.order_detail_id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> Save_Order_detail_Async(Order_detail sos)
        {
            if (sos.order_detail_id != 0)
            {
                return _database.UpdateAsync(sos);
            }
            else
            {
                //是0的就跑這
                Insert_auto_Order_detail(sos);
                sos.order_detail_id = -99;
                return _database.UpdateAsync(sos);
            }
        }

        async void Insert_auto_Order_detail(Order_detail sos)
        {   //自動 +1 的部分
            Order_detail sssss = await _database.Table<Order_detail>().OrderByDescending(x => x.order_detail_id).FirstOrDefaultAsync();

            if (sssss == null)
                sos.order_detail_id = 1;
            else
                sos.order_detail_id = sssss.order_detail_id + 1;
            await _database.InsertAsync(sos);
        }

        public Task<int> Insert_Order_detail_Async(Order_detail sos)
        {
            //專們給匯入用的
            return _database.InsertAsync(sos);
        }

        public Task<int> Delete_Order_detail_Async(Order_detail note)
        {
            return _database.DeleteAsync(note);
        }

        #endregion


    }
}
