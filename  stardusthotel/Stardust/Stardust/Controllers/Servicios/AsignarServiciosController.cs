using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.EntityModel;
using System.Web.Mvc;
using AutoMapper;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;
using log4net;

namespace Stardust.Controllers
{
    public class AsignarServiciosController : Controller
    {
        //
        // GET: /AsignarServicios/
        HotelFacade hotelFac = new HotelFacade();
        ServiciosFacade servFac = new ServiciosFacade();
        private static ILog log = LogManager.GetLogger(typeof(AsignarServiciosController));
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AsignarService()
        {
            return View();
        }

        public ActionResult Listar()
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

        public ViewResult Detalles(int id)
        {
            HotelBean hotel = hotelFac.getHotel(id);
            var hotelVMD = Mapper.Map<HotelBean, HotelViewModelDetails>(hotel);
            hotelVMD.nombreDepartamento = Utils.getNombreDepartamento(hotelVMD.idDepartamento);
            hotelVMD.nombreProvincia = Utils.getNombreProvincia(hotelVMD.idDepartamento, hotelVMD.idProvincia);
            hotelVMD.nombreDistrito = Utils.getNombreDistrito(hotelVMD.idDepartamento, hotelVMD.idProvincia, hotelVMD.idDistrito);
            return View(hotelVMD);
        }

        public ActionResult ListarServicios(int ID)
        {
            ServicioXHotelBean serv;
            HotelBean hotel = hotelFac.getHotel(ID);
            
            List<ServiciosBean> servicios = servFac.ListarServicios("");

            serv = hotelFac.obtenerlista(ID);//lista de los productos en la tabla productoxproveedor
            serv.hotel = hotel.nombre;

            //lista de productos en la tabla de productoxproveedor
            for (int i = 0; i < serv.listServHot.Count; i++)
            {
                ServiciosBean servicio = hotelFac.Getservicio(serv.listServHot[i].id);
                serv.listServHot[i].nombre = servicio.nombre;
                serv.listServHot[i].estado2 = false;
            }
            serv.idhotel = ID;
            return View(serv);
        }

        public ActionResult ModificarServicios(ServicioXHotelBean serv)
        {
            //List<HotelBean> hotel = hotelFac.ListarHotel(serv.hotel);
            //int idhotel = hotel[0].ID;

            //return View(serv);
            List<HotelBean> hotel = hotelFac.ListarHotel(serv.hotel);
            int idhotel = hotel[0].ID;

            ServicioXHotelBean servicio = hotelFac.obtenerlista(idhotel);
            for (int i = 0; i < servicio.listServHot.Count(); i++)
            {
                for (int j = 0; j < serv.listServHot.Count; j++)
                {
                    if (serv.listServHot[j].id == servicio.listServHot[i].id)
                    {
                        serv.listServHot[j].precio = servicio.listServHot[i].precio;

                    }

                }

            }

            //prod.listProdProv = producto.listProdProv;
            return View(serv);

        }


        public ActionResult Guardarservicios2(ServicioXHotelBean serv)
        {
            List<HotelBean> hotel = hotelFac.ListarHotel(serv.hotel);
            int idhotel = hotel[0].ID;
            //for (int i = 0; i < serv.listServHot.Count; i++)
            //{
            //    serv.listServHot[i].precio = Convert.ToDecimal(serv.listServHot[i].precio2) / 100;
            //}
            hotelFac.ModificarserviciosxHotel(idhotel, serv);
            return RedirectToAction("ListarServicios/" + idhotel, "AsignarServicios");
        }
        public ViewResult AsignarServicio(string nombre)
        {
            ServicioXHotelBean serv = new ServicioXHotelBean();
            List<HotelBean> hotel = hotelFac.ListarHotel(nombre);

            ServicioXHotelBean serv2 = hotelFac.obtenerlista(hotel[0].ID); // de la tabla proveedorxproducto

            List<ServiciosBean> servicios = hotelFac.ListarServicio("");

            serv.hotel = nombre; //prov[0].razonSocial;
            serv.idhotel = hotel[0].ID;
            serv.listServHot = new List<ServicioHotel>();
            for (int i = 0; i < servicios.Count; i++)
            {
                ServicioHotel servHotel = new ServicioHotel();

                servHotel.nombre = servicios[i].nombre;
                servHotel.id = servicios[i].id;
                servHotel.estados = false;
                for (int j = 0; j < serv2.listServHot.Count; j++)
                    if (servHotel.id == serv2.listServHot[j].id) servHotel.estado2 = true;

                serv.listServHot.Add(servHotel);

            }


            return View(serv);
        }

        public ActionResult guardarserviciosxHotel(ServicioXHotelBean serv)
        {
            List<HotelBean> hotel = hotelFac.ListarHotel(serv.hotel);
            int idhotel = hotel[0].ID;

            for (int i = 0; i < serv.listServHot.Count; i++)
            {
                List<ServiciosBean> servicios = hotelFac.ListarServicio(serv.listServHot[i].nombre);
                serv.listServHot[i].id = servicios[0].id;
            }
            for (int i = 0; i < serv.listServHot.Count; i++)
            {
                serv.listServHot[i].precio = Convert.ToDecimal(serv.listServHot[i].precio2);
            }
            hotelFac.RegistrarserviciosxHotel(idhotel, serv);

            return RedirectToAction("ListarServicios/" + idhotel, "AsignarServicios");
        }
    }
}
