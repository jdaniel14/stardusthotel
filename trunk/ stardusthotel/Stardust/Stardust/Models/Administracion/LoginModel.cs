using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class LoginModel
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Es necesario que ingrese el Usuario y su Contrasenia")]
        public string Usuario { get; set; }

        [Display(Name = "Contrasenia")]
        [Required(ErrorMessage = "Es necesario que ingrese el Usuario y su Contrasenia")]
        public string Contrasenia { get; set; }

        //[Display(Name = "Recordarme")]
        //public bool RecordarUsuario { get; set; }
    }
}