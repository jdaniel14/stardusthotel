using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportManagement;
using Stardust.Models;

namespace Stardust.Controllers.Compras_y_Ventas
{
    public class ReporteController : PdfViewController
    {
        //
        // GET: /Reporte/

        ProveedorFacade proveedorFacade = new ProveedorFacade();

        public ActionResult Proveedor()
        {
            Proveed prov = new Proveed();
            prov.list = proveedorFacade.GetList();
            return View(prov);
        }

        [HttpPost]
        public ActionResult Proveedor(Proveed prov)
        {
            int ID = Convert.ToInt32(prov.id);
            return RedirectToAction("ListaOC", new { id = ID});
        }

        public ActionResult ListaOC(int id)
        {            
            return View(proveedorFacade.GetOC(id));
            //return RedirectToAction("ListarOC");
        }

        public ActionResult Nada(int id)
        {
            return this.ViewPdf("", "Reporte", proveedorFacade.GetOC(id));
        }
    }
}
