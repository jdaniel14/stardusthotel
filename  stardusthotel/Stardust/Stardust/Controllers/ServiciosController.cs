using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

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
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        public ActionResult EliminarServicio(int id)
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            servicioFacade.EliminarServicio(id);
            return RedirectToAction("Index");
        }

        public ActionResult BuscarServicio()
        {            
            return View();
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
            return View();
        }


    }
}
