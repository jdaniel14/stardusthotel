using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Stardust.Controllers
{
    public class ServiciosController : Controller
    {

        

        //*************SERVICIOS!!!!*************************

        //
        // GET: /Servicios/

        public ViewResult Index()
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            List<ServiciosBean> listaServicios = servicioFacade.ListarServicios("");
            return View(listaServicios);
        }

        [HttpPost]
        public ViewResult Index(List<ServiciosBean> listaServicios)
        {
            return View(listaServicios);
        }

        public ActionResult RegistrarServicio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarServicio(ServiciosBean model)
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            servicioFacade.RegistrarServicio(model);
            return RedirectToAction("BuscarServicio");
        }

        public ActionResult ModificarServicio(int id)            
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            ServiciosBean item = servicioFacade.GetServicio(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarServicio(ServiciosBean item)
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            servicioFacade.ActualizarServicio(item);
            return RedirectToAction("BuscarServicio");
        }

        //public ActionResult EliminarServicio(int id)
        //{
        //    ServiciosFacade servicioFacade = new ServiciosFacade();
        //    servicioFacade.EliminarServicio(id);
        //    return RedirectToAction("Index");
        //}
        public ActionResult Delete(int ID)
        {
            //ServiciosFacade servicioFacade = new ServiciosFacade();
            //servicioFacade.EliminarServicio(id);
            //return RedirectToAction("Index");
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public JsonResult DeleteConfirmed(int ID)
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            servicioFacade.EliminarServicio(ID);
            //return RedirectToAction("Index");
            //produc.Eliminarproducto(ID);
            //return RedirectToAction("Buscar");
            return Json(new { me = "" });
        }

        public ActionResult BuscarServicio(String nombre)
        {            
            ServiciosFacade serviciosFacade = new ServiciosFacade();
            return View( serviciosFacade.ListarServicios(nombre));
        }

                
        public ActionResult MostrarServicios(ServiciosBean item)
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();            
            List<ServiciosBean> listaServicios = servicioFacade.ListarServicios(item.nombre);
            return View(listaServicios);
        }
                

        //**************FIN SERVICIOS*******************


        //***************AMBIENTES********************

        public ViewResult IndexAmbientes()
        {
            AmbienteFacade ambienteFacade = new AmbienteFacade();
            List<AmbienteBean> listaAmbiente = ambienteFacade.ListarAmbiente("","",-1,-1);
            return View(listaAmbiente);
        }

        public ActionResult RegistrarAmbiente()
        {
            ViewBag.listaHoteles = new HotelFacade().getHotelesActivos();
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarAmbiente(AmbienteBean item)
        {
            if (ModelState.IsValid)
            {
                AmbienteFacade ambienteFacade = new AmbienteFacade();
                ambienteFacade.RegistrarAmbiente(item);
                return RedirectToAction("BuscarAmbiente");
            }
            else if (item.idHotel != 0) {

                     ViewBag.listaHoteles = new HotelFacade().getHoteles();                     
                     return View();
                 }

            else
            {
                ViewBag.listaHoteles = new HotelFacade().getHoteles();               
                return View();
            }

            
        }

        public ActionResult ModificarAmbiente(int id)
        {
            AmbienteFacade ambienteFacade = new AmbienteFacade();
            AmbienteBean item = ambienteFacade.GetAmbiente(id);
            ViewBag.listaHoteles = new HotelFacade().getHoteles();
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarAmbiente(AmbienteBean item)
        {
            if (ModelState.IsValid)
            {
                AmbienteFacade ambienteFacade = new AmbienteFacade();
                ambienteFacade.ActualizarAmbiente(item);
                return RedirectToAction("BuscarAmbiente");
            }
            else if (item.idHotel != 0)
            {

                ViewBag.listaHoteles = new HotelFacade().getHoteles();
                return View();
            }

            else
            {
                ViewBag.listaHoteles = new HotelFacade().getHoteles();
                return View();
            }
        }

        public ActionResult Delete2(int ID)
        {
            AmbienteFacade ambienteFacade = new AmbienteFacade();
            AmbienteBean ambiente = ambienteFacade.GetAmbiente(ID);
            if (ambiente !=null){
            ViewBag.Hotel = new HotelFacade().getHotel(ambiente.idHotel).nombre;
            
            }
            else
            {
                ViewBag.Hotel = "";                
            }

            return View();
            //AmbienteFacade ambienteFacade = new AmbienteFacade();
            //ambienteFacade.EliminarAmbiente(id);
            //return RedirectToAction("IndexAmbientes");
        }

        [HttpPost, ActionName("Delete2")]
        public JsonResult Delete2Confirmed(int ID)
        {
            AmbienteFacade ambienteFacade = new AmbienteFacade();
            ambienteFacade.EliminarAmbiente(ID);
          
            return Json(new { me = "" });
        }


        public ActionResult BuscarAmbiente(AmbienteBean item)
        {
            AmbienteFacade ambienteFacade = new AmbienteFacade();
            return View(ambienteFacade.ListarAmbiente(item.nombre,"ACTIVO",-1,-1));
        }

        public ActionResult MostrarAmbiente(AmbienteBean item)
        {
            AmbienteFacade ambienteFacade = new AmbienteFacade();
            List<AmbienteBean> listaAmbientes = ambienteFacade.ListarAmbiente(item.nombre, "ACTIVO", -1, -1);
            //pido la lista que cumpla con el nombre y la paso al view
            return View(listaAmbientes);
        }
        }
}
