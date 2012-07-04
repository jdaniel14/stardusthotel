using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using Stardust.Models.Servicios;
using log4net;

namespace Stardust.Controllers.Servicios
{
    public class ReservarAmbienteController : Controller
    {
        //
        // GET: /ReservarAmbiente/
        AmbienteFacade ambienteFacade = new AmbienteFacade();
        private static ILog log = LogManager.GetLogger(typeof(ReservarAmbienteController));
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reservar()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ConsultarAmbientesDisponibles(RequestResHab request)
        {
            ResAmbRequest response = ambienteFacade.ConsultarAmbientesDisponibles(request.idHotel, request.fechaIni, request.fechaFin);
            return Json(response);
        }

        [HttpPost]
        public JsonResult ResgitrarEventoYAmbientes(RegAmbienteEventoBean registro) {
            MensajeBean mensaje = ambienteFacade.RegistrarEventoYAmbientes(registro);
            return Json(mensaje);
        }

        public ActionResult CheckInAmbiente()
        {
            return View();
        }

        public ActionResult CheckOutAmbiente()
        {
            return View();
        }
    }
}
