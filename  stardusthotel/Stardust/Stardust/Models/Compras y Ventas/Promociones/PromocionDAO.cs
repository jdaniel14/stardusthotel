using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Entity;

namespace Stardust.Models
{
    public class PromocionDAO
    {
        public void RegistrarPromocion(PromocionBean promocion)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            promocion.estado = 1;
            promocion.nombre = "asd";
            promocion.idhotel = Convert.ToInt32(promocion.ID);
            string commandString = "INSERT INTO Promocion VALUES ('" + promocion.nombre + "', '" + promocion.descripcion + "', " + promocion.razon + " , " + promocion.porcDescontar + " ,'" + promocion.estado + "', " + promocion.tipoDescuento + " , " + promocion.idhotel + ")";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
        }
    }
}