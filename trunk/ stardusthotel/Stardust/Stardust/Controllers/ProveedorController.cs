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

        public ViewResult Index()
        {
            
            return View();
        }
                
        public ViewResult Details(int id)
        {
            Proveedor proveedor = db.Proveedor.Find(id);
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
            //var prov = db.Proveedor;
            try
            {
                var model = new Proveedor();

                if ((String.Compare(razon_social, "") != 0))
                {
                    //busco por razon social
                    
                    Proveedor prov = db.Proveedor.Single(r => r.Razon_Social == razon_social);//.Find(razon_social);
                    
                    return View(prov);
                }
                else //es vacio 
                {
                    if (contacto != "")
                    {
                        //busco por contacto
                        Proveedor prove = db.Proveedor.Single(r => r.Contacto == contacto);//.Find(contacto);
                       
                        //return View(prove);

                    }
                    else
                    {
                        //mensaje de error 
                    }
                }

                //razon_social = "vacio1"; //forma 1
                //if (contacto  == "") contacto = "vacio2"; //forma 2

                ViewData["razon"] = razon_social;
                ViewData["contacto"] = contacto;

                return View();
            }
            catch(Exception e)
            {
                ViewBag.lol = e.Message;
                // return View(proveedor);
                return View();
            }
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
                string sql = "Insert into Proveedors (razon_social, ruc, direccion, telefono, pagina_web , contacto, cargo, correo, observacion, ID ) values ( {0} , {1} , {2} , {3} , {4} , {5} , {6} , {7} , {8} , {9} )";
                int N = db.Proveedor.Count(r => r.Razon_Social != "");
               
                int nId;
                if (N == 0)
                    nId = 0;
                else
                    nId = db.Proveedor.Max(r => r.ID) + 1;

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
                // return View(proveedor);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Proveedor proveedor = db.Proveedor.Find(id);
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
            Proveedor proveedor = db.Proveedor.Find(id);
            return View(proveedor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedor proveedor = db.Proveedor.Find(id);
            db.Proveedor.Remove(proveedor);
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
