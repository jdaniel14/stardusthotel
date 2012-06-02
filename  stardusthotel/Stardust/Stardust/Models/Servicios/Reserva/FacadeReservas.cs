using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models.Servicios
{
    public class FacadeReservas
    {
        public List<TipoHabXHotel> listaDisponibles(String idHotel) {
            //return new List<TipoHabXHotel>();
            List<TipoHabXHotel> listadisp = new List<TipoHabXHotel>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();


            string commandString = "SELECT tipHabHot.idTipoHabitacion , tipHab.nombre FROM TipoHabitacionXHotel tipHabHot, TipoHabitacion tipHab WHERE tipHabHot.idTipoHabitacion = tipHab.idTipoHabitacion AND tipHabHot.idHotel = " + idHotel;
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
                tipo.precio = i*10;

                listadisp.Add(tipo);
            }
            dataReader.Close();
            sqlCon.Close();

            return listadisp;
        }
    }
}