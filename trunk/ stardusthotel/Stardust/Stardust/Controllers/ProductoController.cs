using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stardust.Controllers
{
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/

        public ActionResult Create()
        {
            return View();
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

            ViewData["nombre"]=nombre;
            return View();
        }
    }
}
