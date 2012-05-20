using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models.Administracion
{
    public class UsuarioDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

        public void registrarUsuario(UsuarioBean usuario) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            sql.Close();
        }
    }
}