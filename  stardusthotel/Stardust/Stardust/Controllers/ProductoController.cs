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
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/
        private CadenaHotelDB db = new CadenaHotelDB();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Producto producto)
        {
            try
            {
                string sql = "Insert into Producto ( nombre , descripcion ) values ( {0} , {1}  )";
                 db.Database.ExecuteSqlCommand(sql, 
                                                producto.nombre, 
                                                producto.descripcion
                                                );
                db.SaveChanges();
                return RedirectToAction("../Home/Index");
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
