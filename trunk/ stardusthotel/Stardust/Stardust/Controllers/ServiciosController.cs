﻿using System;
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
        // GET: /Servicios/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrarServicio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarServicio(ServiciosBean model)
        {
            
            return RedirectToAction("Index");
        }

    }
}
