using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;
using Stardust.Models;
using log4net;

namespace Stardust
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILog log = LogManager.GetLogger(typeof(MvcApplication));

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("--- Iniciando el Sitio Web Stardust -----");

            log.Debug("Cargando los mapeos dentro del sistema");
            Mapper.CreateMap<HotelViewModelCreate, HotelBean>();
            Mapper.CreateMap<HotelBean, HotelViewModelDetails>();
            Mapper.CreateMap<HotelBean, HotelViewModelEdit>();
            Mapper.CreateMap<HotelViewModelEdit, HotelBean>();
            Mapper.CreateMap<HotelBean, HotelViewModelList>();
            Mapper.CreateMap<HotelBean, HotelViewModelDelete>();
            Mapper.CreateMap<TipoHabitacion, TipoHabitacionXHotelViewModelList>();
            Mapper.CreateMap<TipoHabitacionXHotelViewModelCreate, TipoHabitacionXHotel>();
            Mapper.CreateMap<TipoHabitacionXHotelViewModelEdit, TipoHabitacionXHotel>();
            Mapper.CreateMap<TipoHabitacionXHotelViewModelEdit, TipoHabitacionXHotelViewModelDelete>();
            Mapper.CreateMap<TipoHabitacionXHotelViewModelDelete, TipoHabitacionXHotel>();
            Mapper.CreateMap<TipoHabitacionXHotelXTemporadaViewModelCreate, TipoHabitacionXHotelXTemporada>();
            log.Debug("Termino de cargar los mapeos del sistema");
                //.ForMember(dest => dest.nombre, opt => opt.MapFrom(src => src.nombre))
                //.ForMember(dest => dest.razonSocial, opt => opt.MapFrom(src => src.razonSocial))
                //.ForMember(dest => dest.direccion, opt => opt.MapFrom(src => src.direccion))
                //.ForMember(dest => dest.tlf1, opt => opt.MapFrom(src => src.tlf1))
                //.ForMember(dest => dest.tlf2, opt => opt.MapFrom(src => src.tlf2))
                //.ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
                //.ForMember(dest => dest.nroPisos, opt => opt.MapFrom(src => src.nroPisos))
                //.ForMember(dest => dest.idDepartamento, opt => opt.MapFrom(src => src.idDepartamento))
                //.ForMember(dest => dest.idProvincia, opt => opt.MapFrom(src => src.idProvincia))
                //.ForMember(dest => dest.idDistrito, opt => opt.MapFrom(src => src.idDistrito))
                //.ForMember(dest => dest.Departamentos, opt => opt.MapFrom(src => src.Departamentos))
                //.ForMember(dest => dest.Almacen, opt => opt.MapFrom(src => src.Almacen));
        }
    }
}