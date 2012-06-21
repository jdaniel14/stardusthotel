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
                //ambiente.largo  = (float)dataReader.GetDouble(dataReader.GetOrdinal("largo"));
                //ambiente.ancho = (float)dataReader.GetDouble(dataReader.GetOrdinal("ancho"));
                ambiente.largo = decimal.Parse(dataReader["largo"].ToString());
                ambiente.ancho = decimal.Parse(dataReader["ancho"].ToString());
                //ambiente.precioXhora = (decimal)dataReader.GetDouble(dataReader.GetOrdinal("precioXHora"));
                ambiente.precioXhora = decimal.Parse(dataReader["precioXHora"].ToString()); 
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

            string commandString = "INSERT INTO Ambiente" +
                                    "( nombre , descripcion , cap_maxima , largo , ancho , precioXhora , piso , estado , idHotel ) " +
                                    "VALUES (@nombre, @descripcion, @cap_maxima, @largo, @ancho, @precioXhora, @piso, @estado,  @idHotel )";

                     
           // VALUES ('" + ambiente.nombre + "', '" + ambiente.descripcion + "', " + ambiente.cap_maxima + ", " + ambiente.largo + ", " + ambiente.ancho + ", " + ambiente.precioXhora + ", " + ambiente.piso + ", 'ACTIVO', 1)";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            DAO.agregarParametro(sqlCmd, "nombre", ambiente.nombre);
            DAO.agregarParametro(sqlCmd, "descripcion", ambiente.descripcion);
            DAO.agregarParametro(sqlCmd, "cap_maxima", ambiente.cap_maxima);
            DAO.agregarParametro(sqlCmd, "largo", ambiente.largo);
            DAO.agregarParametro(sqlCmd, "ancho", ambiente.ancho);
            DAO.agregarParametro(sqlCmd, "precioXhora", ambiente.precioXhora);
            DAO.agregarParametro(sqlCmd, "piso", ambiente.piso);
            DAO.agregarParametro(sqlCmd, "estado", "ACTIVO");
            DAO.agregarParametro(sqlCmd, "idHotel", ambiente.idHotel);
            
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

            //string commandString = "UPDATE Ambiente " +
            //                        "SET  nombre = '" + ambiente.nombre + "', descripcion = '" + ambiente.descripcion + "', capacMaxima = " + ambiente.cap_maxima + ", largo = " + ambiente.largo + ", ancho = " + ambiente.ancho + ", precioXHora= " + ambiente.precioXhora + ", piso = " + ambiente.piso + " " +
            //                        //"SETEA estado = 'INACTIVO' " +
            //                        "WHERE idAmbiente = " + ambiente.id.ToString();

            String commandString= "UPDATE Ambiente SET " +
                                   "nombre = @nombre," +
                                   "descripcion = @descripcion," +
                                   "capacMaxima = @capacMaxima," +
                                   "largo = @largo," +
                                   "ancho = @ancho," +
                                   "precioXhora = @precioXhora," +
                                   "piso = @piso," +
                                   "estado =@estado," +
                                   "idHotel = @idHotel," +                                   
                                   "WHERE idAmbiente = @idAmbiente";
          
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            DAO.agregarParametro(sqlCmd, "nombre", ambiente.nombre);
            DAO.agregarParametro(sqlCmd, "descripcion", ambiente.descripcion);
            DAO.agregarParametro(sqlCmd, "capacMaxima", ambiente.cap_maxima);
            DAO.agregarParametro(sqlCmd, "largo", ambiente.largo);
            DAO.agregarParametro(sqlCmd, "ancho", ambiente.ancho);
            DAO.agregarParametro(sqlCmd, "precioXhora", ambiente.precioXhora);
            DAO.agregarParametro(sqlCmd, "piso", ambiente.piso);
            DAO.agregarParametro(sqlCmd, "estado", ambiente.estado);
            DAO.agregarParametro(sqlCmd, "idHotel", ambiente.idHotel);
            DAO.agregarParametro(sqlCmd, "idAmbiente", ambiente.id); 
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
                //ambiente.largo = (float)dataReader.GetDouble(dataReader.GetOrdinal("largo"));
                //ambiente.ancho = (float)dataReader.GetDouble(dataReader.GetOrdinal("ancho"));
                ambiente.largo = decimal.Parse(dataReader["largo"].ToString());
                ambiente.ancho = decimal.Parse(dataReader["ancho"].ToString());
                //ambiente.precioXhora = (decimal)dataReader.GetDouble(dataReader.GetOrdinal("precioXHora"));
                ambiente.precioXhora = decimal.Parse(dataReader["precioXHora"].ToString()); 
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

        public List<AmbienteBean> listarNodisponibles(int idHotel, String fechaIni, String fechaFin)
        {
            List<AmbienteBean> listaNoDisp = new List<AmbienteBean>();
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            String query =  " SELECT DISTINCT idAmbiente " +
                            " FROM AmbienteXEvento aXe " + 
                            " WHERE idHotel = " + idHotel +
                            " AND aXe.estado < 3 "+
                            " AND ((aXe.fechaFin between convert(datetime,'" + fechaIni + "',103)" + " and  convert(datetime,'" + fechaFin + "',103)" + ")  OR (aXe.fechaIni between  convert(datetime,'" + fechaIni + "',103) and  convert(datetime,'" + fechaFin + "',103))) " +
                            " ORDER BY idAmbiente";
            System.Diagnostics.Debug.WriteLine("query Ambiente : " + query);
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                AmbienteBean amb = new AmbienteBean();
                amb.id = (int)dataReader["idAmbiente"];
                listaNoDisp.Add(amb);
            }

            return listaNoDisp;
        }

        public List<AmbienteBean> listarTodas(int idHotel)
        {
            List < AmbienteBean >  listTotal = new List<AmbienteBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query =  " SELECT idAmbiente, nombre, capacMaxima, precioXHora " + 
                            " FROM Ambiente " + 
                            " WHERE idHotel = " + idHotel + " AND estado='ACTIVO'";

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                AmbienteBean amb = new AmbienteBean();
                amb.id = (int)dataReader["idAmbiente"];
                amb.nombre = (String)dataReader["nombre"];
                amb.cap_maxima = (int)dataReader["capacMaxima"];
                amb.precioXhora = decimal.Parse(dataReader["precioXHora"].ToString());
                listTotal.Add(amb);
            }

            return listTotal;
        }
    }
}