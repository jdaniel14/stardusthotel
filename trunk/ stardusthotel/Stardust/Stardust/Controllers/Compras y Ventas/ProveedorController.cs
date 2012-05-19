using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;

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
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor("","");
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
            return RedirectToAction("Index");

        }

        public ActionResult ModificarProveedor(int id)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            ProveedorBean item = proveedorFacade.GetProveedor(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(ProveedorBean item)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.ActualizarProveedor(item);
            return RedirectToAction("Index");
        }
        public ActionResult DetallesProveedor(int id)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            ProveedorBean item = proveedorFacade.GetProveedor(id);
            return View(item);
        }

        public ActionResult EliminarProveedor(int id)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.EliminarProveedor(id);
            return RedirectToAction("Index");
        }

        public ActionResult BuscarProveedor()
        {
            return View();
        }

        public ActionResult MostrarProveedor(ProveedorBean prov)//string razon,string contacto)
        {

            ProveedorFacade proveedorFacade = new ProveedorFacade();
            List<ProveedorBean> listaprov = proveedorFacade.ListarProveedor(prov.razonSocial, prov.contacto);
            return View(listaprov);
        }

        public ActionResult MostrarProveedores(ProveedorBean item)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor(item.razonSocial,"");
            return View(listaProveedor);
        }

    }
}

