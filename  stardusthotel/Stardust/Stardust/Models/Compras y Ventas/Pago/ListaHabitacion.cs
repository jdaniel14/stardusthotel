using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ListaHabitacionEstado
    {
        public string fechaIni { get; set; }
        public string fechaFin { get; set; }
        public int idReserva { get; set; }
        public int estado { get; set; }
    }
    public class ListaHabitacion
    {
        public int idHabitacion { get; set; }
        public List<ListaHabitacionEstado> listaHab { get; set; }
    }
}