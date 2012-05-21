using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class ClienteFacade
    {
        ClienteService clienteService = new ClienteService();
        public List<ClienteBean> ListarClientes(String Nombre)
        {
            //return serviciosService.ListarServicios( Nombre );
            if (Nombre == null) Nombre = "";
            return clienteService.ListarClientes(Nombre);
        }

        public String RegistrarCliente(ClienteBean cliente)
        {
            return clienteService.RegistrarCliente(cliente);
        }

        //public String ActualizarCliente(ClienteBean cliente)
        //{
        //    return clienteService.ActualizarCliente(cliente);
        //}

        //public ClienteBean GetCliente(int id)
        //{
        //    return clienteService.GetCliente(id);
        //}

        //public String EliminarCliente(int id)
        //{
        //    return clienteService.EliminarCliente(id);
        //}
    }
    
}