using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class ProveedorBean
    {

        public int ID { get; set; }

        [Display(Name = "Razon Social")]
        public string razonSocial { get; set; }

        [Display(Name = "RUC")]
        public string ruc { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Display(Name = "Pagina Web")]
        public string web { get; set; }

        [Display(Name = "Contacto")]
        public string contacto { get; set; }

        [Display(Name = "Cargo del Contacto")]
        public string cargoContacto { get; set; }

        [Display(Name = "Correo electrónico")]
        public string emailContacto { get; set; }

        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }

        public int estado { get; set; }
    }
}