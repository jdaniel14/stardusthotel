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
            try
            {
                return View(perfilFac.getPerfil(id));
            }
            catch {
                return View( new PerfilUsuarioBean() );
            }
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
            try
            {
                perfilFac.registrarPerfil(perfilusuariobean);
                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar crear el perfil";
                return View();
            }
        }
        
        //
        // GET: /PerfilUsuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            try
            {
                return View(perfilFac.getPerfil(id));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el perfil";
                return View(new PerfilUsuarioBean());
            }
        }

        //
        // POST: /PerfilUsuario/Edit/5

        [HttpPost]
        public ActionResult Edit(PerfilUsuarioBean perfilusuariobean)
        {
            try
            {
                perfilFac.actualizarPerfil(perfilusuariobean);
                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar modificar el perfil";
                return View();
            }
        }

        //
        // GET: /PerfilUsuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                return View(perfilFac.getPerfil(id));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el perfil";
                return View(new PerfilUsuarioBean());
            }
        }

        //
        // POST: /PerfilUsuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                perfilFac.eliminarPerfil(id);
                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar eliminar el perfil";
                return View( new PerfilUsuarioBean() );
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult List() {
            try
            {
                var model = perfilFac.listarPerfiles();
                return View(model);
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar listar los perfiles";
                return View(new List<PerfilUsuarioBean>());
            }
        }
    }
}