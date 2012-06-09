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
        HotelFacade hotelFac = new HotelFacade();

        // GET: /Hotel/
        public ViewResult Index()
        {
            return View();
        }

        // GET: /Hotel/Details/5
        public ViewResult Details(int id)
        {
            HotelBean hotel = hotelFac.getHotel(id);
            var hotelVMD = Mapper.Map<HotelBean, HotelViewModelDetails>(hotel);
            hotelVMD.nombreDepartamento = Utils.getNombreDepartamento(hotelVMD.idDepartamento);
            hotelVMD.nombreProvincia = Utils.getNombreProvincia(hotelVMD.idDepartamento, hotelVMD.idProvincia);
            hotelVMD.nombreDistrito = Utils.getNombreDistrito(hotelVMD.idDepartamento, hotelVMD.idProvincia, hotelVMD.idDistrito);
            return View(hotelVMD);
        }

        #region Create

        // GET: /Hotel/Create
        public ActionResult Create()
        {
            var hotelVWC = new HotelViewModelCreate();
            hotelVWC.Departamentos = Utils.listarDepartamentos();
            return View(hotelVWC);
        }

        // POST: /Hotel/Create
        [HttpPost]
        public ActionResult Create(HotelViewModelCreate hotelVWC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotel = Mapper.Map<HotelViewModelCreate, HotelBean>(hotelVWC);
                    hotelFac.registrarHotel(hotel);
                    return RedirectToAction("List"); //despues de crear a donde quieres que vaya???
                }
                return View(hotelVWC);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(hotelVWC);
            }
        }

        #endregion

        #region Metodos JSON para combos de Ubigeo
        public ActionResult getProvincias(int idDepartamento)
        {
            return Json(Utils.listarProvincias(idDepartamento), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getDistritos(int idDepartamento, int idProvincia)
        {
            return Json(Utils.listarDistritos(idDepartamento, idProvincia), JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Edit
        // GET: /Hotel/Edit/5
        public ActionResult Edit(int id)
        {
            HotelBean hotel = hotelFac.getHotel(id);
            var hotelVWE = Mapper.Map<HotelBean, HotelViewModelEdit>(hotel);
            hotelVWE.Departamentos = Utils.listarDepartamentos();
            hotelVWE.Provincias = Utils.listarProvincias(hotelVWE.idDepartamento);
            hotelVWE.Distritos = Utils.listarDistritos(hotelVWE.idDepartamento, hotelVWE.idProvincia);
            return View(hotelVWE);
        }

        // POST: /Hotel/Edit/5
        [HttpPost]
        public ActionResult Edit(HotelViewModelEdit hotelVWE)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hotel = Mapper.Map<HotelViewModelEdit, HotelBean>(hotelVWE);
                    hotelFac.actualizarHotel(hotel);
                    return RedirectToAction("List");
                }
                return View(hotelVWE);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(hotelVWE);
            }
        }
        #endregion

        // GET: /Hotel/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.depend = hotelFac.getDependencias(id);
            return View(hotelFac.getHotel(id));
        }

        // POST: /Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            hotelFac.eliminarHotel(id);
            return RedirectToAction("List");
        }

        #region List
        public ActionResult List() {
            List<HotelBean> lstHotel = hotelFac.getHoteles();
            List<HotelViewModelList> hotelVML = new List<HotelViewModelList>();
            foreach (HotelBean hotel in lstHotel)
            {
                HotelViewModelList hotelAux = Mapper.Map<HotelBean, HotelViewModelList>(hotel);
                hotelAux.nombreDepartamento = Utils.getNombreDepartamento(hotelAux.idDepartamento);
                hotelAux.nombreProvincia = Utils.getNombreProvincia(hotelAux.idDepartamento, hotelAux.idProvincia);
                hotelAux.nombreDistrito = Utils.getNombreDistrito(hotelAux.idDepartamento, hotelAux.idProvincia, hotelAux.idDistrito);
                hotelVML.Add(hotelAux);
            }
            return View(hotelVML);
        }
        #endregion

        public ActionResult AsignarTipoHabitacion()
        {
            TipoHabitacionFacade tipoFac = new TipoHabitacionFacade();
            ViewBag.tipos = tipoFac.listarTipoHabitacion();

            ViewBag.hoteles = hotelFac.getHoteles();
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