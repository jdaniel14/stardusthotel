using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    #region Empleado
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
    #endregion

    #region Horario
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
    #endregion

    #region Detalle Horario
    public class HorarioDetalle {

        [Display(Name = "Codigo del Detalle del Empleado ")]
        public int idHorarioDetalle;

        [Display(Name = "Nombre Empleado")]
        public string nombreEmpleado { get; set; }

        [Display(Name = "Dias de la semana")]      
        public string diaSemana;

        [Display(Name = "Hora de Entrada")]
        public DateTime horaEntrada;

         [Display(Name = "Hora de Salida")]
        public DateTime horaSalida;

         [Display(Name = "Horario ")]
        public int idHorario;
    }
    #endregion

    public class HorarioAsistencia
    {

    }
}