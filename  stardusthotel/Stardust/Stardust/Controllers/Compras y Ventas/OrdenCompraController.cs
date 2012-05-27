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
        
        /*--------Orden de Compra----------*/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult buscar()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            OrdenCompraBean ordenCompra = new OrdenCompraBean();
            return View(ordenCompra);
        }

        public ActionResult GetProducto(int id)
        {
            //SelectList productoList = new SelectList(comprasFacade.GetProducto(id), "id", "Nombre");
            return View(comprasFacade.GetProducto(id));
        }


        /*--------Notas de Entrada----------*/


    }
}
