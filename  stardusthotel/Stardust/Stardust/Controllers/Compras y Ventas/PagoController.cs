﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using log4net;

namespace Stardust.Controllers
{
    public class PagoController : Controller
    {

        /*---------Pago de Servicios, Evento y ambientes------*/

        PagoFacade pagoFacade = new PagoFacade();
        private static ILog log = LogManager.GetLogger(typeof(PagoController));
        public ActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(string nombre)
        {
            return RedirectToAction("Listar", new { nom = nombre });
        }

        //public ActionResult Listar(string nom)
        //{
        //    List<ReservaBean> reserva = pagoFacade.GetReserva(nom);
        //    if (reserva.Count == 0)
        //        ViewBag.Error = "No se encontraron reservas o el usuario no existe";
        //    return View(reserva);
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        

        public ActionResult Edit(int id)
        {
            return View();
        }

          [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }           

    }
}
