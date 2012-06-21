using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stardust.Models.Servicios;

namespace Stardust.Models
{
    public class AmbienteFacade
    {
        AmbienteService AmbienteService = new AmbienteService();

        public List<EventoBean> ListarEvento(int estadoPago)        {
            
            return AmbienteService.ListarEvento(estadoPago);
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
    }
}