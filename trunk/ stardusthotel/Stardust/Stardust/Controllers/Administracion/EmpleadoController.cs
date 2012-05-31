﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers.Administracion
{ 
    public class EmpleadoController : Controller
    {
        EmpleadoFacade empleadoFac = new EmpleadoFacade();

        //
        // GET: /Empleado/

        public ViewResult Index()
        {
            return View();
        }

        //
        // GET: /Empleado/Details/5

        public ViewResult Details(int id)
        {
            // <---------------------------------------------- FALTA HACER MANTENIMIENTO DE USUARIO PARA TERMINAR ESTO
            return View( empleadoFac.getEmpleado( id ) ) ;
        }

        //
        // GET: /Empleado/Create

        public ActionResult Create()
        {
            // <--------------------------------------------- NO DEBE EXISTIR ESTA OPCION
            return View();
        } 

        //
        // POST: /Empleado/Create

        [HttpPost]
        public ActionResult Create(EmpleadoBean empleadobean)
        {
            // <-------------------------------------------- DEBE HACERSE AUTOMATICA AL CREAR USUARIO EMPLEADO
            empleadoFac.registrarEmpleado(empleadobean);
            return RedirectToAction("List");
        }
        
        //
        // GET: /Empleado/Edit/5
 
        public ActionResult Edit(int id)
        {
            // <---------------------------------------------- FALTA HACER MANTENIMIENTO DE USUARIO PARA TERMINAR ESTO
            return View( empleadoFac.getEmpleado( id ) );
        }

        //
        // POST: /Empleado/Edit/5

        [HttpPost]
        public ActionResult Edit(EmpleadoBean empleadobean)
        {
            empleadoFac.modificarEmpleado(empleadobean);
            return RedirectToAction("List");
        }

        //
        // GET: /Empleado/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(empleadoFac.getEmpleado(id));
        }

        //
        // POST: /Empleado/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // <---------------------------------------------- FALTA HACER MANTENIMIENTO DE USUARIO PARA TERMINAR ESTO
            empleadoFac.eliminarEmpleado(id);
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //base.Dispose(disposing);
        }

        public ViewResult List() {
            var model = empleadoFac.listarEmpleados();
            // <---------------------------------------------- FALTA HACER MANTENIMIENTO DE USUARIO PARA TERMINAR ESTO
            return View( model );
        }
    }
}