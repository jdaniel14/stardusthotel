using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;
using log4net;

namespace Stardust.Controllers
{
    public class ProveedorController : Controller 
    {
        //
        // GET: /Proveedores/
        private CadenaHotelDB db = new CadenaHotelDB();
        ProveedorFacade proveedorFacade = new ProveedorFacade();
        private static ILog log = LogManager.GetLogger(typeof(ProveedorController));
        
        public ViewResult Index()
        {            
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor("","");
            for (int i = 0; i < listaProveedor.Count; i++)
            {
                if (listaProveedor[i].estado == 1) listaProveedor[i].estado2 = "Activo";
                else listaProveedor[i].estado2 = "Inactivo";

            }
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
            if (proveedorFacade.existeproveedor(proveedor.razonSocial))
            {
                ViewBag.error1 = "El nombre proveedor ya existe";
                return View(proveedor);
            }
            else
            {
                if (proveedorFacade.existeproveedor(proveedor.ruc))
                {
                    ViewBag.error2 = "El Ruc ya existe";
                    return View(proveedor);
                }
                else
                {
                    proveedorFacade.RegistrarProveedor(proveedor);
                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult ModificarProveedor(int ID)
        {            
            ProveedorBean item = proveedorFacade.GetProveedor(ID);
            if (item.estado ==1 ) item.estado2="Activo";
            else item.estado2="Inactivo";
            
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(ProveedorBean item)
        {

            if (item.estado2 == "Activo") item.estado = 1;
            else item.estado = 0;

            proveedorFacade.ActualizarProveedor(item);
            return RedirectToAction("DetallesProveedor/" + item.ID, "Proveedor"); 
        }
        public ActionResult DetallesProveedor(int ID)
        {
            ProveedorBean item = proveedorFacade.GetProveedor(ID);
            if (item.estado == 1) item.estado2 = "Activo";
            else item.estado2 = "Inactivo";

            return View(item);
        }

        public ActionResult Delete(int ID)
        {
            return View(proveedorFacade.GetProveedor(ID));
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int ID)
        {
            proveedorFacade.EliminarProveedor(ID);
            //return RedirectToAction("Buscar");
            return Json(new { me = "" });
        }
        
        public ActionResult Buscar()
        {
            List<ProveedorBean> prov = new List<ProveedorBean>();
            ViewBag.estado = 0;
            return View(prov);
            
        }

        [HttpPost]
        public ActionResult Buscar(string razonsocial, string contacto)
        {

            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor(razonsocial, contacto);
            for (int i = 0; i < listaProveedor.Count; i++)
            {
                if (listaProveedor[i].estado == 1) listaProveedor[i].estado2 = "Activo";
                else listaProveedor[i].estado2 = "Inactivo";

            }
            ViewBag.estado = 1;
            return View(listaProveedor);

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
            prod.idproveedor = ID;
                return View(prod);
        }
        public ViewResult AsignarProductos(string nombre)
        {            
            ProductoxProveedorBean prod = new ProductoxProveedorBean();
            List< ProveedorBean> proveedor = proveedorFacade.ListarProveedor(nombre, "");
            
            ProductoxProveedorBean prod2 = proveedorFacade.obtenerlista(proveedor[0].ID); // de la tabla proveedorxproducto
            
            List<ProductoBean> productos = proveedorFacade.ListarProducto("");

            prod.Proveedor = nombre; //prov[0].razonSocial;
            prod.idproveedor = proveedor[0].ID;
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

            for (int i = 0; i < prod.listProdProv.Count; i++)
            {
                List<ProductoBean> productos = proveedorFacade.ListarProducto(prod.listProdProv[i].nombre);
                prod.listProdProv[i].ID = productos[0].ID;
            }
            //for (int i = 0; i < prod.listProdProv.Count; i++)
            //{
            //    prod.listProdProv[i].precio = Convert.ToDecimal(prod.listProdProv[i].precio2);
            //}
            proveedorFacade.RegistrarproductosxProveedor(idproveedor, prod);

            return RedirectToAction("ListarProductos/"+idproveedor, "Proveedor"); 
        }

        public ActionResult ModificarProductos(ProductoxProveedorBean prod) 
        {
            List<ProveedorBean> proveedor = proveedorFacade.ListarProveedor(prod.Proveedor, "");
            int idproveedor = proveedor[0].ID;

            ProductoxProveedorBean producto = proveedorFacade.obtenerlista(idproveedor);
            for (int i = 0; i < producto.listProdProv.Count(); i++)
            {
                for (int j = 0; j < prod.listProdProv.Count; j++)
                {
                    if (prod.listProdProv[j].ID == producto.listProdProv[i].ID)
                    {
                        prod.listProdProv[j].precio = producto.listProdProv[i].precio;

                    }

                }

            }

            //prod.listProdProv = producto.listProdProv;
            return View(prod);
        }
        public ActionResult Guardarproductos2(ProductoxProveedorBean prod)
        {
            List<ProveedorBean> proveedor = proveedorFacade.ListarProveedor(prod.Proveedor, "");
            int idproveedor = proveedor[0].ID;
            //for (int i = 0; i < prod.listProdProv.Count; i++)
            //{
            //    prod.listProdProv[i].precio = Convert.ToDecimal(prod.listProdProv[i].precio2) ;
            //}
            proveedorFacade.ModificarproductosxProveedor(idproveedor, prod);
            return RedirectToAction("ListarProductos/" + idproveedor, "Proveedor"); 
        }
        
        /**----- Pago de Proveedor-----*/

        public ActionResult PagoProveedor()
        {
            PagoProveedorBean pagoProveedor = new PagoProveedorBean();
            return View(pagoProveedor);
        }

        [HttpPost]
        public ActionResult PagoProveedor(PagoProveedorBean pago)
        {
            int ID = Convert.ToInt32(pago.ID);
            return RedirectToAction("ListarOC", new { id = ID });
        }

        public ActionResult ListarOC(int id)
        {
            OrdenCompras OC = new OrdenCompras();
            OC = proveedorFacade.ObtenerOC(id);
            OC.id = id;
            OC.nombre = proveedorFacade.GetNombre(id);
            return View(OC);
        }

        public ActionResult PagoContado(int id)
        {
            OrdenCompras OC = new OrdenCompras();
            OC = proveedorFacade.ListarOC(id);
            OC.id = id;
            return View(OC);
        }

        [HttpPost]
        public ActionResult PagoContado(OrdenCompras OC)
        {           
            if (OC.pagado == 0)
            {
                ViewBag.error = "El monto a pagar debe ser mayor a 0";
                OC.pagado = 0;
                return View(OC);
            }               
            else
            {                
                proveedorFacade.RegistrarPagoContado(OC);
                if (OC.pagado > OC.total)
                {
                    decimal saldo = OC.total - OC.pagado;
                    return RedirectToAction("Pago", new { id = saldo});
                }
                return RedirectToAction("PagoProveedor");
            } 
        }

        public ActionResult Pago(decimal id)
        {
            ViewBag.Mensaje = "Se ha registrado el pago con un cambio de: "+id;
            return View();
        }

        public ActionResult PagarCredito(int id)
        {
            OrdenCompras OC = new OrdenCompras();
            OC = proveedorFacade.ListarOC(id);
            OC.id = id;
            return View(OC);
        }

        [HttpPost]
        public ActionResult PagarCredito(OrdenCompras OC)
        {
            bool est = false;
            if (OC.interes == 0)
            {
                ViewBag.error = "El interes debe ser mayor a 0";
                est = true;
            }
            if (OC.numCuotas == 0)
            {
                ViewBag.error2 = "El numero de cuotas debe ser mayor a 0";
                est = true;
            }
            if (Convert.ToDecimal(OC.pagado) == 0)
            {
                ViewBag.error3 = "El monto a pagar debe ser mayor a 0";
                est = true;
            }
            if (est)
            {                
                return View(OC);
            }
            else
            {
                proveedorFacade.RegistrarPagoCredito(OC);
                return RedirectToAction("PagoProveedor");
            }
        }
    }
}

