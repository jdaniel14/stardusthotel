using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;
//using Stardust.Models.Compras_y_Ventas.Proveedor;

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

        public ActionResult Buscar(string razonsocial, string contacto)
        {            
            return View(proveedorFacade.ListarProveedor(razonsocial, contacto));
            //return View();
        }

        public ActionResult MostrarProveedor(ProveedorBean prov)
        {            
            List<ProveedorBean> listaprov = proveedorFacade.ListarProveedor(prov.razonSocial, prov.contacto);
            return View(listaprov);
        }
        public ActionResult ListarProductos(int id)
        {
            ProductoxProveedorBean prod;
            ProveedorBean prov = proveedorFacade.GetProveedor(id);

            List<ProductoBean> productos = proveedorFacade.ListarProducto("");

            prod = proveedorFacade.obtenerlista(id);
            prod.Proveedor = prov.razonSocial;

            //lista de productos en la tabla de productoxproveedor
            

            return View(prod);
        }
        public ActionResult AsignarProductos(string nombre)
        {            
            ProductoxProveedorBean prod = new ProductoxProveedorBean();
            
            
            List<ProductoBean> productos = proveedorFacade.ListarProducto("");

            prod.Proveedor = nombre; //prov[0].razonSocial;
            
            prod.listProdProv = new List<ProductoProveedor>();
            for (int i = 0; i < productos.Count; i++)
            {
                ProductoProveedor prodProveedor = new ProductoProveedor();

                prodProveedor.nombre = productos[i].nombre;
                prodProveedor.ID = productos[i].ID;
                prodProveedor.estados = false;

                prod.listProdProv.Add(prodProveedor);
            }
            
            return View(prod);
        }

        public ActionResult guardarproductosxProveedor(ProductoxProveedorBean prod)
        {
            List<ProveedorBean> proveedor = proveedorFacade.ListarProveedor(prod.Proveedor, "");
            int idproveedor = proveedor[0].ID;
            proveedorFacade.RegistrarproductosxProveedor(idproveedor, prod);
            return RedirectToAction("Index"); 
        }

        //public ActionResult guardarproductosxProveedor(ProductoxProveedorBean prod)
        //{
        //    List <ProveedorBean> proveedor = proveedorFacade.ListarProveedor(prod.Proveedor, "");
        //    int idproveedor = proveedor[0].ID;
        //    proveedorFacade.RegistrarproductosxProveedor(idproveedor, prod);
        //    return RedirectToAction("Index"); 
        //}
    }
}

