using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using Stardust.Models.Servicios;

namespace Stardust.Controllers
{
    public class EventoController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();
        EventoFacade eventoFacade = new EventoFacade();

        public ViewResult Index()
        {
            List<EventoBean> listaEvento = eventoFacade.ListarEvento("", "", "");
            return View(listaEvento);
        }

        [HttpPost]
        public ViewResult Index(List<EventoBean> listaEvento)
        {
            return View(listaEvento);
        }

        public ActionResult RegistrarEvento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEvento(EventoBean evento)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
           // if ((DateTime.Parse(evento.fechaIni, provider, DateTimeStyles.AssumeLocal) >= DateTime.Now))
                
            //{
                //if (DateTime.Parse(evento.fechaIni, provider, DateTimeStyles.AssumeLocal)<= DateTime.Parse(evento.fechaFin,provider,DateTimeStyles.AssumeLocal)){
                    eventoFacade.RegistrarEvento(evento);
                    return RedirectToAction("Buscar");
               // else {

                 //   ViewBag.error="La fecha Inicial debe ser menor que la Final";
                   // return View();
                //}
            //}
            //else{
              //  ViewBag.error="La fecha Inicial o Final debe ser mayor que el dia de hoy";
                //    return View();

//            }

            

        }

        public ActionResult ModificarEvento(int ID)
        {
            EventoBean item = eventoFacade.GetEvento(ID);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarEvento(EventoBean item)
        {
            eventoFacade.ActualizarEvento(item);
            //return RedirectToAction("Index");
            return RedirectToAction("DetallesEvento/" + item.ID, "Evento");
        }
        public ActionResult DetallesEvento(int ID)
        {
            EventoBean item = eventoFacade.GetEvento(ID);
            return View(item);
        }

        public ActionResult Delete(int ID)
        {
            return View(eventoFacade.GetEvento(ID));
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int ID)
        {
            eventoFacade.EliminarEvento(ID);
            //return RedirectToAction("../Home/Index");
            return Json(new { me = "" });
        }

       public ActionResult Buscar(string nombre, string fechaini, string fechafin)
        {
            return View(eventoFacade.ListarEvento(nombre,fechaini,fechafin));

        }

        public ActionResult MostrarProveedor(EventoBean evento)
        {
            List<EventoBean> listaeve = eventoFacade.ListarEvento(evento.nombre, evento.fechaIni, evento.fechaFin);
            return View(listaeve);
        }

        

    }
}