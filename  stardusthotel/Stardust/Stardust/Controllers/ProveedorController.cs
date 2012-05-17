using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class ServiciosController : Controller
    {
        //
        // GET: /Proveedores/

        public ViewResult Index()
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor("");
            return View(listaProveedor);
        }

        [HttpPost]
        public ViewResult Index(List<ProveedorBean> listaProveedor)
        {
            return View(listaProveedor);
        }

        public ActionResult RegistrarProveedor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarProveedor(ProveedorBean model)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.RegistrarProveedor(model);
            return RedirectToAction("Index");
        }

        public ActionResult ModificarProveedor(int idProveedor)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            ProveedorBean item = proveedorFacade.GetProveedor(idProveedor);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(ProveedorBean item)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.ActualizarProveedor(item);
            return RedirectToAction("Index");
        }

        public ActionResult EliminarProveedor(int idProveedor)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.EliminarProveedor(idProveedor);
            return RedirectToAction("Index");
        }

        public ActionResult BuscarProveedor()
        {
            return View();
        }

        public ActionResult MostrarProveedor(ProveedorBean item)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor(item.razonSocial);
            return View(listaProveedor);
        }


    }
}


/*
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
            //List<Proveedor> list;
            //ProveedorFacade profacade = new ProveedorFacade();

           var model = db.Proveedor;
            //var model = profacade.listar();
            return View(model);
        }
                
        public ViewResult Details(int id)
        {
            Proveedor proveedor = db.Proveedor.Find(id);
            return View(proveedor);
        }
        //public ViewResult Control()
        //{
            
        //    return View();
        //}

        public ViewResult Control(string razon_social, string contacto)
        {
            var model = from r in db.Proveedor select r;

            ViewBag.resp = "";
            if (!String.IsNullOrEmpty(razon_social))
            {
                model = model.Where(r => r.razonSocial.ToUpper().Contains(razon_social.ToUpper()));
                ViewBag.resp += "1";
            }

            if (!string.IsNullOrEmpty(contacto))
            {
                model = model.Where(r => r.contacto.ToUpper().Contains(contacto.ToUpper()));
                ViewBag.resp += "1";
            }

            ViewBag.coincidencias = model.LongCount();

            return View( model.ToList() );
            
        }

        //[HttpPost]
        //public ActionResult Buscar(string razon_social, string contacto)
        //{
        //    //var prov = db.Proveedor;
        //    try
        //    {
        //        var model = new Proveedor();

        //        if ((String.Compare(razon_social, "") != 0))
        //        {
        //            //busco por razon social
                    
        //            Proveedor prov = db.Proveedor.Single(r => r.Razon_Social == razon_social);//.Find(razon_social);
                    
        //            return View(prov);
        //        }
        //        else //es vacio 
        //        {
        //            if (contacto != "")
        //            {
        //                //busco por contacto
        //                Proveedor prove = db.Proveedor.Single(r => r.Contacto == contacto);//.Find(contacto);
                       
        //                //return View(prove);

        //            }
        //            else
        //            {
        //                //mensaje de error 
        //            }
        //        }

        //        //razon_social = "vacio1"; //forma 1
        //        //if (contacto  == "") contacto = "vacio2"; //forma 2

        //        ViewData["razon"] = razon_social;
        //        ViewData["contacto"] = contacto;

        //        return View();
        //    }
        //    catch(Exception e)
        //    {
        //        ViewBag.lol = e.Message;
        //        // return View(proveedor);
        //        return View();
        //    }
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Proveedor proveedor)
        {
            try
            {
                //db.Proveedor.Add(proveedor);
                string sql = "Insert into Proveedors (razonSocial, ruc, direccion, telefono, web , contacto, cargoContacto, emailContacto, observaciones, estado ) values ( {0} , {1} , {2} , {3} , {4} , {5} , {6} , {7} , {8} ,{9})";
                proveedor.estado = 1;
                //proveedor.idProveedor = 1;
                db.Database.ExecuteSqlCommand(sql, proveedor.razonSocial,
                                                  proveedor.ruc,
                                                  proveedor.direccion,
                                                  proveedor.telefono,
                                                  proveedor.web,
                                                  proveedor.contacto,
                                                  proveedor.cargoContacto,
                                                  proveedor.emailContacto,
                                                  proveedor.Observaciones,
                                                  proveedor.estado
                                                  );
                db.SaveChanges();
                return RedirectToAction("../Home/Index");
                //return RedirectToAction("Control");
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
*/