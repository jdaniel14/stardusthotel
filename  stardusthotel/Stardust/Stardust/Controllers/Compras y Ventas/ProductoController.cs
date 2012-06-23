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


        public ViewResult Error(string error)
        {
            ViewData["Error de Conexión"] = "Error de Conexión";
            return View();
        }
        public ViewResult Index()
        {
            List<ProductoBean> prod = produc.ListarProducto("");
            
            return View(prod);
        }

        [HttpPost]
        public ViewResult Index(List<ProductoBean> prov)
        {
            return View(prov);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductoBean producto)
        {

            List<ProductoBean> prod = new List<ProductoBean>();
            prod = produc.ListarProducto(producto.nombre);

            if (prod.Count > 0)
            {
                ViewBag.error = "El producto ya existe";

                return View(producto);
            }
            else
            {
                string estadoconexion = produc.Registrarproducto(producto);

                if (estadoconexion == "Bien")
                {
                    return RedirectToAction("Buscar");
                }
                else
                {
                    return RedirectToAction("Error/" + estadoconexion);
                }
            }

        }

        public ActionResult Edit(int ID)
        {

            ProductoBean prod = produc.Getproducto(ID);

            if (prod.conexion == "Bien")
            {
                return View(prod);
            }
            else
            {
                return RedirectToAction("Error/" + prod.conexion);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(ProductoBean producto)
        {
            string conexion =produc.ActualizarProducto(producto);
            if (conexion == "Bien")
            {
                return RedirectToAction("Buscar");
            }
            else
            {
                return RedirectToAction("Error/" + conexion);
            }
        }


        public ActionResult Delete(int ID)
        {
            ProductoBean prod = produc.Getproducto(ID);
            if (prod.conexion == "Bien")
            {
                return View(prod);
            }
            else
            {
                return RedirectToAction("Error/" + prod.conexion);
            }
            

        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int ID)
        {
            string conexion=produc.Eliminarproducto(ID);
            if (conexion == "Bien")
            {
                return Json(new { me = "" });
            }
            else
            {
                return Json(new { me = "" });
            }
            

        }
        
        public ActionResult Buscar()
        {
            List<ProductoBean> prod = new List<ProductoBean>();
            ViewBag.estado = 0;
            return View(prod);
        }

        [HttpPost]
        public ActionResult Buscar(string nombre)
        {
            ViewBag.estado = 1;
            return View(produc.ListarProducto(nombre));
        }

        /*--------Asignar Productos a Almacen--------*/

        public ActionResult ListadeHoteles()
        {
            return View(hotelFac.getHoteles());
        }

        public ActionResult ListarProductosdeAlmacen(int id)//idhotel
        {
            ProductoXAlmacenBean prod;
            HotelBean hotel = hotelFac.getHotel(id);
            string conexion = "";

            int idalmacen = productosfacade.obteneralmacen(id);
            if (idalmacen == -1)
            {
                conexion = "Error";
                return RedirectToAction("Error/" + conexion);
                
            }
            else
            {
                List<ProductoBean> productos = produc.ListarProducto("");

                prod = productosfacade.obtenerlistadAlmacen(idalmacen);//lista de los productos en la tabla productoxalmacen

                prod.Hotel = hotel.nombre;
                prod.idhotel = id;
                prod.idalmacen = idalmacen;
                ////lista de productos en la tabla de productoxalmacen
                for (int i = 0; i < prod.listProdalmacen.Count; i++)
                {
                    ProductoBean producto = produc.Getproducto(prod.listProdalmacen[i].ID);
                    prod.listProdalmacen[i].nombre = producto.nombre;
                    prod.listProdalmacen[i].estado2 = false;
                }

                return View(prod);
            }

        }

        public ActionResult AsignarProductosaAlmacen(int idhotel)
        {
            ProductoXAlmacenBean productosalmacen = new ProductoXAlmacenBean();
           
            HotelBean hotel = hotelFac.getHotel(idhotel);
            int idalmacen = productosfacade.obteneralmacen(idhotel);
            string conexion="";
            if (idalmacen == -1)
            {
                conexion = "Error";
                return RedirectToAction("Error/" + conexion);
                
            }
            else
            {
                ProductoXAlmacenBean prod2 = productosfacade.obtenerlistadAlmacen(idalmacen); // de la tabla proveedorxproducto
                List<ProductoBean> productos = produc.ListarProducto("");

                productosalmacen.Hotel = hotel.nombre;
                productosalmacen.idalmacen = idalmacen;
                productosalmacen.idhotel = idhotel;
                int cantidad = 0;
                productosalmacen.listProdalmacen = new List<ProductoAlmacen>();
                for (int i = 0; i < productos.Count; i++)
                {
                    ProductoAlmacen prodProveedor = new ProductoAlmacen();

                    prodProveedor.nombre = productos[i].nombre;
                    prodProveedor.ID = productos[i].ID;
                    prodProveedor.estados = false;
                    for (int j = 0; j < prod2.listProdalmacen.Count; j++)
                        if (prodProveedor.ID == prod2.listProdalmacen[j].ID)
                        {
                            prodProveedor.estado2 = true;
                        }
                        else cantidad++;

                    productosalmacen.listProdalmacen.Add(prodProveedor);

                }

                return View(productosalmacen); 
            }
            
        }

        public ActionResult guardarproductosxAlmacen(ProductoXAlmacenBean prod)
        {
            List<HotelBean> hoteles = hotelFac.getHoteles();

            string conexion =productosfacade.RegistrarproductosxAlmacen(prod);
            if (conexion == "Bien")
            {
                return RedirectToAction("ListarProductosdeAlmacen/" + prod.idhotel, "Producto"); 
            }
            else
            {
                return RedirectToAction("Error/" + conexion);
            }
            
        }

        public ActionResult ModificarProductosAlmacen(ProductoXAlmacenBean prod)
        {

            return View(prod);
        }
        public ActionResult Guardarproductos2(ProductoXAlmacenBean prod)
        {
           // List<HotelBean> hoteles = hotelFac.getHoteles();

            for(int i=0;i<prod.listProdalmacen.Count;i++)
            {
                List<ProductoBean> productos=produc.ListarProducto(prod.listProdalmacen[i].nombre);
                prod.listProdalmacen[i].ID = productos[0].ID;
            }
            string conexion=productosfacade.modificarproductosxalmacen(prod);
            if (conexion == "Bien")
            {
                return RedirectToAction("ListarProductosdeAlmacen/" + prod.idhotel);
            }
            else
            {
                return RedirectToAction("Error/" + conexion);
            }
            
        }
        public ActionResult actualizarStock()
        {
            return View(hotelFac.getHoteles());
        }

        public ActionResult actualizarstock2(int id) //idhotel
        {
            ProductoXAlmacenBean productosalmacen = new ProductoXAlmacenBean();
            string conexion = "";
            HotelBean hotel = hotelFac.getHotel(id);
            int idalmacen = productosfacade.obteneralmacen(id);
            if (idalmacen == -1)
            {
                conexion = "Error";
                return RedirectToAction("Error/" + conexion);
                
            }
            else
            {
                ProductoXAlmacenBean prod2 = productosfacade.obtenerlistadAlmacen(idalmacen); // de la tabla proveedorxproducto
                List<ProductoBean> productos = produc.ListarProducto("");

                productosalmacen.Hotel = hotel.nombre;
                productosalmacen.idalmacen = idalmacen;
                productosalmacen.idhotel = id;
                productosalmacen.listProdalmacen = new List<ProductoAlmacen>();
                for (int i = 0; i < productos.Count; i++)
                {
                    ProductoAlmacen prodProveedor = new ProductoAlmacen();

                    prodProveedor.nombre = productos[i].nombre;
                    prodProveedor.ID = productos[i].ID;
                    prodProveedor.estados = false;
                    for (int j = 0; j < prod2.listProdalmacen.Count; j++)
                        if (prodProveedor.ID == prod2.listProdalmacen[j].ID)
                        {
                            prodProveedor.estado2 = true;
                            prodProveedor.stockactual = prod2.listProdalmacen[j].stockactual;
                        }

                    productosalmacen.listProdalmacen.Add(prodProveedor);
                }
                return View(productosalmacen);
            }
            
        }

        public ActionResult actualizarStock3 (ProductoXAlmacenBean prod)
        {
            string conexion=productosfacade.actualizarStock(prod);

            if (conexion == "Bien")
            {
                return RedirectToAction("actualizarStock");
            }
            else
            {
                conexion = "Error";
                return RedirectToAction("Error/" + conexion);
            }
            
        }       
    }
}
