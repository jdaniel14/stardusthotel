using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using log4net;

namespace Stardust.Controllers
{ 
    public class PromocionesController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();
        public PromocionFacade promocionFacade = new PromocionFacade();
        List<string> param = new List<string>();
        private static ILog log = LogManager.GetLogger(typeof(PromocionesController));
        

        /*-----Promociones---------*/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buscar()
        {
            ViewBag.Hotel = promocionFacade.GetHoteles(1);
            ViewBag.Tipo = promocionFacade.GetTipo(1);
            //ViewData["Tipo"] = promociones.getTipo();
            //ViewData["Hotel"] = promociones.getHoteles();
            return View();

        }

        [HttpPost]
        public ActionResult Buscar(PromocionBean promocion)
        {

            int ID = promocion.tipo + promocion.idhotel;//(hotel*10)+id;
            return RedirectToAction("Detalle",new{id = ID});
        }

        public ActionResult Detalle(int id) 
        {
            int Id, hotel;
            if (id > 100)
            {
                Id = id / 100;
                hotel = id % 100;
            }
            else
            {
                Id = id / 10;
                hotel = id % 10;
            }
            return View(promocionFacade.ListarPromocion(Id,hotel));
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(FormCollection form)
        {
            String id = form["categoria"];
            if (id.Equals("1"))
                return RedirectToAction("RegistrarDia");
            else
                return RedirectToAction("RegistrarAdelanto");
        }

        public ActionResult RegistrarDia()
        {
            ViewBag.Hotel = promocionFacade.GetHoteles(2);
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarDia(PromocionBean promocion)
        {
            promocion.tipoDescuento = 1;

            string b = promocion.porcDescontar2.Remove(2, 1);
            promocion.porcDescontar = Convert.ToInt32(b);
            

            promocion.razon = promocion.dias;

            if (promocion.dias == 0)
            {
                ViewBag.Hotel = promocionFacade.GetHoteles(2);
                ViewBag.Error = "El numero de dias debe ser mayor a 0";
                return View(promocion);
            }

            promocionFacade.RegistrarPromocion(promocion);
            return RedirectToAction("Buscar");
        }

        public ActionResult RegistrarAdelanto()
        {
            ViewBag.Hotel = promocionFacade.GetHoteles(2);
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarAdelanto(PromocionBean promocion)
        {
            promocion.tipoDescuento = 2;

            string a = promocion.adelanto2.Remove(2, 1);
            string b = promocion.porcDescontar2.Remove(2, 1);
            promocion.porcDescontar = Convert.ToInt32(b);
            promocion.adelanto = Convert.ToInt32(a);
            promocion.razon = promocion.adelanto;

            promocionFacade.RegistrarPromocion(promocion);
            return RedirectToAction("Buscar");
        }
 
        public ActionResult Edit(int id)
        {
            ViewBag.Hotel = promocionFacade.GetHoteles(2);
            ViewBag.Tipo = promocionFacade.GetTipo(2);
            return View(promocionFacade.GetPromocion(id));
        }

        [HttpPost]
        public ActionResult Edit(PromocionBean promocion)
        {
            promocion.tipoDescuento = Convert.ToInt32(promocion.tipo);
            promocionFacade.ActualizarPromocion(promocion);
            return RedirectToAction("Buscar");
        }

        public ActionResult Detalles(int id)
        {
            return View(promocionFacade.GetPromocion(id));
        }


        public ActionResult Delete(int ID)
        {
            return View(promocionFacade.GetPromocion(ID));
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            promocionFacade.EliminarPromocion(id);
            return Json(new { me = "" });
        }
    }
}