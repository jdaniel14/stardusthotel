using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "The Avengers rlz!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
