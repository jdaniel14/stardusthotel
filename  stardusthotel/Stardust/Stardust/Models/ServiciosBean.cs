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
        public int id{get; set;}
        public String nombre { get; set; }
        public String descripcion { get; set; }
    }
}