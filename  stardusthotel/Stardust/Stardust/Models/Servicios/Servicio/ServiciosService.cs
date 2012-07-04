using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ServiciosService
    {
        ServiciosDAO serviciosDAO = new ServiciosDAO();
        public List<ServiciosBean> ListarServicios( String Nombre) {
            List<ServiciosBean> listaServicios = serviciosDAO.ListarServicios(Nombre);
            return listaServicios;
        }

        public String RegistrarServicio(ServiciosBean servicio)
        {
            return serviciosDAO.insertarServicio(servicio);
        }

        public String ActualizarServicio(ServiciosBean servicio)
        {
            return serviciosDAO.ActualizarServicio(servicio);
        }

        public ServiciosBean GetServicio(int id) 
        {
            return serviciosDAO.SeleccionarServicio(id);
        }

        public String EliminarServicio(int id)
        {
            return serviciosDAO.DeleteServicio(id);
        }

        public ListaServiciosResponseBean listarServicios(int idHotel, int idTipo)
        {
            return serviciosDAO.listarServicios(idHotel, idTipo);
        }
        public MensajeBean asignarServicios(int idSer, int nroRes, Decimal monto, int flagTipo, int idHotel, String nombServ)
        {
            return serviciosDAO.asignarServicios(idSer, nroRes, monto, flagTipo, idHotel, nombServ);
        }
    }
}