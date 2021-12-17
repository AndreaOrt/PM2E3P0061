using System;
using SQLite;

namespace PM2E3P201820110061.Models
{
    public class Pagos
    {
        public Pagos()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id_pago { get; set; }

        [MaxLength(200)]
        public String Descripcion { get; set; }

        public double Monto { get; set; }

        public double Fecha { get; set; }

        public Byte[] Photo_recibo { get; set; }
    }
}
