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

            System.Diagnostics.Debug.WriteLine(totTipHab);

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
                System.Diagnostics.Debug.WriteLine("-> " + numTip);
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

        public List<UbicacionClienteBean> consutarHabitacionDeCliente(String nombre)
        {
            return reservaHabitacionDAO.ubicarPersona(nombre);
        }

        public String anularReserva(int idReserva) {
            return reservaHabitacionDAO.deleteReserva(idReserva);
        }

        public CheckInBean check_in(int idHotel, int idReserva)
        {
            CheckInBean checkin = new CheckInBean();
            DatosReservaBean reserva = reservaHabitacionDAO.SelectDatosCheckIn(idHotel, idReserva);
            bool result = reserva.me.Equals("");

            //checkin.me = reserva.me;
            //if (result)
            //{
            //    checkin.doc = reserva.doc;
            //    checkin.nomb = reserva.nomb;
            //    checkin.fechaLleg = reserva.fechaLlegada;
            //    checkin.fechaReg = reserva.fechaRegistro;
            //    checkin.lista = reservaHabitacionDAO.listarTipHabReserva(idReserva);
            //}

            return checkin;
        }

        public String RegistrarDatClientesCheckIn(List<ClienteHabBean> listClientHab)
        {
            return reservaHabitacionDAO.InsertClientesHabitacion(listClientHab);
        }

        public MensajeBean registrarReserva(ReservaRegistroBean reserva) {
            MensajeBean mensaje = new MensajeBean();
            int idUsuario = reservaHabitacionDAO.registraCliente(reserva.client);
            int idReserva = reservaHabitacionDAO.resgitrarReserva(reserva.idHotel, idUsuario, reserva.fechaIni, reserva.fechaFin, reserva.coment);
            reservaHabitacionDAO.registrarXtipoHabitacion(idReserva, reserva.idHotel, reserva.listTip);
            reservaHabitacionDAO.resgistrarHabitaciones(reserva.listTip, reserva.fechaIni, reserva.fechaFin, idReserva);
            //if(reserva.rec==1)
            //    reservaHabitacionDAO.registrarServicio(reserva.datRec);
            //mensaje.me = "";
            return mensaje;
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
    }
}