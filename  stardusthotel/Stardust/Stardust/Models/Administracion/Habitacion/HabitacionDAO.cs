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
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "INSERT INTO Habitacion " +
                                    "( piso , estado , nroBanos , aptoFumador , nroCamas , idHotel , idTipoHabitacion ) " +
                                    "VALUES (@piso, @estado, @nroBanos, @aptoFumador, @nroCamas, @idHotel, @idTipoHabitacion)";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "piso", habitacion.piso);
                DAO.agregarParametro(objQuery, "estado", habitacion.estado);
                DAO.agregarParametro(objQuery, "nroBanos", habitacion.nroBanos);
                DAO.agregarParametro(objQuery, "aptoFumador", (habitacion.aptoFumador ? 1 : 0));
                DAO.agregarParametro(objQuery, "nroCamas", habitacion.nroCamas);
                DAO.agregarParametro(objQuery, "idHotel", habitacion.idHotel);
                DAO.agregarParametro(objQuery, "idTipoHabitacion", habitacion.idTipoHabitacion);

                objQuery.ExecuteNonQuery();
                objDB.Close();
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void actualizarHabitacion(HabitacionBean habitacion) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                

                String strQuery = "UPDATE Habitacion SET " +
                                    "piso = @piso," +
                                    "estado = @estado," +
                                    "nroBanos = @nroBanos," +
                                    "nroCamas = @nroCamas," +
                                    "aptoFumador = @aptoFumador," +
                                    "idHotel = @idHotel," +
                                    "idTipoHabitacion = @idTipoHabitacion " +
                                    "WHERE idHabitacion = @idHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "piso", habitacion.piso);
                DAO.agregarParametro(objQuery, "estado", habitacion.estado);
                DAO.agregarParametro(objQuery, "nroBanos", habitacion.nroBanos);
                DAO.agregarParametro(objQuery, "nroCamas", habitacion.nroCamas);
                DAO.agregarParametro(objQuery, "aptoFumador", (habitacion.aptoFumador ? 1 : 0));
                DAO.agregarParametro(objQuery, "idHotel", habitacion.idHotel);
                DAO.agregarParametro(objQuery, "idTipoHabitacion", habitacion.idTipoHabitacion);
                DAO.agregarParametro(objQuery, "idHabitacion", habitacion.ID);

                objQuery.ExecuteNonQuery();
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void eliminarHabitacion(int id) {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "DELETE FROM Habitacion where idHabitacion = @idHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "idHabitacion", id);

                objQuery.ExecuteNonQuery();
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
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
                        habitacion.nombreHotel = this.getNombreHotel(habitacion.idHotel);
                        habitacion.nombreTipoHabitacion = this.getTipoHabitacion(habitacion.idTipoHabitacion);
                        
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

        public List<HabitacionBean> buscarHabitacion(int idTipoHabitacion, int nroCamas , int piso) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Habitacion WHERE nroCamas >= @nroCamas AND piso >= @piso";
                

                
                if (idTipoHabitacion != 0)
                {
                    command += " AND idTipoHabitacion = @idTipoHabitacion ";
                }

                command += "ORDER BY idHotel , idTipoHabitacion";

                SqlCommand query = new SqlCommand(command, sql);
                Utils.agregarParametro(query, "nroCamas", nroCamas);
                Utils.agregarParametro(query, "piso", piso);

                if (idTipoHabitacion != 0)
                {
                    Utils.agregarParametro(query, "idTipoHabitacion", idTipoHabitacion);
                }

                SqlDataReader data = query.ExecuteReader();
            
                List<HabitacionBean> lista = new List<HabitacionBean>();

                while (data.Read()) {
                    HabitacionBean habitacion = new HabitacionBean();

                    habitacion.ID = Convert.ToInt32(data["idHabitacion"]);
                    habitacion.piso = Convert.ToInt32(data["piso"]);
                    habitacion.estado = Convert.ToString(data["estado"]);
                    habitacion.nroBanos = Convert.ToInt32(data["nroBanos"]);
                    habitacion.aptoFumador = Convert.ToBoolean(data["aptoFumador"]);
                    habitacion.nroCamas = Convert.ToInt32(data["nroCamas"]);
                    habitacion.idHotel = Convert.ToInt32(data["idHotel"]);
                    habitacion.idTipoHabitacion = Convert.ToInt32(data["idTipoHabitacion"]);
                    habitacion.nombreHotel = this.getNombreHotel(habitacion.idHotel);
                    habitacion.nombreTipoHabitacion = this.getTipoHabitacion(habitacion.idTipoHabitacion);

                    lista.Add(habitacion);
                }

            sql.Close();

            return lista;
        }

        public String getNombreHotel(int id)
        {
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

        public String getTipoHabitacion(int id)
        {

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

        //public int getIdHotel(String nombre) {
        //    SqlConnection sql = new SqlConnection(cadenaDB);

        //    sql.Open();

        //        String command = "Select idHotel from Hotel where nombre = '" + nombre  + "'" ;

        //        SqlCommand query = new SqlCommand(command, sql);

        //        SqlDataReader data = query.ExecuteReader();

        //        data.Read();

        //        int idHotel = (int)data.GetValue(0);

        //    sql.Close();

        //    return idHotel;
        //}

        //public int getIdTipoHabitacion(String nombre)
        //{
        //    SqlConnection sql = new SqlConnection(cadenaDB);

        //    sql.Open();

        //        String command = "Select idTipoHabitacion from TipoHabitacion where nombre = '" + nombre + "'" ;

        //        SqlCommand query = new SqlCommand(command, sql);

        //        SqlDataReader data = query.ExecuteReader();

        //        data.Read();

        //        int idTipoHabitacion = (int)data.GetValue(0);

        //    sql.Close();

        //    return idTipoHabitacion;
        //}
    }
}