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
        public List<ClienteBean> ListarClientesNatural(String Nombre)
        {

            List<ClienteBean> listaClientes = new List<ClienteBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Usuario WHERE estado = 'ACTIVO' AND tipoDocumento != 'RUC'";
            bool result = Nombre.Equals("");
            if (!result) commandString = commandString + " AND UPPER(nombres) LIKE '%" + Nombre.ToUpper() + "%'";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                ClienteBean cliente = new ClienteBean();
                cliente.nombres = (string)dataReader["nombres"];
                cliente.apPat = (string)dataReader["apPat"];
                cliente.apMat = (string)dataReader["apMat"];
                cliente.tipoDocumento = (string)dataReader["tipoDocumento"];
                cliente.nroDocumento = (string)dataReader["nroDocumento"];

                listaClientes.Add(cliente);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaClientes;
        }

        public List<ClienteBean> ListarClientesJuridica(String Nombre)
        {

            List<ClienteBean> listaClientes = new List<ClienteBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT usu.idUsuario, usu.razonSocial, usu.tipoDocumento, usu.nroDocumento  FROM Usuario usu, Cliente cli WHERE usu.estado = 'ACTIVO' AND usu.tipoDocumento = 'RUC' AND cli.idCliente = usu.idCliente";
            bool result = Nombre.Equals("");
            if (!result) commandString = commandString + " AND UPPER(nombres) LIKE '%" + Nombre.ToUpper() + "%'";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                ClienteBean cliente = new ClienteBean();
                cliente.ID = (int)dataReader["idUsuario"];
                cliente.razonSocial = (string)dataReader["razonSocial"];
                cliente.tipoDocumento = (string)dataReader["tipoDocumento"];
                cliente.nroDocumento = (string)dataReader["nroDocumento"];

                listaClientes.Add(cliente);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaClientes;
        }


        public String insertarCliente(ClienteBean cliente)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            //cliente.estado2 = 1;

            string commandString3 = "INSERT INTO Usuario VALUES (5," +
                     "''" + ", " +
                     "''" + ", " +
                     "'" + cliente.nombres + "'" + ", " +
                     "'" + cliente.apPat + "'" + ", " +
                     "'" + cliente.apMat + "'" + ", " +
                     "''" + ", " +//email
                     "''" + ", " +//celular
                     "'" + cliente.tipoDocumento + "'" + ", " +
                     "'" + cliente.nroDocumento + "'" + ", " +
                     "'" + cliente.razonSocial + "'" + ", " +
                     "'ACTIVO'" + ", " +//estado
                     "1" + ", " +//Distrito
                     "''" + ", " +//Direccion
                     "1" + ", " +//Provincia
                     "1" + //Departamento
                     ")"
                     ;

            SqlCommand sqlCmd3 = new SqlCommand(commandString3, sqlCon);
            sqlCmd3.ExecuteNonQuery();

            /*
            string commandString2 = "SELECT IDENT_CURRENT('Usuario')";
            SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon);
            SqlDataReader dr = sqlCmd2.ExecuteReader();
            dr.Read();
            int lastID = (int)(dr[0]);
            dr.Close();



            string commandString1 = "INSERT INTO Cliente VALUES (" + lastID.ToString() + ", GETDATE(), 'ACTIVO', '" +
                     cliente.tipoTarjeta + "', '" +
                     cliente.nroTarjeta + "')";

            SqlCommand sqlCmd1 = new SqlCommand(commandString1, sqlCon);
            sqlCmd1.ExecuteNonQuery();


            */
            sqlCon.Close();
            return me;
        }

        /*public String ActualizarCliente(ClienteBean cliente)
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
                                    "' WHERE idProveedor = " + proveedor.ID;
        }*/

    }
}
