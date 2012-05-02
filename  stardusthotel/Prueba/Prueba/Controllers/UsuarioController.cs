﻿using System;
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
        CadenaHotelDB _db = new CadenaHotelDB();

        public ActionResult Index()
        {
            var model = _db.Usuarios;
            ViewBag.Enunciado = "Index";
            
            return View( model );
        }

    }
}
