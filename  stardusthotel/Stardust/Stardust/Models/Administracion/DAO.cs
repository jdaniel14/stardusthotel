using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class DAO
    {
        public static void agregarParametro(SqlCommand objQuery, String nombreParametro, object valorParametro)
        {
            SqlParameter objParametro = new SqlParameter();
            objParametro.ParameterName = nombreParametro;
            // en caso valorParametro sea null se asigna el DBNull.value al valor del parametro
            objParametro.Value = valorParametro ?? DBNull.Value;
            objQuery.Parameters.Add(objParametro);
        }
    }
}