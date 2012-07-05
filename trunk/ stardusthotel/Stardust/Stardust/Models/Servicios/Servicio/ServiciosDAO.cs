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
                servicio.estado = (string)dataReader["estado"];
                servicio.estado1=Convert.ToInt32(dataReader["flag_res_eve"]);
            
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

            //try
            //{

                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();
                servicio.estado = "ACTIVO";

                string commandString = "INSERT INTO Servicio VALUES ('" + servicio.nombre + "', '" + servicio.descripcion + "', '" + servicio.estado + "', '" + servicio.estado1 + "')";
                    
                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                sqlCon.Close();
                servicio.conexion = "Bien";
            //}
            //catch {
            //    servicio.conexion = "Falla en la conexión";
           
            //}

            return me;
        }
        public String ActualizarServicio(ServiciosBean servicio){
            String me = "";
            //try
            //{
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "UPDATE Servicio " +
                                        "SET nombre = '" + servicio.nombre + 
                                        "', descripcion = '" + servicio.descripcion +
                                        "', estado = '" + servicio.estado +
                                        "', flag_res_eve = '" + servicio.estado1 +
                                        "' WHERE idServicio = " + servicio.id;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                servicio.conexion = "Bien";
            //}
            //catch {
            //    servicio.conexion = "Error en conexion";
            
            //}
            return me ;        
        }

        public ServiciosBean SeleccionarServicio(int id){
            ServiciosBean servicio = new ServiciosBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Servicio WHERE idServicio = " + id;
            //if (!Nombre.Equals(""))  commandString = commandString + "LIKE %"+Nombre+"%";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {                
                servicio.id = (int)dataReader["idServicio"];
                servicio.nombre = (string)dataReader["nombre"];
                servicio.descripcion = (string)dataReader["descripcion"];
                servicio.estado = (string)dataReader["estado"];
                servicio.estado1=Convert.ToInt32(dataReader["flag_res_eve"]);
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

        public ListaServiciosResponseBean listarServicios(int idHotel, int idTipo) {
            ListaServiciosResponseBean response = new ListaServiciosResponseBean();
            response.me = "";
            
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            try
            {
                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                List<ServiciosBean> lista = new List<ServiciosBean>();

                String query = "SELECT s.idServicio, s.nombre FROM ServicioXHotel sh, Servicio s WHERE idHotel="+ idHotel + " and s.flag_res_eve = " + idTipo + " and s.idServicio = sh.idServicio";
                System.Diagnostics.Debug.WriteLine("QUERY SERVICE : " + query);

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
            
            }catch(Exception e){
                response.me = "Error en BD";
                return response;
            }
            return response;
        }
        public MensajeBean asignarServicios(int idSer, int nroRes, Decimal monto, int flagTipo, int idHotel, String nombServ)
        {
            MensajeBean mensaje = new MensajeBean ();
            mensaje.me = "";
            String query = "";

            DocumentoPagoBean documento = buscarDocumentoPago(nroRes, idHotel , flagTipo);

            bool result = documento.me.Equals("");
            if (!result) {
                mensaje.me = documento.me;
                return mensaje;
            }
                        
            query = "INSERT INTO DocumentoPago_Detalle(idDocPago, detalle, cantidad, precioUnitario, total, es_habitacion) VALUES ( " + documento.idDocPago + " , '" + nombServ + "' , 1 , " + monto + " , " + monto + " , 0  )";
            
            System.Diagnostics.Debug.WriteLine("QUERY DE ASIGNAR : " + query);

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
                mensaje.me = "Error en conexion a BD " + e.Message;
                return mensaje;
            }

            return mensaje;
        }

        public DocumentoPagoBean buscarDocumentoPago(int  nroRes, int idHotel , int flagTipo)
        {
            DocumentoPagoBean response = new DocumentoPagoBean();
            response.me = "";

            String query;

            if (flagTipo == 2)
            {
                query = " SELECT d.idDocPago " +
                                " FROM DocumentoPago d, Reserva r " +
                                " WHERE r.estado = 3 and r.idReserva = " + nroRes + " AND d.idReserva = R.idReserva AND r.idHotel = " + idHotel;
            }
            else
            {
                query = " SELECT d.idDocPago " +
                                " FROM DocumentoPago d, Evento e " +
                                " WHERE e.estado = 3 and e.idEvento = " + nroRes + " AND d.idEvento = e.idEvento AND e.idHotel = " + idHotel;
            }
            System.Diagnostics.Debug.WriteLine("QUERY SERVICE PAGO: " + query);

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            try
            {
                sqlCon.Open();
            }
            catch (Exception e) {
                response.me = "Error en conexion a BD";
                return response;
            }

            try
            {   
                SqlCommand sqlCmd1 = new SqlCommand(query, sqlCon);
                SqlDataReader dataReader = sqlCmd1.ExecuteReader();
                if (dataReader.Read())
                {
                    response.idDocPago = (int)dataReader["idDocPago"];
                }
                else {
                    response.me = "No se llegó a encontrar a la reserva";
                    return response;                
                }
            }
            catch (Exception e)
            {
                response.me = "Error en BD";
                return response;
            }
            return response;
        }
    }
}