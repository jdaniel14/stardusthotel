using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations ;

namespace Stardust.Models
{
    public class VariablesBean
    {
        [Display(Name = "Horas para confirmar reserva")]
        [Range(0, 72, ErrorMessage="La cantidad de horas mínima debe estar entre 0 y 72")] //Como máximo se esperará 3 dias para la confirmación de la reserva
        [Required(ErrorMessage="El sistema necesita saber la cantidad de horas mínima para CONFIRMAR la reserva")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int horasEsperaConfirmarReserva { get; set; }

        
        [Display( Name = "Porcentaje de adelanto" ) ]
        [Range(0, 100, ErrorMessage="El valor debe estar entre 0 y 100")]
        [Required(ErrorMessage = "El sistema necesita saber el porcentaje de ADELANTO para CONFIRMAR la reserva")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int porcAdelanto { get; set; }
        
        
        [Display(Name = "Días máximos antes de retener")]
        [Range(0,10, ErrorMessage="Los días de espera, en el peor de los casos serán 10")] //Una vez reservado tiene hasta 10 días para cancelar la reserva
        [Required(ErrorMessage = "El sistema necesita saber la cantidad de días de espera como máximo para CANCELAR la reserva")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int diasEsperarSinRetener { get; set; }

        [Display(Name = "Porcentaje a retener")]
        [Range(0, 100, ErrorMessage = "El valor debe estar entre 0 y 100")]
        [Required(ErrorMessage = "El sistema necesita saber el porcentaje a RETENER pasado los días de espera para poder CANCELAR la reserva")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int porcRetencion { get; set; }
    }
}