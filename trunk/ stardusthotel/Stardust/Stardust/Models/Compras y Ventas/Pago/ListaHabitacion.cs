using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ListaHabitacionEstado
    {
        public int idReserva { get; set; }
        public string estado { get; set; }
    }
    public class ListaHabitacion
    {
        public int idHabit { get; set; }
        public string nHabit { get; set; }
        public List<ListaHabitacionEstado> listaFechas { get; set; }
        public ListaHabitacion()
        {
            listaFechas = new List<ListaHabitacionEstado>();
        }
    }
}