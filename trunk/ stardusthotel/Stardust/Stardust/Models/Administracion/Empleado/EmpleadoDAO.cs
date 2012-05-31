using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class EmpleadoDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        
        public EmpleadoBean getEmpleado(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Empleado where idEmpleado = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                EmpleadoBean empleado = new EmpleadoBean();

                empleado.ID = (int)data.GetValue(0);
                empleado.fechaIngreso = (string)data.GetValue(1);
                empleado.fechaSalida = (string)data.GetValue(2);
                empleado.estado = (string)data.GetValue(3);

            sql.Close();

            return empleado;
        }

        public void registrarEmpleado(EmpleadoBean empleado) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Insert into Empleado (fechaIngreso , fechaSalida , estado) values ( '"
                                + empleado.fechaIngreso + "', '"
                                + empleado.fechaSalida + "', 'activo' )";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void modificarEmpleado(EmpleadoBean empleado) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update Empleado SET fechaIngreso = '" + empleado.fechaIngreso
                                    + "', fechaSalida = '" + empleado.fechaSalida
                                    + "', estado = '" + empleado.estado
                                    + "' WHERE idEmpleado = " + empleado.ID;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();

        }

        public void eliminarEmpleado(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update Empleado SET estado = 'inactivo' WHERE idEmpleado = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public List<EmpleadoBean> listarEmpleados() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Empleado";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<EmpleadoBean> lista = new List<EmpleadoBean>();
            
                while (data.Read()) {
                    EmpleadoBean empleado = new EmpleadoBean();

                    empleado.ID = (int)data.GetValue(0);
                    empleado.fechaIngreso = (string)data.GetValue(1);
                    empleado.fechaSalida = (string)data.GetValue(2);
                    empleado.estado = (string)data.GetValue(3);

                    lista.Add(empleado);
                }

            sql.Close();

            return lista;
        }
    }
}