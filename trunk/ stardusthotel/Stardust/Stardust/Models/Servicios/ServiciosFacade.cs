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

            //return serviciosService.ListarServicios( Nombre );
            if (Nombre == null) Nombre = "";
            return serviciosService.ListarServicios(Nombre);
        }

        public String RegistrarServicio(ServiciosBean servicio)
        {
            return serviciosService.RegistrarServicio(servicio);
        }

        public List<ServiciosBean> ListarServicio(String nombre)
        {
            return serviciosService.ListarServicios(nombre);
        }

        public String ActualizarServicio(ServiciosBean servicio)
        {
            return serviciosService.ActualizarServicio(servicio);
        }

        public ServiciosBean GetServicio(int id) { 
            return serviciosService.GetServicio(id);
        }

        public String EliminarServicio(int id) {
            return serviciosService.EliminarServicio(id);
        }

        public ListaServiciosResponseBean listarServicios(int idHotel, int idTipo){
            return serviciosService.listarServicios(idHotel, idTipo);
        }
        
        public MensajeBean asignarServicios(int idSer, int nroRes, Decimal monto, int flagTipo, int idHotel, String nombServ){
            return serviciosService.asignarServicios(idSer, nroRes, monto, flagTipo, idHotel, nombServ);
        }
    }
}