using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class PerfilUsuarioBean
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Nombre")]
        [Required( ErrorMessage = "Debe ingresar un nombre para el perfil de usuario" ) ]
        public string nombre { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        public string token { get; set; }
    }
}