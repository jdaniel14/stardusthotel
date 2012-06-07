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
    public class EmpleadoController : Controller
    {
        EmpleadoFacade empleadoFac = new EmpleadoFacade();

        //
        // GET: /Empleado/

        public ViewResult Index()
        {
            return View();
        }

        //
        // GET: /Empleado/Details/5

        public ViewResult Details(int id)
        {
            return View( empleadoFac.getEmpleado( id ) ) ;
        }

        //
        // GET: /Empleado/Create

        public ActionResult Create()
        {
            UsuarioFacade usuarioFac = new UsuarioFacade();
            ViewBag.empleados = usuarioFac.listarUsuarios();
            return View();
        } 

        //
        // POST: /Empleado/Create

        [HttpPost]
        public ActionResult Create(EmpleadoBean empleadobean)
        {
            empleadoFac.registrarEmpleado(empleadobean);
            return RedirectToAction("List");
        }
        
        //
        // GET: /Empleado/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View( empleadoFac.getEmpleado( id ) );
        }

        //
        // POST: /Empleado/Edit/5

        [HttpPost]
        public ActionResult Edit(EmpleadoBean empleadobean)
        {
            empleadoFac.modificarEmpleado(empleadobean);
            return RedirectToAction("List");
        }

        //
        // GET: /Empleado/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(empleadoFac.getEmpleado(id));
        }

        //
        // POST: /Empleado/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            empleadoFac.eliminarEmpleado(id);
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //base.Dispose(disposing);
        }

        public ViewResult List() {
            var model = empleadoFac.listarEmpleados();
            return View( model );
        }
    }
}