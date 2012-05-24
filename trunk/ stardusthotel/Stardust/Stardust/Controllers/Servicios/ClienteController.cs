using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;
using Stardust.Models.Servicios;


namespace Stardust.Controllers.Servicios
{
    public class ClienteController: Controller
    {

        public ViewResult Index()
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            List<ClienteBean> listaClientes = clienteFacade.ListarClientesNatural("");
            return View(listaClientes);
        }
        
        public ViewResult IndexJuridicas()
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            List<ClienteBean> listaClientes = clienteFacade.ListarClientesJuridica("");
            return View(listaClientes);
        }

        public ActionResult RegistrarPersonaNatural()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarPersonaNatural(ClienteBean item)
        {
            ClienteFacade clienteFacade = new ClienteFacade();            
            item.tipoDocumento = "DNI";
            clienteFacade.RegistrarCliente(item);

            return RedirectToAction("Index");
        }

        public ActionResult RegistrarPersonaJuridica()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarPersonaJuridica(ClienteBean item)
        {
            ClienteFacade clienteFacade = new ClienteFacade(); 
            item.tipoDocumento = "RUC";
            clienteFacade.RegistrarCliente(item);
            return RedirectToAction("IndexJuridicas");
        }

        public ActionResult ModificarClienteNatural(int id)
        {
            //cojo el item con ese id
            return View(/*item*/);
        }

        [HttpPost]
        public ActionResult ModificarClienteNatural(ClienteBean item)
        {
            //actualizo el item natural
            return RedirectToAction("Index");
        }

        public ActionResult ModificarClienteJuridico(int id) 
        {
            return View(/*item*/);
        }

        [HttpPost]
        public ActionResult ModificarClienteJuridico(ClienteBean item)
        {
            return RedirectToAction("IndexJuridicas");
        }

        public ActionResult EliminarClienteNatural(int id)
        {
            return RedirectToAction("Index");
        }

        public ActionResult EliminarClienteJurico(int id)
        {
            return RedirectToAction("IndexJuridicas");
        }

        public ActionResult BuscarClienteNatural(ClienteBean item)
        {
            return View(/*Lista de clientes*/);
        }

        public ActionResult BuscarClienteJuridico(ClienteBean item)
        {
            return View(/*Lista de clientes*/);
        }
    }
}