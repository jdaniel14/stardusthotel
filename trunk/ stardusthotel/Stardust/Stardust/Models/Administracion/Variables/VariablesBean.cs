using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations ;

namespace Stardust.Models
{
    public class VariablesBean
    {
        [Range(0, 72)] //Como máximo se esperará 3 dias para la confirmación de la reserva
        [Required(ErrorMessage="El sistema necesita saber la cantidad de horas mínima para CONFIRMAR la reserva")]
        [Display( Name = "Horas para confirmar reserva" ) ]
        public int horasEsperaConfirmarReserva { get; set; }

        [Range(0, 100)]
        [Required(ErrorMessage = "El sistema necesita saber el porcentaje de ADELANTO para CONFIRMAR la reserva")]
        [Display( Name = "Porcentaje de adelanto" ) ] 
        public int porcAdelanto { get; set; }

        [Range(0,10)] //Una vez reservado tiene hasta 10 días para cancelar la reserva
        [Required(ErrorMessage = "El sistema necesita saber la cantidad de días de espera como máximo para CANCELAR la reserva")]
        [Display( Name = "Días máximos antes de retener" ) ]
        public int diasEsperarSinRetener { get; set; }

        [Range(0, 100)]
        [Required(ErrorMessage = "El sistema necesita saber el porcentaje a RETENER pasado los días de espera para poder CANCELAR la reserva")]
        [Display( Name = "Porcentaje a retener" ) ]
        public int porcRetencion { get; set; }
    }
}