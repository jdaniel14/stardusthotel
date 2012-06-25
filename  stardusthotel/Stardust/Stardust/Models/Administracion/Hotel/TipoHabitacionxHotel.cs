using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class TipoHabitacion
    {
        [Key]
        public int idTipoHabitacion { get; set; }
        
        [Display(Name = "Tipo de Habitación")]
        [Required(ErrorMessage = "Ingrese un nombre para el tipo de habitación")]
        public string nombre { get; set; }

        [Display( Name = "Descripcion" ) ]
        [Required(ErrorMessage = "Ingrese una descripción para el tipo de habitación")]
        public string descripcion { get; set; }

    }

    public class TipoHabitacionXHotel
    {
        public int idHotel { get; set; }
        public int idTipoHabitacion { get; set; }
        public decimal precio { get; set; }
        public int nroPersonas { get; set; }
    }

    public class TipoHabitacionXHotelViewModelCreate
    {
        [Display(Name="Hotel")]
        [Required(ErrorMessage="Seleccione un Hotel")]
        public int idHotel { get; set; }

        [Key]
        [Display(Name = "Tipo de Habitacion")]
        [Required(ErrorMessage="Seleccion un Tipo de Habitacion")]
        public int idTipoHabitacion { get; set; }
        
        [Display(Name = "Precio S/.")]
        [Required(ErrorMessage="Ingrese el precio")]
        public decimal? precio { get; set; }

        [Display(Name = "Número de Personas")]
        [Required(ErrorMessage = "Ingrese el número de Personas")]
        public int? nroPersonas { get; set; }

        public List<HotelBean> Hoteles { get; set; }
        public List<TipoHabitacion> TipoHabitaciones { get; set; }

    }

    public class TipoHabitacionXHotelViewModelEdit
    {
        [Display(Name = "Hotel")]
        [Required(ErrorMessage = "Seleccione un Hotel")]
        public int idHotel { get; set; }

        [Key]
        [Display(Name = "Tipo de Habitacion")]
        [Required(ErrorMessage = "Seleccion un Tipo de Habitacion")]
        public int idTipoHabitacion { get; set; }

        [Display(Name = "Precio S/.")]
        [Required(ErrorMessage = "Ingrese el precio")]
        public decimal precio { get; set; }

        [Display(Name = "Número de Personas")]
        [Required(ErrorMessage = "Ingrese el número de Personas")]
        public int nroPersonas { get; set; }

        public List<HotelBean> Hoteles { get; set; }
        public List<TipoHabitacion> TipoHabitaciones { get; set; }

    }

    public class TipoHabitacionXHotelViewModelDelete
    {
        [Display(Name = "Hotel")]
        public string nombreHotel { get; set; }

        [Display(Name = "Tipo de Habitacion")]
        public string nombreTipoHabitacion { get; set; }

        public int idHotel { get; set; }
        
        public int idTipoHabitacion { get; set; }

        [Display(Name = "Precio S/.")]
        public decimal precio { get; set; }

        [Display(Name = "Número de Personas")]
        public int nroPersonas { get; set; }

        [Display(Name= "Temporadas asignadas")]
        public int nroTemporadasAsignadas { get; set; }
    }

    public class TipoHabitacionXHotelViewModelList
    {
        
        [Key]
        public int idTipoHabitacion { get; set; } //facilita la busqueda del precio, por eso lo jalo
        public string nombre { get; set; }
        public string descripcion { get; set; }

        //public string nombreHotel { get; set; }
        public decimal precio { get; set; }
        public int nroPersonas { get; set; }
    }

}