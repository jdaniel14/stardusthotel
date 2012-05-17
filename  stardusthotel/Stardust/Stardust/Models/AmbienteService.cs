using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class AmbienteService
    {
        AmbienteDAO AmbienteDAO = new AmbienteDAO();
        public List<AmbienteBean> ListarAmbiente(String Nombre, String estado, float precio_menor, float precio_mayor)
        {
            List<AmbienteBean> listaAmbiente = AmbienteDAO.ListarAmbiente(Nombre, estado, precio_menor, precio_mayor);
            return listaAmbiente;
        }

        public String RegistrarAmbiente(AmbienteBean ambiente)
        {
            return AmbienteDAO.insertarAmbiente(ambiente);
        }

        public String ActualizarAmbiente(AmbienteBean ambiente)
        {
            return AmbienteDAO.ActualizarAmbiente(ambiente);
        }

        public AmbienteBean GetAmbiente(int id)
        {
            return AmbienteDAO.SeleccionarAmbiente(id);
        }

        public String EliminarAmbiente(int id)
        {
            return AmbienteDAO.DeleteAmbiente(id);
        }
    }
}