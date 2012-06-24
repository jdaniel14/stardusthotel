﻿using System;
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

        public List<HotelBean> getHoteles()
        {
            return hotelDAO.getHoteles();
        }

        public ServicioXHotelBean obtenerlista(int idhotel)
        {
            return hotelDAO.obtenerlistaservicios(idhotel);
        }

        public List<HotelBean> getHotelesActivos()
        {
            return hotelDAO.getHotelesActivos();
        }
        public void AsignarServiciosXHotel(int idhotel, ServicioXHotelBean serv)
        {
            hotelDAO.InsertarHotelxServicio(idhotel, serv);
        }

        public void registrarHotel(HotelBean hotel){
            hotelDAO.registrarHotel(hotel);
        }

        public void actualizarHotel(HotelBean hotel){
            hotelDAO.actualizarHotel(hotel);
        }

        public void desactivarHotel(int id)
        {
            hotelDAO.desactivarHotel(id);
        }

        public List<HotelBean> ListarHotel(String Nombre)
        {
            return hotelDAO.ListarHotel(Nombre);
        }

        //Parte para dar información antes de desactivar un Hotel
        //--------------------------------------------------------
        public int getNHabitacionesXHotel(int idHotel)
        {
            return hotelDAO.getNHabitacionesXHotel(idHotel);
        }

        public int getNTipoHabitacionesXHotel(int idHotel)
        {
            return hotelDAO.getNTipoHabitacionesXHotel(idHotel);
        }

        public int getNAmbientesXHotel(int idHotel)
        {
            return hotelDAO.getNAmbientesXHotel(idHotel);
        }

        public int getNServiciosXHotel(int idHotel)
        {
            return hotelDAO.getNServiciosXHotel(idHotel);
        }

        public int getNPromocionesXHotel(int idHotel)
        {
            return hotelDAO.getNPromocionesXHotel(idHotel);
        }

        public int getNAlmacenesXHotel(int idHotel)
        {
            return hotelDAO.getNAlmacenesXHotel(idHotel);
        }
        //--------------------------------------------------------

        public bool existeTipoHabitacion_Hotel(TipoHabitacionXHotel tipoHabitacion)
        {
            return hotelDAO.existeTipoHabitacion_Hotel(tipoHabitacion);
        }

        public void registrarTipoHabitacion(TipoHabitacionXHotel tipo)
        {
            hotelDAO.registrarTipoHabitacion(tipo);
        }

        public void actualizarTipoHabitacion(TipoHabitacionXHotel tipoHabitacionXhotel)
        {
            hotelDAO.actualizarTipoHabitacion(tipoHabitacionXhotel);
        }

        public List<TipoHabitacionXHotelViewModelList> getTipoHabitacionXHotel(int id)
        {
            return hotelDAO.getTipoHabitacionXHotel(id);
        }

        public TipoHabitacionXHotelViewModelEdit getTipoHabitacionXHotel(int idHotel, int idTipoHabitacion)
        {
            return hotelDAO.getTipoHabitacionXHotel(idHotel, idTipoHabitacion);
        }

        public List<TipoHabitacion> getTipoHabitaciones()
        {
            return hotelDAO.getTipoHabitaciones();
        }

        public decimal getPrecioTipoHabitacionXHotel(int idHotel, int idTipoHabitacion)
        {
            return hotelDAO.getPrecioTipoHabitacionXHotel(idHotel, idTipoHabitacion);
        }
    }
}