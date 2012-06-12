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
        public string nombreEmpleado { get; set; } // <------- SE DEBE JALAR DEL USUARIO
        
        [Display( Name = "Fecha de ingreso" ) ]
        public DateTime fechaIngreso { get; set; }

        [Display( Name = "Fecha de salida" ) ]
        public DateTime fechaSalida { get; set; }

        [Display( Name = "Estado" ) ]
        public string estado { get; set; }
    }

    public class horario
    {
            [Display(Name = " codigo de horario empleado ")]
            public int ID { get; set; }

            [Display(Name = "Nombre Empleado")] // 
            public string nombreEmpleado { get; set; }
           
            [Display(Name = "Fecha inicio de Horario ")]
            public DateTime fechainiciohorario { get; set; }

            [Display(Name = "Fecha fin de Horario ")]
            public DateTime fechafinhorario { get; set; }

            [Display(Name = "Codigo del Empleado")]
            public int idempleado { get; set; }

        }

    public class horariodetalle {

        [Display(Name = "Codigo del Detalle del Empleado ")]
        public int horariodetalle;

        [Display(Name = "Nombre Empleado")] // 
        public string nombreEmpleado { get; set; }

        [Display(Name = " Dias de la semana")]      
        public string diasemana;

        [Display(Name = "Horario de Entrada ")]
        public string horasentrada;

         [Display(Name = "Horario de Salida ")]
        public string horasSalida;

         [Display(Name = "Horario ")]
        public int horario;

    
    
    }

    public class horarioasistencia
    {
          [Display(Name = "Codigo de Asistencia")]
        public int horarioassitencia;

          [Display(Name = "Hora de Marca")]
        public string horamarcada;

          [Display(Name = "Tipo de Estado")]
        public string tipoE;

          [Display(Name = "Codigo del empleado")]
        public string estado;

          [Display(Name = "Codigo del Detalle del Empleado")]
        public int idhorariodetalle;



    }
}