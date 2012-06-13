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

        /* ======== EMPLEADO ======== */
        #region Empleado
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
            return View(empleadoFac.getEmpleado(id));
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
            return View(empleadoFac.getEmpleado(id));
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

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            empleadoFac.eliminarEmpleado(ID);
            return RedirectToAction("List");
        }


        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //base.Dispose(disposing);
        }

        public ViewResult List()
        {
            var model = empleadoFac.listarEmpleados();
            return View(model);
        }
        #endregion

        /* ======== HORARIO ======== */
        #region Horario
        public ActionResult indice()
        {
            ViewBag.empleados = empleadoFac.listarEmpleados();
            return View();
        }

        [HttpPost]
        public ActionResult indice(EmpleadoBean empleado)
        {
            int codigoempleado = empleado.ID;
            // empleadoFac.listarHorario(codigoempleado);
            return RedirectToAction("ListHorario", new { id = codigoempleado });
        }

        public ActionResult CrearHorario()
        {
            EmpleadoFacade empleadoFac = new EmpleadoFacade();
            ViewBag.empleados = empleadoFac.listarEmpleados();
            return View();
        }

        [HttpPost]
        public ActionResult CrearHorario(Horario horario)
        {
            DateTime ini = horario.fechaInicioHorario;
            DateTime fin = horario.fechaFinHorario;
            if (ini <= fin)
            {
                int resp = empleadoFac.asignarHorario(horario);
                if (resp == -1) {
                    ViewBag.error = "Existe un horario que se cruza con dichas fechas";
                    ViewBag.empleados = empleadoFac.listarEmpleados();
                    return View();
                }
                return RedirectToAction("ListHorario", new { id = horario.idEmpleado });
            }
            ViewBag.empleados = empleadoFac.listarEmpleados();
            ViewBag.error = "Ingrese fechas válidas";
            return View();
        }

        public ViewResult ListHorario(int id)
        {
            var model = empleadoFac.listarHorario(id);
            return View(model);
        }

        public ActionResult EditarHorario(int id)
        {
            var model = empleadoFac.getHorario(id);
            return View(model);
        }

        //
        // POST: /Empleado/Edit/5
        [HttpPost]
        public ActionResult EditarHorario(Horario horario)
        {
            //System.Diagnostics.Debug.WriteLine("idEmpleado = " + horario.idEmpleado);
            DateTime ini = horario.fechaInicioHorario;
            DateTime fin = horario.fechaFinHorario;
            if (ini <= fin)
            {
                int resp = empleadoFac.modificarHorario(horario);
                if (resp == -1)
                {
                    ViewBag.error = "Existe un horario que se cruza con dichas fechas";
                    return View();
                }
                return RedirectToAction("ListHorario", new { id = horario.idEmpleado });
            }
            ViewBag.error = "Ingrese fechas válidas";
            return View();
        }
        #endregion

        //crear detalle 
        /*
        public ActionResult Creardetalle( horario horariobeam)
        {
            horariodetalle horariodetallebeam = new horariodetalle();

            horariodetallebeam.horario = horariobeam.ID;

            var modelo = horariodetallebeam;
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Creardetalle(horariodetalle horariodetallebeam,int horario)
        {
            EmpleadoFacade empleadoFac = new EmpleadoFacade();
            horariodetallebeam.horario = horario;
            empleadoFac.asignarDetalle(horariodetallebeam);
            //harcodeo 
            //horariodetallebeam.horario = 1;
            //fin harcodeo
            horario horar = empleadoFac.gethorario(horariodetallebeam.horario);
            int id = horar.idempleado;
            return RedirectToAction("ListH", new { id = id});
        }
        */

        

        //horariodetalle
        /*
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

        */
    }
        
}