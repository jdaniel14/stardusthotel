using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class TipoHabitacionDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        
        public TipoHabitacionBean getTipo(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from TipoHabitacion where idTipoHabitacion = " + id;
                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data =  query.ExecuteReader();

                data.Read();

                TipoHabitacionBean tipo = new TipoHabitacionBean();

                tipo.ID = (int)data.GetValue(0);
                tipo.nombre = (string)data.GetValue(1);
                tipo.descripcion = (string)data.GetValue(2);

            sql.Close();

            return tipo;
        }

        public void registrarTipoHabitacion(TipoHabitacionBean tipo) {
            SqlConnection sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Insert into TipoHabitacion ( nombre , descripcion ) values ('" +
                                    tipo.nombre + "', '" +
                                    tipo.descripcion + "')";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void actualizarTipoHabitacion(TipoHabitacionBean tipo) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update TipoHabitacion SET " +
                                    "nombre = '" + tipo.nombre + "', " +
                                    "descripcion = '" + tipo.descripcion + "' " +
                                    "where idTipoHabitacion = " + tipo.ID;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void eliminarTipoHabitacion(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Delete from TipoHabitacion where idTipoHabitacion = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public List<TipoHabitacionBean> listarTipoHabitacion() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from TipoHabitacion";
                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<TipoHabitacionBean> lista = new List<TipoHabitacionBean>();
            
                while (data.Read()) {
                    TipoHabitacionBean tipo = new TipoHabitacionBean();

                    tipo.ID = (int)data.GetValue(0);
                    tipo.nombre = (string)data.GetValue(1);
                    tipo.descripcion = (string)data.GetValue(2);

                    lista.Add(tipo);
                }

            sql.Close();

            return lista;
        }
    }
}