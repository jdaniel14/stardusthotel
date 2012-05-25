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

namespace Stardust.Controllers
{
    public class ProductoController : Controller
    {
       
        /*--------Producto--------*/

        private CadenaHotelDB db = new CadenaHotelDB();
        ProveedorFacade produc = new ProveedorFacade();

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



    }
}
