using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class ProveedorController : Controller
    {
        //
        // GET: /Proveedores/
        private CadenaHotelDB db = new CadenaHotelDB();
        public ViewResult Index()
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor("");
            return View(listaProveedor);
        }

        [HttpPost]
        public ViewResult Index(List<ProveedorBean> listaProveedor)
        {
            return View(listaProveedor);
        }

        public ActionResult RegistrarProveedor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarProveedor(ProveedorBean proveedor)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.RegistrarProveedor(proveedor);
            return RedirectToAction("../Home/Index");

        }

        public ActionResult ModificarProveedor(int idProveedor)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            ProveedorBean item = proveedorFacade.GetProveedor(idProveedor);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(ProveedorBean item)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.ActualizarProveedor(item);
            return RedirectToAction("MostrarProveedores");
        }

        public ActionResult EliminarProveedor(int idProveedor)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.EliminarProveedor(idProveedor);
            return RedirectToAction("MostrarProveedores");
        }

        public ActionResult BuscarProveedor()
        {
            return View();
        }

        public ActionResult MostrarProveedores(ProveedorBean item)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor(item.razonSocial);
            return View(listaProveedor);
        }
    }
}

