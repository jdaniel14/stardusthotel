using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Web.Security;

using AutoMapper;
using log4net;

namespace Stardust.Controllers
{ 
    public class UsuarioController : Controller
    {
        UsuarioFacade usuarioFac = new UsuarioFacade();
        private static ILog log = LogManager.GetLogger(typeof(UsuarioController));

        // GET: /Usuario/
        public ActionResult Index()
        {
            return View();
        }

        #region Details
        // GET: /Usuario/Details/5
        public ViewResult Details(int id)
        {
            var usuarioVMD = new UsuarioViewModelDetails();
            try
            {
                var usuario = usuarioFac.getUsuario(id);
                usuarioVMD = Mapper.Map<UsuarioBean, UsuarioViewModelDetails>(usuario);

                usuarioVMD.nombrePerfilUsuario = usuarioFac.getNombrePerfilUsuario(usuarioVMD.idPerfilUsuario);
                usuarioVMD.nombreDepartamento = Utils.getNombreDepartamento(usuarioVMD.idDepartamento);
                usuarioVMD.nombreProvincia = Utils.getNombreProvincia(usuarioVMD.idDepartamento, usuarioVMD.idProvincia);
                usuarioVMD.nombreDistrito = Utils.getNombreDistrito(usuarioVMD.idDepartamento, usuarioVMD.idProvincia, usuarioVMD.idDistrito);

                return View(usuarioVMD);
            }
            catch (Exception ex)
            {
                log.Error("Details - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuarioVMD);
            }
        }
        #endregion

        #region Create
        // GET: /Usuario/Create
        public ActionResult Create()
        {
            var usuarioVMC = new UsuarioViewModelCreate();
            try
            {
                usuarioVMC.Departamentos = Utils.listarDepartamentos();
                usuarioVMC.Documentos = new List<TipoDocumento>();
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "DNI" });
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "RUC" });
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "PASAPORTE" });
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "CARNET DE EXTRANJERIA" });
                usuarioVMC.PerfilesUsuario = new PerfilUsuarioFacade().listarPerfiles();
                return View(usuarioVMC);
            }
            catch(Exception ex){
                log.Error("Create - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuarioVMC);
            }
        } 

        // POST: /Usuario/Create
        [HttpPost]
        public ActionResult Create(UsuarioViewModelCreate usuarioVMC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!usuarioFac.yaExisteUsuario(usuarioVMC.user_account))
                    {
                        var usuario = Mapper.Map<UsuarioViewModelCreate, UsuarioBean>(usuarioVMC);
                        usuario.estado = "ACTIVO";
                        usuarioFac.registrarUsuario(usuario);
                        return RedirectToAction("List");
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
            catch(Exception ex){
                log.Error("Create - POST(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuarioVMC);
            }
        }
        #endregion

        #region Edit
        // GET: /Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            var usuarioVME = new UsuarioViewModelEdit();
            try
            {
                UsuarioBean usuario = usuarioFac.getUsuario(id);
                usuarioVME = Mapper.Map<UsuarioBean, UsuarioViewModelEdit>(usuario);

                usuarioVME.Departamentos = Utils.listarDepartamentos();
                usuarioVME.Provincias = Utils.listarProvincias(usuario.idDepartamento); 
                usuarioVME.Distritos = Utils.listarDistritos(usuario.idDepartamento, usuario.idProvincia);
                
                usuarioVME.Documentos = new List<TipoDocumento>();
                usuarioVME.Documentos.Add(new TipoDocumento() { nombre = "DNI" });
                usuarioVME.Documentos.Add(new TipoDocumento() { nombre = "RUC" });
                usuarioVME.Documentos.Add(new TipoDocumento() { nombre = "PASAPORTE" });
                usuarioVME.Documentos.Add(new TipoDocumento() { nombre = "CARNET DE EXTRANJERIA" });

                usuarioVME.PerfilesUsuario = new PerfilUsuarioFacade().listarPerfiles();
                return View(usuarioVME);
            }
            catch (Exception ex)
            {
                log.Error("Edit - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuarioVME);
            }
        }

        // POST: /Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(UsuarioViewModelEdit usuarioVME)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = Mapper.Map<UsuarioViewModelEdit, UsuarioBean>(usuarioVME);
                    usuarioFac.actualizarUsuario(usuario);
                    return RedirectToAction("List");
                }
                return View(usuarioVME);
            }
            catch (Exception ex)
            {
                log.Error("Edit - POST(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(usuarioVME);
            }
        }
        #endregion

        //// GET: /Usuario/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    try
        //    {
        //        var model = usuarioFac.getUsuario(id);
        //        //System.Diagnostics.Debug.WriteLine("Perfil del usuario = " + model.idPerfilUsuario);

        //        PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
        //        ViewBag.perfiles = perfilFac.listarPerfiles();

        //        List<TipoDocumento> docs = new List<TipoDocumento>();
        //        TipoDocumento d1 = new TipoDocumento() { nombre = "DNI" };
        //        TipoDocumento d2 = new TipoDocumento() { nombre = "RUC" };
        //        TipoDocumento d3 = new TipoDocumento() { nombre = "PASAPORTE" };
        //        TipoDocumento d4 = new TipoDocumento() { nombre = "CARNE DE EXTRANJERIA" };
        //        docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
        //        ViewBag.documentos = docs;

        //        ViewBag.departamentos = Utils.listarDepartamentos();
        //        ViewBag.provincias = Utils.listarProvincias(model.idDepartamento);
        //        ViewBag.distritos = Utils.listarDistritos(model.idDepartamento, model.idProvincia);

        //        return View(model);
        //    }catch( Exception e ) {
        //        log.Error("Edit - GET(EXCEPTION): ", e);
        //        ViewBag.results = "Ocurrió un error al intentar cargar el usuario";
        //        return View(new UsuarioBean());
        //    }
        //}

        //// POST: /Usuario/Edit/5
        //[HttpPost]
        //public ActionResult Edit(UsuarioBean usuariobean)
        //{
        //    try
        //    {
        //        //System.Diagnostics.Debug.WriteLine("Perfil de Usuario = " + usuariobean.idPerfilUsuario);
        //        //if (ModelState.IsValid)
        //        //{
        //        usuarioFac.actualizarUsuario(usuariobean);
        //        return RedirectToAction("Details", new { id = usuariobean.ID } );
        //    }
        //    catch(Exception e ) {
        //        log.Error("Edit - POST(EXCEPTION): ", e);
        //        ViewBag.results = "Ocurrió un error al intentar modificar el usuario";
        //        return View(usuariobean);
        //    }
        //    //}
        //    //return View();
        //}

        // GET: /Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var model = usuarioFac.getUsuario(id);
                return View(model);
            }
            catch( Exception e ){
                log.Error("Delete - GET(EXCEPTION): ", e);
                ViewBag.results = "Ocurrió un error al intentar cargar el usuario";
                return View( new UsuarioBean() );
            }
        }

        // POST: /Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                usuarioFac.eliminarUsuario(id);
                return RedirectToAction("List");
            }
            catch( Exception e ) {
                log.Error("Edit - POST(EXCEPTION): ", e);
                ViewBag.results = "Ocurrió un error al intentar eliminar el usuario";
                return View();
            }
        }

        #region List
        public ActionResult List()
        {
            var lstUsuarioVML = new List<UsuarioViewModelList>();
            try
            {
                var usuarios = usuarioFac.listarUsuarios();
                foreach (UsuarioBean usuario in usuarios)
                {
                    var usuarioVML = Mapper.Map<UsuarioBean, UsuarioViewModelList>(usuario);
                    usuarioVML.nombrePerfilUsuario = usuarioFac.getNombrePerfilUsuario(usuarioVML.idPerfilUsuario);
                    lstUsuarioVML.Add(usuarioVML);
                }
                return View(lstUsuarioVML);
            }
            catch (Exception ex)
            {
                log.Error("List - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(lstUsuarioVML);
            }
        }
        #endregion

        public ViewResult Buscar(string account , string nombre, string apPat, string apMat , string tipoDocumento , string nroDocumento ) {
            try
            {
                var model = usuarioFac.buscarUsuario(account, nombre, apPat, apMat, tipoDocumento, nroDocumento);
                List<TipoDocumento> docs = new List<TipoDocumento>();
                TipoDocumento d1 = new TipoDocumento() { nombre = "DNI" };
                TipoDocumento d2 = new TipoDocumento() { nombre = "RUC" };
                TipoDocumento d3 = new TipoDocumento() { nombre = "PASAPORTE" };
                TipoDocumento d4 = new TipoDocumento() { nombre = "CARNE DE EXTRANJERIA" };
                docs.Add(d1); docs.Add(d2); docs.Add(d3); docs.Add(d4);
                ViewBag.documentos = docs;
                return View(model);
            }
            catch( Exception e ) {
                log.Error("Buscar - POST(EXCEPTION): ", e);
                ViewBag.results = "Ocurrió un error al intentar buscar usuarios";
                return View( new List<UsuarioBean>() );
            }
        }

        //public JsonResult LoginResult()
        //{
        //    var usuario = usuarioFac.getUsuario(58);
        //    return new JsonResult() { Data = usuario , JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        [HttpPost]
        public JsonResult LoginResult(String user, String password)
        {
            var usuario = usuarioFac.getLogin(user, password);
            if (usuario != null && !usuario.estado.Equals( "ONLINE" ) )
            {
                FormsAuthentication.SetAuthCookie(user, false);
                usuarioFac.marcarOnline(usuario.ID);
            }
            return new JsonResult() { Data = usuario };
        }

        // GET: /Usuario/Create
        public ActionResult Registrar()
        {
            var usuarioVMC = new UsuarioViewModelCreate();
            try
            {
                usuarioVMC.Departamentos = Utils.listarDepartamentos();
                usuarioVMC.Documentos = new List<TipoDocumento>();
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "DNI" });
                usuarioVMC.Documentos.Add(new TipoDocumento() { nombre = "RUC" });
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
        public ActionResult Registrar(UsuarioViewModelCreate usuarioVMC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!usuarioFac.yaExisteUsuario(usuarioVMC.user_account))
                    {
                        var usuario = Mapper.Map<UsuarioViewModelCreate, UsuarioBean>(usuarioVMC);
                        usuario.idPerfilUsuario = new PerfilUsuarioDAO().getPerfilID("Cliente");
                        usuario.estado = "ACTIVO";
                        //usuario.idPerfilUsuario = new PerfilUsuarioFacade();
                        usuarioFac.registrarUsuario(usuario);
                        return RedirectToAction("Index" , "Home");
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

    }
}