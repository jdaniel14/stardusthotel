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
            try
            {
                return View(tipoFac.getTipo(id));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el tipo de habitación";
                return View(new TipoHabitacionBean());
            }
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
            try
            {
                tipoFac.registrarTipoHabitacion(tipohabitacion);

                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar crear el tipo de habitación";
                return View(new TipoHabitacionBean());
            }
        }
        
        //
        // GET: /TipoHabitacion/Edit/5
 
        public ActionResult Edit(int id)
        {
            try
            {
                return View(tipoFac.getTipo(id));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el tipo de habitación";
                return View(new TipoHabitacionBean());
            }
        }

        //
        // POST: /TipoHabitacion/Edit/5

        [HttpPost]
        public ActionResult Edit(TipoHabitacionBean tipohabitacion)
        {
            try
            {
                tipoFac.actualizarTipoHabitacion(tipohabitacion);
                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar modificar el tipo de habitación";
                return View(new TipoHabitacionBean());
            }
        }

        //
        // GET: /TipoHabitacion/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                return View(tipoFac.getTipo(id));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el tipo de habitación";
                return View(new TipoHabitacionBean());
            }
        }

        //
        // POST: /TipoHabitacion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                tipoFac.eliminarTipoHabitacion(id);
                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar eliminar el tipo de habitación";
                return View(new TipoHabitacionBean());
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult List()
        {
            try
            {
                var model = tipoFac.listarTipoHabitacion();
                return View(model);
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar listar los tipo de habitación";
                return View(new List<TipoHabitacionBean>());
            }
        }
    }
}