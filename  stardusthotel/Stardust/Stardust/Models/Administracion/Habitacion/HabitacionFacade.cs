using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class HabitacionFacade
    {
        HabitacionService habitacionServ = new HabitacionService();
        
        public HabitacionBean getHabitacion(int id) {
            return habitacionServ.getHabitacion(id);
        }

        public void registrarHabitacion(HabitacionBean habitacion) {
            habitacionServ.registrarHabitacion(habitacion);
        }

        public void actualizarHabitacion(HabitacionBean habitacion) {
            habitacionServ.actualizarHabitacion(habitacion);
        }

        public void eliminarHabitacion( int id ) {
            habitacionServ.eliminarHabitacion(id);
        }

        public List<HabitacionBean> listarHabitaciones() {
            return habitacionServ.listarHabitaciones();
        }

        public List<HabitacionBean> buscarHabitacion(int idTipoHabitacion , int nroCamas , int piso)
        {
            if (idTipoHabitacion == nroCamas && nroCamas == piso && piso == 0) return new List<HabitacionBean>();
            return habitacionServ.buscarHabitacion(idTipoHabitacion, nroCamas , piso);
        }

        public bool existeNumeroHabitacionYA(int idHotel, string numeroHabitacion)
        {
            return habitacionServ.existeNumeroHabitacionYA(idHotel, numeroHabitacion);
        }
    }
}