﻿using System;
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
            ResponseResHabXTipo response = new ResponseResHabXTipo();

            List<HabitacionReserva> listaNoDisp = reservaHabitacionDAO.listarNoDisponibles(idHotel, fechaIni, fechaFin);
            List<HabitacionReserva> listaTodas = reservaHabitacionDAO.listarHabitaciones();
            List<ReservaTipoHabitacion> listaTipHab = reservaHabitacionDAO.listaTipoHabitacion(idHotel);
            int totTipHab = listaTipHab.Count;
            int tam = listaTodas.Count;
            bool [] disp = new bool [tam];
            for (int i = 0; i < tam; i++) disp[i] = true;
            int k = 0; int tam_noDisp = listaNoDisp.Count;
            int l = 0; int tam_tot = listaTodas.Count;
            while(k<tam_noDisp && l<tam_tot){
                if (listaNoDisp[k].idHabitacion == listaTodas[l].idHabitacion) {
                    disp[l] = false;
                    k++;
                    l++;                    
                }
                else if(listaNoDisp[k].idHabitacion < listaTodas[l].idHabitacion) {
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
                dictTipoHab[numTip] = i;

            }
            for (int i = 0; i < tam_tot; i++) {
                if (disp[i]) {
                    int tipo = dictTipoHab[ listaTodas[i].idTipoHabitacion ];
                    ReservaHabitacionBean habRes = new ReservaHabitacionBean();
                    habRes.idHab = listaTodas[i].idHabitacion;
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

        public String anularReserva(int idReserva, String nroDocumento) {
            return reservaHabitacionDAO.deleteReserva(idReserva, nroDocumento);
        }

        public List<CheckInBean> check_in(int idHotel, String documento)
        {
            return reservaHabitacionDAO.SelectDatosCheckIn(idHotel, documento);
        }
        public MensajeBean registrarReserva(ReservaRegistroBean reserva) {
            MensajeBean mensaje = new MensajeBean();
            int idUsuario = reservaHabitacionDAO.registraCliente(reserva.client);
            int idReserva = reservaHabitacionDAO.resgitrarReserva(reserva.idHotel, idUsuario, reserva.fechaIni, reserva.fechaFin, reserva.coment);
            reservaHabitacionDAO.resgistrarHabitaciones(reserva.listTip, reserva.fechaIni, reserva.fechaFin, idReserva);
            //if(reserva.rec==1)
            //    reservaHabitacionDAO.registrarServicio(reserva.datRec);
            //mensaje.me = "";
            return mensaje;
        }
    }
}