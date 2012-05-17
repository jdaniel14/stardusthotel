using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class ServiciosDAO
    {
        public List<ServiciosBean> ListarServicios( String Nombre) {

            List<ServiciosBean> listaServicios = new List<ServiciosBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Servicio ";
            bool result = Nombre.Equals("");
            if (!result)  commandString = commandString + "WHERE UPPER(nombre) LIKE '%"+Nombre.ToUpper()+"%'";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
                        
            while (dataReader.Read())
            {
                ServiciosBean servicio = new ServiciosBean();
                servicio.id = (int)dataReader["idServicio"];
                servicio.nombre = (string)dataReader["nombre"];
                servicio.descripcion = (string)dataReader["descripcion"];

                listaServicios.Add(servicio);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaServicios;
        }

        public String insertarServicio(ServiciosBean servicio) {
            String me = "";
            
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "INSERT INTO Servicio VALUES ('" + servicio.nombre + "', '" + servicio.descripcion + "', 'ACTIVO')";
            
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();
         
            sqlCon.Close();
            return me;
        }
        public String ActualizarServicio(ServiciosBean servicio){
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString =  "UPDATE Servicio " +
                                    "SET nombre = '" + servicio.nombre + "', descripcion = '" + servicio.descripcion + "' "+
                                    "WHERE idServicio = " + servicio.id;                               

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;        
        }

        public ServiciosBean SeleccionarServicio(int id){
            ServiciosBean servicio = new ServiciosBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Servicio WHERE idServicio = " + id.ToString();
            //if (!Nombre.Equals(""))  commandString = commandString + "LIKE %"+Nombre+"%";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {                
                servicio.id = (int)dataReader["idServicio"];
                servicio.nombre = (string)dataReader["nombre"];
                servicio.descripcion = (string)dataReader["descripcion"];             
            }
            dataReader.Close();
            sqlCon.Close();

            return servicio;
        }

        public String DeleteServicio(int id){
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString =  "DELETE " +
                                    "FROM Servicio " +
                                    "WHERE idServicio = " + id.ToString();

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            return me;
        }
    }
}