using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using Stardust.Models.Servicios;
using System.Net.Mail;
using System.Json;

namespace Stardust.Controllers.Servicios
{
    public class ReservarHabitacionController : Controller
    {
        //
        // GET: /ReservarHabitacion/
        FacadeReservas facadeReservas = new FacadeReservas();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult infoReserva(ReservaRequest request)
        {   
            System.Diagnostics.Debug.WriteLine(request.idHotel);
            List<TipoHabXHotel> lista = facadeReservas.listaDisponibles(request.idHotel);            
            var res = lista;            
            return Json (res);
        }

        [HttpPost]
        public JsonResult cerrarReserva(FinReserva reserva) {
            String message="";
            message = "Estimado "+reserva.nombre+", gracias por su reservacion, esperaremos que cancele para asignarle sus habitaciones";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            System.Net.NetworkCredential cred = new System.Net.NetworkCredential("stardusthotelperu@gmail.com", "stardust123456");

            mail.To.Add(reserva.email);
            mail.Subject = "Stardust Reservacion";

            mail.From = new System.Net.Mail.MailAddress("jkliose14@gmail.com");
            mail.IsBodyHtml = true;
            mail.Body = message;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            smtp.Send(mail);
            String me = "";
            return Json(me);
        }
                
        public ActionResult EstadoReserva()
        {
            return View();
        }

        [HttpPost]
        public JsonResult mostrarReservas(ReservaRequest request)
        {
            List<ReservaMostreo> lista = facadeReservas.listaReservas();
            var rpta = lista;
            return Json(rpta);
        }

        [HttpPost]
        public JsonResult consultarDisponibles(ReservaRequest request)
        {
            //DateTime fechaIni = DateTime.ParseExact("11-06-2012", "dd-MM-yyyy", null);
            //DateTime fechaFin = DateTime.ParseExact("17-06-2012", "dd-MM-yyyy", null);
            //String.Format("{0:yyyy-M-d}", fechaIni);
            System.Diagnostics.Debug.Write("Ini : " + String.Format("{0:MM/dd/yyyy}", request.fechaIni));
            System.Diagnostics.Debug.Write("Fin : " + String.Format("{0:MM/dd/yyyy}", request.fechaFin));
            ResponseResHabXTipo res = facadeReservas.consultarHabitacionDisponibles(request.idHotel, request.fechaIni, request.fechaFin);
            //return "Here we go!!!!";
            //var rpta = facadeReservas.consultarHabitacionDisponibles(request.idHotel, request.fechaIni, request.fechaFin);
            return Json(res);
        }

        public ActionResult CheckIn()
        {
            return View();
        }

        [HttpPost]
        public JsonResult habitacionesXReserva(Object parametro)
        {
            var rpta = "";
            return Json(rpta);
        }

    }
}
