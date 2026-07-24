using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Api_Instrumentos.Models
{
    public class Instrumento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public string Provedor { get; set; }
        public double precio_compra { get; set; }
        public double precio_venta { get; set; }
        public int stock { get; set; }
        public int stock_minimo { get; set; }
        public string descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}