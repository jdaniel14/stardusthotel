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

            ViewBag.departamentos = Utils.listarDepartamentos();

            List<TipoDocumento> docs = new List<TipoDocumento>();
            TipoDocumento d1 = new TipoDocumento("DNI");
            TipoDocumento d2 = new TipoDocumento("RUC");
            TipoDocumento d3 = new TipoDocumento("PASAPORTE");
            TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
            docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
            ViewBag.documentos = docs;

            return View();
        } 

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(UsuarioBean usuariobean)
        {
            if (ModelState.IsValid)
            {
                usuarioFac.registrarUsuario(usuariobean);
                return RedirectToAction("List");
            }
            return View( usuariobean ) ;
        }
        
        //
        // GET: /Usuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = usuarioFac.getUsuario(id);
            //System.Diagnostics.Debug.WriteLine("Perfil del usuario = " + model.idPerfilUsuario);

            PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
            ViewBag.perfiles = perfilFac.listarPerfiles();

            List<TipoDocumento> docs = new List<TipoDocumento>();
            TipoDocumento d1 = new TipoDocumento("DNI");
            TipoDocumento d2 = new TipoDocumento("RUC");
            TipoDocumento d3 = new TipoDocumento("PASAPORTE");
            TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
            docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
            ViewBag.documentos = docs;

            ViewBag.departamentos = Utils.listarDepartamentos();
            ViewBag.provincias = Utils.listarProvincias(model.idDepartamento);
            ViewBag.distritos = Utils.listarDistritos(model.idDepartamento, model.idProvincia);

            return View( model );
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(UsuarioBean usuariobean)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine("Perfil de Usuario = " + usuariobean.idPerfilUsuario);
                //if (ModelState.IsValid)
                //{
                usuarioFac.actualizarUsuario(usuariobean);
                return RedirectToAction("List");
            }
            catch(Exception e ) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return View(usuariobean);
            }
            //}
            //return View();
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

        //[HttpPost, ActionName("Delete")]
        //public JsonResult DeleteConfirmed(int ID)
        //{
        //    usuarioFac.eliminarUsuario(ID);
        //    //return RedirectToAction("../Home/Index");
        //    return Json(new { me = "" });
        //}
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ViewResult List() {
            var model = usuarioFac.listarUsuarios();
            return View( model );
        }

        public ViewResult Buscar(string account , string nombre, string apPat, string apMat , string tipoDocumento , string nroDocumento ) {
            var model = usuarioFac.buscarUsuario(account , nombre, apPat, apMat, tipoDocumento , nroDocumento);
            List<TipoDocumento> docs = new List<TipoDocumento>();
            TipoDocumento d1 = new TipoDocumento("DNI");
            TipoDocumento d2 = new TipoDocumento("RUC");
            TipoDocumento d3 = new TipoDocumento("PASAPORTE");
            TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
            docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
            ViewBag.documentos = docs;
            return View( model ) ;
        }
    }
}