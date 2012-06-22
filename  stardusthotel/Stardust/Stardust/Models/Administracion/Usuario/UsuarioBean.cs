using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Stardust.Models
{
    public class TipoDocumento {
        public string nombre { get; set; }
        public TipoDocumento(string s) {
            this.nombre = s;
        }
    }

    public class UsuarioBean
    {
        public int ID { get; set; }

        [Display(Name = "Perfil Usuario")]
        [Required(ErrorMessage = "Debe ingresar un perfil al usuario")]
        public int idPerfilUsuario { get; set; }

        [Display(Name = "Perfil Usuario")]
        public string nombrePerfilUsuario { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Debe ingresar un nombre para la cuenta de usuario")]
        [MaxLength( 50 , ErrorMessage = "El nombre de usuario no debe sobrepasar los 50 caracteres" ) ]
        public string user_account { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [MaxLength(50, ErrorMessage = "La contraseña no debe sobrepasar los 50 caracteres")]
        public string pass { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar su nombre")]
        [MaxLength(50, ErrorMessage = "El nombre no debe sobrepasar los 50 caracteres")]
        public string nombres { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "Debe ingresar su apellido paterno")]
        [MaxLength(50, ErrorMessage = "El apellido paterno no debe sobrepasar los 50 caracteres")]
        public string apPat { get; set; }

        [Display(Name = "Apellido Materno")]
        [Required(ErrorMessage = "Debe ingresar su apellido materno")]
        [MaxLength(50, ErrorMessage = "El apellido materno no debe sobrepasar los 50 caracteres")]
        public string apMat { get; set; }

        [Display(Name = "E-mail")]
        [Remote("ValidaEmail", "Validation" )]
        public string email { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Debe ingresar 7 dígitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe tener la sintaxis de un telefóno")]
        //[Remote("ValidaFonoNoRequerido", "Validation")]
        public string celular { get; set; }

        [Display(Name = "Tipo de documento")]
        [Required( ErrorMessage = "Debe ingresar un tipo de documento" ) ]
        public string tipoDocumento { get; set; }

        [Display(Name = "Nro. de Documento")]
        [Required( ErrorMessage = "Debe ingresar un número de documento" ) ]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Debe ingresar mínimo 8 dígitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe tener la sintaxis de un documento")]
        public string nroDocumento { get; set; }

        [Display(Name = "Razón Social")]
        [Required(ErrorMessage = "Debe ingresar una razón social")]
        [MaxLength(50, ErrorMessage = "La razón social no debe sobrepasar los 50 caracteres")]
        public string razonSocial { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "La dirección no debe sobrepasar los 100 caracteres")]
        [Required(ErrorMessage = "Debe ingresar dirección")]
        public string direccion { get; set; }

        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "Debe elegir un distrito")]
        public int idDistrito { get; set; }

        [Display(Name = "Distrito")]
        public string nombreDistrito { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Debe elegir una provincia")]
        public int idProvincia { get; set; }

        [Display(Name = "Provincia")]
        public string nombreProvincia { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Debe elegir un departamento")]
        public int idDepartamento { get; set; }

        [Display(Name = "Departamento")]
        public string nombreDepartamento { get; set; }
    }

}