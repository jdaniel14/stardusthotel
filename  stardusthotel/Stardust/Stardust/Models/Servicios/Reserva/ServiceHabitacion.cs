using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stardust.Models;


namespace Stardust.Models.Servicios
{
    public class ServiceHabitacion
    {
        ReservaHabitacionDAO reservaHabitacionDAO = new ReservaHabitacionDAO();
        public ResponseResHabXTipo consultarHabitacionDisponibles(int idHotel, String fechaIni, String fechaFin)
        {
            System.Diagnostics.Debug.WriteLine("Hotel : " + idHotel);
            ResponseResHabXTipo response = new ResponseResHabXTipo();

            DateTime fFin = DateTime.ParseExact(fechaFin, "dd-MM-yyyy", null);
            DateTime fIni = DateTime.ParseExact(fechaIni, "dd-MM-yyyy", null);
            TimeSpan ts = fFin - fIni;
            response.cantDias = ts.Days;
            System.Diagnostics.Debug.WriteLine("Diferencia de dias " + response.cantDias);

            List<HabitacionReserva> listaNoDisp = reservaHabitacionDAO.listarNoDisponibles(idHotel, fechaIni, fechaFin);
            List<HabitacionReserva> listaTodas = reservaHabitacionDAO.listarHabitaciones(idHotel);
            List<ReservaTipoHabitacion> listaTipHab = reservaHabitacionDAO.listaTipoHabitacion(idHotel);
            int totTipHab = listaTipHab.Count;
            int tam = listaTodas.Count;
            bool [] disp = new bool [tam];
            for (int i = 0; i < tam; i++) disp[i] = true;
            int k = 0; int tam_noDisp = listaNoDisp.Count;
            int l = 0; int tam_tot = listaTodas.Count;
            while(k<tam_noDisp && l<tam_tot){
                if (listaNoDisp[k].idHab == listaTodas[l].idHab) {
                    disp[l] = false;
                    k++;
                    l++;
                }
                else if(listaNoDisp[k].idHab< listaTodas[l].idHab) {
                    k++;
                }
                else { 
                    l++;
                }
            }
            List<TipoHabDisp> listaRespuesta = new List<TipoHabDisp>();

            Dictionary<int, int> dictTipoHab = new Dictionary<int, int>();

            System.Diagnostics.Debug.WriteLine("TOTAL TIPO HABITACION : " + totTipHab);

            for (int i = 0; i < totTipHab; i++) {
                //System.Diagnostics.Debug.WriteLine("-> "+i);
                TipoHabDisp tipoHabDisp = new TipoHabDisp();
                tipoHabDisp.idTipoHab = listaTipHab[i].idTipoHabitacion;
                tipoHabDisp.nombreTipoHab = listaTipHab[i].nombre;
                tipoHabDisp.precioTipoHab = listaTipHab[i].precioBaseXDia;
                tipoHabDisp.desc = listaTipHab[i].descripcion;
                tipoHabDisp.nroHab = 0;
                tipoHabDisp.listaDisp = new List<ReservaHabitacionBean>();
                listaRespuesta.Add(tipoHabDisp);
                int numTip = listaRespuesta[i].idTipoHab;
                System.Diagnostics.Debug.WriteLine(i + " -> " + numTip);
                dictTipoHab[numTip] = i;

            }
            for (int i = 0; i < tam_tot; i++) {
                if (disp[i]) {
                    System.Diagnostics.Debug.WriteLine(listaTodas[i].idTipoHabitacion);
                    int tipo = dictTipoHab[ listaTodas[i].idTipoHabitacion ];
                    ReservaHabitacionBean habRes = new ReservaHabitacionBean();
                    habRes.idHab = listaTodas[i].idHab;
                    //System.Diagnostics.Debug.WriteLine("Habitacion ==> " + listaTodas[i].idHabitacion);
                    habRes.numero = listaTodas[i].numero;
                    habRes.piso = listaTodas[i].piso;
                    listaRespuesta[tipo].listaDisp.Add(habRes);
                }
            }
            for (int i = 0; i < totTipHab; i++)
            {
                listaRespuesta[i].nroHab = listaRespuesta[i].listaDisp.Count;
            //    System.Diagnostics.Debug.WriteLine("*** " + i);
            //    int totHabitaciones = listaRespuesta[i].listaDisp.Count;
            //    System.Diagnostics.Debug.WriteLine("--> " + listaRespuesta[i].idTipoHab);
            //    for (int j = 0; j < totHabitaciones; j++)
            //        System.Diagnostics.Debug.WriteLine("------>" + listaRespuesta[i].listaDisp[j].numero);
            }
            response.me = "";
            response.listaXTipo = listaRespuesta;            
            return response;
        }

        /*public List<UbicacionClienteBean> consutarHabitacionDeCliente(String nombre)
        {
            return reservaHabitacionDAO.ubicarPersona(nombre);
        }*/

        public String anularReserva(int idReserva) {
            return reservaHabitacionDAO.deleteReserva(idReserva);
        }

        public CheckInBean check_in(int idHotel, int idReserva)
        {
            CheckInBean checkin = new CheckInBean();
            DatosReservaBean reserva = reservaHabitacionDAO.SelectDatosCheckIn(idHotel, idReserva);
            bool result = reserva.me.Equals("");

            checkin.me = reserva.me;
            
                checkin.doc = reserva.doc;
                checkin.nomb = reserva.nomb;
                checkin.fechaLleg = reserva.fechaLlegada;
                checkin.fechaReg = reserva.fechaRegistro;
            if (result)
            {
                checkin.lista = reservaHabitacionDAO.listarTipHabReserva(idReserva);
            }

            return checkin;
        }

        public String RegistrarDatClientesCheckIn(List<ClienteHabBean> listClientHab)
        {
            return reservaHabitacionDAO.InsertClientesHabitacion(listClientHab);
        }

        public string ActualizarReserva(List<ClienteHabBean> listClientHab)
        {
            return reservaHabitacionDAO.ActualizarReserva(listClientHab);
        }

        public UsuarioResBean login(String mail, String pass)
        {
            return reservaHabitacionDAO.login(mail, pass);
        }

        public MensajeBean registrarReserva(ReservaRegistroBean reservaRequest) {
            MensajeBean mensaje = new MensajeBean();
            mensaje.me = "";
            bool result;

            UsuarioResBean usuarioRes = new UsuarioResBean ();
            System.Diagnostics.Debug.WriteLine("FLAG : " + reservaRequest.tipoRegistro);
            if (reservaRequest.tipoRegistro == 0)
            {
                usuarioRes = reservaHabitacionDAO.registraCliente(reservaRequest.client, reservaRequest.pass); // 0=> hubo error ; 1 => natural; 2 => juridico    
                result = usuarioRes.me.Equals("");
                if (!result)
                {
                    mensaje.me = usuarioRes.me;
                    return mensaje;
                }
            }
            else {
                usuarioRes.idUsuario = reservaRequest.idUsuario;
                usuarioRes.tipoDocumento = reservaRequest.client.tipoDoc;
            }

            ReservaResBean reservaRes = reservaHabitacionDAO.resgitrarReserva(reservaRequest.idHotel, usuarioRes.idUsuario, reservaRequest.fechaIni, reservaRequest.fechaFin, reservaRequest.total, reservaRequest.coment);
            result = reservaRes.me.Equals("");
            if (!result) {
                mensaje.me = reservaRes.me;
                return mensaje;
            }

            System.Diagnostics.Debug.WriteLine("DOCUMENTO : " + usuarioRes.tipoDocumento);
            DocumentoPagoBean docPago = reservaHabitacionDAO.registrarDocumentoPago(usuarioRes, reservaRes);
            result = docPago.me.Equals("");
            if (!result)
            {
                mensaje.me = docPago.me;
                return mensaje;
            }


            reservaHabitacionDAO.registrarXtipoHabitacion(reservaRes.idReserva, reservaRequest.idHotel, reservaRequest.listTip);
            reservaHabitacionDAO.resgistrarHabitaciones(reservaRequest.idHotel, reservaRequest.listTip, reservaRequest.fechaIni, reservaRequest.fechaFin, reservaRes.idReserva);


            DateTime fFin = DateTime.ParseExact(reservaRequest.fechaFin, "dd-MM-yyyy", null);
            DateTime fIni = DateTime.ParseExact(reservaRequest.fechaIni, "dd-MM-yyyy", null);
            TimeSpan ts = fFin - fIni;

            String mensajeDetalle = reservaHabitacionDAO.registrarDetalleFactura(docPago.idDocPago, reservaRequest.listTip, ts.Days);
            result = mensajeDetalle.Equals("");
            if (!result)
            {
                mensaje.me = mensajeDetalle;
                return mensaje;
            }

            System.Diagnostics.Debug.WriteLine("Tipo : " + reservaRequest.rec);
            String mensajeServicio = "";
            /*if(reservaRequest.rec==1){
                String descripcionServicio = reservaRequest.datRec
                mensajeServicio = reservaHabitacionDAO.registrarServicio(reservaRequest.datRec);
            }*/
            result = mensajeServicio.Equals("");
            if (!result)
            {
                mensaje.me = mensajeServicio;
                return mensaje;
            }


            int resEmail = envioEmail(reservaRes.idReserva, reservaRequest.client.nomb, reservaRequest.client.email);
            System.Diagnostics.Debug.WriteLine("estado de email " + resEmail);
            if (resEmail != 0) {
                mensaje.me = "No se pudo enviar el email";
                return mensaje;
            }

            return mensaje;
        }

        public int envioEmail(int idReserva, String nombres, String email) {
            try
            {
                String message = "Estimado " + nombres + ", gracias por su reservacion, esperaremos que cancele para asignarle sus habitaciones; por el momento puede ver los datos de su reserva con el sgte codigo : '"+ idReserva + "'. Agradecemos su preferencia";
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
            catch(Exception ex){
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return 1;
            }
            return 0;
        }

        public ConsultaReservaBean consultarReserva(int idHotel, int idReserva, String documento) { 
            ConsultaReservaBean  consulta = new ConsultaReservaBean();
            DatosReservaBean reserva = reservaHabitacionDAO.consultarReserva(idHotel, idReserva, documento);
            consulta.me = reserva.me;
            bool result = reserva.me.Equals("");
            if (result)
            {
                consulta.idReserva = idReserva;
                consulta.fechaIni = reserva.fechaLlegada;
                consulta.fechaFin = reserva.fechaSalida;                
                consulta.doc = reserva.doc;
                consulta.Nombre = reserva.nomb;
                consulta.listaHab = reservaHabitacionDAO.listarTipHabReserva(idReserva);
            }
            return consulta;
        }

        public UbicacPersResponse consultarHabitacionDeCliente(int idHotel, String nombre)
        {
            return reservaHabitacionDAO.ubicarPersona(idHotel, nombre);
        }
    }
}