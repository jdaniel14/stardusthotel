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
    }
}
