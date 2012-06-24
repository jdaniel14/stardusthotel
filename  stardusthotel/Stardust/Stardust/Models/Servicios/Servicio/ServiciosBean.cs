using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class ServiciosBean
    {
        [Display(Name = "Id")]
        public int id{get; set;}

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar nombre del Servicio")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public String nombre { get; set; }

        [Display(Name = "Descripcion")]
        public String descripcion { get; set; }

        [Display(Name = "Estado")]
        public String estado { get; set; }

        public int estado1 { get; set; }
        [Display(Name = "Estado")]
        public string estado2 { get; set; }

        public string conexion { get; set; }
    }
}