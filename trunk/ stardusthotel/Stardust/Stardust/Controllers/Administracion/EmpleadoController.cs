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
            try
            {
                return View(empleadoFac.getEmpleado(id));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el empleado";
                return View(new EmpleadoBean());
            }
        }

        //
        // GET: /Empleado/Create

        public ActionResult Create()
        {
            try
            {
                UsuarioFacade usuarioFac = new UsuarioFacade();
                ViewBag.empleados = usuarioFac.listarUsuarios();

                return View();
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar la data";
                return View();
            }
        }

        //
        // POST: /Empleado/Create

        [HttpPost]
        public ActionResult Create(EmpleadoBean empleadobean)
        {
            try
            {
                empleadoFac.registrarEmpleado(empleadobean);
                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar crear el empleado";
                return View(new EmpleadoBean());
            }
        }

        //
        // GET: /Empleado/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                List<TipoDocumento> estados = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento("ACTIVO");
                TipoDocumento d2 = new TipoDocumento("INACTIVO");
                estados.Add(d1); estados.Add(d2);
                ViewBag.estados = estados;
                return View(empleadoFac.getEmpleado(id));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el empleado";
                List<TipoDocumento> estados = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento("ACTIVO");
                TipoDocumento d2 = new TipoDocumento("INACTIVO");
                estados.Add(d1); estados.Add(d2);
                ViewBag.estados = estados;
                return View(new EmpleadoBean());
            }
        }

        //
        // POST: /Empleado/Edit/5

        [HttpPost]
        public ActionResult Edit(EmpleadoBean empleadobean)
        {
            try
            {
                empleadoFac.modificarEmpleado(empleadobean);
                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar modificar el empleado";
                return View( new EmpleadoBean() );
            }
        }

        //
        // GET: /Empleado/Delete/5

        public ActionResult Delete(int ID)
        {
            try
            {
                return View(empleadoFac.getEmpleado(ID));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el empleado";
                return View(new EmpleadoBean());
            }
        }

        //
        // POST: /Empleado/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            try
            {
                empleadoFac.eliminarEmpleado(ID);
                return RedirectToAction("List");
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar eliminar el empleado";
                return View(new EmpleadoBean());
            }
        }


        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //base.Dispose(disposing);
        }

        public ViewResult List()
        {
            try
            {
                var model = empleadoFac.listarEmpleados();
                return View(model);
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar listar los empleados";
                return View(new List<EmpleadoBean>());
            }
        }

        public ViewResult Buscar(string nombre , string fechaInicio ) {
            try
            {
                var model = empleadoFac.buscarEmpleado(nombre, fechaInicio);
                if (model == null)
                    model = new List<EmpleadoBean>();
                else
                    ViewBag.results = model.Count;
                return View(model);
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar buscar empleados";
                return View(new List<EmpleadoBean>());
            }
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
           // EmpleadoFacade f = new EmpleadoFacade();
           
            return View();
        }

        public ViewResult ListHorario(int id)
        {
            var model = empleadoFac.listarHorario(id);
            ViewBag.idempleado = id;
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
            EmpleadoFacade empleadoFac = new EmpleadoFacade();

            ViewBag.idempleado = empleadoFac.getHorario(id).idEmpleado;
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

                List<DiaSemana> docs = new List<DiaSemana>();
                DiaSemana d1 = new DiaSemana("Lunes");
                DiaSemana d2 = new DiaSemana("Martes");
                DiaSemana d3 = new DiaSemana("Miercoles");
                DiaSemana d4 = new DiaSemana("Jueves");
                DiaSemana d5 = new DiaSemana("Viernes");
                DiaSemana d6 = new DiaSemana("Sabado");
                DiaSemana d7 = new DiaSemana("Domingo");

                docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4); docs.Add(d5); docs.Add(d6); docs.Add(d7);
                ViewBag.semana = docs;


               
                var model = horariodetallebeam;
                System.Diagnostics.Debug.WriteLine(" dia  =" + horariodetallebeam.diaSemana);
                System.Diagnostics.Debug.WriteLine(" entrada  =" + horariodetallebeam.horaEntrada);
                System.Diagnostics.Debug.WriteLine(" salida  =" + horariodetallebeam.horaSalida);
                System.Diagnostics.Debug.WriteLine(" idhorario  =" + horariodetallebeam.idHorario);

                return View(model);
           
        }

        [HttpPost]
        public ActionResult Creardetalle(HorarioDetalle horariodetallebeam)
        {
          
           // System.Diagnostics.Debug.WriteLine(" despues  dia  =" + horariodetallebeam.diaSemana);
           // System.Diagnostics.Debug.WriteLine(" despues entrada  =" + horariodetallebeam.horaEntrada);
           // System.Diagnostics.Debug.WriteLine("despues  salida  =" + horariodetallebeam.horaSalida);
           // System.Diagnostics.Debug.WriteLine(" despues idhorario  =" + horariodetallebeam.idHorario);
           // System.Diagnostics.Debug.WriteLine(" despues iddetalle  =" + horariodetallebeam.idHorarioDetalle);  


            EmpleadoFacade empleadoFac = new EmpleadoFacade();
            try{
            int resp = empleadoFac.asignarDetalle(horariodetallebeam);
            if ((resp == -1) || (resp == 0))
            {
                List<DiaSemana> docs = new List<DiaSemana>();
                DiaSemana d1 = new DiaSemana("Lunes");
                DiaSemana d2 = new DiaSemana("Martes");
                DiaSemana d3 = new DiaSemana("Miercoles");
                DiaSemana d4 = new DiaSemana("Jueves");
                DiaSemana d5 = new DiaSemana("Viernes");
                DiaSemana d6 = new DiaSemana("Sabado");
                DiaSemana d7 = new DiaSemana("Domingo");

                docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4); docs.Add(d5); docs.Add(d6); docs.Add(d7);
                ViewBag.semana = docs;
                if (resp == -1) ViewBag.error = "Este dia ya a sido asignado";
                if (resp == 0) ViewBag.error = "La hora de entrada debe de ser menor";  
                return View();
            }
            
            return RedirectToAction("ListDetalle", new { idhorario=horariodetallebeam.idHorario });
            }
            catch
            {return View();}
                  
           // return horariodetallebeam.idHorario.ToString();
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
            EmpleadoFacade empleadoFac = new EmpleadoFacade();
            int resp = empleadoFac.modificarHorarioDetalle(horariodetallebeam);
            if (resp == -1)
            {
                var model = horariodetallebeam;
                ViewBag.error = "La hora inicial debe ser menor a la hora final";
                return View(model);
            }
            return RedirectToAction("ListDetalle", new { idhorario = horariodetallebeam.idHorario });
           
        }
         
        #endregion
    




     public ActionResult CapturaAsistencia()
        {
            return View();           
        }
        
        [HttpPost]
        public ActionResult CapturaAsistencia(TomarAsistencia tomoasistencia)
        {
            EmpleadoFacade empleadoFac = new EmpleadoFacade();

            int resp=empleadoFac.compruebaasistencia(tomoasistencia);
            if (resp == -1) ViewBag.error = "Error al escribir Usuario o Contraseña";
            if (resp == 0) ViewBag.error = "Error al registrar - Usted no es empleado - Contactar con administracion de ser erroneo ";
            if (resp == 1) ViewBag.error = "Error al registrar - Es un empleado inactivo - Contactar con administracion de ser erroneo";
            if (resp == 2) ViewBag.error = "Su contrato no es de esta fecha - Contactar con administracion ";
            if (resp == 3) ViewBag.error = "Sus horarios asignados no son de esta fecha - Contactar con administracion de ser erroneo";
            if (resp == 4) ViewBag.error = "No tiene este dia asignado en su Horario- Contactar con administracion de ser erroneo";
            if (resp == 5) ViewBag.error = "Usted ya se registro 2 veces (su entrada y su salida)-Intente otro dia ";
            if (resp == 6) ViewBag.error = "Su registro de Asistencia a sido procesado correctamente ";
            return View();
        }
        
  }
}