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
    public class ProveedorController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();

        public ActionResult Index()
        {
            return View();
            //return View(db.Proveedores.ToList());
        }

        public ActionResult Control()
        {
            return View();
        }
        //public ViewResult Details(int id)
        //{
        //    Usuario usuario = db.Proveedores.Find(id);
        //    return View(proveedor);
        //}

        public ActionResult Create()
        {
            return View();
        }
        
        //[HttpPost]
        //public ActionResult Create(Proveedor proveedor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int X = db.Proveedores.Max(r => r.ID);
        //        string q = "Insert into Proveedores values ( " + (X + 1) + " , '" + proveedor.Razon_Social + "' , '" + proveedor.RUC + "' , '" + proveedor.Categoria + "' ,  '" + proveedor.Direccion + "' , '" + proveedor.Telefono + "' , '" + proveedor.Pagina_Web + "' , '" + proveedor.Contacto + "' , '" + proveedor.Cargo + "' , '" + proveedor.Correo + "' , '" + proveedor.Observaciones + "')";
        //        db.Database.ExecuteSqlCommand(q);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    return View(proveedor);
        //}

        public ActionResult Edit(int id)
        {
            Proveedor proveedor = db.Proveedores.Find(id);
            return View(proveedor);
        }

        //[HttpPost]
        //public ActionResult Edit(Proveedor proveedor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(proveedor).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(proveedor);
        //}

        public ActionResult Delete(int id)
        {
            Proveedor proveedor = db.Proveedores.Find(id);
            return View(proveedor);
        }

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Proveedor proveedor = db.Proveedores.Find(id);
        //    db.Proveedores.Remove(proveedor);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
