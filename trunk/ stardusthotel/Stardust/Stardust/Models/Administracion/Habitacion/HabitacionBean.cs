using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class HabitacionViewModelSearch
    {
        public string idTipoHabitacion { get; set; }
        public int piso { get; set; }
        public int nroCamas { get; set; }
        public bool aptoFumador { get; set; }

        public List<TipoHabitacionBean> TipoHabitaciones { get; set; }
    }

    public class HabitacionBean
    {
        [Key]
        public int ID { get; set; }

        [Display( Name = "Piso" ) ]
        [Range( 0 , 20, ErrorMessage="El piso máximo de una habitación es 20" )]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int piso { get; set; }

        [Display( Name = "Estado" )]
        public string estado { get; set; }

        [Display( Name = "Número de baños" ) ]
        [Range(0, 9, ErrorMessage = "El número máximo de Baños es 9")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int nroBanos { get; set; }

        [Display( Name = "Admite fumadores" ) ]
        public bool aptoFumador { get; set; }

        [Display( Name = "Número de camas" ) ]
        [Range(0, 9, ErrorMessage = "El Número máximo de camas es 9")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int nroCamas { get; set; }

        [Display( Name = "Hotel" ) ]
        [Required(ErrorMessage="Es necesario asignar un Hotel a la habitación")]
        public int idHotel { get; set; }

        [Display( Name = "Tipo de habitación" ) ]
        [Required(ErrorMessage="Es necesario asignar un tipo de habitación a la habitación")]
        public int idTipoHabitacion { get; set; }

        public string nombreHotel { get; set; }
        public string numero { get; set; }

        public string nombreTipoHabitacion { get; set; }
    }
}