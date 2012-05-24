using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class AmbienteDAO
    {
        public List<AmbienteBean> ListarAmbiente(String Nombre, String estado, float precio_menor, float precio_mayor)
        {

            List<AmbienteBean> listaAmbientes = new List<AmbienteBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Ambiente ";
            bool result;

            //result = estado.Equals("");
            //if (!result) 
            commandString = commandString + "WHERE estado = 'ACTIVO'";

            result = Nombre.Equals("");
            if (!result) commandString = commandString + "AND  UPPER(nombre) LIKE '%" + Nombre.ToUpper() + "%' ";

            if (precio_menor > 0) commandString = commandString + " AND precioXhora  >= " + precio_menor;
            if (precio_mayor > 0) commandString = commandString + " AND precioXhora  <= " + precio_mayor;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                AmbienteBean ambiente = new AmbienteBean();
                ambiente.id = (int)dataReader["idAmbiente"];
                ambiente.nombre = (string)dataReader["nombre"];
                ambiente.descripcion = (string)dataReader["descripcion"];
                ambiente.cap_maxima = (int)dataReader["capacMaxima"];
                ambiente.largo  = (float)dataReader.GetDouble(dataReader.GetOrdinal("largo"));
                ambiente.ancho = (float)dataReader.GetDouble(dataReader.GetOrdinal("ancho"));
                //ambiente.precioXhora = (decimal)dataReader.GetDouble(dataReader.GetOrdinal("precioXHora"));
                ambiente.precioXhora = decimal.Parse(dataReader["Price"].ToString()); 
                ambiente.largo_escenario = (float)dataReader.GetDouble(dataReader.GetOrdinal("largoEscenario"));
                ambiente.ancho_escenario = (float)dataReader.GetDouble(dataReader.GetOrdinal("anchoEscenario"));
                ambiente.proyector = (int)dataReader["proyector"];
                ambiente.piso = (int)dataReader["piso"];
                ambiente.estado = (string)dataReader["estado"];
                listaAmbientes.Add(ambiente);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaAmbientes;
        }

        public String insertarAmbiente(AmbienteBean ambiente)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "INSERT INTO Ambiente VALUES ('" + ambiente.nombre + "', '" + ambiente.descripcion + "', " + ambiente.cap_maxima + ", " + ambiente.largo + ", " + ambiente.ancho + ", " + ambiente.precioXhora + ", " + ambiente.largo_escenario + ", " + ambiente.ancho_escenario + ", " + ambiente.proyector + ", " + ambiente.piso + ", 'ACTIVO', 2)";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;
        }
        public String ActualizarAmbiente(AmbienteBean ambiente)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Ambiente " +
                                    "SET  nombre = '" + ambiente.nombre + "', descripcion = '" + ambiente.descripcion + "', capacMaxima = " + ambiente.cap_maxima + ", largo = " + ambiente.largo + ", ancho = " + ambiente.ancho + ", precioXHora= " + ambiente.precioXhora + ", largoEscenario = " + ambiente.largo_escenario + ", anchoEscenario = " + ambiente.ancho_escenario + ", proyector = " + ambiente.proyector + ", piso = " + ambiente.piso +  " " +
                                    //"SETEA estado = 'INACTIVO' " +
                                    "WHERE idAmbiente = " + ambiente.id.ToString();

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;
        }

        public AmbienteBean SeleccionarAmbiente(int id)
        {
            AmbienteBean ambiente = new AmbienteBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Ambiente WHERE idAmbiente = " + id.ToString();            
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                ambiente.id = (int)dataReader["idAmbiente"];
                ambiente.nombre = (string)dataReader["nombre"];
                ambiente.descripcion = (string)dataReader["descripcion"];
                ambiente.cap_maxima = (int)dataReader["capacMaxima"];
                ambiente.largo = (float)dataReader.GetDouble(dataReader.GetOrdinal("largo"));
                ambiente.ancho = (float)dataReader.GetDouble(dataReader.GetOrdinal("ancho"));
                //ambiente.precioXhora = (decimal)dataReader.GetDouble(dataReader.GetOrdinal("precioXHora"));
                ambiente.precioXhora = decimal.Parse(dataReader["Price"].ToString()); 
                ambiente.largo_escenario = (float)dataReader.GetDouble(dataReader.GetOrdinal("largoEscenario"));
                ambiente.ancho_escenario = (float)dataReader.GetDouble(dataReader.GetOrdinal("anchoEscenario"));
                ambiente.proyector = (int)dataReader["proyector"];
                ambiente.piso = (int)dataReader["piso"];
                ambiente.estado = (string)dataReader["estado"];
            }
            dataReader.Close();
            sqlCon.Close();

            return ambiente;
        }

        public String DeleteAmbiente(int id)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Ambiente " +
                                    "SET estado = 'INACTIVO' " +
                                    "WHERE idAmbiente = " + id.ToString();

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            return me;
        }
    }
}