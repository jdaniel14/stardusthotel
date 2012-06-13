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
                    hotel.estado = true;
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

        #region Delete
        // GET: /Hotel/Delete/5
        public ActionResult Delete(int id)
        {
            HotelBean hotel = hotelFac.getHotel(id);
            var hotelVMDe = Mapper.Map<HotelBean, HotelViewModelDelete>(hotel);
            hotelVMDe.nombreDepartamento = Utils.getNombreDepartamento(hotelVMDe.idDepartamento);
            hotelVMDe.nombreProvincia = Utils.getNombreProvincia(hotelVMDe.idDepartamento, hotelVMDe.idProvincia);
            hotelVMDe.nombreDistrito = Utils.getNombreDistrito(hotelVMDe.idDepartamento, hotelVMDe.idProvincia, hotelVMDe.idDistrito);

            hotelVMDe.nTipoHabitacion = hotelFac.getNTipoHabitacionesXHotel(hotelVMDe.ID);
            hotelVMDe.nHabitacion = hotelFac.getNHabitacionesXHotel(hotelVMDe.ID);
            hotelVMDe.nAmbientes = hotelFac.getNAmbientesXHotel(hotelVMDe.ID);
            hotelVMDe.nServicios = hotelFac.getNServiciosXHotel(hotelVMDe.ID);
            hotelVMDe.nPromociones = hotelFac.getNPromocionesXHotel(hotelVMDe.ID);
            hotelVMDe.nAlmacenes = hotelFac.getNAlmacenesXHotel(hotelVMDe.ID);

            return View(hotelVMDe);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(HotelViewModelDelete hotelVMD)
        {
            try
            {
                hotelFac.desactivarHotel(hotelVMD.ID);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(hotelVMD);
            }
        }
        #endregion

        #region List
        public ActionResult List() 
        {
            List<HotelViewModelList> hotelVML = new List<HotelViewModelList>();
            try
            {
                List<HotelBean> lstHotel = hotelFac.getHotelesActivos();
                //List<HotelBean> lstHotel = hotelFac.getHoteles();
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(hotelVML);
            }
        }
        #endregion

        #region AsignarTipoHabitacion
        public ActionResult AsignarTipoHabitacion(int id)
        {
            var tipoHabitacionXhotelVMC = new TipoHabitacionXHotelViewModelCreate();
            try
            {
                tipoHabitacionXhotelVMC.idHotel = id;
                tipoHabitacionXhotelVMC.Hoteles = hotelFac.getHotelesActivos();
                tipoHabitacionXhotelVMC.TipoHabitaciones = hotelFac.getTipoHabitaciones();
                return View(tipoHabitacionXhotelVMC);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXhotelVMC);
            }
        }

        [HttpPost]
        public ActionResult AsignarTipoHabitacion(TipoHabitacionXHotelViewModelCreate tipoHabitacionXHotelVMC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tipoHabitacion = Mapper.Map<TipoHabitacionXHotelViewModelCreate, TipoHabitacionXHotel>(tipoHabitacionXHotelVMC);
                    hotelFac.registrarTipoHabitacion(tipoHabitacion);
                    return RedirectToAction("VerTiposHabitacion", new { id = tipoHabitacion.idHotel});
                }
                return View(tipoHabitacionXHotelVMC);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXHotelVMC);
            }
        }
        #endregion

        //Muestra todos los tipos de habitacion que ya han sido asignado a un determinado hotel
        public ActionResult VerTiposHabitacion(int id)
        {
            List<TipoHabitacion> lstTipoHabitacionXHotel = hotelFac.getTipoHabitacionXHotel(id);
            List<TipoHabitacionXHotelViewModelList> tipoHabitacionXHotelVML =  new List<TipoHabitacionXHotelViewModelList>();
            //string nombreHotel = hotelFac.getHotel(idHotel).nombre; // uno puede asegurar que todos los tipos de habitacion vienen del mismo hotel

            foreach (TipoHabitacion tipoHXH in lstTipoHabitacionXHotel)
            {
                TipoHabitacionXHotelViewModelList tipoHabitacionAux = Mapper.Map<TipoHabitacion, TipoHabitacionXHotelViewModelList>(tipoHXH);

                //tipoHabitacionAux.nombreHotel = nombreHotel;
                tipoHabitacionAux.precio = hotelFac.getPrecioTipoHabitacionXHotel(id, tipoHabitacionAux.ID);
                tipoHabitacionXHotelVML.Add(tipoHabitacionAux);
            }

            ViewBag.nombreHotel = hotelFac.getHotel(id).nombre;
            ViewBag.idHotel = id;
            return View(tipoHabitacionXHotelVML);
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