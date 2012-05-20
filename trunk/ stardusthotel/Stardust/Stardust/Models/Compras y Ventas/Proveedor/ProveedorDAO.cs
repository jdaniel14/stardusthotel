using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class ProveedorDAO
    {
        public List<ProveedorBean> ListarProveedor (String razonSocial, String contacto) {

            List<ProveedorBean> listaProveedor = new List<ProveedorBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            
            string commandString = "SELECT * FROM Proveedor WHERE estado=1";
            bool result1 = String.IsNullOrEmpty(razonSocial);//Nombre.Equals("") ;
            bool result2 = String.IsNullOrEmpty(contacto);// Contacto.Equals("");

            if (!result1)
                commandString = commandString + " AND UPPER(razonSocial) LIKE '%" + razonSocial.ToUpper() + "%'";

            if (!result2)
                commandString = commandString + " AND UPPER(contacto) LIKE '%" + contacto.ToUpper() + "%'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
                        
            while (dataReader.Read())
            {
                ProveedorBean proveedor = new ProveedorBean();

                
                proveedor.ID = (int)dataReader["idProveedor"];
                proveedor.razonSocial = (string)dataReader["razonSocial"];
                proveedor.contacto = (string)dataReader["contacto"];
                proveedor.emailContacto = (string)dataReader["emailContacto"];
                proveedor.cargoContacto = (string)dataReader["cargoContacto"];
                proveedor.ruc = (string)dataReader["ruc"];
                proveedor.web = (string)dataReader["web"];
                proveedor.telefono = (string)dataReader["telefono"];
                proveedor.direccion = (string)dataReader["direccion"];
                proveedor.observaciones = (string)dataReader["observaciones"];
                proveedor.estado = 1;
                listaProveedor.Add(proveedor);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaProveedor;
        }

        public String insertarProveedor(ProveedorBean proveedor) {
            String me = "";
            
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            proveedor.estado = 1;
            string commandString = "INSERT INTO Proveedor VALUES ('" + 
			       proveedor.razonSocial + "', '" + 
				   proveedor.contacto + "', '" + 
				   proveedor.emailContacto + "', '" + 
				   proveedor.cargoContacto + "', '" + 
				   proveedor.ruc + "', '" + 
				   proveedor.web + "', '" + 
				   proveedor.telefono + "', '" + 
				   proveedor.direccion + "', '" + 
				   proveedor.observaciones + "', '" + 
				   proveedor.estado + "')";
            
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();
         
            sqlCon.Close();
            return me;
        }

        public String ActualizarProveedor(ProveedorBean proveedor){
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString =  "UPDATE Proveedor " +
                                    "SET razonSocial = '" + proveedor.razonSocial + 
									"', contacto = '" +	proveedor.contacto + 
									"', emailContacto = '" + proveedor.emailContacto +
									"', cargoContacto = '" + proveedor.cargoContacto + 
									"', web = '" + proveedor.web +
									"', telefono = '" + proveedor.telefono + 
									"', direccion = '" + proveedor.direccion + 
									"', observaciones = '" + proveedor.observaciones + 
                                    "' WHERE idProveedor = " + proveedor.ID;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;        
        }

        public ProveedorBean SeleccionarProveedor(int idProveedor){
            ProveedorBean proveedor = new ProveedorBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Proveedor WHERE  idProveedor = " + idProveedor;
            //if (!Nombre.Equals(""))  commandString = commandString + "LIKE %"+Nombre+"%";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                proveedor.ID = (int)dataReader["idProveedor"];
                proveedor.razonSocial = (string)dataReader["razonSocial"];
                proveedor.contacto = (string)dataReader["contacto"];
                proveedor.emailContacto = (string)dataReader["emailContacto"];
                proveedor.cargoContacto = (string)dataReader["cargoContacto"];
                proveedor.ruc = (string)dataReader["ruc"];
                proveedor.web = (string)dataReader["web"];
                proveedor.telefono = (string)dataReader["telefono"];
                proveedor.direccion = (string)dataReader["direccion"];
                proveedor.observaciones = (string)dataReader["observaciones"];
                proveedor.estado = Convert.ToInt32(dataReader["estado"]);         
            }
            dataReader.Close();
            sqlCon.Close();

            return proveedor;
        }

        public String DeleteProveedor(int idProveedor){
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString =  "UPDATE Proveedor SET estado=0 WHERE idProveedor = " + idProveedor;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            return me;
        }
        public void InsertarProveedorxProducto(int idproveedor, ProductoxProveedorBean prod)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

        }
    }
}




