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
    }
}