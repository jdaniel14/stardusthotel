using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class AmbienteFacade
    {
        AmbienteService AmbienteService = new AmbienteService();
        public List<AmbienteBean> ListarAmbiente(int idHotel, String Nombre, String estado, float precio_menor, float precio_mayor)
        {
            if (Nombre == null) Nombre = "";
            return AmbienteService.ListarAmbiente(idHotel,Nombre, estado, precio_menor, precio_mayor);
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
        
    }
}