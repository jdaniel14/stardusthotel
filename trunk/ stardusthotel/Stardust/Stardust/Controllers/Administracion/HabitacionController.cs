using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

using log4net;

namespace Stardust.Controllers
{
    public class HabitacionController : Controller
    {
        HabitacionFacade habitacionFac = new HabitacionFacade();
        TipoHabitacionFacade tipoHabitacionFac = new TipoHabitacionFacade();
        private static ILog log = LogManager.GetLogger(typeof(HabitacionController));
        
        // GET: /Habitacion/
        public ActionResult Index()
        {
            return View();
        }

        #region Details
        // GET: /Habitacion/Details/5
        public ActionResult Details(int id)
        {
            HabitacionBean habitacion = habitacionFac.getHabitacion(id);
            try
            {
                if (habitacion != null)
                {
                    ViewBag.Hotel = new HotelFacade().getHotel(habitacion.idHotel).nombre;
                    ViewBag.TipoHabitacion = new TipoHabitacionFacade().getTipoHabitacion(habitacion.idTipoHabitacion).nombre;
                }
                else
                {
                    ViewBag.Hotel = "";
                    ViewBag.TipoHabitacion = "";
                }
                return View(habitacion);
            }
            catch (Exception ex)
            {
                log.Error("Details - GET(EXCEPTION): ", ex);
                ModelState.AddModelError("", ex.Message);
                return View(habitacion ?? new HabitacionBean());
            }
        }
        #endregion

        #region Create
        // GET: /Habitacion/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.listaHoteles = new HotelFacade().getHotelesActivos();
                ViewBag.listaTipoHabitacion = new List<TipoHabitacionBean>();
                return View();
            }
            catch (Exception ex)
            {
                log.Error("Create - GET(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        } 

        // POST: /Habitacion/Create
        [HttpPost]
        public ActionResult Create( HabitacionBean habitacion )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int pisoMaximo = new HotelFacade().getHotel(habitacion.idHotel).nroPisos ?? 0;
                    if (pisoMaximo >= habitacion.piso )
                    {
                        string piso = habitacion.piso.ToString();
                        bool numeroHabitacionValido = habitacion.numero.StartsWith(piso);
                        if (numeroHabitacionValido && habitacion.numero.Length == (piso.Length + 2))
                        {
                            bool existeNumeroHabitacion = habitacionFac.existeNumeroHabitacionYA(habitacion.idHotel, habitacion.numero);
                            if (!existeNumeroHabitacion)
                            {
                                habitacion.estado = "ACTIVO";
                                habitacionFac.registrarHabitacion(habitacion);
                                return RedirectToAction("List");
                            }
                            else
                            {
                                ModelState.AddModelError("", "El número de habitacion ya ha sido asignado para ese Hotel");
                                ViewBag.listaHoteles = new HotelFacade().getHoteles();
                                ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                                return View(habitacion);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "El número de habitacion no comienza con el número del piso");
                            ViewBag.listaHoteles = new HotelFacade().getHoteles();
                            ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                            return View(habitacion);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "El número de piso ingresado es mayor al número de pisos del Hotel");
                        ViewBag.listaHoteles = new HotelFacade().getHoteles();
                        ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                        return View(habitacion);
                    }
                }
                else if (habitacion.idHotel != 0) //si en el View selecciono algun hotel
                {
                    ViewBag.listaHoteles = new HotelFacade().getHoteles();
                    ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                    return View(habitacion);
                }
                else //si no selecciono ningún Hotel
                {
                    ViewBag.listaHoteles = new HotelFacade().getHoteles();
                    ViewBag.listaTipoHabitacion = new List<TipoHabitacionBean>();
                    return View(habitacion);
                }
            }
            catch (Exception ex)
            {
                log.Error("Create - POST(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(habitacion);
            }
        }
        #endregion

        #region Edit
        // GET: /Habitacion/Edit/5
        public ActionResult Edit(int id)
        {
            HabitacionBean habitacion = habitacionFac.getHabitacion(id);
            try
            {
                ViewBag.listaHoteles = new HotelFacade().getHoteles();
                ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                return View(habitacion);
            }
            catch (Exception ex)
            {
                log.Error("Edit - GET(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(habitacion);
            }
        }

        // POST: /Habitacion/Edit/5
        [HttpPost]
        public ActionResult Edit( HabitacionBean habitacion )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int pisoMaximo = new HotelFacade().getHotel(habitacion.idHotel).nroPisos ?? 0;
                    if (pisoMaximo >= habitacion.piso)
                    {
                        string piso = habitacion.piso.ToString();
                        bool numeroHabitacionValido = habitacion.numero.StartsWith(piso);
                        if (numeroHabitacionValido && habitacion.numero.Length == (piso.Length + 2))
                        {
                            bool existeNumeroHabitacion = habitacionFac.existeNumeroHabitacionYA(habitacion.idHotel, habitacion.numero);
                            if (!existeNumeroHabitacion)
                            {
                                habitacionFac.actualizarHabitacion(habitacion);
                                return RedirectToAction("List");
                            }
                            else
                            {
                                ModelState.AddModelError("", "El número de habitacion ya ha sido asignado para ese Hotel");
                                ViewBag.listaHoteles = new HotelFacade().getHoteles();
                                ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                                return View(habitacion);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "El número de habitacion no comienza con el número del piso");
                            ViewBag.listaHoteles = new HotelFacade().getHoteles();
                            ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                            return View(habitacion);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "El número de piso ingresado es mayor al número de pisos del Hotel");
                        ViewBag.listaHoteles = new HotelFacade().getHoteles();
                        ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                        return View(habitacion);
                    }
                }
                else if (habitacion.idHotel != 0)
                {
                    ViewBag.listaHoteles = new HotelFacade().getHoteles();
                    ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().getTipoHabitacionXHotel(habitacion.idHotel);
                    return View();
                }
                else
                {
                    ViewBag.listaHoteles = new HotelFacade().getHoteles();
                    ViewBag.listaTipoHabitacion = new List<TipoHabitacionBean>();
                    return View();
                }
            }
            catch (Exception ex)
            {
                log.Error("Edit - POST(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(habitacion);
            }
        }
        #endregion

        #region Delete
        // GET: /Habitacion/Delete/5
        public ActionResult Delete(int id)
        {
            HabitacionBean habitacion = habitacionFac.getHabitacion(id);
            if (habitacion != null)
            {
                ViewBag.Hotel = new HotelFacade().getHotel(habitacion.idHotel).nombre;
                ViewBag.TipoHabitacion = new TipoHabitacionFacade().getTipoHabitacion(habitacion.idTipoHabitacion).nombre;
            }
            else
            {
                ViewBag.Hotel = "";
                ViewBag.TipoHabitacion = "";
            }
            return View(habitacion);
        }

        // POST: /Habitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            habitacionFac.eliminarHabitacion(id);
            return RedirectToAction("List");
        }
        #endregion

        #region List
        public ActionResult List() {
            List<HabitacionBean> lstHabitaciones = new List<HabitacionBean>();            
            try
            {
                lstHabitaciones = habitacionFac.listarHabitaciones();
                return View(lstHabitaciones);
            }
            catch (Exception ex)
            {
                log.Error("List - GET(EXCEPTION):", ex);
                ModelState.AddModelError("", ex.Message);
                return View(lstHabitaciones);
            }
        }
        #endregion

        public ActionResult Buscar( string idTipoHabitacion , string nroCamas , string piso )
        {
            int A , B , C ;
            if (!String.IsNullOrEmpty( idTipoHabitacion ) ) A = Convert.ToInt32(idTipoHabitacion);
            else A = 0;

            if (!String.IsNullOrEmpty(nroCamas)) B = Convert.ToInt32(nroCamas);
            else B = 0;

            if (!String.IsNullOrEmpty( piso ) ) C = Convert.ToInt32(piso);
            else C = 0;

            var model = habitacionFac.buscarHabitacion(A,B,C);
            ViewBag.listaTipoHabitacion = new TipoHabitacionFacade().listarTipoHabitacion();
            return View( model );
        }

        //public ActionResult Buscar()
        //{
        //    HabitacionViewModelSearch habitacionVMS = new HabitacionViewModelSearch();
        //    habitacionVMS.TipoHabitaciones = tipoHabitacionFac.listarTipoHabitacion();

        //    return View();
        //}
    }
}
