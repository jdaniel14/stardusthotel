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

        public int buscarHotel(HotelBean hotel)
        {
            return hotelServ.buscarHotel(hotel);
        }

        public void actualizarHotel(HotelBean hotel) {
            hotelServ.actualizarHotel(hotel);
        }

        public void desactivarHotel(int id)
        {
            hotelServ.desactivarHotel(id);
        }

        public void registrarAlmacen(int idHotel, AlmacenBean almacen)
        {
            hotelServ.registrarAlmacen(idHotel, almacen);
        }

        #region DaianaXServicios
        ServiciosService servicioServ = new ServiciosService();

        public List<ServiciosBean> ListarServicio(String nombre)
        {
            return servicioServ.ListarServicios(nombre);
        }
        public void RegistrarserviciosxHotel(int idhotel, ServicioXHotelBean serv)
        {
            hotelServ.AsignarServiciosXHotel(idhotel, serv);
        }
        public ServicioXHotelBean obtenerlista(int id)
        {
            return hotelServ.obtenerlista(id);
        }
        public List<HotelBean> ListarHotel(String nombre)
        {
            return hotelServ.ListarHotel(nombre);
        }

        public ServiciosBean Getservicio(int idServicio)
        {
            return servicioServ.GetServicio(idServicio);
        }
        public void ModificarserviciosxHotel(int idhotel, ServicioXHotelBean serv)
        {
            hotelServ.ModificarserviciosxHotel(idhotel, serv);
        }
        #endregion

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

        public void registrarTipoHabitacion(TipoHabitacionXHotel tipo)
        {
            hotelServ.registrarTipoHabitacion(tipo);
        }

        public void actualizarTipoHabitacion(TipoHabitacionXHotel tipoHabitacionXhotel)
        {
            hotelServ.actualizarTipoHabitacion(tipoHabitacionXhotel);
        }

        public List<TipoHabitacionXHotelViewModelList> getTipoHabitacionXHotel(int id)
        {
            return hotelServ.getTipoHabitacionXHotel(id);
        }

        public TipoHabitacionXHotelViewModelEdit getTipoHabitacionXHotel(int idHotel, int idTipoHabitacion)
        {
            return hotelServ.getTipoHabitacionXHotel(idHotel, idTipoHabitacion);
        }

        public List<TipoHabitacion> getTipoHabitaciones()
        {
            return hotelServ.getTipoHabitaciones();
        }

        public decimal getPrecioTipoHabitacionXHotel(int idHotel, int idTipoHabitacion)
        {
            return hotelServ.getPrecioTipoHabitacionXHotel(idHotel, idTipoHabitacion);
        }

        public void eliminarTipoHabitacionXHotel(TipoHabitacionXHotel tipohabitacionXhotel)
        {
            hotelServ.eliminarTipoHabitacionXHotel(tipohabitacionXhotel);
        }

        public int getNroTemporadasAsignadas(int idHotel, int idTipoHabitacion)
        {
            return hotelServ.getNroTemporadasAsignadas(idHotel, idTipoHabitacion);
        }
    }
}