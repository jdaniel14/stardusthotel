using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using Stardust.Models.Servicios;

namespace Stardust.Controllers.Servicios
{
    public class ReservarHabitacionController : Controller
    {
        //
        // GET: /ReservarHabitacion/
        FacadeReservas facadeReservas = new FacadeReservas();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult infoReserva(String idHotel)
        {
            
            /*TipoHabXHotel tip = new TipoHabXHotel();               

            tip.idTipoHab = 1;
            tip.nombreTipoHab = "Suite";
            tip.numPos = 5;*/
                
                
            List<TipoHabXHotel> lista = facadeReservas.listaDisponibles("2");
            //List<TipoHabXHotel> lista = new List<TipoHabXHotel>();

            //lista.Add(tip);
            var res = lista;
            return new JsonResult() { Data = res };
        }

    }
}
