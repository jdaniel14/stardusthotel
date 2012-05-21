using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers.Administracion
{
    public class TipoHabitacionxHotelController : Controller
    {
        //
        // GET: /TipoHabitacionxHotel/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TipoHabitacionxHotel/Details/5

        public ActionResult Details(int id)
        {
            return View( );
        }

        //
        // GET: /TipoHabitacionxHotel/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TipoHabitacionxHotel/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /TipoHabitacionxHotel/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TipoHabitacionxHotel/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TipoHabitacionxHotel/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TipoHabitacionxHotel/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
