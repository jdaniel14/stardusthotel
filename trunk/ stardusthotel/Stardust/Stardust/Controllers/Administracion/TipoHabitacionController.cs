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
        TipoHabitacionFacade tipoFac = new TipoHabitacionFacade();

        //
        // GET: /TipoHabitacion/

        public ViewResult Index()
        {
            return View();
        }

        //
        // GET: /TipoHabitacion/Details/5

        public ViewResult Details(int id)
        {
            return View( tipoFac.getTipo( id ) );
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
        public ActionResult Create(TipoHabitacionBean tipohabitacion)
        {
            tipoFac.registrarTipoHabitacion(tipohabitacion);

            return RedirectToAction("List");
        }
        
        //
        // GET: /TipoHabitacion/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View( tipoFac.getTipo( id ) );
        }

        //
        // POST: /TipoHabitacion/Edit/5

        [HttpPost]
        public ActionResult Edit(TipoHabitacionBean tipohabitacion)
        {
            tipoFac.actualizarTipoHabitacion(tipohabitacion);
            return RedirectToAction( "List" ) ;
        }

        //
        // GET: /TipoHabitacion/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View( tipoFac.getTipo( id ) );
        }

        //
        // POST: /TipoHabitacion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tipoFac.eliminarTipoHabitacion(id);
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult List()
        {

            var model = tipoFac.listarTipoHabitacion();
            return View(model);
        }
    }
}