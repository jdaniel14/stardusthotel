﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stardust.Models.Servicios;

namespace Stardust.Models
{
    public class AmbienteFacade
    {
        AmbienteService AmbienteService = new AmbienteService();
        AmbienteDAO ambienteDAO = new AmbienteDAO();

        public List<EventoBean> ListarEvento(int estadoPago)        {
            
            return AmbienteService.ListarEvento( estadoPago);
        }
        public List<AmbienteBean> ListarAmbiente(int idHotel, String Nombre, String estado)
        {
            if (Nombre == null) Nombre = "";
           // if (idTipoHabitacion == nroCamas && nroCamas == piso && precio_menor == 0) return new List<AmbienteBean>();
            return AmbienteService.ListarAmbiente(idHotel,Nombre, estado);
        }

        public String RegistrarAmbiente(AmbienteBean ambiente)
        {
            return AmbienteService.RegistrarAmbiente(ambiente);
        }

        public String ActualizarAmbiente(AmbienteBean ambiente)
        {
            return AmbienteService.ActualizarAmbiente(ambiente);
        }

        public AmbienteBean GetAmbiente(int id)
        {
            return AmbienteService.GetAmbiente(id);
        }

        public String EliminarAmbiente(int id)
        {
            return AmbienteService.EliminarAmbiente(id);
        }

        public ResAmbRequest ConsultarAmbientesDisponibles(int idHotel, String fechaIni, String fechaFin)
        {
            return AmbienteService.ConsultarAmbientesDisponibles(idHotel, fechaIni, fechaFin);
        }

        public MensajeBean RegistrarEventoYAmbientes(RegAmbienteEventoBean registro)
        {
            return AmbienteService.RegistrarEventoYAmbientes(registro);
        }

        public ReservaAmbBean CheckIn(int id)
        {
            return ambienteDAO.CheckIn(id);
        }

        public MensajeBean RegistrarCheckIn(int id)
        {
            return ambienteDAO.RegistrarCheckIn(id);
        }

        public Evento GetEvento(int id)
        {
            return ambienteDAO.GetEvento(id);
        }

        public Evento GetEvento2(int id)
        {
            return ambienteDAO.GetEvento2(id);
        }

        public MensajeBean CheckOut(int id)
        {
            return ambienteDAO.CheckOut(id);
        }
    }
}