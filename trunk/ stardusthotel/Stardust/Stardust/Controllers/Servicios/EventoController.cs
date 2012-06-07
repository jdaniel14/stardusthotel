using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;

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
            eventoFacade.RegistrarEvento(evento);
            return RedirectToAction("Index");

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
            return RedirectToAction("Index");
        }
        public ActionResult DetallesProveedor(int ID)
        {
            EventoBean item = eventoFacade.GetEvento(ID);
            return View(item);
        }

        public ActionResult Eliminar(int ID)
        {
            return View(eventoFacade.GetEvento(ID));
        }

        [HttpPost, ActionName("Eliminar")]
        public ActionResult DeleteConfirmed(int ID)
        {
            eventoFacade.EliminarEvento(ID);
            return RedirectToAction("../Home/Index");
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