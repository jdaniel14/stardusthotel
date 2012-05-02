using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba.Models
{
    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string categoria { get; set; }
        public string descripcion { get; set; }
        public int stock_minimo { get; set; }
        public int stock_maximo { get; set; }
    }
}