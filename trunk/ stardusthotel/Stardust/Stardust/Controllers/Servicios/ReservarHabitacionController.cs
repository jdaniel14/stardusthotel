using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using Stardust.Models.Servicios;
using System.Net.Mail;

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
        public ActionResult infoReserva(String idHotel)
        {
            
            /*TipoHabXHotel tip = new TipoHabXHotel();               

            tip.idTipoHab = 1;
            tip.nombreTipoHab = "Suite";
            tip.numPos = 5;*/
            
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            System.Net.NetworkCredential cred = new System.Net.NetworkCredential("jkliose14@gmail.com", "aprenderc");

            mail.To.Add("j.astuvilcaf@pucp.pe");
            mail.Subject = "subject";

            mail.From = new System.Net.Mail.MailAddress("jkliose14@gmail.com");
            mail.IsBodyHtml = true;
            mail.Body = "message";

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            smtp.Send(mail);

            List<TipoHabXHotel> lista = facadeReservas.listaDisponibles("2");
            //List<TipoHabXHotel> lista = new List<TipoHabXHotel>();

            //lista.Add(tip);
            var res = lista;
            //return new JsonResult() { Data = res };
            return Json(new { data = res });
        }

        public ActionResult DatosReserva()
        {
            return View();
        }

    }
}
