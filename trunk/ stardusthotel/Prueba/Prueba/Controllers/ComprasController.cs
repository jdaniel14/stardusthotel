using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba.Models;

namespace Prueba.Controllers
{
    public class ComprasController : Controller
    {
        //
        // GET: /Compras/
        CadenaHotelDB db = new CadenaHotelDB();

        public ActionResult Producto()
        {
            var model = db.Categorias;
            return View(model);
        }

        public ActionResult Control()
        {
            return View();
        }

        public ActionResult OrdenCompra()
        {
            return View();
        }

    }
}
