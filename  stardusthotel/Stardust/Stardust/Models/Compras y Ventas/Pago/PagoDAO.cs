using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class PagoDAO
    {
        public int GetID(string doc)
        {
            int id=0;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Usuario WHERE nroDocumento = '"+doc+"'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                id = (int)dataReader["idUsuario"];
            }

            sqlCon.Close();

            return id;
        }

        public List<ReservaBean> GetReserva(string nombre)
        {
            List<ReservaBean> ListReserva = new List<ReservaBean>();

            int id = GetID(nombre);

            if (id == 0)
                return null;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Reserva WHERE idUsuario = "+id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                ReservaBean reserva = new ReservaBean();
                reserva.id = (int)dataReader["idReserva"];
                reserva.fechaRegistro = (DateTime)dataReader["fechaRegistro"];
                reserva.fechaCheckOut = (DateTime)dataReader["fechaSalida"];
                reserva.estado = (string)dataReader["estado"];
                reserva.pagoIni = (decimal)dataReader["pagoInicial"];
                reserva.total = (decimal)dataReader["montoTotal"];
                reserva.idHotel = (int)dataReader["idHotel"];
                reserva.idUsuario = (int)dataReader["idUsuario"];
                ListReserva.Add(reserva);
            }
            sqlCon.Close();

            return ListReserva;
        }

        public string GetNombreUsuario(int id)
        {
            string nombre = null;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Usuario WHERE idUsuario = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                nombre = (string)dataReader["nombres"];
            }

            dataReader.Close();
            sqlCon.Close();

            return nombre;
        }

        public string GetNombreHotel(int id)
        {
            string nombre = null;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Hotel WHERE idHotel = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                nombre = (string)dataReader["nombre"];
            }

            dataReader.Close();
            sqlCon.Close();

            return nombre;
        }

        public string GetNombreTipoHab(int id)
        {
            string nombre = null;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM TipoHabitacion WHERE idTipoHabitacion = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                nombre = (string)dataReader["nombre"];
            }

            dataReader.Close();
            sqlCon.Close();

            return nombre;
        }

        public decimal GetPrecioTipoHab(int id)
        {
            decimal precio = 0;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM TipoHabitacionXHotel WHERE idTipoHabitacion = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                precio = (decimal)dataReader["precioBaseXDia"];
            }

            dataReader.Close();
            sqlCon.Close();

            return precio;
        }

        public List<TipoHab> ListaHab(int id)
        {
            List<TipoHab> listaHab = new List<TipoHab>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM ReservaXTipoHabitacionXHotel WHERE idReserva = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                TipoHab hab = new TipoHab();
                hab.id = (int)dataReader["idTipoHabitacion"];
                hab.cantidad = (int)dataReader["cantidad"];
                hab.nombre = GetNombreTipoHab(hab.id);
                hab.precio = GetPrecioTipoHab(hab.id);
                listaHab.Add(hab);
            }

            dataReader.Close();
            sqlCon.Close();

            return listaHab;
        }

        public Reserva ObtenerReserva(int id)
        {
            Reserva reserva = new Reserva();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Reserva WHERE idReserva = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                reserva.id = (int)dataReader["idReserva"];
                reserva.pagoIni = (decimal)dataReader["pagoInicial"];
                reserva.subTotal = (decimal)dataReader["montoTotal"]; //Sin IGV :S
                reserva.idHotel = (int)dataReader["idHotel"];
                reserva.idUsuario = (int)dataReader["idUsuario"];
            }

            decimal subtotal = reserva.subTotal;
            decimal igv = subtotal * 18/100;
            decimal total = subtotal + igv;

            reserva.listaHab = ListaHab(reserva.id);
            reserva.igv = igv;
            reserva.total = total;
            reserva.hotel = GetNombreHotel(reserva.idHotel);
            reserva.usuario = GetNombreUsuario(reserva.idUsuario);

            dataReader.Close();
            sqlCon.Close();

            return reserva;
        }
    }
}
