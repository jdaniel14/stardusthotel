using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class HabitacionBean
    {
        public int ID { get; set; }

        [Display( Name = "Piso" ) ]
        public int piso { get; set; }

        [Display( Name = "Estado" ) ]
        public string estado { get; set; }

        [Display( Name = "Número de baños" ) ]
        public int nroBanos { get; set; }

        [Display( Name = "Admite fumadores" ) ]
        public bool aptoFumador { get; set; }

        [Display( Name = "Número de camas" ) ]
        public int nroCamas { get; set; }

        [Display( Name = "Hotel" ) ]
        public string hotel { get; set; }

        [Display( Name = "Tipo de habitación" ) ]
        public string tipoHabitacion { get; set; }
    }
}