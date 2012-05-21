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
            return View();
        } 

        //
        // POST: /Hotel/Create

        [HttpPost]
        public ActionResult Create(HotelBean hotelbean)
        {
            hotelFac.registrarHotel(hotelbean);

            return RedirectToAction("List");
        }
        
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
    }
}