using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class EmpleadoBean
    {
        [Display(Name = "Codigo del Empleado")]
        public int ID { get; set; }

        [Display( Name = "Nombre" ) ]
        public string nombreEmpleado { get; set; }
        
        [Display( Name = "Fecha de ingreso" ) ]
        public DateTime fechaIngreso { get; set; }

        [Display( Name = "Fecha de salida" ) ]
        public DateTime fechaSalida { get; set; }

        [Display( Name = "Estado" ) ]
        public string estado { get; set; }
    }

    public class Horario
    {
        [Display(Name = "Código de Horario Empleado ")]
        public int ID { get; set; }

        [Display(Name = "Nombre Empleado")]
        public string nombreEmpleado { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha Inicio de Horario ")]
        public DateTime fechaInicioHorario { get; set; }
            
        [Display(Name = "Fecha Fin de Horario ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fechaFinHorario { get; set; }

        [Display(Name = "Codigo del Empleado")]
        public int idEmpleado { get; set; }
    }

    public class horariodetalle {

        [Display(Name = "Codigo del Detalle del Empleado ")]
        public int idhorariodetalle;

        [Display(Name = "Nombre Empleado")]
        public string nombreEmpleado { get; set; }

        [Display(Name = " Dias de la semana")]      
        public string diasemana;

        //solo me importa la hora
        [Display(Name = "Horario de Entrada ")]
        public DateTime horasentrada;

         [Display(Name = "Horario de Salida ")]
        public DateTime horasSalida;

         [Display(Name = "Horario ")]
        public int horario;
    }

    public class horarioasistencia
    {
          [Display(Name = "Codigo de Asistencia")]
        public int horarioassitencia;

          [DataType(DataType.Date)] 

          [Display(Name = "Hora de Marca")]
        public string horamarcada;


        [DataType(DataType.Time)] 
          [Display(Name = "Tipo de Estado")]
        public string tipoE;

          [Display(Name = "Codigo del empleado")]
        public string estado;

          [Display(Name = "Codigo del Detalle del Empleado")]
        public int idhorariodetalle;



    }
}