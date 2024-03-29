﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models.Servicios
{
    public class FacadeReservas
    {
        ServiceHabitacion serviceHabitacion = new ServiceHabitacion();
        //SERVICE CLIENTE
        
        //SERVICE HABITACION
        public ResponseResHabXTipo consultarHabitacionDisponibles(int idHotel, String fechaIni, String fechaFin)
        {
            return serviceHabitacion.consultarHabitacionDisponibles(idHotel, fechaIni, fechaFin);
        }

        public UbicacPersResponse consultarHabitacionDeCliente(int idHotel, String nombre)
        {
            return serviceHabitacion.consultarHabitacionDeCliente(idHotel, nombre);
        }


        //SERVICE RESERVA
        public MensajeBean anularReserva(int idReserva) {
            MensajeBean mensaje = new MensajeBean();
            mensaje.me = serviceHabitacion.anularReserva(idReserva);
            return mensaje;
        }

        public UsuarioResBean login(String mail, String pass)
        {
            return serviceHabitacion.login(mail, pass);
        }

        public MensajeBean registrarReserva(ReservaRegistroBean reserva) {
            return serviceHabitacion.registrarReserva(reserva);
        }

        public CheckInBean check_in(int idHotel, int idReserva)
        {
            CheckInBean check = serviceHabitacion.check_in(idHotel, idReserva);
            return check;
        }

        public String RegistrarDatClientesCheckIn(List<ClienteHabBean> listClientHab) {
            return serviceHabitacion.RegistrarDatClientesCheckIn(listClientHab);
        }

        public string ActualizarReserva(List<ClienteHabBean> listClientHab)
        {
            return serviceHabitacion.ActualizarReserva(listClientHab);
        }

        public ConsultaReservaBean consultarReserva(int idHotel, int idReserva, String documento) {
            return serviceHabitacion.consultarReserva(idHotel, idReserva, documento);
        }

        public List<TipoHabXHotel> listaDisponibles(int idHotel) {
            //return new List<TipoHabXHotel>();
            List<TipoHabXHotel> listadisp = new List<TipoHabXHotel>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();


            string commandString = "SELECT tipHabHot.idTipoHabitacion , tipHab.nombre, tipHabHot.precioBaseXDia FROM TipoHabitacionXHotel tipHabHot, TipoHabitacion tipHab WHERE tipHabHot.idTipoHabitacion = tipHab.idTipoHabitacion AND tipHabHot.idHotel = " + idHotel.ToString();
            System.Diagnostics.Debug.WriteLine(commandString);

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            int i = 0;
            while (dataReader.Read())
            {
                i = i + 1;
                TipoHabXHotel tipo = new TipoHabXHotel();
                tipo.idTipoHab = i;
                tipo.nombreTipoHab = (string)dataReader["nombre"];
                tipo.numPos = i;
                tipo.precio = decimal.Parse(dataReader["precioBaseXDia"].ToString());
                listadisp.Add(tipo);
            }
            dataReader.Close();
            sqlCon.Close();

            return listadisp;
        }

        public List<ReservaMostreo> listaReservas()
        {
            List<ReservaMostreo> lista = new List<ReservaMostreo>();

            ReservaMostreo dato = new ReservaMostreo();
            dato.codReserva = 1;
            dato.nombCliente = "Pedro";
            dato.fechaReserva = "12/01/12";

            lista.Add(dato);

            ReservaMostreo dato2 = new ReservaMostreo();
            dato2.codReserva = 2;
            dato2.nombCliente = "Juan";
            dato2.fechaReserva = "11/02/02";

            lista.Add(dato2);

            return lista;
        }

        //SERVICE RESERVA
        //public void registrarReserva() { }
        
        public void checkOut() { }
        //SERVICE CLIENTE
        public void enSistemaCliente() { }
        public void registrarCliente() { }
                
        public void ubicacion_cliente() { }
        

    }
}