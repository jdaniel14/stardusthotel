using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class HabitacionService
    {
        HabitacionDAO habitacionDAO = new HabitacionDAO();

        public HabitacionBean getHabitacion(int id) {
            return habitacionDAO.getHabitacion(id);
        }

        public void registrarHabitacion( HabitacionBean habitacion) {
            habitacionDAO.registrarHabitacion(habitacion);
        }

        public void actualizarHabitacion(HabitacionBean habitacion) {
            habitacionDAO.actualizarHabitacion(habitacion);
        }

        public void eliminarHabitacion(int id) {
            habitacionDAO.eliminarHabitacion(id);
        }

        public List<HabitacionBean> listarHabitaciones() {
            return habitacionDAO.listarHabitaciones();
        }

        public List<HabitacionBean> buscarHabitacion(int idTipoHabitacion, int nroCamas , int piso)
        {
            return habitacionDAO.buscarHabitacion(idTipoHabitacion, nroCamas,piso);
        }

        public bool existeNumeroHabitacionYA(int idHotel, string numeroHabitacion)
        {
            return habitacionDAO.existeNumeroHabitacionYA(idHotel, numeroHabitacion);
        }
    }
}