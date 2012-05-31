using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class HotelService
    {
        HotelDAO hotelDAO = new HotelDAO();

        public HotelBean getHotel(int id){
            return hotelDAO.getHotel(id);
        }

        public List<HotelBean> getHoteles(){
            return hotelDAO.getHoteles();
        }

        public void registrarHotel(HotelBean hotel){
            hotelDAO.registrarHotel(hotel);
        }

        public void actualizarHotel(HotelBean hotel){
            hotelDAO.actualizarHotel(hotel);
        }

        public void eliminarHotel(int id) {
            hotelDAO.eliminarHotel(id);
        }

        public List<HotelBean> listarHoteles() {
            return hotelDAO.listarHoteles();
        }

        public void registrarTipoHabitacion(TipoHabitacionxHotel tipo) {
            hotelDAO.registrarTipoHabitacion(tipo);
        }

        public List<TipoHabitacionxHotel> listarTipos(int id) {
            return hotelDAO.listarTipos(id);
        }
    }
}