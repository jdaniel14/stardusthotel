using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class TipoHabitacionxHotel
    {
        [Display( Name = "Tipo de Habitación" ) ]
        public int idTipoHabitacion { get; set; }

        public string nombreTipoHabitacion { get; set; }

        [Display( Name = "Hotel" ) ]
        public int idHotel { get; set; }

        [Display( Name = "Precio base" ) ]
        [Range( 0 , 10000 )]
        public decimal precioBase { get; set; }

    }
}