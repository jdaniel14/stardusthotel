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
            if (model.estado2 == "Evento") model.estado1 = 1;
            else if (model.estado2 == "Habitación") model.estado1 = 2;
            else if (model.estado2 == "Otros") model.estado1 = 3;

            servicioFacade.RegistrarServicio(model);
            return RedirectToAction("BuscarServicio");
        }

        public ActionResult ModificarServicio(int id)            
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            ServiciosBean item = servicioFacade.GetServicio(id);
            

            if (item.estado1 == 1) item.estado2 = "Evento";
            else if (item.estado1 == 2) item.estado2 = "Habitación";
            else if(item.estado1 == 3) item.estado2 = "Otros";
            if (item.estado3 ==0 ) item.estado="ACTIVO";
            else if (item.estado3 == 1) item.estado = "INACTIVO";
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarServicio(ServiciosBean item)
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            if (item.estado == "ACTIVO") item.estado3 = 0;
            else  if (item.estado == "INACTIVO") item.estado3 = 1;

            if (item.estado2 == "Evento") item.estado1 = 1;
            else if (item.estado2 == "Habitación") item.estado1 = 2;
            else if (item.estado2=="Otros") item.estado1 = 3;
            servicioFacade.ActualizarServicio(item);
            if (item.estado == "ACTIVO")
                return RedirectToAction("DetallesServicio/" + item.id, "Servicios");
            else return RedirectToAction("BuscarServicio", "Servicios");
            
        }

        public ActionResult DetallesServicio(int ID)
        {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            ServiciosBean item = servicioFacade.GetServicio(ID);
            if (item.estado1 == 1) item.estado2 = "Evento";
            else if (item.estado1 == 2) item.estado2 = "Habitación";
            else if (item.estado1 == 3) item.estado2 = "Otros";


            return View(item);
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
        public ActionResult BuscarServicio()
        {
            List<ServiciosBean> serv = new List<ServiciosBean>();
            ViewBag.estado = 0;
            return View(serv);

        }

        [HttpPost]
        public ActionResult BuscarServicio(String nombre)
        {            
            ServiciosFacade serviciosFacade = new ServiciosFacade();
            
                
            List<ServiciosBean> listaServicios = serviciosFacade.ListarServicio(nombre);
            for (int i = 0; i < listaServicios.Count; i++)
            {
                if (listaServicios[i].estado1 == 1) listaServicios[i].estado2 = "Evento";
                else if (listaServicios[i].estado1 == 2) listaServicios[i].estado2 = "Habitación";
                else if (listaServicios[i].estado1 == 3) listaServicios[i].estado2 = "Otros";


            }
            ViewBag.estado = 1;


            return View(listaServicios);
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
            //List<AmbienteBean> listaAmbiente = ambienteFacade.ListarAmbiente("","",-1,-1);
            return View();//listaAmbiente);
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

        public ActionResult Details(int id)
        {
            AmbienteFacade ambienteFacade = new AmbienteFacade();
            AmbienteBean ambiente = ambienteFacade.GetAmbiente(id);
            if (ambiente != null)
            {
                ViewBag.Hotel = new HotelFacade().getHotel(ambiente.idHotel).nombre;                
            }
            else
            {
                ViewBag.Hotel = "";               
            }
            return View(ambiente);
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
                if (item.estado == "ACTIVO")
                    return RedirectToAction("Details/" + item.id, "Servicios");
                else return RedirectToAction("BuscarcarAmbiente", "Servicios");
            
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

        public ActionResult BuscarAmbiente()
        {
            ViewBag.listaHoteles = new HotelFacade().getHoteles();
            ViewBag.estado = 0;
            return View();

        }

        [HttpPost]
        public ActionResult BuscarAmbiente(string idHotel, string nombre, string estado)
        {
            int A;
            string B, C;
            AmbienteFacade ambienteFacade = new AmbienteFacade();

            if (!String.IsNullOrEmpty(idHotel)) A = Convert.ToInt32(idHotel);
            else A = 0;

            if (!String.IsNullOrEmpty(nombre)) B = Convert.ToString(nombre);
            else B = "";

            if (!String.IsNullOrEmpty(estado)) C = Convert.ToString(estado);
            else C = "";


            var model = ambienteFacade.ListarAmbiente(A, B, C);
            ViewBag.listaHoteles = new HotelFacade().getHoteles();
            //ViewBag.listaClientes = new ClienteFacade().getClientes();
            return View(model);

        }

        //public ActionResult MostrarAmbiente(AmbienteBean item)
        //{
        //    AmbienteFacade ambienteFacade = new AmbienteFacade();
        //    //List<AmbienteBean> listaAmbientes = ambienteFacade.ListarAmbiente(item.nombre, "ACTIVO", -1, -1);
        //    //pido la lista que cumpla con el nombre y la paso al view
        //    return View();//listaAmbientes);
        //}

        ///////////////////////ASIGNACION DE SERVICIOS
        [HttpPost]
        public JsonResult ConsultarServicio(ServicioRequest request) {
            ServiciosFacade servicioFacade = new ServiciosFacade();
            ListaServiciosResponseBean response = servicioFacade.listarServicios(request.idHotel, request.dni, request.nroRes);
            return Json(response);
        }

        [HttpPost]
        public JsonResult AsignarServicioReserva(ServicioAsignBean request)
        {
            System.Diagnostics.Debug.WriteLine("ASIGNAR : " + request.nroRes);
            ServiciosFacade servicioFacade = new ServiciosFacade();
            MensajeBean mensaje = servicioFacade.asignarServicios(request.idSer, request.nroRes, request.dni, request.nRecib, request.monto, request.flagTipo, request.idHotel);
            return Json(mensaje);
        }
    }
}
