using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class ClienteDAO
    {
        public String insertarCliente(ClienteBean cliente)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

          /* cliente.estado2 = 1;

            string commandString = "INSERT INTO Usuario VALUES ('" +
                   cliente.user_account + "', '" +
                   cliente.pass + "', '" +
                   cliente.nombres + "', '" +
                   cliente.apPat + "', '" +
                   cliente.apMat + "', '" +
                   cliente.email + "', '" +
                   cliente.celular+ "', '" +
                   cliente.tipoDocumento + "', '" +
                   cliente.nroDocumento + "', '" +
                   cliente.razonSocial + "','" +
                   cliente.estado + "','" +
                   cliente.direccion+ "')";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            string commandString2 = "INSERT INTO Cliente VALUES ('" +
                   cliente.fechaRegistro + "', '" +
                   cliente.estado + "', '" +
                   cliente.tipoTajeta + "', '" +
                   cliente.nroTarjeta+ "', '" + "')";

            SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon);
            sqlCmd2.ExecuteNonQuery();

            sqlCon.Close();*/
            return me;
        }

      /* public String ActualizarCliente(ClienteBean cliente)
        {   
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Cliente " +
                                    "SET razonSocial = '" + proveedor.razonSocial +
                                    "', contacto = '" + proveedor.contacto +
                                    "', emailContacto = '" + proveedor.emailContacto +
                                    "', cargoContacto = '" + proveedor.cargoContacto +
                                    "', web = '" + proveedor.web +
                                    "', telefono = '" + proveedor.telefono +
                                    "', direccion = '" + proveedor.direccion +
                                    "', observaciones = '" + proveedor.observaciones +
                                    "' WHERE idProveedor = " + proveedor.ID;*/
        }
    }
