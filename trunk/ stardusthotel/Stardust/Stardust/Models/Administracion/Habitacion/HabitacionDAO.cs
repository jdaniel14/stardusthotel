using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Stardust.Models
{
    public class HabitacionDAO
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        
        public HabitacionBean getHabitacion(int id) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                HabitacionBean habitacion = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Habitacion WHERE idHabitacion = @idHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "idHabitacion", id);

                SqlDataReader data = objQuery.ExecuteReader();
                if (data.HasRows)
                {
                    data.Read();
                    habitacion = new HabitacionBean();

                    habitacion.ID = (int)data.GetValue(0);
                    habitacion.piso = (int)data.GetValue(1);
                    habitacion.estado = (string)data.GetValue(2);
                    habitacion.nroBanos = (int)data.GetValue(3);
                    habitacion.aptoFumador = (bool)data.GetValue(4);
                    habitacion.nroCamas = (int)data.GetValue(5);
                    habitacion.idHotel = (int)data.GetValue(6);
                    habitacion.idTipoHabitacion = (int)data.GetValue(7);
                }
                return habitacion;
            }
            //catch (Exception ex)
            //{
            //    log.Error("Errror getHabitacion, ex");
            //    de ser necesario throw ex;
            //}
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void registrarHabitacion(HabitacionBean habitacion) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();
            
                int idHotel = habitacion.idHotel ;
                int idTipoHabitacion = habitacion.idTipoHabitacion ;
                int apto = (habitacion.aptoFumador ? 1 : 0);

                String command = "Insert into Habitacion ( piso , estado , nroBanos , aptoFumador , nroCamas , idHotel , idTipoHabitacion ) values (" +
                                    habitacion.piso + ", '" +
                                    habitacion.estado + "', " +
                                    habitacion.nroBanos + ", " +
                                    apto + ", " +
                                    habitacion.nroCamas + ", " +
                                    idHotel + ", " +
                                    idTipoHabitacion + ")";
            
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void actualizarHabitacion(HabitacionBean habitacion) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                int idHotel = habitacion.idHotel;
                int idTipoHabitacion = habitacion.idTipoHabitacion;

                String command = "UPDATE Habitacion SET " +
                                    "piso = " + habitacion.piso + ", " + 
                                    "estado = '" + habitacion.estado + "', " +
                                    "nroBanos = " + habitacion.nroBanos + ", " +
                                    "aptoFumador = " + habitacion.nroCamas + ", " +
                                    "idHotel = " + idHotel + ", " +
                                    "idTipoHabitacion = " + idTipoHabitacion +
                                    " where idHabitacion = " + habitacion.ID  ;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void eliminarHabitacion(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Delete from Habitacion where idHabitacion = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public List<HabitacionBean> listarHabitaciones() {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<HabitacionBean> lstHabitaciones = null;

                objDB.Open();
                String strQuery =   "SELECT A.*, B.nombre, C.nombre " +
                                    "FROM Habitacion A, Hotel B, TipoHabitacion C " + 
                                    "WHERE A.idHotel = B.idHotel and A.idTipoHabitacion = C.idTipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    lstHabitaciones = new List<HabitacionBean>();

                    while (objDataReader.Read())
                    {
                        HabitacionBean habitacion = new HabitacionBean();

                        habitacion.ID = (int)objDataReader.GetValue(0);
                        habitacion.piso = (int)objDataReader.GetValue(1);
                        habitacion.estado = (string)objDataReader.GetValue(2);
                        habitacion.nroBanos = (int)objDataReader.GetValue(3);
                        habitacion.aptoFumador = (bool)objDataReader.GetValue(4);
                        habitacion.nroCamas = (int)objDataReader.GetValue(5);
                        habitacion.idHotel = (int)objDataReader.GetValue(6);
                        habitacion.idTipoHabitacion = (int)objDataReader.GetValue(7);
                        habitacion.nombreHotel = (string)objDataReader.GetValue(8);
                        habitacion.nombreTipoHabitacion = (string)objDataReader.GetValue(9);
                        
                        lstHabitaciones.Add(habitacion);
                    }
                }

                return lstHabitaciones;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public String getNombreHotel(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select nombre from Hotel where idHotel = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                String hotel = (string)data.GetValue(0);

            sql.Close();

            return hotel;
        }

        public String getTipoHabitacion(int id) {

            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select nombre from TipoHabitacion where idTipoHabitacion = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                String tipoHabitacion = (string)data.GetValue(0);

            sql.Close();

            return tipoHabitacion;
        }

        public int getIdHotel(String nombre) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select idHotel from Hotel where nombre = '" + nombre  + "'" ;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                int idHotel = (int)data.GetValue(0);

            sql.Close();

            return idHotel;
        }

        public int getIdTipoHabitacion(String nombre)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select idTipoHabitacion from TipoHabitacion where nombre = '" + nombre + "'" ;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                int idTipoHabitacion = (int)data.GetValue(0);

            sql.Close();

            return idTipoHabitacion;
        }
    }
}