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
        public string nombre { get; set; }

        [Display( Name = "Descripcion" ) ]
        public string descripcion { get; set; }

    }

    public class TipoHabitacionXHotel
    {
        public int idHotel { get; set; }
        public int idTipoHabitacion { get; set; }
        public decimal precio { get; set; }
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
        
        [Display(Name = "Precio")]
        [Required(ErrorMessage="Ingrese el precio")]
        public decimal? precio { get; set; }

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

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Ingrese el precio")]
        public decimal precio { get; set; }

        public List<HotelBean> Hoteles { get; set; }
        public List<TipoHabitacion> TipoHabitaciones { get; set; }

    }

    public class TipoHabitacionXHotelViewModelList
    {
        
        [Key]
        public int idTipoHabitacion { get; set; } //facilita la busqueda del precio, por eso lo jalo
        public string nombre { get; set; }
        public string descripcion { get; set; }

        //public string nombreHotel { get; set; }
        public decimal precio { get; set; }
    }

}