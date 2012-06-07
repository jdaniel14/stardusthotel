using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Stardust.Models
{
    public class HotelBean
    {
        [Key]
        public int ID { get; set; }

        [Display( Name = "Nombre" ) ]
        [Required (ErrorMessage="Debe ingresar el nombre del Hotel")]
        //[RegularExpression("^([a-zA-Z-9ÑñáéíóúÁÉÍÓÚ]*\\w*\\d*\\s*)*$", ErrorMessage="El nombre ingresado no es válido")]
        public string nombre { get; set; }

        [Display( Name = "Razón Social" ) ]
        //[RegularExpression("^([a-zA-Z-9ÑñáéíóúÁÉÍÓÚ]*\\w*\\d*\\s*)*$", ErrorMessage = "La razón social no es válida")]
        public string razonSocial { get; set; }

        [Display( Name = "Dirección" ) ]
        [Required(ErrorMessage = "Debe ingresar la dirección del Hotel")]
        //[RegularExpression("^([a-zA-Z-9ÑñáéíóúÁÉÍÓÚ]*\\w*\\d*\\s*)*$", ErrorMessage = "La dirección no es válida")]
        public string direccion { get; set; }

        [Display( Name = "Teléfono Principal" ) ]
        [Required(ErrorMessage="Debe ingresar el teléfono")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Debe ingresar 9 dígitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe tener la sintaxis de un telefóno")]
        public string tlf1 { get; set; }

        [Display( Name = "Teléfono Secundario" ) ]
        [Remote("ValidaFonoNoRequerido","Hotel")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public string tlf2 { get; set; }

        [Display( Name = "E-mail") ]
        [Remote("ValidaEmail", "Hotel")]
        //[RegularExpression(@"([a-z0-9!#$%&'*+/=?^_`B|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|pe)\b){0,1}", ErrorMessage = "El email ingresado es incorrecto")]
        //Problema: Ver que la expresion regular pueda aceptar email vacio al no ser required
        public string email { get; set; }

        [Display( Name = "Número de pisos" ) ]
        [Required(ErrorMessage = "Debe indicar el número de pisos")]
        [Range( 0 , 999, ErrorMessage = "El número de pisos es incorrecto")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int nroPisos { get; set; }

        [Display( Name = "Departamento" ) ]
        [Required(ErrorMessage = "Debe seleccionar un departamento")]
        public int idDepartamento { get; set; }

        public string nombreDepartamento { get; set; }
        
        [Display( Name = "Provincia" )]
        [Required(ErrorMessage = "Debe seleccionar una provincia")]
        public int idProvincia { get; set; }

        public string nombreProvincia { get; set; }

        [Display( Name = "Distrito") ]
        [Required(ErrorMessage = "Debe seleccionar un distrito")]
        public int idDistrito { get; set; }

        public string nombreDistrito { get; set; }


        public List<Departamento> Departamentos { get; set; }
    }
}