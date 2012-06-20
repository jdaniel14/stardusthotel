using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stardust.Models.Servicios;

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

        public ResAmbRequest ConsultarAmbientesDisponibles(int idHotel, String fechaIni, String fechaFin)
        {
            ResAmbRequest response = new ResAmbRequest();
            List<AmbienteBean> listNoDisp = AmbienteDAO.listarNodisponibles(idHotel, fechaIni, fechaFin);
            List<AmbienteBean> listTot = AmbienteDAO.listarTodas(idHotel);
            int k = 0; int tamNoDisp = listNoDisp.Count;
            int l = 0; int tamTot = listTot.Count;
            bool [] disp = new bool [tamTot];
            for (int i = 0; i < tamTot; i++) disp[i] = true;
            while (k < tamNoDisp && l < tamTot)
            {
                if (listNoDisp[k].id == listTot[l].id)
                {
                    disp[l] = false;
                    k++;
                    l++;
                }
                else if (listNoDisp[k].id < listTot[l].id)
                {
                    k++;
                }
                else
                {
                    l++;
                }
            }
            List<AmbienteBean> listaRespuesta = new List<AmbienteBean>();
            for (int i = 0; i < tamTot; i++) {
                if (disp[i]) {
                    listaRespuesta.Add(listTot[i]);
                }
            }

            return response;
        }
    }
}