using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class HabitacionController : Controller
    {
        HabitacionFacade habitacionFac = new HabitacionFacade();
        
        //
        // GET: /Habitacion/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Habitacion/Details/5

        public ActionResult Details(int id)
        {
            return View( habitacionFac.getHabitacion( id ) );
        }

        //
        // GET: /Habitacion/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Habitacion/Create

        [HttpPost]
        public ActionResult Create( HabitacionBean habitacion )
        {
            habitacionFac.registrarHabitacion(habitacion);
            return RedirectToAction("List");
        }
        
        //
        // GET: /Habitacion/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View( habitacionFac.getHabitacion( id ) );
        }

        //
        // POST: /Habitacion/Edit/5

        [HttpPost]
        public ActionResult Edit( HabitacionBean habitacion )
        {
            habitacionFac.actualizarHabitacion(habitacion);
            return RedirectToAction("List");
        }

        //
        // GET: /Habitacion/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View( habitacionFac.getHabitacion( id ) );
        }

        //
        // POST: /Habitacion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            habitacionFac.eliminarHabitacion(id);
            return RedirectToAction("List");
        }

        public ActionResult List() {
            return View(habitacionFac.listarHabitaciones());
        }
    }
}
