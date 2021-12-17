using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using PM2E3JOSE0072.Models;

namespace PM2E3JOSE0072.DataBase
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection dataBase;
        private static DataBase instance { get; set; }
        private DataBase(string _dbPath)
        {
            dataBase = new SQLiteAsyncConnection(_dbPath);
            dataBase.CreateTableAsync<Pagos>().Wait();
        }

        public static DataBase CurrentDB
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dbmvvm.db3"));
                }
                return instance;
            }
        }

        public Task<List<Pagos>> GetAllPagos()
        {
            return dataBase.Table<Pagos>().ToListAsync();
        }
        public Task<int> GetPagosCount()
        {
            return dataBase.Table<Pagos>().CountAsync();
        }

        public Task<Pagos> GetPagosById(int id)
        {
            return dataBase.Table<Pagos>().Where(i => i.IdPagos == id).FirstOrDefaultAsync();
        }
        public Task<int> SavePagos(Pagos pago)
        {
            if (pago.IdPagos != 0)
            {
                return dataBase.UpdateAsync(pago);
            }
            else
            {
                return dataBase.InsertAsync(pago);
            }
        }

        public Task<int> DeletePagos(Pagos pago)
        {
            return dataBase.DeleteAsync(pago);
        }
    }
}
