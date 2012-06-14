using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class EventoDAO
    {
        public List<EventoBean> ListarEvento(String nombre, String fechaini, string fechafin)
        {

            List<EventoBean> listaEvento = new List<EventoBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Evento WHERE estado=1";
            bool result1 = String.IsNullOrEmpty(nombre);//Nombre.Equals("") ;  
            bool result2 = String.IsNullOrEmpty(fechaini);
            bool result3 = String.IsNullOrEmpty(fechafin);


            if (!result1)
                commandString = commandString + " AND UPPER(nombre) LIKE '%" + nombre.ToUpper() + "%'";

            if (!result2) 
                commandString = commandString + " AND (CONVERT(VARCHAR(10),fechaIni,103) LIKE'%" + fechaini + "%' )";

            if (!result3)
                commandString = commandString + " AND (CONVERT(VARCHAR(10),fechaFin,103) LIKE'%" + fechafin + "%' )";
            if (!result1 && !result2)
                commandString = commandString + " AND " + fechaini + " BETWEEN (CONVERT(VARCHAR(10),fechaIni,103) AND (CONVERT(VARCHAR(10),fechaFin,103) ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                EventoBean evento = new EventoBean();

                evento.ID = (int)dataReader["idEvento"];
                evento.nombre = (string)dataReader["nombre"];
                evento.descripcion = (string)dataReader["descripcion"];
                evento.fechaIni = Convert.ToString(dataReader["fechaIni"]);
                evento.fechaFin = Convert.ToString(dataReader["fechaFin"]);
                evento.nroParticipantes = (int)dataReader["nroParticipantes"];
                evento.horaIni=(string)dataReader["horaIni"];
                evento.horaFin = (string)dataReader["horaFin"];

                listaEvento.Add(evento);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaEvento;
        }

        public String insertarEvento(EventoBean evento)
        {
            String me = "";
             evento.estado = 1;
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            
            string commandString = "INSERT INTO Evento VALUES ('" +
                   evento.nombre + "', '" +
                   evento.descripcion + "', '" +
                   evento.fechaIni + "', '" +
                   evento.fechaFin +"', '" +
                   evento.nroParticipantes + "', '" + null + "', '" +
                   evento.horaIni + "', '" +
                   evento.horaFin + "', '" +
                   evento.estado +
                   " ')";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;
        }

        public String ActualizarEvento(EventoBean evento)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Evento " +
                                    "SET nombre = '" + evento.nombre +
                                    "', descripcion  = '" + evento.descripcion +
                                    "', fechaIni = '" + evento.fechaIni +
                                    "', horaIni = '" + evento.horaIni +
                                    "', fechaFin = '" + evento.fechaFin +
                                    "', horaFin = '" + evento.horaFin +
                                    "', nroParticipantes = '" + evento.nroParticipantes +                                
                                    "' WHERE idEvento = " + evento.ID;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;
        }

        public EventoBean SeleccionarEvento(int idEvento)
        {

            EventoBean evento = new EventoBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Evento WHERE  idEvento = " + idEvento;
            //if (!Nombre.Equals(""))  commandString = commandString + "LIKE %"+Nombre+"%";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                evento.ID = (int)dataReader["idEvento"];
                evento.nombre = (string)dataReader["nombre"];
                evento.descripcion = (string)dataReader["descripcion"];
                evento.fechaIni = Convert.ToString(dataReader["fechaIni"]);                
                evento.fechaFin = Convert.ToString(dataReader["fechaFin"]);                             
                evento.nroParticipantes = (int)dataReader["nroParticipantes"];
                evento.horaIni = (string)dataReader["horaIni"];
                evento.horaFin = (string)dataReader["horaFin"];  
            }
            dataReader.Close();
            sqlCon.Close();

            return evento;
        }

        public String DeleteEvento(int idEvento)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Evento SET estado=0 WHERE idEvento = " + idEvento;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            return me;
        }

        public string GetNombre(int id)
        {
            string nombre;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM Evento  WHERE idEvento = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            dataReader.Read();

            nombre = (string)dataReader["nombre"];

            dataReader.Close();
            sqlCon.Close();

            return nombre;
        }

    }
}