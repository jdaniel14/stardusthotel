using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations ;

namespace Stardust.Models
{
    public class VariablesBean
    {
        [Display( Name = "Horas para confirmar reserva" ) ]
        public int horasEsperaConfirmarReserva { get; set; }

        [Display( Name = "Porcentaje de adelanto" ) ] 
        public int porcAdelanto { get; set; }

        [Display( Name = "Días máximos antes de retener" ) ]
        public int diasEsperarSinRetener { get; set; }

        [Display( Name = "Porcentaje a retener" ) ]
        public int porcRetencion { get; set; }
    }
}