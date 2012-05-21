﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{ 
    public class PromocionesController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();
        public PromocionFacade promocionFacade = new PromocionFacade();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Default1/Details/5

        public ViewResult Details(int id)
        {
            PromocionBean promociones = db.Promociones.Find(id);
            return View(promociones);
        }

        //
        // GET: /Default1/Create

        public ActionResult Registrar()
        {
            PromocionBean promocion = new PromocionBean();
            return View(promocion);
        }

        [HttpPost]
        public ActionResult Registrar(PromocionBean promocion)
        {
            String id = promocion.ID;
            if (id.Equals("2"))
                return RedirectToAction("RegistrarDia");
            else
                return RedirectToAction("RegistrarAdelanto");
        }

        public ActionResult RegistrarDia()
        {
            PromocionBean promocion = new PromocionBean();
            return View(promocion);
        }

        [HttpPost]
        public ActionResult RegistrarDia(PromocionBean promocion)
        {
            promocion.tipoDescuento = 1;
            promocion.razon = promocion.dias;
            promocionFacade.RegistrarPromocion(promocion);
            return RedirectToAction("Buscar");
        }

        public ActionResult RegistrarAdelanto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarAdelanto(PromocionBean promocion)
        {
            return RedirectToAction("Registrar");
        }

        //
        // POST: /Default1/Create

        //[HttpPost]
        //public ActionResult Create(PromocionBean promociones)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Promociones.Add(promociones);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");  
        //    }

        //    return View(promociones);
        //}
        
        //
        // GET: /Default1/Edit/5
 
        public ActionResult Edit()
        {
            return View();
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(PromocionBean promociones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promociones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promociones);
        }

        //
        // GET: /Default1/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            PromocionBean promociones = db.Promociones.Find(id);
            db.Promociones.Remove(promociones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}