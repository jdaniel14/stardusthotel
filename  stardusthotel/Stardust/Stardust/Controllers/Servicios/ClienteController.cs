using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;
using Stardust.Models;
using Stardust.Models.Servicios;

namespace Stardust.Controllers.Servicios
{
    public class ClienteController: Controller
    {
        public ViewResult Index()
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            List<ClienteBean> listaClientes = clienteFacade.ListarClientes("");
            return View(/*listaClientes*/);
        }
    }
}