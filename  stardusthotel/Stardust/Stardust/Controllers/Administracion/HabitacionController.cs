using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class HabitacionController : Controller
    {
        HabitacionFacade habitacionFac = new HabitacionFacade();
        
        //
        // GET: /Habitacion/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Habitacion/Details/5

        public ActionResult Details(int id)
        {
            HabitacionBean habitacion = habitacionFac.getHabitacion(id);
            if (habitacion != null)
            {
                ViewBag.Hotel = new HotelFacade().getHotel(habitacion.idHotel).nombre;
                ViewBag.TipoHabitacion = new TipoHabitacionFacade().getTipo(habitacion.idTipoHabitacion).nombre;
            }
            else
            {
                ViewBag.Hotel = "";
                ViewBag.TipoHabitacion = "";
            }
            return View(habitacion);
        }

        //
        // GET: /Habitacion/Create

        public ActionResult Create()
        {
            ViewBag.listaHoteles = new HotelFacade().getHoteles();
            ViewBag.listaTipoHabitacion = new List<TipoHabitacionBean>();
            return View();
        } 

        //
        // POST: /Habitacion/Create

        [HttpPost]
        public ActionResult Create( HabitacionBean habitacion )
        {
            //habitacion.estado = "ACTIVO"; //Cuando se crea una habitación, automáticamente se pone como ACTIVA
            if (ModelState.IsValid)
            {
                
                habitacionFac.registrarHabitacion(habitacion);
                return RedirectToAction("List");
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
        
        //
        // GET: /Habitacion/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View( habitacionFac.getHabitacion( id ) );
        }

        //
        // POST: /Habitacion/Edit/5

        [HttpPost]
        public ActionResult Edit( HabitacionBean habitacion )
        {
            habitacionFac.actualizarHabitacion(habitacion);
            return RedirectToAction("List");
        }

        //
        // GET: /Habitacion/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View( habitacionFac.getHabitacion( id ) );
        }

        //
        // POST: /Habitacion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            habitacionFac.eliminarHabitacion(id);
            return RedirectToAction("List");
        }

        public ActionResult List() {
            List<HabitacionBean> lstHabitaciones = habitacionFac.listarHabitaciones();
            if (lstHabitaciones == null)
            {
                lstHabitaciones = new List<HabitacionBean>();
            }
            return View(lstHabitaciones);
        }
    }
}
