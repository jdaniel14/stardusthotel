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

        public List<HotelBean> getHotelesActivos()
        {
            return hotelServ.getHotelesActivos();
        }

        public void registrarHotel(HotelBean hotel) {
            hotelServ.registrarHotel(hotel);
        }

        public void actualizarHotel(HotelBean hotel) {
            hotelServ.actualizarHotel(hotel);
        }

        public void desactivarHotel(int id)
        {
            hotelServ.desactivarHotel(id);
        }

        public void registrarTipoHabitacion(TipoHabitacionXHotel tipo) {
            hotelServ.registrarTipoHabitacion(tipo);
        }

        public List<TipoHabitacion> getTipoHabitacionXHotel(int id)
        {
            return hotelServ.getTipoHabitacionXHotel(id);
        }

        public List<TipoHabitacion> getTipoHabitaciones()
        {
            return hotelServ.getTipoHabitaciones();
        }

        public decimal getPrecioTipoHabitacionXHotel(int idHotel, int idTipoHabitacion)
        {
            return hotelServ.getPrecioTipoHabitacionXHotel(idHotel, idTipoHabitacion);
        }

        //Parte para dar información antes de desactivar un Hotel
        //--------------------------------------------------------
        public int getNHabitacionesXHotel(int idHotel)
        {
            return hotelServ.getNHabitacionesXHotel(idHotel);
        }

        public int getNTipoHabitacionesXHotel(int idHotel)
        {
            return hotelServ.getNTipoHabitacionesXHotel(idHotel);
        }

        public int getNAmbientesXHotel(int idHotel)
        {
            return hotelServ.getNAmbientesXHotel(idHotel);
        }

        public int getNServiciosXHotel(int idHotel)
        {
            return hotelServ.getNServiciosXHotel(idHotel);
        }

        public int getNPromocionesXHotel(int idHotel)
        {
            return hotelServ.getNPromocionesXHotel(idHotel);
        }

        public int getNAlmacenesXHotel(int idHotel)
        {
            return hotelServ.getNAlmacenesXHotel(idHotel);
        }
        //--------------------------------------------------------

        public bool existeTipoHabitacion_Hotel(TipoHabitacionXHotel tipoHabitacion)
        {
            return hotelServ.existeTipoHabitacion_Hotel(tipoHabitacion);
        }
    }
}