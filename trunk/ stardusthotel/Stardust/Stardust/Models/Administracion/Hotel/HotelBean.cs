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
        [Required]
        public string nombre { get; set; }

        [Display( Name = "Razón Social" ) ]
        [Required]
        public string razonSocial { get; set; }

        [Display( Name = "Dirección" ) ]
        [Required]
        public string direccion { get; set; }

        [Display( Name = "Teléfono 1" ) ]
        [Required]
        [MinLength( 9 )]
        [MaxLength( 9 )]
        public string tlf1 { get; set; }

        [Display( Name = "Teléfono 2" ) ]
        [Required]
        [MinLength(9)]
        [MaxLength(9)]
        public string tlf2 { get; set; }

        [Display( Name = "E-mail" ) ]
        [Required]
        public string email { get; set; }

        [Display( Name = "Número de pisos" ) ]
        [Required]
        [Range( 0 , 20 )]
        public int nroPisos { get; set; }

        [Display( Name = "Departamento" ) ]
        public int idDepartamento { get; set; }

        [Display(Name = "Departamento")]
        public string nombreDepartamento { get; set; }
        
        [Display( Name = "Provincia" ) ]
        public int idProvincia { get; set; }

        [Display(Name = "Provincia")]
        public string nombreProvincia { get; set; }

        [Display( Name = "Distrito") ]
        public int idDistrito { get; set; }

        [Display(Name = "Distrito")]
        public string nombreDistrito { get; set; }
    }
}