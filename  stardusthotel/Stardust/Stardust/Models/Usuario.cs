using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Display( Name = "User" )]
        [Required]
        public string user_account{ get; set; }

        [Display(Name = "Password")]
        [Required]
        public string pass { get; set; }

        [Display(Name = "Nombre")]
        public string nombres { get; set; }

        [Display(Name = "Ap. Paterno")]
        public string apPat { get; set; }

        [Display(Name = "Ap. Materno")]
        public string apMat { get; set; }

        [Display(Name = "DNI")]
        public string dni { get; set; }

        [Display(Name = "Pasaporte")]
        public string pasaporte { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "RUC")]
        public string ruc { get; set; }

        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Display(Name = "Celular")]
        public string celular { get; set; }

        [Display(Name = "Razón Social")]
        public string razonSocial { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }
    }
}