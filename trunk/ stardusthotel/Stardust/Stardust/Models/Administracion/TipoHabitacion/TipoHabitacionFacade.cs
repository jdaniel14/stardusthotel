using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class TipoHabitacionFacade
    {
        TipoHabitacionService tipoServ = new TipoHabitacionService();
        
        public TipoHabitacionBean getTipo(int id) {
            return tipoServ.getTipo(id);
        }

        public IEnumerable<TipoHabitacionBean> getTipoHabitacionXHotel(int idHotel)
        {
            return tipoServ.getTipoHabitacionXHotel(idHotel);
        }

        public void registrarTipoHabitacion(TipoHabitacionBean tipo) {
            tipoServ.registrarTipoHabitacion(tipo);
        }

        public void actualizarTipoHabitacion(TipoHabitacionBean tipo) {
            tipoServ.actualizarTipoHabitacion(tipo);
        }

        public void eliminarTipoHabitacion( int id ) {
            tipoServ.eliminarTipoHabitacion( id );
        }

        public List<TipoHabitacionBean> listarTipoHabitacion() {
            return tipoServ.listarTipoHabitacion();
        }
    }
}