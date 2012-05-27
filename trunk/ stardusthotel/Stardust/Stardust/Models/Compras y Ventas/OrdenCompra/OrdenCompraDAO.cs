using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Stardust.Models
{
    public class OrdenCompraDAO
    {
        public IEnumerable<Producto> GetProducto(int id)
        {
            List<Producto> listaProducto = new List<Producto>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT pxp.idProducto, p.nombre FROM ProductoXProveedor pxp , Producto p WHERE pxp.idProveedor = " + id + " AND pxp.idProducto = p.idProducto";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                Producto producto = new Producto();
                producto.id = Convert.ToString(dataReader["pxp.idProducto"]);
                producto.Nombre = (string)dataReader["p.nombre"];

                listaProducto.Add(producto);
            }

            return listaProducto;
        }

    }
}