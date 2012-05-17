using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.EntityModel;
namespace Stardust.Controllers
{
    public class HotelController : Controller
    {
        //
        // GET: /Hotel/
        private CadenaHotelDB db = new CadenaHotelDB();

        public ActionResult Index()
        {
            var hotels = from m in db.Hotell
                         select m;
            return View(hotels.ToList());
        }

        //
        // GET: /Hotel/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Create(Hotel newhotel)
        {
           if (ModelState.IsValid) {
               db.Hotell.Add(newhotel);
               
               db.SaveChanges(); 
               return RedirectToAction("Index"); 
           } else {
               return View(newhotel); 
           }
        }
        
        
        //
        // GET: /Hotel/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Hotel/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hotel/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Hotel/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
