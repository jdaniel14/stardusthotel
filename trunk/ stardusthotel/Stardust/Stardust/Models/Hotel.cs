using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class Hotel
    {
        [Display(Name = "")]
        [Required(ErrorMessage = "Titulo es requerido")]
        [Key]
        public string idhotel { get; set; }

        [Display(Name = "Descripcion-Caracteristicas")]
        
        public string descripcion { get; set; }

        [Display(Name = " Razon Social")]
        [Required(ErrorMessage = "RS es requerido")]
        public string razonsocial { get; set; }

        [Display(Name = " Registro")]
        [Required(ErrorMessage = "registro requerido")]
        public string regid { get; set; }

        [Display(Name = " Direccion")]
        [Required(ErrorMessage = "direccion es requerido")]
        public string direccion { get; set; }

      

        [Display(Name = "Telefono 1")]
        [Required(ErrorMessage = "RS es requerido")]
        public string telf1 { get; set; }


        [Display(Name = "telefono 2")]        
        public string telf2 { get; set; }

        [Display(Name = " Fax")]
        [Required(ErrorMessage = "RS es requerido")]
        public string fax { get; set; }


    }
}