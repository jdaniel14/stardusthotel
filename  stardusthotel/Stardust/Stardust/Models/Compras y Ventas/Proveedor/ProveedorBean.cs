﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Stardust.Models
{
    public class Proveedors
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Proveed
    {
        public string id { get; set; }
        public List<Proveedors> list { get; set; }
    }

    public class ProveedorBean
    {

        public int ID { get; set; }


        
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar la Razon Social")]
        public string razonSocial { get; set; }

        [Display(Name = "RUC")]
        [Required(ErrorMessage = "Debe ingresar RUC")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Debe ingresar 11 dígitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado es incorrecto")]
        public string ruc { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar la direccion")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Debe ingresar el telefono")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Debe ingresar 9 dígitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe tener la sintaxis de un telefóno")]
        public string telefono { get; set; }

        [Display(Name = "Pagina Web")]
        //[RegularExpression("@/[\w-\.]{3,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/", ErrorMessage = "El valor ingresado debe tener la sintaxis de un telefóno")]
        public string web { get; set; }

        [Display(Name = "Contacto")]
        [Required(ErrorMessage = "Debe ingresar el contacto")]
        public string contacto { get; set; }

        [Display(Name = "Cargo del Contacto")]
        [Required(ErrorMessage = "Debe ingresar el cargo del contacto")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public string cargoContacto { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Debe ingresar el e-mail  del contacto")]
        [RegularExpression(@"([a-z0-9!#$%&'*+/=?^_`B|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|pe)\b){0,1}", ErrorMessage = "El email ingresado es incorrecto")]
        public string emailContacto { get; set; }

        [Display(Name = "Teléfono Contacto")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Debe ingresar 9 dígitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe tener la sintaxis de un telefóno")]
        public string telefonocontacto { get; set; }
        
        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }

        public int estado { get; set; }
        [Display(Name = "Estado")]
        public string estado2 { get; set; }
    }
}