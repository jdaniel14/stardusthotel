using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/
        private CadenaHotelDB db = new CadenaHotelDB();

        public ActionResult Create()
        {
            return View(new Producto());
        }

        [HttpPost]
        public ActionResult Create(Producto producto)
        {
            try
            {
                string sql = "Insert into Producto ( nombre , idCategoria, idProducto ) values ( {0} , 1 , 1 )";
                //int N = db.Productos.Count(r => r.nombre != null);

                int nId=1;
                int id = 1;
                //if (N == 0) nId = 0;
                //else nId = db.Productos.Max(r => r.ID) + 1;

                db.Database.ExecuteSqlCommand(sql, producto.nombre
                                             );
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.lol = e.Message;
                return View();
            }
        }

        public ActionResult Control()
        {
            return View();
        }

        public ActionResult Buscar() {
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(string nombre) {

            if (nombre != "") 
            {

            }
            ViewData["nombre"]=nombre;
           
            return View();
        }
    }
}
