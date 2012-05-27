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
                cliente.ID = (int)dataReader["idUsuario"];
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

            string commandString = "SELECT usu.idUsuario, usu.razonSocial, usu.tipoDocumento, usu.nroDocumento  FROM Usuario usu WHERE usu.estado = 'ACTIVO' AND usu.tipoDocumento = 'RUC' ";
            bool result = Nombre.Equals("");
            if (!result) commandString = commandString + " AND UPPER(razonSocial) LIKE '%" + Nombre.ToUpper() + "%'";
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

            string commandString2 = "SELECT IDENT_CURRENT('" + "Usuario" + "') as lastId";
            SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon);
            SqlDataReader dataReader = sqlCmd2.ExecuteReader();

            int last_id = 0;
            if (dataReader.Read())
            {
                //last_id = (int)dataReader["lastId"];
                last_id = Int16.Parse(dataReader["lastId"].ToString()); 
                //listaClientes.Add(cliente);
            }
            dataReader.Close();

            string commandString1 = "INSERT INTO Cliente VALUES (" + last_id.ToString() + 
                     ", GETDATE()" + ", " +
                     "'ACTIVO'" + ", " +
                     "'" + cliente.tipoTarjeta + "'" + ", " +
                     "'" + cliente.nroTarjeta + "'" + ")"
                     ;

            SqlCommand sqlCmd1 = new SqlCommand(commandString1, sqlCon);
            sqlCmd1.ExecuteNonQuery();

            sqlCon.Close();            

            return me ;
        }

        
        public String DeleteCliente(int id)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Usuario " +
                                    "SET estado = 'INACTIVO' " +
                                    "WHERE idUsuario = " + id.ToString();

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            return me;
        }
        
        public ClienteBean GetCliente(int id)
        {
            ClienteBean cliente = new ClienteBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Usuario WHERE idUsuario = " + id.ToString();
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                cliente.ID = (int)dataReader["idUsuario"];
                cliente.razonSocial = (string)dataReader["razonSocial"];
                cliente.nombres = (string)dataReader["nombres"];
                cliente.apPat = (string)dataReader["apPat"];
                cliente.apMat = (string)dataReader["apMat"];
                cliente.tipoDocumento = (string)dataReader["tipoDocumento"];
                cliente.nroDocumento = (string)dataReader["nroDocumento"];
            }
            dataReader.Close();
            sqlCon.Close();

            return cliente;
        }
        public String ActualizarCliente(ClienteBean cliente)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            /*public int ID { get; set; }
            public string nombres { get; set; }
            public string apPat { get; set; }
            public string apMat { get; set; }
            public string razonSocial { get; set; }
            public string tipoDocumento { get; set; }//DNI, Carne de Extranjeria, RUC
            public string nroDocumento { get; set; }
            public string tipoTarjeta { get; set; }
            public string nroTarjeta { get; set; }*/

            string commandString = "UPDATE Usuario " +
                                    "SET  nombres = '" + cliente.nombres
                                        + "', apPat = '" + cliente.apPat
                                        + "', apMat = '" + cliente.apMat
                                        + "', razonSocial = '" + cliente.razonSocial
                                        + "', tipoDocumento = '" + cliente.tipoDocumento
                                        + "', nroDocumento= '" + cliente.nroDocumento
                                        //+ "', tipoTarjeta = '" + cliente.tipoTarjeta
                                        //+ "', nroTarjeta = '" + cliente.nroTarjeta 
                                        + "' " +
                                    "WHERE idUsuario = " + cliente.ID.ToString();

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;
        }
    }

}
