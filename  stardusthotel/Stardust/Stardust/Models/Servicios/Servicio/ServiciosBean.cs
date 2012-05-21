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
        public String nombre { get; set; }

        [Display(Name = "Descripcion")]
        public String descripcion { get; set; }

        [Display(Name = "Estado")]
        public String estado { get; set; }
    }
}