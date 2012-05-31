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
            objParametro.Value = valorParametro;
            objQuery.Parameters.Add(objParametro);
        }
    }
}