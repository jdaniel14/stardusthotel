using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class OrdenCompraController : Controller
    {
        public ComprasFacade comprasFacade = new ComprasFacade();
        public ProveedorFacade proveedorFacade = new ProveedorFacade(); 
        
        /*--------Orden de Compra----------*/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buscar()
        {
            return View();
        }

        
        public ActionResult Buscar2(string nombre, string fecha1, string fecha2)
        {

            
            
            return View(comprasFacade.buscarOrdenes(nombre, fecha1, fecha2));
        }

        public ActionResult Lista(OrdenCompraBean orden)
        {

            return View(orden);
        }


        public ActionResult Registrar()
        {
            OrdenCompraBean ordenCompra = new OrdenCompraBean();
            return View(ordenCompra);
        }

        [HttpPost]
        public ActionResult Registrar(OrdenCompraBean ordenCompra)
        {
            int ID = Convert.ToInt32(ordenCompra.idProv);
            return RedirectToAction("Registrar2", new { id = ID});
        }

        public ViewResult Registrar2(int id)
        {
            OrdenProducto prod = new OrdenProducto();
            ProveedorBean prov = proveedorFacade.GetProveedor(id);
            List<Producto> produ = new List<Producto>();

            ProductoxProveedorBean productos = proveedorFacade.obtenerlista(id);

            for (int i = 0; i < productos.listProdProv.Count; i++)
            {
                Producto producto = comprasFacade.GetProducto(productos.listProdProv.ElementAt(i).ID);
                if (producto != null)
                {
                    producto.precio = productos.listProdProv.ElementAt(i).precio;
                    produ.Add(producto);//lista de los productos en la tabla productoxproveedor                
                }
            }

            prod.listaProducto = produ;
            prod.proveedor = prov.razonSocial;
            prod.id = id;

            //lista de productos en la tabla de productoxproveedor
            for (int i = 0; i < prod.listaProducto.Count; i++)
            {
                ProductoBean producto = proveedorFacade.Getproducto(Convert.ToInt32(prod.listaProducto[i].id));
                prod.listaProducto[i].Nombre = producto.nombre;
            }

            return View(prod);
        }

        
        public ActionResult RegistrarOC(OrdenProducto producto)
        {
            comprasFacade.GuardarOrdenCompra(producto);
            return RedirectToAction("Buscar");
        }
        /*--------Notas de Entrada----------*/


    }
}
