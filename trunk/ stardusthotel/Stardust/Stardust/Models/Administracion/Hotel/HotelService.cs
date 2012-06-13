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

        public List<HotelBean> getHotelesActivos()
        {
            return hotelDAO.getHotelesActivos();
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

        public void registrarTipoHabitacion(TipoHabitacionxHotel tipo) {
            hotelDAO.registrarTipoHabitacion(tipo);
        }

        public List<TipoHabitacionxHotel> listarTipos(int id) {
            return hotelDAO.listarTipos(id);
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
    }
}