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
            List<TipoDocumento> estados = new List<TipoDocumento>();
            TipoDocumento d1 = new TipoDocumento("ACTIVO");
            TipoDocumento d2 = new TipoDocumento("INACTIVO");
            estados.Add(d1); estados.Add(d2);
            ViewBag.estados = estados;
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

        public ViewResult Buscar(string nombre , string fechaInicio ) {
            var model = empleadoFac.buscarEmpleado(nombre, fechaInicio);
            if (model == null)
                model = new List<EmpleadoBean>();
            else
                ViewBag.results = model.Count;
            return View( model );
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
            EmpleadoFacade f = new EmpleadoFacade();
           
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

        /* ======== DETALLE HORARIO ======== */
        #region Detalle Horario
        public ActionResult IndiceDetalle(int id) {
            ViewBag.codigo=id;
            return View();
        }
        
        //crear detalle 
        
        public ActionResult Creardetalle( int id)
        {
            EmpleadoFacade empleadoFac = new EmpleadoFacade();

            Horario horar = empleadoFac.getHorario(id);
          
            HorarioDetalle horariodetallebeam = new HorarioDetalle();
            horariodetallebeam.nombreEmpleado = horar.nombreEmpleado;
            horariodetallebeam.idHorario = horar.ID;

            var diassemana = new List<String>{
									"Lunes", 
									"Martes", 
									"Miercoles",
									"Jueves",
									"Viernes",
									"Sabado",
								};

            ViewBag.diassemana = diassemana;
            var model = horariodetallebeam;
            return View(model);
        }

        [HttpPost]
        public ActionResult Creardetalle(HorarioDetalle horariodetallebeam)
        {
            System.Diagnostics.Debug.WriteLine(" codigo  =" + horariodetallebeam.diaSemana);
            System.Diagnostics.Debug.WriteLine(" codigo  =" + horariodetallebeam.horaEntrada);
            System.Diagnostics.Debug.WriteLine(" codigo  =" + horariodetallebeam.horaSalida);
            System.Diagnostics.Debug.WriteLine(" codigo  =" + horariodetallebeam.idHorario);

            EmpleadoFacade empleadoFac = new EmpleadoFacade();

            empleadoFac.asignarDetalle(horariodetallebeam);

            //harcodeo 
            //horariodetallebeam.horario = 1;
            //fin harcodeo
           // horario horar = empleadoFac.gethorario(horariodetallebeam.horario);
            int ide = horariodetallebeam.idHorario;
            return RedirectToAction("ListDetalle", new { id = ide });
        }
        /*
        public ViewResult IndiceDetalle(int id)
        {
            var model = id;
            ViewBag.codigo = id;
            return View();
        }
        */
       
      
        public ViewResult ListDetalle(int idhorario)
        {
            
            var model = empleadoFac.listarDetalle(idhorario);
            ViewBag.horario = idhorario;
            
            return View(model);
        }

        public ActionResult EditarDetalle(int id)
        {
            EmpleadoFacade empleadofac = new EmpleadoFacade();
            var model= empleadofac.gethorarioDetalle(id);
            return View(model);
        }

      

        [HttpPost]
        public ActionResult EditarDetalle(HorarioDetalle horariodetallebeam)
        {
            empleadoFac.modificarHorarioDetalle(horariodetallebeam);
            int codigohorario = empleadoFac.getHorario(horariodetallebeam.idHorario).ID;
            return RedirectToAction("ListDetalle", new { id = codigohorario });
        }
         
        #endregion
    }
}