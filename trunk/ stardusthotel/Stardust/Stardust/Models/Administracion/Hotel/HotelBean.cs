using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class HotelBean
    {
        public int ID { get; set; }

        [Display( Name = "Nombre" ) ]
        public string nombre { get; set; }

        [Display( Name = "Razón Social" ) ]
        public string razonSocial { get; set; }

        [Display( Name = "Dirección" ) ]
        public string direccion { get; set; }

        [Display( Name = "Teléfono 1" ) ]
        public string tlf1 { get; set; }

        [Display( Name = "Teléfono 2" ) ]
        public string tlf2 { get; set; }

        [Display( Name = "E-mail" ) ]
        public string email { get; set; }

        [Display( Name = "Número de pisos" ) ]
        public int nroPisos { get; set; }

        [Display( Name = "Departamento" ) ]
        public string departamento { get; set; }
        
        [Display( Name = "Provincia" ) ]
        public string provincia { get; set; }

        [Display( Name = "Distrito") ]
        public string distrito { get; set; }
    }
}