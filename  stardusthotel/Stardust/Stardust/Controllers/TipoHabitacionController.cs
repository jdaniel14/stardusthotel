using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{ 
    public class TipoHabitacionController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();

        //
        // GET: /TipoHabitacion/

        public ViewResult Index()
        {
            return View(db.TipoHabitacion.ToList());
        }

        //
        // GET: /TipoHabitacion/Details/5

        public ViewResult Details(int id)
        {
            TipoHabitacion tipohabitacion = db.TipoHabitacion.Find(id);
            return View(tipohabitacion);
        }

        //
        // GET: /TipoHabitacion/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TipoHabitacion/Create

        [HttpPost]
        public ActionResult Create(TipoHabitacion tipohabitacion)
        {
            if (ModelState.IsValid)
            {
                db.TipoHabitacion.Add(tipohabitacion);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tipohabitacion);
        }
        
        //
        // GET: /TipoHabitacion/Edit/5
 
        public ActionResult Edit(int id)
        {
            TipoHabitacion tipohabitacion = db.TipoHabitacion.Find(id);
            return View(tipohabitacion);
        }

        //
        // POST: /TipoHabitacion/Edit/5

        [HttpPost]
        public ActionResult Edit(TipoHabitacion tipohabitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipohabitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipohabitacion);
        }

        //
        // GET: /TipoHabitacion/Delete/5
 
        public ActionResult Delete(int id)
        {
            TipoHabitacion tipohabitacion = db.TipoHabitacion.Find(id);
            return View(tipohabitacion);
        }

        //
        // POST: /TipoHabitacion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TipoHabitacion tipohabitacion = db.TipoHabitacion.Find(id);
            db.TipoHabitacion.Remove(tipohabitacion);
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