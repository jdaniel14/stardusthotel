using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ClienteService
    {
        ClienteDAO clienteDAO = new ClienteDAO();

        public List<ClienteBean> ListarClientes(String Nombre)
        {
            //return serviciosService.ListarServicios( Nombre );
            if (Nombre == null) Nombre = "";
            return clienteDAO.ListarClientes(Nombre);
        }

        public String RegistrarCliente(ClienteBean cliente)
        {
            return clienteDAO.insertarCliente(cliente);
        }

        /*public String ActualizarCliente(ClienteBean cliente)
        {
            return clienteDAO.ActualizarCliente(cliente);
        }*/

       /* public ClienteBean GetCliente(int id)
        {
            return clienteDAO.GetCliente(id);
        }

        public String EliminarCliente(int id)
        {
            return clienteDAO.EliminarCliente(id);
        }*/
    }
}