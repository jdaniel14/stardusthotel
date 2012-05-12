using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string id_categoria { get; set; }
        public string descripcion { get; set; }
        public string stock_minimo { get; set; }
        public string stock_maximo { get; set; }
       
    }
}