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
    }
}