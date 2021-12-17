using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using PM2E3P201820110061.Models;
using SQLite;
namespace PM2E3P201820110061.Controller
{
    public class DataBaseSQLite
    {
        readonly SQLiteAsyncConnection db;

        public DataBaseSQLite(String pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Pagos>().Wait();
        }

        public Task<int> GuardarPago(Pagos pago)
        {
            if (pago.Id_pago != 0)
            {
                return db.UpdateAsync(pago);
            }
            else
            {
                return db.InsertAsync(pago);
            }
        }

        public Task<int> EliminarPago(Pagos pago)
        {
            return db.DeleteAsync(pago);
        }

        public async Task<List<Pagos>> ObtenerListaPago()
        {
            return await Task.FromResult(await db.Table<Pagos>().OrderByDescending(descPago => descPago.Id_pago).ToListAsync());
        }
    }
}
