using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class Proveedor
    {
        public int ID { get; set; }
        [Display(Name = "Razon Social")]
        public string Razon_Social { get; set; }
        [Display(Name = "RUC")]
        public string RUC { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Pagina Web")]
        public string Pagina_Web { get; set; }
        [Display(Name = "Contacto")]
        public string Contacto { get; set; }
        [Display(Name = "Cargo del Persona")]
        public string Cargo { get; set; }
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }
        [Display(Name = "Observacion")]
        public string Observaciones { get; set; }
        public int estado { get; set; }
    }
}