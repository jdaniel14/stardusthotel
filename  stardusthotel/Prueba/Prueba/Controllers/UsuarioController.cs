using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba.Models;

namespace Prueba.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        CadenaHotelDB db = new CadenaHotelDB();

        public ActionResult Index()
        {
            return View();
        }

    }
}
