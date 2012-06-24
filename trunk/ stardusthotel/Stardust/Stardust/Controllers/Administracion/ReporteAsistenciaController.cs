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
        public ActionResult indice(EmpleadoBean emp)
        {

          //  try
          //  {
                ViewBag.empleados = empleadoFac.listarEmpleados();
                if (emp.Value == true)
                {

                    ReporteAllEmpleados reporte = new ReporteAllEmpleados();

                    reporte.allempleados = empleadoFac.listartodoempleado();
                    var model = reporte;
                    return this.ViewPdf("", "ReporteAsistencia", model);

                }
                else
                {

                    int codigoempleado = emp.ID;
                    EmpleadoBean empleado = empleadoFac.getEmpleado(codigoempleado);
                    ReporteEmpleado RE = new ReporteEmpleado();
                    RE.empleado = empleado;

                    RE.horarios = empleadoFac.listarReporte(codigoempleado);
                    // var model = empleadoFac.guardartodo(codigoempleado);



                    var model = RE;

                    return this.ViewPdf("", "ReporteAsistencia1", model);

                }
            /*
            }
            catch
            {
                ViewBag.empleados = empleadoFac.listarEmpleados();
                ViewBag.error = "Error al tratar de generar el reporte -intetarlo en otro momento";
                return View();
            }
            */
        }




    }
}