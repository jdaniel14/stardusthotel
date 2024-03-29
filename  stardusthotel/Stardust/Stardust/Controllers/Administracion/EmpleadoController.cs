﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using log4net;
using AutoMapper;

namespace Stardust.Controllers
{
    public class EmpleadoController : Controller
    {
        private static ILog log = LogManager.GetLogger(typeof(EmpleadoController));
        EmpleadoFacade empleadoFac = new EmpleadoFacade();
        UsuarioFacade usuarioFac = new UsuarioFacade();

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

        //public ActionResult Create()
        //{
        //    try
        //    {
        //        UsuarioFacade usuarioFac = new UsuarioFacade();
        //        ViewBag.empleados = usuarioFac.listarUsuarios();

        //        return View();
        //    }
        //    catch {
        //        UsuarioFacade usuarioFac = new UsuarioFacade();
        //        ViewBag.empleados = usuarioFac.listarUsuarios();
        //        ViewBag.results = "Ocurrió un error al intentar cargar la data";
        //        return View();
        //    }
        //}

        ////
        //// POST: /Empleado/Create

        //[HttpPost]
        //public ActionResult Create(EmpleadoBean empleadobean)
        //{
        //    try
        //    {
        //        empleadoFac.registrarEmpleado(empleadobean);
        //        return RedirectToAction("List");
        //    }
        //    catch {
        //        UsuarioFacade usuarioFac = new UsuarioFacade();
        //        ViewBag.empleados = usuarioFac.listarUsuarios();
        //        ViewBag.results = "Ocurrió un error al intentar crear el empleado";
        //        return View(new EmpleadoBean());
        //    }
        //}
        // GET: /Usuario/Create
        public ActionResult Create()
        {
            var usuarioVMC = new UsuarioViewModelCreate();
            try
            {
                usuarioVMC.Departamentos = Utils.listarDepartamentos();
                usuarioVMC.Documentos = new List<TipoDocumento>();
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "DNI" });
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "PASAPORTE" });
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "CARNET DE EXTRANJERIA" });
                usuarioVMC.PerfilesUsuario = new PerfilUsuarioFacade().listarPerfiles();
                return View(usuarioVMC);
            }
            catch (Exception ex)
            {
                log.Error("Create - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuarioVMC);
            }
        }

        // POST: /Usuario/Create
        [HttpPost]
        public ActionResult Create(UsuarioViewModelCreate usuarioVMC , string fechaIni , string fechaFin )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!usuarioFac.yaExisteUsuario(usuarioVMC.user_account))
                    {
                        var usuario = Mapper.Map<UsuarioViewModelCreate, UsuarioBean>(usuarioVMC);
                        usuario.idPerfilUsuario = new PerfilUsuarioDAO().getPerfilID("Empleado");
                        usuario.estado = "ACTIVO";
                        //usuario.idPerfilUsuario = new PerfilUsuarioFacade();
                        usuarioFac.registrarUsuario(usuario);
                        
                        EmpleadoBean empleado = new EmpleadoBean();
                        empleado.ID = usuarioFac.getLogin(usuario.user_account, usuario.pass).ID ;
                        empleado.fechaIngreso = Convert.ToDateTime( fechaIni ) ;
                        empleado.fechaSalida = Convert.ToDateTime( fechaFin ) ;

                        empleadoFac.registrarEmpleado(empleado);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        log.Warn("El nombre de usuario:\"" + usuarioVMC.user_account + "\" ya ha sido creado");
                        ModelState.AddModelError("", "El nombre del Usuario ya ha sido asignado");
                        return View(usuarioVMC);
                    }
                }
                return View(usuarioVMC);
            }
            catch (Exception ex)
            {
                log.Error("Create - POST(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuarioVMC);
            }
        }


        //
        // GET: /Empleado/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                List<TipoDocumento> estados = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento() { nombre = "ACTIVO" };
                TipoDocumento d2 = new TipoDocumento() { nombre = "INACTIVO"};
                estados.Add(d1); estados.Add(d2);
                ViewBag.estados = estados;
                return View(empleadoFac.getEmpleado(id));
            }
            catch {
                ViewBag.results = "Ocurrió un error al intentar cargar el empleado";
                List<TipoDocumento> estados = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento() { nombre = "ACTIVO" };
                TipoDocumento d2 = new TipoDocumento() { nombre = "INACTIVO" };
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

        public ActionResult CrearHorario(int id)
        {
            EmpleadoFacade empleadoFac = new EmpleadoFacade();
            try
            {
                ViewBag.empleados = empleadoFac.listarEmpleados();
                EmpleadoBean empleado = empleadoFac.getEmpleado(id);
                Horario horario = new Horario();
                horario.idEmpleado = empleado.ID;
                var model = horario;
                return View(model);
            }
            catch {
              ViewBag.error="Error al cargar los datos";
              ViewBag.idhorario = id;
              Horario horario = new Horario();
              horario.idEmpleado = id;
              var model = horario;
              return View(model);
            }
        }

        [HttpPost]
        public ActionResult CrearHorario(Horario horario)
        {
            try
            {
                DateTime ini = horario.fechaInicioHorario;
                DateTime fin = horario.fechaFinHorario;
                if (ini <= fin)
                {
                    int resp = empleadoFac.asignarHorario(horario);
                    if (resp == -1)
                    {
                        ViewBag.error = "Existe un horario que se cruza con dichas fechas";
                        ViewBag.empleados = empleadoFac.listarEmpleados();
                        ViewBag.idhorario = horario.ID;
                        return View();
                    }
                    return RedirectToAction("ListHorario", new { id = horario.idEmpleado });
                }
                ViewBag.empleados = empleadoFac.listarEmpleados();
                ViewBag.error = "Ingrese fechas válidas";
                ViewBag.idhorario = horario.ID;
                // EmpleadoFacade f = new EmpleadoFacade();

                return View();
            }
            catch { 

                return View(); }
        }

        public ViewResult ListHorario(int id)
        {
            var model = empleadoFac.listarHorario(id);
            ViewBag.idempleado = id;
            
            return View(model);
        }

        public ActionResult EditarHorario(int id)
        {
            try
            {
                Horario horario = empleadoFac.getHorario(id);
                var model = horario;
               
                return View(model);
            }
            
            catch{
                
                ViewBag.error = "Error al intentar cargar la data";
               
               return View();
            }
        }

        //
        // POST: /Empleado/Edit/5
        [HttpPost]
        public ActionResult EditarHorario(Horario horario)
        {
            //System.Diagnostics.Debug.WriteLine("idEmpleado = " + horario.idEmpleado);
            DateTime ini = horario.fechaInicioHorario;
            DateTime fin = horario.fechaFinHorario;
            try
            {

                if (ini <= fin)
                {
                    int resp = empleadoFac.modificarHorario(horario);
                    if (resp == -1)
                    {
                        ViewBag.idhorario = horario.ID;
                        ViewBag.error = "Existe un horario que se cruza con dichas fechas";
                        return View();
                    }
                    return RedirectToAction("ListHorario", new { id = horario.idEmpleado });
                }
                ViewBag.error = "Ingrese fechas válidas";
                ViewBag.idhorario = horario.ID;
                return View();
            }
            catch {
                ViewBag.idhorario = horario.ID;
                ViewBag.error = "Error al registrar a intentar";
                return View();
            }
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
                ViewBag.idhorario = id;
                try
                {
                    Horario horar = empleadoFac.getHorario(id);


                    HorarioDetalle horariodetallebeam = new HorarioDetalle();
                    horariodetallebeam.nombreEmpleado = horar.nombreEmpleado;
                    horariodetallebeam.idHorario = horar.ID;

                    var model = horariodetallebeam;
                    /*
                        System.Diagnostics.Debug.WriteLine(" dia  =" + horariodetallebeam.diaSemana);
                        System.Diagnostics.Debug.WriteLine(" entrada  =" + horariodetallebeam.horaEntrada);
                        System.Diagnostics.Debug.WriteLine(" salida  =" + horariodetallebeam.horaSalida);
                        System.Diagnostics.Debug.WriteLine(" idhorario  =" + horariodetallebeam.idHorario);
                    */
                    return View(model);
                }
                catch {
                    ViewBag.error = "Error al intentar cargar la data";
                    return View();
                }
           
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
                ViewBag.idhorario = horariodetallebeam.idHorario;
                if (resp == -1) ViewBag.error = "Este dia ya a sido asignado";
                if (resp == 0) ViewBag.error = "La hora de entrada debe de ser menor";  
                return View();
            }
            
            return RedirectToAction("ListDetalle", new { idhorario=horariodetallebeam.idHorario });
            }
            catch
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
                ViewBag.error = "Error al registrar la data ";
                ViewBag.idhorario = horariodetallebeam.idHorario;
                return View();}
                  
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
            try
            {
                var model = empleadoFac.listarDetalle(idhorario);
                ViewBag.idempleado = empleadoFac.getHorario(idhorario).idEmpleado;
                ViewBag.horario = idhorario;
                return View(model);
            }
            catch {
                ViewBag.error = "Error al intentar cargar la data";
                return View();
            }
        }

        public ActionResult EditarDetalle(int id)
        {
            ViewBag.idhorario = id;
            try
            {
                EmpleadoFacade empleadofac = new EmpleadoFacade();
                var model = empleadofac.gethorarioDetalle(id);
                return View(model);
            }
            catch {

                ViewBag.error = "Error al intentar cargar la data";
                
                return View();
              
            }
        }


      

        [HttpPost]
        public ActionResult EditarDetalle(HorarioDetalle horariodetallebeam)
        {
            var model = horariodetallebeam;
            ViewBag.idhorario = horariodetallebeam.idHorario;
            try
            {
                EmpleadoFacade empleadoFac = new EmpleadoFacade();
                int resp = empleadoFac.modificarHorarioDetalle(horariodetallebeam);
                if (resp == -1)
                {

                    ViewBag.error = "La hora inicial debe ser menor a la hora final";
                    return View(model);
                }
                return RedirectToAction("ListDetalle", new { idhorario = horariodetallebeam.idHorario });
            }
            catch {
                ViewBag.error = "Error al intentar guardar la data";
                return View(model); }
        }
         
        #endregion



        #region Asistencia

        public ActionResult CapturaAsistencia()
        {
            return View();           
        }
        
        [HttpPost]
        public ActionResult CapturaAsistencia(TomarAsistencia tomoasistencia)
        {
            EmpleadoFacade empleadoFac = new EmpleadoFacade();
            try
            {
                int resp = empleadoFac.compruebaasistencia(tomoasistencia);
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
            finally { }
            //catch {
            //    ViewBag.error = "Se produjo un error - intentarlo denuevo";
            //    return View(); }
        }
        #endregion 

        #region Reporte
           

        #endregion
    }
}