using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class HabitacionDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        
        public HabitacionBean getHabitacion(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Habitacion where idHabitacion = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                HabitacionBean habitacion = new HabitacionBean();
                habitacion.ID = (int)data.GetValue(0);
                habitacion.piso = (int)data.GetValue(1);
                habitacion.estado = (string)data.GetValue(2);
                habitacion.nroBanos = (int)data.GetValue(3);
                habitacion.aptoFumador = (bool)data.GetValue(4);
                habitacion.nroCamas = (int)data.GetValue(5);
                habitacion.hotel = (string)this.getNombreHotel((int)data.GetValue(6));
                habitacion.tipoHabitacion = (string)this.getTipoHabitacion((int)data.GetValue(7));

            sql.Close();

            return habitacion;
        }

        public void registrarHabitacion(HabitacionBean habitacion) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();
            
                int idHotel = this.getIdHotel( habitacion.hotel ) ;
                int idTipoHabitacion = this.getIdTipoHabitacion( habitacion.tipoHabitacion ) ;
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

                int idHotel = this.getIdHotel(habitacion.hotel);
                int idTipoHabitacion = this.getIdTipoHabitacion(habitacion.tipoHabitacion);

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
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Habitacion";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<HabitacionBean> lista = new List<HabitacionBean>();
            
                while (data.Read()) {
                    HabitacionBean habitacion = new HabitacionBean();

                    habitacion.ID = (int)data.GetValue(0);
                    habitacion.piso = (int)data.GetValue(1);
                    habitacion.estado = (string)data.GetValue(2);
                    habitacion.nroBanos = (int)data.GetValue(3);
                    habitacion.aptoFumador = (bool)data.GetValue(4);
                    habitacion.nroCamas = (int)data.GetValue(5);
                    habitacion.hotel = (string)this.getNombreHotel((int)data.GetValue(6));
                    habitacion.tipoHabitacion = (string)this.getTipoHabitacion((int)data.GetValue(7));

                    lista.Add(habitacion);
                }

            sql.Close();

            return lista;
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