using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.EntityModel;
namespace Stardust.Controllers
{
    public class TipoHabitacionController : Controller
    {
        //
        // GET: /TipoHabitacion/
        private CadenaHotelDB db = new CadenaHotelDB();
        public ActionResult Index()
        {
            var m = db.TipoHab;

            return View(m);
        }

        //
        // GET: /TipoHabitacion/Details/5

        

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TipoHabitacion/Create

        [HttpPost]
        public ActionResult Create(TipoHabitacion tp)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    db.TipoHab.Add(tp);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tp);
                }
            }
            catch
            {
                return View(tp);
            }
        }
        
        //
        // GET: /TipoHabitacion/Edit/5
 
        public ActionResult Edit(int id)
        {
            TipoHabitacion tp= db.TipoHab.Find(id);

            return View(tp);
        }

        //
        // POST: /TipoHabitacion/Edit/5

        [HttpPost]
        public ActionResult Edit(TipoHabitacion tp)
        {
            try
            {
                // TODO: Add update logic here
                db.Entry(tp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View(tp);
            }
        }

        //
        // GET: /TipoHabitacion/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
            
            TipoHabitacion tp= db.TipoHab.Find(id);
            return View(tp);
            }
                catch
            {
                return Index();
            }
        }

        //
        // POST: /TipoHabitacion/Delete/5
        /*
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                TipoHabitacion tp = db.TipoHab.Find(id);
                db.TipoHab.Remove(tp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
          */
    }
}
