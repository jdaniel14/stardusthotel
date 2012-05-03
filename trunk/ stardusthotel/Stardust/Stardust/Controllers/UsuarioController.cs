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
            return View(db.Usuarios.ToList());
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
            if (ModelState.IsValid)
            {
                //db.Usuarios.Add(usuario);
                //db.SaveChanges();
                int X = db.Usuarios.Max(r => r.ID);
                string q = "Insert into Usuarios values ( " + (X + 1) + " , '" + usuario.Nombre + "' , '" + usuario.Apellido + "' , " + usuario.Tipo + ")";
                db.Database.ExecuteSqlCommand(q);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usuario);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}