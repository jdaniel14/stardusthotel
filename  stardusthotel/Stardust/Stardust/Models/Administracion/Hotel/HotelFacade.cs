using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class HotelFacade
    {
        HotelService hotelServ = new HotelService();
        
        public HotelBean getHotel(int id) {
            return hotelServ.getHotel( id );
        }

        public List<HotelBean> getHoteles()
        {
            return hotelServ.getHoteles();
        }

        public void registrarHotel(HotelBean hotel) {
            hotelServ.registrarHotel(hotel);
        }

        public void actualizarHotel(HotelBean hotel) {
            hotelServ.actualizarHotel(hotel);
        }

        public void eliminarHotel(int id) {
            hotelServ.eliminarHotel(id);
        }

        public List<HotelBean> listarHoteles() {
            return hotelServ.listarHoteles();
        }

        public void registrarTipoHabitacion(TipoHabitacionxHotel tipo) {
            hotelServ.registrarTipoHabitacion(tipo);
        }

        public List<TipoHabitacionxHotel> listarTipos(int id) {
            return hotelServ.listarTipos(id);
        }
    }
}