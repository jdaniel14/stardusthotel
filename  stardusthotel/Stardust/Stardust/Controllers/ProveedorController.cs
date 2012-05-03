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
        
        
        public ActionResult Registrar(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                int X;
            }
            return View();
        }

        public ActionResult Control()
        {
            return View();
        }

    }
}
