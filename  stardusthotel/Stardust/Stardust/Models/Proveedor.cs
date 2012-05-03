using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class Proveedor
    {
        public int ID { get; set; }
        public string Razon_Social { get; set; }
        public string RUC { get; set; }
        public string Categoria { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Pagina_Web { get; set; }
        public string Contacto { get; set; }
        public string Cargo { get; set; }
        public string Correo { get; set; }
        public string Observaciones { get; set; }
    }
}