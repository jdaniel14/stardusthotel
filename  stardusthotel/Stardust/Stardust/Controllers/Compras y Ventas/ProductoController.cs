using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;
using Stardust.Controllers;

namespace Stardust.Controllers
{
    public class ProductoController : Controller
    {
       
        /*--------Producto--------*/

        private CadenaHotelDB db = new CadenaHotelDB();
        ProveedorFacade produc = new ProveedorFacade();
        HotelFacade hotelFac = new HotelFacade();
        ComprasFacade productosfacade = new ComprasFacade();


        public ViewResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductoBean producto)
        {
            produc.Registrarproducto(producto);
            return RedirectToAction("Buscar");//("../Home/Index");
        }

        public ActionResult Edit(int ID)
        {
            return View(produc.Getproducto(ID));
        }

        [HttpPost]
        public ActionResult Edit(ProductoBean producto)
        {
            produc.ActualizarProducto(producto);
            return RedirectToAction("Buscar");
        }

        public ActionResult Delete(int ID)
        {
            return View(produc.Getproducto(ID));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int ID)
        {
            produc.Eliminarproducto(ID);
            return RedirectToAction("Buscar");
        }

        public ActionResult Buscar(string nombre)
        {
            return View(produc.ListarProducto(nombre));
        }

        /*--------Asignar Productos a Almacen--------*/

        public ActionResult ListadeHoteles()
        {
            return View(hotelFac.listarHoteles());
        }

        public ActionResult ListarProductosdeAlmacen(int id)
        {
            ProductoXAlmacenBean prod;
            HotelBean hotel = hotelFac.getHotel(id);
            //ProveedorBean prov = proveedorFacade.GetProveedor(ID);
           
            List<ProductoBean> productos = produc.ListarProducto("");

            prod = productosfacade.obtenerlistadAlmacen(id);//lista de los productos en la tabla productoxalmacen

            prod.Hotel = hotel.nombre;
            prod.idhotel = id;
            ////lista de productos en la tabla de productoxalmacen
            for (int i = 0; i < prod.listProdalmacen.Count; i++)
            {
                ProductoBean producto = produc.Getproducto(prod.listProdalmacen[i].ID);
                prod.listProdalmacen[i].nombre = producto.nombre;
                prod.listProdalmacen[i].estado2 = false;
            }

            return View(prod);
        }

        public ViewResult AsignarProductosaAlmacen(int id)
        {
            ProductoXAlmacenBean productosalmacen = new ProductoXAlmacenBean();
           
            //List<HotelBean> hoteles = hotelFac.listarHoteles();
            //int id;
            //for (int i = 0; i < hoteles.Count; i++)
            //{
            //    if (hoteles[i].nombre == nombre)
            //    {
            //        id = hoteles[i].ID;
                    
            //        break;
            //    }
            //}
            HotelBean hotel = hotelFac.getHotel(id);
            ProductoXAlmacenBean prod2 = productosfacade.obtenerlistadAlmacen(id); // de la tabla proveedorxproducto
            List<ProductoBean> productos = produc.ListarProducto("");

            productosalmacen.Hotel = hotel.nombre; //prov[0].razonSocial;

            productosalmacen.listProdalmacen= new List<ProductoAlmacen>();
            for (int i = 0; i < productos.Count; i++)
            {
                ProductoAlmacen prodProveedor = new ProductoAlmacen();

                prodProveedor.nombre = productos[i].nombre;
                prodProveedor.ID = productos[i].ID;
                prodProveedor.estados = false;
                for (int j = 0; j < prod2.listProdalmacen.Count; j++)
                    if (prodProveedor.ID == prod2.listProdalmacen[j].ID) prodProveedor.estado2 = true;

                productosalmacen.listProdalmacen.Add(prodProveedor);

            }


            return View(productosalmacen);
        }

        public ActionResult guardarproductosxAlmacen(ProductoXAlmacenBean prod)
        {
            List<HotelBean> hoteles = hotelFac.listarHoteles();
            //int idhotel;
            //for (int i = 0; i < hoteles.Count; i++)
            //{
            //    if (hoteles[i].nombre == prod.Hotel) idhotel = hoteles[i].ID;
            //}
            
            //List<ProveedorBean> proveedor = produc.ListarProveedor(prod.Proveedor, "");
            //int idhotel = proveedor[0].ID;

            productosfacade.RegistrarproductosxAlmacen(prod);

            return RedirectToAction("Index");
        }

        public ActionResult ModificarProductosAlmacen(ProductoXAlmacenBean prod)
        {
            //List<ProveedorBean> proveedor = prod.ListarProveedor(prod.Proveedor, "");
            //int idproveedor = proveedor[0].ID;

            return View(prod);
        }
        public ActionResult Guardarproductos2(ProductoXAlmacenBean prod)
        {
            List<HotelBean> hoteles = hotelFac.listarHoteles();
            //int idhotel;
            //for (int i = 0; i < hoteles.Count; i++)
            //{
            //    if (hoteles[i].nombre == prod.Hotel)
            //    {
            //        int idhotel = hoteles[i].ID;
            //    }
            //}
            //List<ProveedorBean> proveedor = proveedorFacade.ListarProveedor(prod.Proveedor, "");
            //int idproveedor = proveedor[0].ID;
            //proveedorFacade.ModificarproductosxProveedor(idproveedor, prod);
            productosfacade.modificarproductosxalmacen(prod);
            return RedirectToAction("Index");
        }

       
    }
}
