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

            Utils utils = new Utils();
            ViewBag.departamentos = utils.listarDepartamentos();
            ViewBag.provincias = new List<Provincia>() ;
            ViewBag.distritos = new List<Distrito>() ;

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
            else if (usuariobean.idDepartamento != 0 && usuariobean.idProvincia != 0){
                PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
                ViewBag.perfiles = perfilFac.listarPerfiles();

                List<TipoDocumento> docs = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento("DNI");
                TipoDocumento d2 = new TipoDocumento("RUC");
                TipoDocumento d3 = new TipoDocumento( "PASAPORTE" ) ;
                TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
                docs.Add(d1);                docs.Add(d2);                docs.Add(d3);                docs.Add(d4);
                ViewBag.documentos = docs;

                Utils utils = new Utils();
                ViewBag.departamentos = utils.listarDepartamentos();
                ViewBag.provincias = utils.listarProvincias(usuariobean.idDepartamento);
                ViewBag.distritos = utils.listarDistritos(usuariobean.idDepartamento, usuariobean.idProvincia);
                return View();
            }
            else if( usuariobean.idDepartamento != 0 ){
                PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
                ViewBag.perfiles = perfilFac.listarPerfiles();

                List<TipoDocumento> docs = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento("DNI");
                TipoDocumento d2 = new TipoDocumento("RUC");
                TipoDocumento d3 = new TipoDocumento("PASAPORTE");
                TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
                docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
                ViewBag.documentos = docs;

                Utils utils = new Utils();
                ViewBag.departamentos = utils.listarDepartamentos();
                ViewBag.provincias = utils.listarProvincias( usuariobean.idDepartamento ) ;
                ViewBag.distritos = new List<Distrito>();
                return View();
            }
            
            else {
                PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
                ViewBag.perfiles = perfilFac.listarPerfiles();

                List<TipoDocumento> docs = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento("DNI");
                TipoDocumento d2 = new TipoDocumento("RUC");
                TipoDocumento d3 = new TipoDocumento("PASAPORTE");
                TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
                docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
                ViewBag.documentos = docs;

                Utils utils = new Utils();
                ViewBag.departamentos = utils.listarDepartamentos();
                ViewBag.provincias = new List<Provincia>();
                ViewBag.distritos = new List<Distrito>();
                return View();
            }
        }
        
        //
        // GET: /Usuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = usuarioFac.getUsuario(id);

            PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
            ViewBag.perfiles = perfilFac.listarPerfiles();

            List<TipoDocumento> docs = new List<TipoDocumento>();
            TipoDocumento d1 = new TipoDocumento("DNI");
            TipoDocumento d2 = new TipoDocumento("RUC");
            TipoDocumento d3 = new TipoDocumento("PASAPORTE");
            TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
            docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
            ViewBag.documentos = docs;

            return View( model );
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(UsuarioBean usuariobean)
        {
            if (ModelState.IsValid)
            {
                usuarioFac.actualizarUsuario(usuariobean);
                return RedirectToAction("List");
            }
            else if (usuariobean.idDepartamento != 0 && usuariobean.idProvincia != 0)
            {
                PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
                ViewBag.perfiles = perfilFac.listarPerfiles();

                List<TipoDocumento> docs = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento("DNI");
                TipoDocumento d2 = new TipoDocumento("RUC");
                TipoDocumento d3 = new TipoDocumento("PASAPORTE");
                TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
                docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
                ViewBag.documentos = docs;

                Utils utils = new Utils();
                ViewBag.departamentos = utils.listarDepartamentos();
                ViewBag.provincias = utils.listarProvincias(usuariobean.idDepartamento);
                ViewBag.distritos = utils.listarDistritos(usuariobean.idDepartamento, usuariobean.idProvincia);
                return View();
            }
            else if (usuariobean.idDepartamento != 0)
            {
                PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
                ViewBag.perfiles = perfilFac.listarPerfiles();

                List<TipoDocumento> docs = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento("DNI");
                TipoDocumento d2 = new TipoDocumento("RUC");
                TipoDocumento d3 = new TipoDocumento("PASAPORTE");
                TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
                docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
                ViewBag.documentos = docs;

                Utils utils = new Utils();
                ViewBag.departamentos = utils.listarDepartamentos();
                ViewBag.provincias = utils.listarProvincias(usuariobean.idDepartamento);
                ViewBag.distritos = new List<Distrito>();
                return View();
            }

            else
            {
                PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
                ViewBag.perfiles = perfilFac.listarPerfiles();

                List<TipoDocumento> docs = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento("DNI");
                TipoDocumento d2 = new TipoDocumento("RUC");
                TipoDocumento d3 = new TipoDocumento("PASAPORTE");
                TipoDocumento d4 = new TipoDocumento("CARNE DE EXTRANJERIA");
                docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
                ViewBag.documentos = docs;

                Utils utils = new Utils();
                ViewBag.departamentos = utils.listarDepartamentos();
                ViewBag.provincias = new List<Provincia>();
                ViewBag.distritos = new List<Distrito>();
                return View();
            }
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

        public ViewResult Buscar(string nombre, string apPat, string apMat) {
            var model = usuarioFac.buscarUsuario(nombre, apPat, apMat);
            return View( model ) ;
        }
    }
}