using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Prueba.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        
        public string Nombre { get; set; }

        public string Apellido { get; set; }
    }
}