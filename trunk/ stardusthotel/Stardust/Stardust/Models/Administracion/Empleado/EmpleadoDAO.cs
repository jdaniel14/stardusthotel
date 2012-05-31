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

                UsuarioBean usuario = new UsuarioFacade().getUsuario(empleado.ID);
                empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;   

                empleado.fechaIngreso = (DateTime)data.GetValue(1);
                //empleado.fechaSalida = (DateTime)data.GetValue(2);
                empleado.estado = (string)data.GetValue(3);

            sql.Close();

            return empleado;
        }

        public void registrarEmpleado(EmpleadoBean empleado) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            DateTime date = empleado.fechaIngreso;

            String fechaIngreso = date.Date.ToShortDateString() ;

            String command = "Insert into Empleado ( idEmpleado , fechaIngreso , estado) values ( "
                                + empleado.ID + ", '"
                                + fechaIngreso + "', "
                                + "'ACTIVO' )";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void modificarEmpleado(EmpleadoBean empleado) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update Empleado SET "
                                    + "fechaSalida = '" + empleado.fechaSalida
                                    + "', estado = '" + empleado.estado
                                    + "' WHERE idEmpleado = " + empleado.ID;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();

        }

        public void eliminarEmpleado(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                DateTime date = new DateTime();

                String fechaSalida = date.Date.ToShortDateString();

                String command = "Update Empleado SET estado = 'INACTIVO' , fechaSalida = '" + fechaSalida + 
                        "' WHERE idEmpleado = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public List<EmpleadoBean> listarEmpleados() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Empleado ORDER BY fechaIngreso";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<EmpleadoBean> lista = new List<EmpleadoBean>();
            
                while (data.Read()) {
                    EmpleadoBean empleado = new EmpleadoBean();

                    empleado.ID = (int)data.GetValue(0);
                    UsuarioBean usuario = new UsuarioFacade().getUsuario( empleado.ID ) ;
                    empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;
                    empleado.fechaIngreso = (DateTime)data.GetValue(1);
                    //var aux = data.GetValue(2);
                    //if (aux != null)
                    //    empleado.fechaSalida = (DateTime)aux;
                    // NO FUNCIONA U_U
                    empleado.estado = (string)data.GetValue(3);

                    lista.Add(empleado);
                }

            sql.Close();

            return lista;
        }

        
    }

}