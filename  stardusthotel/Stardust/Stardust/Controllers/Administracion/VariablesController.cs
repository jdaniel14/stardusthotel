﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class VariablesController : Controller
    {
        VariablesFacade variablesFac = new VariablesFacade();
        
        //
        // GET: /Variables/

        public ActionResult Index()
        {
            return View( variablesFac.getVariables() );
        }

        //
        // GET: /Variables/Edit
 
        public ActionResult Edit()
        {
            return View( variablesFac.getVariables() ) ;
        }

        //
        // POST: /Variables/Edit

        [HttpPost]
        public ActionResult Edit( VariablesBean variables )
        {
            variablesFac.actualizarVariables( variables );
            return RedirectToAction("Index");
        }
    }
}
