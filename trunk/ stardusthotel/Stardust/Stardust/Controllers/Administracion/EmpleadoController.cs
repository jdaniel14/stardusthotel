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
            return View( empleadoFac.getEmpleado( id ) ) ;
        }

        //
        // GET: /Empleado/Create

        public ActionResult Create()
        {
            UsuarioFacade usuarioFac = new UsuarioFacade();
            ViewBag.empleados = usuarioFac.listarUsuarios();
            
            return View();
        } 

        //
        // POST: /Empleado/Create

        [HttpPost]
        public ActionResult Create(EmpleadoBean empleadobean)
        {
            empleadoFac.registrarEmpleado(empleadobean);
            return RedirectToAction("List");
        }
        
        //
        // GET: /Empleado/Edit/5
 
        public ActionResult Edit(int id)
        {
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
 
        public ActionResult Delete(int ID)
        {
            return View(empleadoFac.getEmpleado(ID));
        }

        //
        // POST: /Empleado/Delete/5

     //   [HttpPost, ActionName("Delete")]
    //    public ActionResult DeleteConfirmed(int id)
     //   {
    //        empleadoFac.eliminarEmpleado(id);
     //       return RedirectToAction("List");
     //   }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int ID)
        {
            empleadoFac.eliminarEmpleado(ID);
            //return RedirectToAction("../Home/Index");
            return Json(new { me = "" });
        }


        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //base.Dispose(disposing);
        }

        public ViewResult List() {
            var model = empleadoFac.listarEmpleados();
            return View( model );
        }

        //HORARIO

        public ActionResult indice(){
            EmpleadoFacade empleado = new EmpleadoFacade();

            ViewBag.empleados = empleado.listarEmpleados();
            return View();
        }

        [HttpPost]
        public ActionResult indice(EmpleadoBean empl)
        {
            int codigoempleado = empl.ID;
           // empleadoFac.listarHorario(codigoempleado);
            return RedirectToAction("ListH",  new { id = codigoempleado });

            
        }

    //crear horarioo   

        public ActionResult crearhorario()
        {
            
            EmpleadoFacade empleadoFac = new EmpleadoFacade();
            ViewBag.empleados = empleadoFac.listarEmpleados();
            return View();
        }

        [HttpPost]
        public ActionResult crearhorario(horario horariobeam)
        {
            
            empleadoFac.asignarhorario(horariobeam);
          //  int horario = horariobeam.idEmpleado;
            var m = horariobeam;
           // int codigoempleado = horariobeam.idempleado;
            return RedirectToAction("Creardetalle", m
            );
        }
//crear detalle 

        public ActionResult Creardetalle( horario horariobeam)
        {
            horariodetalle horariodetallebeam = new horariodetalle();

            horariodetallebeam.horario = horariobeam.ID;

            var modelo = horariodetallebeam;
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Creardetalle(horariodetalle horariodetallebeam)
        {
            EmpleadoFacade empleadoFac = new EmpleadoFacade();
            empleadoFac.asignarDetalle(horariodetallebeam);
            //harcodeo 
            horariodetallebeam.horario = 1;
            //fin harcodeo
            horario horar = empleadoFac.gethorario(horariodetallebeam.horario);
            int id = horar.idempleado;
            return RedirectToAction("ListH", new { id = id});
        }
        

        public ViewResult ListH(int id)
        {
             EmpleadoFacade empleados = new EmpleadoFacade();
             ViewBag.empleados = empleados.getEmpleado(id);
            var model = empleadoFac.listarHorarioEmpleado(id);
            return View(model);
           // return View()
        }


        public ActionResult Editar(int id)
        {
            return View(empleadoFac.gethorario(id));
        }

        //
        // POST: /Empleado/Edit/5


        [HttpPost]
        public ActionResult Editar(horario horariobeam)
        {
            empleadoFac.modificarHorario(horariobeam);
          //  horariobeam.idempleado = 1;
            return RedirectToAction("ListH", new { id = horariobeam.idempleado});
        }


 //horariodetalle

        public ViewResult listarDetalle(int idhorario)
        {
            
           // var model = empleadoFac.listarDetalle(codigohorario);
            //ViewBag.horario = codigohorario;
            return View(empleadoFac.gethorario(idhorario));
        }

        public ActionResult EditarDetalle(int idhorario)
        {
            horariodetalle horariod = empleadoFac.gethorarioDetalle(idhorario);
            return View(horariod);
        }

        //
        // POST: /Empleado/Edit/5


        [HttpPost]
        public ActionResult EditarDetalle(horariodetalle horariodetallebeam)
        {
            empleadoFac.asignarModificarDetalle(horariodetallebeam);

            return RedirectToAction("ListH", new { id = empleadoFac.gethorario(horariodetallebeam.horario).idempleado });
        }



    }

    
}