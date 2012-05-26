using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class Almacen
    {
        public int ID { get; set; }
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        [Display(Name = "capacidad")]
        public string capacidad { get; set; }
        [Display(Name="IdHotel")]
        public int IDhotel { get; set; }
    }
}