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
        public string tipoHabitacion { get; set; }
        //public int idTipoHabitacion { get; set; }

        [Display( Name = "Hotel" ) ]
        public string hotel { get; set; }
        //public int idHotel { get; set; }

        [Display( Name = "Precio base" ) ]
        [Range( 0 , 10000 )]
        public decimal precioBase { get; set; }
    }
}