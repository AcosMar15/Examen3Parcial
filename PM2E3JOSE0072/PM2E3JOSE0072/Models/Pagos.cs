using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2E3JOSE0072.Models
{
    public class Pagos
    {
        [PrimaryKey, AutoIncrement]
        public int IdPagos { get; set; }

        [MaxLength(100)]
        public string Descripcion { get; set; }

        public double Monto { get; set; }

        public double Fecha { get; set; }


    }
}
