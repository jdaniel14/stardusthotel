using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class TipoHabitacionXHotelXTemporada
    {
        public int idHotel { get; set; }
        public int idTipoHabitacion { get; set; }
        public int idTemporada { get; set; }
        public int porcDescuento { get; set; }
    }

    public class TemporadaBean
    {
        public int idTemporada { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fechaFin { get; set; }
    }

    public class TipoHabitacionXHotelXTemporadaViewModelList
    {
        public int idTemporada { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fechaIni { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fechaFin { get; set; }

        //[DisplayFormat(DataFormatString = "{0:.00%}")]
        public int porcDescuento { get; set; }
    }

    public class TipoHabitacionXHotelXTemporadaViewModelCreate
    {
        [Display(Name = "Hotel")]
        [Required(ErrorMessage = "Seleccione un hotel")]
        public int idHotel { get; set; }

        [Display(Name = "Tipo de Habitación")]
        [Required(ErrorMessage = "Seleccione un tipo de habitación")]
        public int idTipoHabitacion { get; set; }

        [Display(Name = "Temporada")]
        [Required(ErrorMessage = "Seleccione una temporada")]
        public int idTemporada { get; set; }

        [Display(Name = "Porcentaje de la temporada (%)")]
        [Required(ErrorMessage = "Ingrese un porcentaje")]
        public int? porcDescuento { get; set; }

        public List<HotelBean> Hoteles { get; set; }
        public List<TipoHabitacion> TipoHabitaciones { get; set; }
        public List<TemporadaBean> Temporadas { get; set; }
    }

    public class TipoHabitacionXHotelXTemporadaViewModelEdit
    {
        [Display(Name = "Hotel")]
        [Required(ErrorMessage = "Seleccione un hotel")]
        public int idHotel { get; set; }

        [Display(Name = "Tipo de Habitación")]
        [Required(ErrorMessage = "Seleccione un tipo de habitación")]
        public int idTipoHabitacion { get; set; }

        [Display(Name = "Temporada")]
        [Required(ErrorMessage = "Seleccione una temporada")]
        public int idTemporada { get; set; }

        [Display(Name = "Porcentaje de la temporada (%)")]
        [Required(ErrorMessage = "Ingrese un porcentaje")]
        public int porcDescuento { get; set; }

        public List<HotelBean> Hoteles { get; set; }
        public List<TipoHabitacion> TipoHabitaciones { get; set; }
        public List<TemporadaBean> Temporadas { get; set; }
    }
}