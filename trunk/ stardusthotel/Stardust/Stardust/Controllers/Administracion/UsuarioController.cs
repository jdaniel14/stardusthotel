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
    public class UsuarioController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();
        UsuarioFacade usuarioFac = new UsuarioFacade();

        //
        // GET: /Usuario/

        public ViewResult Index()
        {
            return View();
        }

        //
        // GET: /Usuario/Details/5

        public ViewResult Details(int id)
        {
            var model = usuarioFac.getUsuario(id);
            return View( model );
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade() ;
            ViewBag.perfiles = perfilFac.listarPerfiles();

            ViewBag.documentos = "" ;

            return View();
        } 

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(UsuarioBean usuariobean)
        {
            usuarioFac.registrarUsuario(usuariobean);
            return RedirectToAction( "List" ) ;
        }
        
        //
        // GET: /Usuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = usuarioFac.getUsuario(id);

            PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
            ViewBag.perfiles = perfilFac.listarPerfiles();

            ViewBag.documentos = "";

            return View( model );
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(UsuarioBean usuariobean)
        {
            usuarioFac.actualizarUsuario(usuariobean);
            return RedirectToAction( "List" ) ;
        }

        //
        // GET: /Usuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            var model = usuarioFac.getUsuario(id);
            return View( model );
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            usuarioFac.eliminarUsuario(id);
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ViewResult List() {
            var model = usuarioFac.listarUsuarios();
            return View( model );
        }
    }
}