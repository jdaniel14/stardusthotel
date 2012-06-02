﻿using System;
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
            ClienteFacade clienteFacade = new ClienteFacade();
            ClienteBean item = clienteFacade.GetCliente(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarClienteNatural(ClienteBean item)
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            clienteFacade.ActualizarCliente(item);
            return RedirectToAction("Index");
        }

        public ActionResult ModificarClienteJuridico(int id)
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            ClienteBean item = clienteFacade.GetCliente(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarClienteJuridico(ClienteBean item)
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            clienteFacade.ActualizarCliente(item);
            return RedirectToAction("IndexJuridicas");
        }

        public ActionResult EliminarClienteNatural(int id)
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            clienteFacade.EliminarCliente(id);
            return RedirectToAction("Index");
        }

        public ActionResult EliminarClienteJuridico(int id)
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            clienteFacade.EliminarCliente(id);
            return RedirectToAction("IndexJuridicas");
        }

        public ActionResult BuscarClienteNatural(ClienteBean item)
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            List<ClienteBean> listaClientes = clienteFacade.ListarClientesNatural(item.nombres);
            return View(listaClientes);
        }

        public ActionResult BuscarClienteJuridico(ClienteBean item)
        {
            ClienteFacade clienteFacade = new ClienteFacade();
            List<ClienteBean> listaClientes = clienteFacade.ListarClientesJuridica(item.razonSocial);
            return View(listaClientes);
        }

        public ActionResult BuscarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscarCliente(ClienteBean item, FormCollection form)
        {
            ClienteBean rpta = new ClienteBean();
            //ClienteBean rpta;

            String id = form["tipoDocumento"];
            item.tipoDocumento = id.ToString();
            
            //mando a buscar el item y guardo el resultado en rpta

            if (rpta == null)
            {
                return RedirectToAction("BuscarClienteNoEncontrado", rpta);
            }

            return RedirectToAction("BuscarClienteEncontrado", rpta);
            
        }

        public ActionResult BuscarClienteNoEncontrado()
        {
            return View();
        }

        public ActionResult BuscarClienteEncontrado(ClienteBean rpta)
        {
            return View();
        }



    }
}