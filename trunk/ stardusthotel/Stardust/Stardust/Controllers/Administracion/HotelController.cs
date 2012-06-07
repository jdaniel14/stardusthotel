using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Text.RegularExpressions;
using AutoMapper;

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
            //var hotel = new HotelViewModelCreate();
            var hotel = new HotelBean();
            hotel.Departamentos = new Utils().listarDepartamentos();
            return View(hotel);
        }

        #region Metodos JSON para combos de Ubigeo
        public ActionResult getProvincias(int idDepartamento)
        {
            return Json(new Utils().listarProvincias(idDepartamento), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getDistritos(int idDepartamento, int idProvincia)
        {
            return Json(new Utils().listarDistritos(idDepartamento, idProvincia), JsonRequestBehavior.AllowGet);
        }
        #endregion

        //
        // POST: /Hotel/Create

        [HttpPost]
        //public ActionResult Create(HotelViewModelCreate hotel)
        public ActionResult Create(HotelBean hotel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var aux = Mapper.Map<HotelViewModelCreate, HotelBean>(hotel);
                    hotelFac.registrarHotel(hotel);
                    return RedirectToAction("List");
                }
                return View(hotel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(hotel);
            }
        }

        //
        // GET: /Hotel/Edit/5
 
        public ActionResult Edit(int id)
        {
            Utils utils = new Utils();
            HotelBean hotel = hotelFac.getHotel(id);
            hotel.Departamentos = utils.listarDepartamentos();
            hotel.Provincias = utils.listarProvincias(hotel.idDepartamento);
            hotel.Distritos = utils.listarDistritos(hotel.idDepartamento, hotel.idProvincia);
            return View( hotel );
        }

        //
        // POST: /Hotel/Edit/5

        [HttpPost]
        public ActionResult Edit(HotelBean hotel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hotelFac.actualizarHotel(hotel);
                    return RedirectToAction("List");
                }
                return View(hotel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(hotel);
            }
        }

        //
        // GET: /Hotel/Delete/5
 
        public ActionResult Delete(int id)
        {
            ViewBag.depend = hotelFac.getDependencias(id);
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
            //ViewBag.hoteles = hotelFac.getHotel( id );

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