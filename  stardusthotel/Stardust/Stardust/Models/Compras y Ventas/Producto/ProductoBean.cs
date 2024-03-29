﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class ProductoBean
    {
        public int ID { get; set; }
        
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar nombre de Producto")]
        [RegularExpression("^[a-zA-Z áéíóúAÉÍÓÚÑñ]+$", ErrorMessage = "El nombre ingresado no es válido")]
        public string nombre { get; set; }
        
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        
        public int estado { get; set; }

        public string conexion { get; set; }
    }
}