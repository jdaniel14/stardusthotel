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
        ProveedorFacade proveedorFacade = new ProveedorFacade();
        
        public ViewResult Index()
        {
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
            
            proveedorFacade.RegistrarProveedor(proveedor);
            return RedirectToAction("Index");

        }

        public ActionResult ModificarProveedor(int id)
        {
          
            ProveedorBean item = proveedorFacade.GetProveedor(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(ProveedorBean item)
        {
           
            proveedorFacade.ActualizarProveedor(item);
            return RedirectToAction("Index");
        }
        public ActionResult DetallesProveedor(int id)
        {
         
            ProveedorBean item = proveedorFacade.GetProveedor(id);
            return View(item);
        }

        public ActionResult EliminarProveedor(int id)
        {
           
            proveedorFacade.EliminarProveedor(id);
            return RedirectToAction("Index");
        }

        public ActionResult BuscarProveedor()//string razonsocial, string contacto)
        {
            
            //return View(proveedorFacade.ListarProveedor(razonsocial, contacto));
            return View();
        }

        public ActionResult MostrarProveedor(ProveedorBean prov)
        {

            List<ProveedorBean> listaprov = proveedorFacade.ListarProveedor(prov.razonSocial, prov.contacto);
            return View(listaprov);
        }


    }
}

