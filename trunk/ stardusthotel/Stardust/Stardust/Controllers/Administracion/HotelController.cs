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
using log4net;

namespace Stardust.Controllers
{ 
    public class HotelController : Controller
    {
        HotelFacade hotelFac = new HotelFacade();
        TipoHabitacionFacade tipoHabitacionFac = new TipoHabitacionFacade();
        private static ILog log = LogManager.GetLogger(typeof(HotelController));

        // GET: /Hotel/
        public ViewResult Index()
        {
            return View();
        }

        #region Details
        // GET: /Hotel/Details/5
        public ActionResult Details(int id)
        {
            var hotelVMD = new HotelViewModelDetails();
            try
            {
                HotelBean hotel = hotelFac.getHotel(id);
                hotelVMD = Mapper.Map<HotelBean, HotelViewModelDetails>(hotel);
                hotelVMD.nombreDepartamento = Utils.getNombreDepartamento(hotelVMD.idDepartamento);
                hotelVMD.nombreProvincia = Utils.getNombreProvincia(hotelVMD.idDepartamento, hotelVMD.idProvincia);
                hotelVMD.nombreDistrito = Utils.getNombreDistrito(hotelVMD.idDepartamento, hotelVMD.idProvincia, hotelVMD.idDistrito);
                return View(hotelVMD);
            }
            catch (Exception ex)
            {
                log.Error("Details - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(hotelVMD);
            }
        }
        #endregion

        #region Create
        // GET: /Hotel/Create
        public ActionResult Create()
        {
            var hotelVWC = new HotelViewModelCreate();
            try
            {
                hotelVWC.Departamentos = Utils.listarDepartamentos();
                return View(hotelVWC);
            }
            catch (Exception ex)
            {
                log.Error("Create - GET(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(hotelVWC);
            }
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
                    int idHotelAux = hotelFac.buscarHotel(hotel);
                    hotelFac.registrarAlmacen(idHotelAux, hotel.Almacen);
                    return RedirectToAction("List"); //despues de crear a donde quieres que vaya???
                }
                return View(hotelVWC);
            }
            catch (Exception ex)
            {
                log.Error("Create - POST(EXCEPTION):", ex);
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
            var hotelVWE = new HotelViewModelEdit();
            try
            {
                HotelBean hotel = hotelFac.getHotel(id);
                hotelVWE = Mapper.Map<HotelBean, HotelViewModelEdit>(hotel);
                hotelVWE.Departamentos = Utils.listarDepartamentos();
                hotelVWE.Provincias = Utils.listarProvincias(hotelVWE.idDepartamento);
                hotelVWE.Distritos = Utils.listarDistritos(hotelVWE.idDepartamento, hotelVWE.idProvincia);
                return View(hotelVWE);
            }
            catch (Exception ex)
            {
                log.Error("Edit - GET(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(hotelVWE);
            }
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
                log.Error("Edit - POST(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(hotelVWE);
            }
        }
        #endregion

        #region Delete
        // GET: /Hotel/Delete/5
        public ActionResult Delete(int id)
        {
            var hotelVMDe = new HotelViewModelDelete();
            try
            {
                HotelBean hotel = hotelFac.getHotel(id);
                hotelVMDe = Mapper.Map<HotelBean, HotelViewModelDelete>(hotel);
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
            catch (Exception ex)
            {
                log.Error("Delete - GET(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(hotelVMDe);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(HotelViewModelDelete hotelVMD)
        {
            try
            {
                if (hotelVMD.nTipoHabitacion == 0 && hotelVMD.nHabitacion == 0 &&
                    hotelVMD.nAmbientes == 0 && hotelVMD.nServicios == 0 &&
                    hotelVMD.nPromociones == 0 && hotelVMD.nAlmacenes == 0)
                {
                    hotelFac.desactivarHotel(hotelVMD.ID);
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("", "Operacion Inválida: No se debe eliminar el Hotel");
                    return View(hotelVMD);
                }
            }
            catch (Exception ex)
            {
                log.Error("Delete - POST(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(hotelVMD);
            }
        }
        #endregion

        #region List
        public ActionResult List() 
        {
            var hotelVML = new List<HotelViewModelList>();
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
                log.Error("List - GET(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(hotelVML);
            }
        }
        #endregion

        #region CreateTipoHabitacionXHotel
        public ActionResult CreateTipoHabitacionXHotel(int? id)
        {
            var tipoHabitacionXhotelVMC = new TipoHabitacionXHotelViewModelCreate();
            try
            {
                tipoHabitacionXhotelVMC.idHotel = id ?? 0;
                tipoHabitacionXhotelVMC.idTipoHabitacion = 0;
                tipoHabitacionXhotelVMC.Hoteles = hotelFac.getHotelesActivos();
                tipoHabitacionXhotelVMC.TipoHabitaciones = hotelFac.getTipoHabitaciones();
                return View(tipoHabitacionXhotelVMC);
            }
            catch (Exception ex)
            {
                log.Error("CreateTipoHabitacionXHotel - GET (EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXhotelVMC);
            }
        }

        [HttpPost]
        public ActionResult CreateTipoHabitacionXHotel(TipoHabitacionXHotelViewModelCreate tipoHabitacionXHotelVMC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tipoHabitacion = Mapper.Map<TipoHabitacionXHotelViewModelCreate, TipoHabitacionXHotel>(tipoHabitacionXHotelVMC);
                    if (!hotelFac.existeTipoHabitacion_Hotel(tipoHabitacion))
                    {
                        hotelFac.registrarTipoHabitacion(tipoHabitacion);
                        return RedirectToAction("ListTiposHabitacionXHotel", new { id = tipoHabitacion.idHotel });
                    }
                    else
                    {
                        ModelState.AddModelError("","El Tipo de Habitacion y el Hotel ya han sido asignados");
                        return View(tipoHabitacionXHotelVMC);
                    }
                }
                return View(tipoHabitacionXHotelVMC);
            }
            catch (Exception ex)
            {
                log.Error("CreateTipoHabitacionXHotel - POST (EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXHotelVMC);
            }
        }
        #endregion

        #region ListTipoHabitacionXHotel
        //Muestra todos los tipos de habitacion que ya han sido asignado a un determinado hotel
        public ActionResult ListTiposHabitacionXHotel(int id)
        {
            var lstTipoHabitacionXHotelVML = new List<TipoHabitacionXHotelViewModelList>();
            try
            {
                lstTipoHabitacionXHotelVML = hotelFac.getTipoHabitacionXHotel(id);

                ViewBag.nombreHotel = hotelFac.getHotel(id).nombre;
                ViewBag.idHotel = id;
                return View(lstTipoHabitacionXHotelVML);
            }
            catch (Exception ex)
            {
                log.Error("ListTiposHabitacionXHotel - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex);
                return View(lstTipoHabitacionXHotelVML);
            }
        }
        #endregion

        #region EditTipoHabitacionXHotel
        public ActionResult EditTipoHabitacionXHotel(int idTipoHabitacion, int idHotel)//
        {
            var tipoHabitacionXhotelVME = new TipoHabitacionXHotelViewModelEdit();
            try
            {
                tipoHabitacionXhotelVME = hotelFac.getTipoHabitacionXHotel(idHotel, idTipoHabitacion);
                tipoHabitacionXhotelVME.Hoteles = hotelFac.getHotelesActivos();
                tipoHabitacionXhotelVME.TipoHabitaciones = hotelFac.getTipoHabitaciones();
                return View(tipoHabitacionXhotelVME);
            }
            catch (Exception ex)
            {
                log.Error("EditTipoHabitacionXHotel - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXhotelVME);
            }
        }

        [HttpPost]
        public ActionResult EditTipoHabitacionXHotel(TipoHabitacionXHotelViewModelEdit tipoHabitacionXHotelVME)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tipoHabitacionXHotel = Mapper.Map<TipoHabitacionXHotelViewModelEdit, TipoHabitacionXHotel>(tipoHabitacionXHotelVME);
                    hotelFac.actualizarTipoHabitacion(tipoHabitacionXHotel);
                    return RedirectToAction("ListTiposHabitacionXHotel", new { id = tipoHabitacionXHotel.idHotel });
                }
                else
                {
                    ModelState.AddModelError("", "El Tipo de Habitacion y el Hotel ya han sido asignados");
                    return View(tipoHabitacionXHotelVME);
                }
            }
            catch (Exception ex)
            {
                log.Error("EditTipoHabitacionXHotel - POST(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXHotelVME);
            }
        }
        #endregion

        #region DeleteTipoHabitacionXHotel
        public ActionResult DeleteTipoHabitacionXHotel(int idTipoHabitacion, int idHotel)
        {
            var tipoHabitacionXhotelVMD = new TipoHabitacionXHotelViewModelDelete();
            try
            {
                var tipoHabitacionXhotelVME = hotelFac.getTipoHabitacionXHotel(idHotel, idTipoHabitacion);
                tipoHabitacionXhotelVMD = Mapper.Map<TipoHabitacionXHotelViewModelEdit, TipoHabitacionXHotelViewModelDelete>(tipoHabitacionXhotelVME);
                tipoHabitacionXhotelVMD.nombreHotel = hotelFac.getHotel(idHotel).nombre;
                tipoHabitacionXhotelVMD.nombreTipoHabitacion = tipoHabitacionFac.getTipoHabitacion(idTipoHabitacion).nombre;
                tipoHabitacionXhotelVMD.nroTemporadasAsignadas = hotelFac.getNroTemporadasAsignadas(idHotel, idTipoHabitacion);
                return View(tipoHabitacionXhotelVMD);
            }
            catch (Exception ex)
            {
                log.Error("DeleteTipoHabitacionXHotel - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXhotelVMD);
            }
        }

        [HttpPost]
        public ActionResult DeleteTipoHabitacionXHotel(TipoHabitacionXHotelViewModelDelete tipohabitacionXhotelVMD)
        {
            try
            {
                if (tipohabitacionXhotelVMD.nroTemporadasAsignadas == 0)
                {
                    var tipoHabitacionXHotel = Mapper.Map<TipoHabitacionXHotelViewModelDelete, TipoHabitacionXHotel>(tipohabitacionXhotelVMD);
                    hotelFac.eliminarTipoHabitacionXHotel(tipoHabitacionXHotel);
                    return RedirectToAction("ListTiposHabitacionXHotel", new { id = tipoHabitacionXHotel.idHotel });
                }
                else
                {
                    ModelState.AddModelError("", "Operacion Inválida: No se debe eliminar el tipo habitacion asignado la hotel");
                    return View(tipohabitacionXhotelVMD);
                }
            }
            catch (Exception ex)
            {
                log.Error("DeleteTipoHabitacionXHotel - POST(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(tipohabitacionXhotelVMD);
            }
        }
        #endregion

        #region ListTipoHabitacionXHotelXTemporada
        public ActionResult ListTipoHabitacionXHotelXTemporada(int idHotel, int idTipoHabitacion)
        {
            var lstTipoHabitacionXHotelXTemporadaVML = new List<TipoHabitacionXHotelXTemporadaViewModelList>();
            try
            {
                lstTipoHabitacionXHotelXTemporadaVML = hotelFac.getTipoHabitacionXHotelXTemporada(idHotel, idTipoHabitacion);

                ViewBag.nombreHotel = hotelFac.getHotel(idHotel).nombre;
                ViewBag.nombreTipoHabitacion = tipoHabitacionFac.getTipoHabitacion(idTipoHabitacion).nombre;

                ViewBag.idHotel = idHotel;
                ViewBag.idTipoHabitacion = idTipoHabitacion;

                return View(lstTipoHabitacionXHotelXTemporadaVML);
            }
            catch (Exception ex)
            {
                log.Error("ListTipoHabitacionXHotelXTemporada - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex);
                return View(lstTipoHabitacionXHotelXTemporadaVML);
            }
        }

        #endregion

        #region CreateTipoHabitacionXHotelXTemporada
        public ActionResult CreateTipoHabitacionXHotelXTemporada(int idHotel, int idTipoHabitacion)
        {
            var tipoHabitacionXhotelVMC = new TipoHabitacionXHotelXTemporadaViewModelCreate();
            try
            {
                tipoHabitacionXhotelVMC.idHotel = idHotel;
                tipoHabitacionXhotelVMC.idTipoHabitacion = idTipoHabitacion;
                tipoHabitacionXhotelVMC.idTemporada = 0;

                tipoHabitacionXhotelVMC.Hoteles = hotelFac.getHotelesActivos();
                tipoHabitacionXhotelVMC.TipoHabitaciones = hotelFac.getTipoHabitaciones();
                tipoHabitacionXhotelVMC.Temporadas = hotelFac.getTemporadas();
                return View(tipoHabitacionXhotelVMC);
            }
            catch (Exception ex)
            {
                log.Error("CreateTipoHabitacionXHotel - GET (EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXhotelVMC);
            }
        }

        [HttpPost]
        public ActionResult CreateTipoHabitacionXHotelXTemporada(TipoHabitacionXHotelXTemporadaViewModelCreate tipoHabitacionXhotelXtemporadaVMC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var thXhXtemporada = Mapper.Map<TipoHabitacionXHotelXTemporadaViewModelCreate, TipoHabitacionXHotelXTemporada>(tipoHabitacionXhotelXtemporadaVMC);
                    if (!hotelFac.existeTipoHabitacion_Hotel_Temporada(thXhXtemporada))
                    {
                        hotelFac.registrarTipoHabitacion_Hotel_Temporada(thXhXtemporada);
                        return RedirectToAction("ListTipoHabitacionXHotelXTemporada", new { idHotel = thXhXtemporada.idHotel, idTipoHabitacion = thXhXtemporada.idTipoHabitacion});
                    }
                    else
                    {
                        ModelState.AddModelError("", "La temporada, el tipo de habitacion y el hotel ya han sido asignados");
                        return View(tipoHabitacionXhotelXtemporadaVMC);
                    }
                }
                return View(tipoHabitacionXhotelXtemporadaVMC);
            }
            catch (Exception ex)
            {
                log.Error("CreateTipoHabitacionXHotelXTemporada - POST (EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(tipoHabitacionXhotelXtemporadaVMC);
            }
        }
        #endregion
		
		#region EditTipoHabitacionXHotelXTemporada
        public ActionResult EditTipoHabitacionXHotelXTemporada(int idHotel, int idTipoHabitacion, int idTemporada)
        {
            var thXhXtemporadaVME = new TipoHabitacionXHotelXTemporadaViewModelEdit();
            try
            {
                thXhXtemporadaVME = hotelFac.getTipoHabitacionXHotelXTemporada(idHotel, idTipoHabitacion, idTemporada);
                
                thXhXtemporadaVME.Hoteles = hotelFac.getHotelesActivos();
                thXhXtemporadaVME.TipoHabitaciones = hotelFac.getTipoHabitaciones();
                thXhXtemporadaVME.Temporadas = hotelFac.getTemporadas();
                return View(thXhXtemporadaVME);
            }
            catch (Exception ex)
            {
                log.Error("EditTipoHabitacionXHotelXTemporada - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(thXhXtemporadaVME);
            }
        }

        [HttpPost]
        public ActionResult EditTipoHabitacionXHotelXTemporada(TipoHabitacionXHotelXTemporadaViewModelEdit thXhXtemporadaVME)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var thXhXtemporada = Mapper.Map<TipoHabitacionXHotelXTemporadaViewModelEdit, TipoHabitacionXHotelXTemporada>(thXhXtemporadaVME);
                    hotelFac.actualizarTipoHabitacionXHotelXTemporada(thXhXtemporada);
                    return RedirectToAction("ListTipoHabitacionXHotelXTemporada", new { idHotel = thXhXtemporada.idHotel, idTipoHabitacion = thXhXtemporada.idTipoHabitacion });
                }
                else
                {
                    ModelState.AddModelError("", "El Tipo de Habitacion y el Hotel ya han sido asignados");
                    return View(thXhXtemporadaVME);
                }
            }
            catch (Exception ex)
            {
                log.Error("EditTipoHabitacionXHotelXTemporada - POST(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(thXhXtemporadaVME);
            }
        }
        #endregion

        #region DeleteTipoHabitacionXHotelXTemporada
        public ActionResult DeleteTipoHabitacionXHotelXTemporada(int idHotel, int idTipoHabitacion, int idTemporada)
        {
            var thXhXtemporadaVMD = new TipoHabitacionXHotelXTemporadaViewModelDelete();
            try
            {
                var thXhXtemporadaVME = hotelFac.getTipoHabitacionXHotelXTemporada(idHotel, idTipoHabitacion, idTemporada);
                thXhXtemporadaVMD = Mapper.Map<TipoHabitacionXHotelXTemporadaViewModelEdit, TipoHabitacionXHotelXTemporadaViewModelDelete>(thXhXtemporadaVME);
                thXhXtemporadaVMD.nombreHotel = hotelFac.getHotel(idHotel).nombre;
                thXhXtemporadaVMD.nombreTipoHabitacion = tipoHabitacionFac.getTipoHabitacion(idTipoHabitacion).nombre;
                thXhXtemporadaVMD.nombreTemporada = hotelFac.getTemporada(idTemporada).nombre;//esto esta mal pero no queda de otra xD!
                return View(thXhXtemporadaVMD);
            }
            catch (Exception ex)
            {
                log.Error("DeleteTipoHabitacionXHotelXTemporada - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(thXhXtemporadaVMD);
            }
        }

        [HttpPost]
        public ActionResult DeleteTipoHabitacionXHotelXTemporada(TipoHabitacionXHotelXTemporadaViewModelDelete thXhXtemporadaVMD)
        {
            try
            {
                var thXhXtemporada = Mapper.Map<TipoHabitacionXHotelXTemporadaViewModelDelete, TipoHabitacionXHotelXTemporada>(thXhXtemporadaVMD);
                hotelFac.eliminarTipoHabitacionXHotelXTemporada(thXhXtemporada);
                return RedirectToAction("ListTipoHabitacionXHotelXTemporada", new { idHotel = thXhXtemporada.idHotel, idTipoHabitacion = thXhXtemporada.idTipoHabitacion });
            }
            catch (Exception ex)
            {
                log.Error("DeleteTipoHabitacionXHotelXTemporada - POST(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(thXhXtemporadaVMD);
            }
        }
        #endregion

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