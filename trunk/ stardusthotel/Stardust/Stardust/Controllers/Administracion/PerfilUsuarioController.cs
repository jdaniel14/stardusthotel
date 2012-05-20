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
    public class PerfilUsuarioController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();
        PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();

        //
        // GET: /PerfilUsuario/

        public ViewResult Index()
        {
            return View();
        }

        //
        // GET: /PerfilUsuario/Details/5

        public ViewResult Details(int id)
        {
            return View( perfilFac.getPerfil( id ) );
        }

        //
        // GET: /PerfilUsuario/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PerfilUsuario/Create

        [HttpPost]
        public ActionResult Create(PerfilUsuarioBean perfilusuariobean)
        {
            perfilFac.registrarPerfil(perfilusuariobean);
            return RedirectToAction( "List" ) ;
        }
        
        //
        // GET: /PerfilUsuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(perfilFac.getPerfil(id));
        }

        //
        // POST: /PerfilUsuario/Edit/5

        [HttpPost]
        public ActionResult Edit(PerfilUsuarioBean perfilusuariobean)
        {
            perfilFac.actualizarPerfil(perfilusuariobean);
            return RedirectToAction("List");
        }

        //
        // GET: /PerfilUsuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View( perfilFac.getPerfil( id ) );
        }

        //
        // POST: /PerfilUsuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            perfilFac.eliminarPerfil(id);
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult List() {
            var model = perfilFac.listarPerfiles();
            return View( model );
        }
    }
}