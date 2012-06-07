using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Text.RegularExpressions;

namespace Stardust.Controllers
{ 
    public class HotelController : Controller
    {
        private CadenaHotelDB db = new CadenaHotelDB();
        HotelFacade hotelFac = new HotelFacade();

        //
        // GET: /Hotel/

        public ViewResult Index()
        {
            return View(hotelFac.listarHoteles());
        }

        //
        // GET: /Hotel/Details/5

        public ViewResult Details(int id)
        {
            return View( hotelFac.getHotel( id ) );
        }

        //
        // GET: /Hotel/Create

        public ActionResult Create()
        {
            Utils utils = new Utils();
            ViewBag.departamentos = utils.listarDepartamentos();
            ViewBag.provincias = new List<Provincia>();
            ViewBag.distritos = new List<Distrito>();

            HotelBean hotel = new HotelBean();
            hotel.Departamentos = utils.listarDepartamentos();
            return View(hotel);
        }

        public ActionResult getProvincias(int idDepartamento)
        {
            return Json(new Utils().listarProvincias(idDepartamento), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getDistritos(int idDepartamento, int idProvincia)
        {
            return Json(new Utils().listarDistritos(idDepartamento, idProvincia), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Hotel/Create

        [HttpPost]
        public ActionResult Create(HotelBean hotel)
        {
            if (ModelState.IsValid)
            {
                hotelFac.registrarHotel(hotel);
                return RedirectToAction("List");
            }
            return View(hotel);
        }

        //[HttpPost]
        //public ActionResult Create(HotelBean hotelbean)
        //{
        //    if (ModelState.IsValid)
        //    //hotelbean.idDepartamento != 0 && hotelbean.idProvincia != 0 && hotelbean.idDistrito != 0
        //    {
        //        hotelFac.registrarHotel(hotelbean);
        //        return RedirectToAction("List");
        //    }
        //    else if (hotelbean.idDepartamento != 0 && hotelbean.idProvincia != 0) // && hotelbean.idDistrito == 0
        //    {
        //        hotelbean.idDistrito = 0;
        //        Utils utils = new Utils();
        //        ViewBag.departamentos = utils.listarDepartamentos();
        //        ViewBag.provincias = utils.listarProvincias(hotelbean.idDepartamento);
        //        ViewBag.distritos = utils.listarDistritos(hotelbean.idDepartamento, hotelbean.idProvincia);
        //        return View(hotelbean);
        //    }
        //    else if (hotelbean.idDepartamento != 0) // hotelbean.idProvincia == 0 && hotelbean.idDistrito == 0
        //    {
        //        hotelbean.idProvincia = hotelbean.idDistrito = 0;
        //        Utils utils = new Utils();
        //        ViewBag.departamentos = utils.listarDepartamentos();
        //        ViewBag.provincias = utils.listarProvincias(hotelbean.idDepartamento);
        //        ViewBag.distritos = new List<Distrito>();
        //        return View(hotelbean);
        //    }
        //    else // hotelbean.idDepartamento == 0 && hotelbean.idProvincia == 0 && hotelbean.idDistrito == 0
        //    {
        //        hotelbean.idDepartamento = hotelbean.idProvincia = hotelbean.idDistrito = 0;
        //        Utils utils = new Utils();
        //        ViewBag.departamentos = utils.listarDepartamentos();
        //        ViewBag.provincias = new List<Provincia>();
        //        ViewBag.distritos = new List<Distrito>();
        //        return View(hotelbean);
        //    }
        //}
        
        //
        // GET: /Hotel/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View( hotelFac.getHotel( id ) );
        }

        //
        // POST: /Hotel/Edit/5

        [HttpPost]
        public ActionResult Edit(HotelBean hotelbean)
        {
            hotelFac.actualizarHotel(hotelbean);
            return RedirectToAction("List");
        }

        //
        // GET: /Hotel/Delete/5
 
        public ActionResult Delete(int id)
        {

            return View(hotelFac.getHotel(id));
        }

        //
        // POST: /Hotel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            hotelFac.eliminarHotel(id);
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult List() {
            return View(hotelFac.listarHoteles());
        }

        public ActionResult AsignarTipoHabitacion()
        {
            TipoHabitacionFacade tipoFac = new TipoHabitacionFacade();
            ViewBag.tipos = tipoFac.listarTipoHabitacion();

            ViewBag.hoteles = hotelFac.listarHoteles();

            return View();
        }

        [HttpPost]
        public ActionResult AsignarTipoHabitacion( TipoHabitacionxHotel tipo ) {
            hotelFac.registrarTipoHabitacion(tipo);
            return RedirectToAction("List") ;
        }

        public ViewResult VerTiposHabitacion( int id ) {
            var model = hotelFac.listarTipos(id);
            ViewBag.hotel = hotelFac.getHotel(id).nombre;
            return View( model );
        }

        public ActionResult ValidaEmail(string email)
        {
            if (String.IsNullOrEmpty(email) ||
                Regex.IsMatch(email, @"[a-z0-9!#$%&'*+/=?^_`B|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|pe)\b"))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(String.Format("El correo {0} no es válido", email), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidaFonoNoRequerido(string tlf2)
        {
            if (String.IsNullOrEmpty(tlf2) || Regex.IsMatch(tlf2, "([0-9]+)"))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(String.Format("El valor ingresado debe tener la sintaxis de un telefóno", tlf2), JsonRequestBehavior.AllowGet);
        }
    }
}