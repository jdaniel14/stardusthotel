using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class TipoHabitacionService
    {
        TipoHabitacionDAO tipoDAO = new TipoHabitacionDAO();

        public TipoHabitacionBean getTipo(int id) {
            return tipoDAO.getTipo(id);
        }

        public void registrarTipoHabitacion(TipoHabitacionBean tipo) {
            tipoDAO.registrarTipoHabitacion(tipo);
        }

        public void actualizarTipoHabitacion(TipoHabitacionBean tipo) {
            tipoDAO.actualizarTipoHabitacion(tipo);
        }

        public void eliminarTipoHabitacion( int id ) {
            tipoDAO.eliminarTipoHabitacion( id );
        }

        public List<TipoHabitacionBean> listarTipoHabitacion() {
            return tipoDAO.listarTipoHabitacion();
        }
    }
}