using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ClienteService
    {
        ClienteDAO clienteDao = new ClienteDAO();
        public String RegistrarCliente(ClienteBean cliente)
        {
            return clienteDao.insertarCliente(cliente);
        }
    }
}