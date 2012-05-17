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

        //
        // GET: /Usuario/

        public ViewResult Index()
        {
            var model = db.Usuarios;
            return View( model );
        }

        //
        // GET: /Usuario/Details/5

        public ViewResult Details(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            return View(usuario);
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch( Exception e )
            {
                ViewBag.lol = e.Message ;
                return View(usuario);
               // return View();
            }
        }
        
        //
        // GET: /Usuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            return View(usuario);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        //
        // GET: /Usuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            return View(usuario);
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ViewResult Buscar( string nombre , string apPat ) {
            var model = from r in db.Usuarios select r;

            ViewBag.resp = "";
            if (!String.IsNullOrEmpty(nombre))
            {
                model = model.Where(r => r.nombres.ToUpper().Contains(nombre.ToUpper()));
                ViewBag.resp += "1";
            }

            if (!string.IsNullOrEmpty(apPat))
            {
                model = model.Where(r => r.apPat.ToUpper().Contains(apPat.ToUpper()));
                ViewBag.resp += "1";
            }

            ViewBag.coincidencias = model.LongCount();

            return View( model.ToList() );
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}