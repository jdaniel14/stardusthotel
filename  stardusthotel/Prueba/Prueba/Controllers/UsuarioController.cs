using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba.Models;

namespace Prueba.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        CadenaHotelDB db = new CadenaHotelDB();

        public ActionResult Index()
        {
            var model = db.Usuarios.OrderBy( r => r.Nombre );
            ViewBag.Enunciado = "Index";
            
            return View( model );
        }

        public ActionResult Create()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Create( Usuario nuevo ){
            try {
                int X = db.Usuarios.Max(r => r.ID);
                string q = "Insert into Usuarios values ( " + (X + 1) + " , '" + nuevo.Nombre + "' , '" + nuevo.Apellido + "' )";
                db.Database.ExecuteSqlCommand(q);
                //nuevo.ID = X + 1;
                //db.Usuarios.Add(nuevo);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            catch{
                ViewBag.Errores = "OCURRIO UN ERROR";
                return View();
            }
        }

        public ActionResult Delete( int ID ) {
            var model = db.Usuarios.Single(r => r.ID == ID);

            return View( model );
        }

        [HttpPost]
        public ActionResult Delete(int ID , FormCollection form ) {

            Usuario X = db.Usuarios.Single(r => r.ID == ID);
            db.Usuarios.Remove(X);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit( int ID ) {
            var model = db.Usuarios.Single(r => r.ID == ID);
            return View( model );
        }

        [HttpPost]
        public ActionResult Edit( int ID , Usuario cambio ) {
            try
            {
                var model = db.Usuarios.Single(r => r.ID == ID);
                string q = "Update Usuarios Set Nombre = '" + cambio.Nombre + "' , Apellido = '" + cambio.Apellido + "' where ID = " + ID;
                db.Database.ExecuteSqlCommand(q);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Errores = "OCURRIO UN ERROR";
                return View();
            }
        }

    }
}