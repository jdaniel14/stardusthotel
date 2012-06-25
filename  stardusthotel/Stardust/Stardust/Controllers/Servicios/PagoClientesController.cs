using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stardust.Models;
using Stardust.Models.Servicios;
using System.Web.Mvc;
using ReportManagement;
using log4net;

namespace Stardust.Controllers.Servicios
{
    public class PagoClientesController : PdfViewController
    {
        PagoFacade pagoFacade = new PagoFacade();
        private static ILog log = LogManager.GetLogger(typeof(PagoClientesController));


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult checkOut()
        {
            return View();
        }

        [HttpPost]
        public JsonResult registroCheckOut(RequestCheckOut request)
        {
            ReservaCheckOut res = pagoFacade.GetReserva(request.idReserva);
            System.Diagnostics.Debug.WriteLine("NOMBRE DEL CHECK OUT : " + res.nombre);
            return Json(res);
        }

        [HttpPost]
        public JsonResult registrarCheckOut(RequestCheckOut request)
        {
            MensajeBean me = pagoFacade.RegistrarCheckOut(request.idReserva);
            return Json(me);
        }
    
        [HttpPost]
        public JsonResult PagoAdelantado(RequestPagoAde request)
        {
            PagoAdelantadoBean res = new PagoAdelantadoBean();
            res = pagoFacade.PagoAdelantado(request);
            return Json(res);
        }

        [HttpPost]
        public JsonResult RegistrarPagoAdelantado(RequestRegPago request)
        {
            MensajeBean mensaje = pagoFacade.RegistrarPagoAdelantado(request);
            return Json(mensaje);
        }

        public ActionResult PagarInicial()
        {
            return View();
        }

        public ActionResult GenerarDocumento(int id)
        {
            ReservaCheckOut reserva = pagoFacade.GetReserva2(id);

            if(reserva.tipoDoc.Equals("DNI"))
                return this.ViewPdf("Boleta", "Documento", reserva);
            else
                return this.ViewPdf("Factura", "Documento", reserva);
        }
    }
}
