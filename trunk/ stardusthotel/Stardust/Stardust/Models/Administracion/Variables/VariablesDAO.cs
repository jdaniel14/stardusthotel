using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using log4net;

namespace Stardust.Models
{
    public class VariablesDAO
    {
        //String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(VariablesDAO));

        public VariablesBean getVariables() {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                VariablesBean variables = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Politica";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows) // si existe información
                {
                    objDataReader.Read(); // entonces lee
                    variables = new VariablesBean(); //recien creo el objeto que almacenará la info
                    variables.horasEsperaConfirmarReserva = Convert.ToInt32(objDataReader[0]);
                    variables.porcAdelanto = Convert.ToInt32(objDataReader["porcAdelanto"].ToString().Trim());
                    variables.diasEsperarSinRetener = Convert.ToInt32(objDataReader["diasEsperarSinRetener"].ToString().Trim());
                    variables.porcRetencion = Convert.ToInt32(objDataReader["porcRetencion"].ToString().Trim());
                }

                return variables;
                
            }
            catch (Exception e)
            {
                log.Error("getVariables(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        

        public void actualizarVariables( VariablesBean variables ) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery =   "UPDATE Politica "+
                                    "SET horasEsperaConfirmarReserva = @horasEsperaConfirmarReserva" + "," +
                                        "porcAdelanto = @porcAdelanto" + "," +
                                        "diasEsperarSinRetener = @diasEsperarSinRetener" + "," +
                                        "porcRetencion = @porcRetencion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                
                DAO.agregarParametro(objQuery, "horasEsperaConfirmarReserva", variables.horasEsperaConfirmarReserva);
                DAO.agregarParametro(objQuery, "porcAdelanto", variables.porcAdelanto);
                DAO.agregarParametro(objQuery, "diasEsperarSinRetener", variables.diasEsperarSinRetener);
                DAO.agregarParametro(objQuery, "porcRetencion", variables.porcRetencion);

                objQuery.ExecuteNonQuery();                
            }
            catch (Exception e)
            {
                log.Error("actualizarVariables(EXCEPTION): ", e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
            
        }

        //public void valorDefault() {
        //    SqlConnection sql = new SqlConnection(cadenaDB);

        //    sql.Open();

        //    String command = "Select * from Politica";

        //    SqlCommand query = new SqlCommand(command, sql);

        //    SqlDataReader data = query.ExecuteReader();
                            
        //    if (!data.HasRows)
        //    {
        //        sql.Close();
        //        sql.Open();
        //        String command2 = "Insert into Politica values ( 10 , 12 , 15 , 0 ) ";
        //        SqlCommand query2 = new SqlCommand(command2, sql);
        //        query2.ExecuteNonQuery();
        //    }

        //    sql.Close();
        //}
    }
}