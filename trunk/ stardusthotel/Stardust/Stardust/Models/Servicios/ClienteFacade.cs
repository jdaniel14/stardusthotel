using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class ClienteFacade
    {
        ClienteService clienteService = new ClienteService();
        public List<ClienteBean> ListarClientesNatural(String Nombre)
        {
            //return serviciosService.ListarServicios( Nombre );
            if (Nombre == null) Nombre = "";
            return clienteService.ListarClientesNatural(Nombre);
        }

        public List<ClienteBean> ListarClientesJuridica(String Nombre)
        {
            //return serviciosService.ListarServicios( Nombre );
            if (Nombre == null) Nombre = "";
            return clienteService.ListarClientesJuridica(Nombre);
        }

        public String RegistrarCliente(ClienteBean cliente)
        {            
            if (cliente.nombres == null) cliente.nombres = "";
            if (cliente.apPat == null) cliente.apPat = "";
            if (cliente.apMat == null) cliente.apMat = "";
            if (cliente.razonSocial == null) cliente.razonSocial = "";
            return clienteService.RegistrarCliente(cliente);
        }

        public String ActualizarCliente(ClienteBean cliente)
        {
            if (cliente.nombres == null) cliente.nombres = "";
            if (cliente.apPat == null) cliente.apPat = "";
            if (cliente.apMat == null) cliente.apMat = "";
            if (cliente.razonSocial == null) cliente.razonSocial = "";
            return clienteService.ActualizarCliente(cliente);
        }

        public ClienteBean GetCliente(int id)
        {
            return clienteService.GetCliente(id);
        }

        public String EliminarCliente(int id)
        {
            return clienteService.EliminarCliente(id);
        }
    }   
}