using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class VariablesDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

        public VariablesBean getVariables() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Politica";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                VariablesBean variables = new VariablesBean();

                variables.horasEsperaConfirmarReserva = (int)data.GetValue(0);
                variables.porcAdelanto = (int)data.GetValue(1);
                variables.diasEsperarSinRetener = (int)data.GetValue(2);
                variables.porcRetencion = (int)data.GetValue(3);

            sql.Close();

            return variables;
        }

        public void actualizarVariables( VariablesBean variables ) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update Politica SET horasEsperaConfirmarReserva = " + variables.horasEsperaConfirmarReserva + 
                                ", porcAdelanto = " + variables.porcAdelanto +
                                ", diasEsperarSinRetener = " + variables.diasEsperarSinRetener +
                                ", porcRetencion = " + variables.porcRetencion ;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void valorDefault() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Politica";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                int filas = data.RecordsAffected;

                if (filas != 0) {
                    sql.Close();
                    sql.Open();
                    String command2 = "Insert into Politica values ( 0 , 0 , 0 , 0 ) ";
                    SqlCommand query2 = new SqlCommand(command2, sql);
                    query2.ExecuteNonQuery();
                }

            sql.Close();
        }
    }
}