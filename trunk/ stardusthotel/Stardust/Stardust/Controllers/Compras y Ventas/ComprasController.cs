using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stardust.Controllers
{
    public class ComprasController : Controller
    {
        //
        // GET: /Compras/

        public ActionResult Producto()
        {
            return View();
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
