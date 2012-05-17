using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ServiciosFacade
    {
        ServiciosService serviciosService = new ServiciosService();
        public List<ServiciosBean> ListarServicios(String Nombre) {
            return serviciosService.ListarServicios( Nombre );
        }

        public String RegistrarServicio(ServiciosBean servicio)
        {
            return serviciosService.RegistrarServicio(servicio);
        }

        public String ActualizarServicio(ServiciosBean servicio)
        {
            return serviciosService.ActualizarServicio(servicio);
        }

        public ServiciosBean GetServicio(int id) { 
            return serviciosService.GetServicio(id);
        }
    }
}