using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stardust.Models.Servicios;

namespace Stardust.Models
{
    public class AmbienteService
    {
        AmbienteDAO AmbienteDAO = new AmbienteDAO();
        public List<AmbienteBean> ListarAmbiente(int idHotel, String Nombre, String estado)
        {
            List<AmbienteBean> listaAmbiente = AmbienteDAO.ListarAmbiente(idHotel, Nombre, estado);
            return listaAmbiente;
        }

        public String RegistrarAmbiente(AmbienteBean ambiente)
        {
            return AmbienteDAO.insertarAmbiente(ambiente);
        }

        public String ActualizarAmbiente(AmbienteBean ambiente)
        {
            return AmbienteDAO.ActualizarAmbiente(ambiente);
        }

        public AmbienteBean GetAmbiente(int id)
        {
            return AmbienteDAO.SeleccionarAmbiente(id);
        }

        public String EliminarAmbiente(int id)
        {
            return AmbienteDAO.DeleteAmbiente(id);
        }

        public ResAmbRequest ConsultarAmbientesDisponibles(int idHotel, String fechaIni, String fechaFin)
        {
            ResAmbRequest response = new ResAmbRequest();
            DateTime fFin = DateTime.ParseExact(fechaFin, "dd-MM-yyyy", null);
            DateTime fIni = DateTime.ParseExact(fechaIni, "dd-MM-yyyy", null);
            TimeSpan ts = fFin - fIni;
            response.cantDias = ts.Days;
            System.Diagnostics.Debug.WriteLine("Diferencia de Dias : " + response.cantDias);
            if (response.cantDias < 0)
            {
                response.me = "Ingrese Fecha Correctas";
                return response;
            }
            List<AmbienteBean> listNoDisp = AmbienteDAO.listarNodisponibles(idHotel, fechaIni, fechaFin);
            List<AmbienteBean> listTot = AmbienteDAO.listarTodas(idHotel);
            int k = 0; int tamNoDisp = listNoDisp.Count;
            int l = 0; int tamTot = listTot.Count;
            bool [] disp = new bool [tamTot];
            for (int i = 0; i < tamTot; i++) disp[i] = true;
            while (k < tamNoDisp && l < tamTot)
            {
                if (listNoDisp[k].id == listTot[l].id)
                {
                    disp[l] = false;
                    k++;
                    l++;
                }
                else if (listNoDisp[k].id < listTot[l].id)
                {
                    k++;
                }
                else
                {
                    l++;
                }
            }
            List<AmbienteBean> listaRespuesta = new List<AmbienteBean>();
            for (int i = 0; i < tamTot; i++) {
                if (disp[i]) {
                    listaRespuesta.Add(listTot[i]);
                }
            }

            response.listaAmbientes = listaRespuesta;
            response.me = "";
            return response;
        }

        public MensajeBean RegistrarEventoYAmbientes(RegAmbienteEventoBean registro)
        {
            MensajeBean mensaje = new MensajeBean();
            //registrar Cliente
            bool result;
            ReservaHabitacionDAO reservaHabitacionDAO = new ReservaHabitacionDAO();

            UsuarioResBean usuarioRes = reservaHabitacionDAO.registraCliente(registro.client); // 0=> hubo error ; 1 => natural; 2 => juridico
            result = usuarioRes.me.Equals("");
            if (!result)
            {
                mensaje.me = usuarioRes.me;
                return mensaje;
            }
            
            //registrar Evento

            ReservaAmbBean reservaRes = AmbienteDAO.registrarEvento(registro.evento, registro.idHotel, usuarioRes.idUsuario, registro.fechaIni, registro.fechaFin, registro.total, registro.coment);
            result = reservaRes.me.Equals("");
            if (!result)
            {
                mensaje.me = reservaRes.me;
                return mensaje;
            }

            //RegistrarAmbiente  documento de pago
            DocumentoPagoBean docPago = AmbienteDAO.registrarDocumentoPago(usuarioRes, reservaRes);
            result = docPago.me.Equals("");
            if (!result)
            {
                mensaje.me = docPago.me;
                return mensaje;
            }

            //registro ambientes en el evento
            String mens = AmbienteDAO.resgistrarAmbientes(registro.listAmbi, registro.fechaIni, registro.fechaFin, reservaRes.idEvento);
            result = mens.Equals("");
            if (!result)
            {
                mensaje.me = mens;
                return mensaje;
            }

            //registrar detalle factura
            DateTime fFin = DateTime.ParseExact(registro.fechaFin, "dd-MM-yyyy", null);
            DateTime fIni = DateTime.ParseExact(registro.fechaIni, "dd-MM-yyyy", null);
            TimeSpan ts = fFin - fIni;

            String mensajeDetalle = AmbienteDAO.registrarDetalleFactura(docPago.idDocPago, registro.listAmbi, ts.Days);
            result = mensajeDetalle.Equals("");
            if (!result)
            {
                mensaje.me = mensajeDetalle;
                return mensaje;
            }

            int resEmail = envioEmail(reservaRes.idEvento, registro.client.nomb, registro.client.email);
            System.Diagnostics.Debug.WriteLine("estado de email " + resEmail);
            if (resEmail != 0)
            {
                mensaje.me = "No se pudo enviar el email";
                return mensaje;
            }
            return mensaje;
        }

        public int envioEmail(int idEvento, String nombres, String email)
        {
            try
            {
                String message = "Estimado " + nombres + ", gracias por su reservacion de Ambientes, esperaremos que cancele para Confirmar sus Ambientes. Agradecemos su preferencia";
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                System.Net.NetworkCredential cred = new System.Net.NetworkCredential("stardusthotelperu@gmail.com", "stardust123456");

                mail.To.Add(email);
                mail.Subject = "Stardust Reservacion";

                mail.From = new System.Net.Mail.MailAddress("stardusthotelperu@gmail.com");
                mail.IsBodyHtml = true;
                mail.Body = message;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = cred;
                smtp.Port = 587;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 1;
            }
            return 0;
        }
    }
}