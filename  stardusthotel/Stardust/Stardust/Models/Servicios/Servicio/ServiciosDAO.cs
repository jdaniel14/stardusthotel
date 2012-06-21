using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;

namespace Stardust.Models
{
    public class ServiciosDAO
    {
        public List<ServiciosBean> ListarServicios( String Nombre) {

            List<ServiciosBean> listaServicios = new List<ServiciosBean>();
            try
            {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Servicio WHERE estado = 'ACTIVO' ";
            bool result = Nombre.Equals("");
            if (!result)  commandString = commandString + " AND UPPER(nombre) LIKE '%"+Nombre.ToUpper()+"%'";
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

            }
            catch
            {

            }

            return listaServicios;
        }

        public String insertarServicio(ServiciosBean servicio) {
            String me = "";

            try
            {

                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "INSERT INTO Servicio VALUES ('" + servicio.nombre + "', '" + servicio.descripcion + "', 'ACTIVO')";

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                sqlCon.Close();
                servicio.conexion = "Bien";
            }
            catch {
                servicio.conexion = "Falla en la conexión";
           
            }

            return me;
        }
        public String ActualizarServicio(ServiciosBean servicio){
            String me = "";
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "UPDATE Servicio " +
                                        "SET nombre = '" + servicio.nombre + "', descripcion = '" + servicio.descripcion + "' " +
                                        "WHERE idServicio = " + servicio.id;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                servicio.conexion = "Bien";
            }
            catch {
                servicio.conexion = "Error en conexion";
            
            }
            return me
;        
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
            try
            {

                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "UPDATE Servicio " +
                                        "SET estado = 'INACTIVO' " +
                                        "WHERE idServicio = " + id.ToString();

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                
            }
            catch { 
            
            }
            return me;
        }

        public ListaServiciosResponseBean listarServicios(int idHotel, String dni, int nroRes) {
            ListaServiciosResponseBean response = new ListaServiciosResponseBean();
            response.me = "";
            
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            try
            {
                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                //String query = " SELECT COUNT(*) as res" +
                //                " FROM ReservaXHabitacionXCliente " +
                //                " WHERE idReserva = " + nroRes + " AND dni = '" + nroRes + "'";

                //SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                //SqlDataReader dataReader = sqlCmd.ExecuteReader();

                //int res = 0;
                //if (dataReader.Read())
                //{
                //    res = (int)dataReader["res"];
                //}
                //dataReader.Close();


                //if (res == 0)
                //{
                //    response.me = "No se encontro a dicho cliente";
                //    return response;
                //}
                //else
                //{
                    List<ServiciosBean> lista = new List<ServiciosBean>();
                    String query = "SELECT idServicio, nombre FROM Servicio WHERE idHotel = " + idHotel;
                    SqlCommand sqlCmd1 = new SqlCommand(query, sqlCon);
                    SqlDataReader dataReader = sqlCmd1.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ServiciosBean serv = new ServiciosBean();
                        serv.id = (int)dataReader["idServicio"];
                        serv.nombre = (String)dataReader["nombre"];
                        lista.Add(serv);
                    }
                    response.lista = lista;
            //    }
            }catch(Exception e){
                response.me = "Error en BD";
                return response;
            }
            return response;
        }
        public MensajeBean asignarServicios(int idSer, int nroRes, String dni, int nRecib, Decimal monto, int flagTipo, int idHotel)
        {
            MensajeBean mensaje = new MensajeBean ();
            mensaje.me = "";
            String query = "";
            if (flagTipo == 0)
            { //Evento
                query = "INSERT INTO AmbienteXEventoXServicioTerc VALUES ( "+idSer+ " , " + idHotel + " , " + nroRes + " , " + dni + " , " + monto + " , " + nRecib + " , SYSDATE() , 1 )" ;
            }
            else { //reserva
                query = "INSERT INTO ServicioTercerizadoXReserva VALUES ( " + nroRes + " , " +idSer + " , "+idHotel + ", " + nRecib + " , " + monto + " , SYSDATE() )";
            }


            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();                
            }
            catch(Exception e )
            {
                mensaje.me = "Error en conexion a BD";

            }

            return mensaje;
        }
    }
}