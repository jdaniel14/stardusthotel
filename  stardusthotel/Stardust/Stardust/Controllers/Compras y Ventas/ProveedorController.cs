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

        public ActionResult ModificarProveedor(int ID)
        {            
            ProveedorBean item = proveedorFacade.GetProveedor(ID);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(ProveedorBean item)
        {            
            proveedorFacade.ActualizarProveedor(item);
            return RedirectToAction("Index");
        }
        public ActionResult DetallesProveedor(int ID)
        {            
            ProveedorBean item = proveedorFacade.GetProveedor(ID);
            return View(item);
        }

        public ActionResult Eliminar(int ID)
        {
            return View(proveedorFacade.GetProveedor(ID));
        }

        [HttpPost, ActionName("Eliminar")]
        public ActionResult DeleteConfirmed(int ID)
        {
            proveedorFacade.EliminarProveedor(ID);
            return RedirectToAction("../Home/Index");
        }

        public ActionResult Buscar(string razonsocial, string contacto)
        {            
            return View(proveedorFacade.ListarProveedor(razonsocial, contacto));
          
        }

        public ActionResult MostrarProveedor(ProveedorBean prov)
        {            
            List<ProveedorBean> listaprov = proveedorFacade.ListarProveedor(prov.razonSocial, prov.contacto);
            return View(listaprov);
        }

        /* Asignar Productos a Proveedor */

        public ActionResult ListarProductos(int ID)
        {
            ProductoxProveedorBean prod;
            ProveedorBean prov = proveedorFacade.GetProveedor(ID);

            List<ProductoBean> productos = proveedorFacade.ListarProducto("");

            prod = proveedorFacade.obtenerlista(ID);//lista de los productos en la tabla productoxproveedor
            prod.Proveedor = prov.razonSocial;

            //lista de productos en la tabla de productoxproveedor
            for (int i = 0; i < prod.listProdProv.Count; i++)
            {
                ProductoBean producto=proveedorFacade.Getproducto(prod.listProdProv[i].ID);
                prod.listProdProv[i].nombre = producto.nombre;
                prod.listProdProv[i].estado2 = false;
            }

                return View(prod);
        }
        public ViewResult AsignarProductos(string nombre)
        {            
            ProductoxProveedorBean prod = new ProductoxProveedorBean();
            List< ProveedorBean> proveedor = proveedorFacade.ListarProveedor(nombre, "");
            
            ProductoxProveedorBean prod2 = proveedorFacade.obtenerlista(proveedor[0].ID); // de la tabla proveedorxproducto
            
            List<ProductoBean> productos = proveedorFacade.ListarProducto("");

            prod.Proveedor = nombre; //prov[0].razonSocial;
            
            prod.listProdProv = new List<ProductoProveedor>();
            for (int i = 0; i < productos.Count; i++)
            {
                ProductoProveedor prodProveedor = new ProductoProveedor();

                prodProveedor.nombre = productos[i].nombre;
                prodProveedor.ID = productos[i].ID;
                prodProveedor.estados = false;
                for (int j = 0; j < prod2.listProdProv.Count; j++)
                    if (prodProveedor.ID == prod2.listProdProv[j].ID) prodProveedor.estado2 = true;
         
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

        public ActionResult ModificarProductos(ProductoxProveedorBean prod) 
        {
            List<ProveedorBean> proveedor = proveedorFacade.ListarProveedor(prod.Proveedor, "");
            int idproveedor = proveedor[0].ID;
            
            return View(prod);
        }
        public ActionResult Guardarproductos2(ProductoxProveedorBean prod)
        {
            List<ProveedorBean> proveedor = proveedorFacade.ListarProveedor(prod.Proveedor, "");
            int idproveedor = proveedor[0].ID;
            proveedorFacade.ModificarproductosxProveedor(idproveedor, prod);
            return RedirectToAction("Index");
        }


        /**----- Pago de Proveedor-----*/



    }
}

