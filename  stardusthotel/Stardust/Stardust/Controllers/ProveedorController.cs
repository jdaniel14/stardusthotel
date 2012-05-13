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

        //public ViewResult Index()
        //{
        //    var model = db.Proveedores;
        //    return View(model);
        //}
                
        public ViewResult Details(int id)
        {
            Proveedor proveedor = db.Proveedores.Find(id);
            return View(proveedor);
        }
        public ViewResult Control()
        {
            
            return View();
        }

        public ActionResult Buscar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(string razon_social, string contacto)
        {
            
            if ((String.Compare(razon_social,"")==0)) razon_social = "vacio1"; //forma 1
            if (contacto  == "") contacto = "vacio2"; //forma 2
           
            ViewData["razon"] = razon_social;
            ViewData["contacto"] = contacto;

            var model = db.Proveedores;
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Proveedor proveedor)
        {
            try
            {
                //db.Proveedores.Add(proveedor);
                string sql = "Insert into proveedores (Razon_Social, RUC, Direccion, Telefono, Pagina_Web , Contacto, Cargo, Correo, Observaciones, ID ) values ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})";
                int N = db.Proveedores.Count(r => r.Razon_Social != "");

                int nId;

                if (N == 0) nId = 0;
                else nId = db.Proveedores.Max(r => r.ID) + 1;

                db.Database.ExecuteSqlCommand(sql, proveedor.Razon_Social,
                                                   proveedor.RUC,
                                                   proveedor.Direccion,
                                                   proveedor.Telefono,
                                                   proveedor.Pagina_Web,
                                                   proveedor.Contacto,
                                                   proveedor.Cargo,
                                                   proveedor.Correo,
                                                   proveedor.Observaciones,
                                                   nId
                                             );
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.lol = e.Message;
                return View(proveedor);
            }
        }

        public ActionResult Edit(int id)
        {
            Proveedor proveedor = db.Proveedores.Find(id);
            return View(proveedor);
        }

        [HttpPost]
        public ActionResult Edit(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        public ActionResult Delete(int id)
        {
            Proveedor proveedor = db.Proveedores.Find(id);
            return View(proveedor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedor proveedor = db.Proveedores.Find(id);
            db.Proveedores.Remove(proveedor);
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
