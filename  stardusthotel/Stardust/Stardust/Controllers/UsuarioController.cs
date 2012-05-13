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
                //db.Usuarios.Add(usuario);
                string sql = "Insert into Usuarios ( nombres , user_1 , pass , apPat , apMat , dni , pasaporte , direccion , email , ruc , telefono , celular , razonSocial , estado , ID ) values ( {0} , {1} , {2} , {3} , {4} , {5} , {6} , {7} , {8} , {9} , {10} , {11} , {12} , {13} , {14} )";
                int N = db.Usuarios.Count(r => r.nombres != "");

                int nId;

                if (N == 0) nId = 0;
                else nId = db.Usuarios.Max(r => r.ID) + 1 ;

                db.Database.ExecuteSqlCommand(sql, usuario.nombres,
                                                     usuario.user_1,
                                                     usuario.pass,
                                                     usuario.apPat,
                                                     usuario.apMat,
                                                     usuario.dni,
                                                     usuario.pasaporte,
                                                     usuario.direccion,
                                                     usuario.email,
                                                     usuario.ruc,
                                                     usuario.telefono,
                                                     usuario.celular,
                                                     usuario.razonSocial,
                                                     usuario.estado ,
                                                     nId
                                             );
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}