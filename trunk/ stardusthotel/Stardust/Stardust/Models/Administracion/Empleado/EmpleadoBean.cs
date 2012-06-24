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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        [Required( ErrorMessage = "Ingrese una fecha de ingreso" ) ]
        public DateTime fechaIngreso { get; set; }

        [Display( Name = "Fecha de salida" ) ]
        public DateTime fechaSalida { get; set; }

        [Display( Name = "Estado" ) ]
        public string estado { get; set; }

        public List<Horario> horarios { get; set; }
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

        public List<HorarioDetalle> horariodetalles { get; set; }

       
    }
    #endregion

    #region Detalle Horario
    public class HorarioDetalle {

        [Display(Name = "Codigo del Detalle del Empleado ")]

        public int horariodetalles {get; set;}

        
        public int idHorarioDetalle { get; set; }
        
        [Display(Name = "Nombre Empleado")]
        public string nombreEmpleado { get; set; }

        [Display(Name = "Dias de la semana")]      
        public string diaSemana { get; set; }

       
        [Display(Name = "Hora de Entrada")]
        public String horaEntrada { get; set; }

         [Display(Name = "Hora de Salida")]
        public String horaSalida { get; set; }

         [Display(Name = "Horario ")]
         public int idHorario { get; set; }

         public List<Asistencia> asistencias { get; set; }
    }
    

    public class DiaSemana
    {
        public string dia { get; set; }
        public DiaSemana(string s)
        {
            this.dia = s;
        }
    }

    public class Horarios {

       public List<Horario>horarios { get; set; }

         public Horarios(){
        horarios = new List<Horario>();
       }

   }

   

    #endregion

  #region Asistencia
    public class Asistencia
    {

        public int idasistencia {set; get;}

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime horaasistencia { set; get; }
        public char TipoES { set; get; }
        public  string estado { set; get; }
        public int idHorarioDetalle { set; get; }
        public string stringtipoES { set; get; }

    }



    public class TomarAsistencia
    {
        public int id { set; get; }

        [Display(Name = "Usser Acount")]
        public string usuario { set; get; }
        [Display(Name = "Password ")]
        public string pasword { set; get; }

    }

  #endregion

    #region Reporte

    public class ReporteEmpleado
    {

        public EmpleadoBean empleado { get; set; }
        public List<Horario> horarios { get; set; }


         public ReporteEmpleado(){
        horarios = new List<Horario>();
        empleado = new EmpleadoBean();
       }
    }
    #endregion

}