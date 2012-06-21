using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportManagement;
using Stardust.Models;

namespace Stardust.Controllers.Administracion
{
    public class ReporteAsistenciaController : PdfViewController
    {
        EmpleadoFacade empleadoFac = new EmpleadoFacade();

        public ActionResult indice()
        {
            ViewBag.empleados = empleadoFac.listarEmpleados();
            return View();
        }

        [HttpPost]
        public ActionResult indice(EmpleadoBean empleado)
        {
            int codigoempleado = empleado.ID;
            // empleadoFac.listarHorario(codigoempleado);
           // return RedirectToAction("Lista", new { id = codigoempleado });
            return this.ViewPdf("", "Reporte", empleadoFac.listarHorario(codigoempleado));
        }

    }
}