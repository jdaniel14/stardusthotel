using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{ 
    public class PromocionesController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();
        public PromocionFacade promocionFacade = new PromocionFacade();
        List<string> param = new List<string>();

        /*-----Promociones---------*/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buscar()
        {
            PromocionBean promociones = new PromocionBean(1);
            //ViewData["Tipo"] = promociones.getTipo();
            //ViewData["Hotel"] = promociones.getHoteles();
            return View(promociones);
            //PromocionBean promociones = db.Promociones.Find(id);
        }

        [HttpPost]
        public ActionResult Buscar(PromocionBean promocion)
        {
            //int id = Convert.ToInt32(promocion.ID);
            //int hotel = Convert.ToInt32(promocion.tipo);
            int ID = Convert.ToInt32(promocion.tipo + promocion.ID);//(hotel*10)+id;
            return RedirectToAction("Detalle",new{id = ID});
        }

        public ActionResult Detalle(int id) 
        {
            int Id, hotel;
            if ((id % 100) == 0)
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
            PromocionBean promocion = new PromocionBean(2);
            return View(promocion);
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
            PromocionBean promocion = new PromocionBean(2);
            return View(promocion);
        }

        [HttpPost]
        public ActionResult RegistrarDia(PromocionBean promocion)
        {
            promocion.tipoDescuento = 1;
            promocion.razon = promocion.dias;
            promocionFacade.RegistrarPromocion(promocion);
            return RedirectToAction("Buscar");
        }

        public ActionResult RegistrarAdelanto()
        {
            PromocionBean promocion = new PromocionBean(2);
            return View(promocion);
        }

        [HttpPost]
        public ActionResult RegistrarAdelanto(PromocionBean promocion)
        {
            promocion.tipoDescuento = 2;
            promocion.razon = promocion.adelanto;
            promocionFacade.RegistrarPromocion(promocion);
            return RedirectToAction("Buscar");
        }
 
        public ActionResult Edit(int id)
        {
            return View(promocionFacade.GetPromocion(id));
        }

        [HttpPost]
        public ActionResult Edit(PromocionBean promocion)
        {
            promocion.tipoDescuento = Convert.ToInt32(promocion.tipo);
            promocionFacade.ActualizarPromocion(promocion);
            return RedirectToAction("../Home/Index");
        }

        public ActionResult Detalles(int id)
        {
            return View(promocionFacade.GetPromocion(id));
        }

        public ActionResult Delete(int id)
        {
            return View(promocionFacade.GetPromocion(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            promocionFacade.EliminarPromocion(id);
            return RedirectToAction("../Home/Index");
        }


    }
}