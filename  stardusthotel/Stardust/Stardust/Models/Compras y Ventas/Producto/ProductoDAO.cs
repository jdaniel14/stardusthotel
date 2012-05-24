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
    public class ProductoDAO
    {
        public List<ProductoBean> ListarProducto(string nombre)
        {
            List<ProductoBean> listaProducto = new List<ProductoBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "";

            if (!String.IsNullOrEmpty(nombre))
            {
                commandString = "Select * FROM Producto WHERE estado = 1 AND UPPER(nombre) LIKE '%" + nombre.ToUpper() + "%'";
            }
            else
            {
                commandString = "SELECT * FROM Producto WHERE estado = 1 ";
            }

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                ProductoBean producto = new ProductoBean();
                producto.ID = (int)dataReader["idProducto"];
                producto.nombre = (string)dataReader["nombre"];
                producto.descripcion = (string)dataReader["descripcion"];
                producto.estado = Convert.ToInt32(dataReader["estado"]);

                listaProducto.Add(producto);
            }
            return listaProducto;
        }

        public void RegistrarProducto(ProductoBean producto)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            producto.estado = 1;
            string commandString = "INSERT INTO Producto VALUES ('" + producto.nombre + "', '" + producto.descripcion + "', '" + producto.estado +"')";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
        }
        public ProductoBean GetProducto(int id)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Producto WHERE idProducto = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            dataReader.Read();

            ProductoBean producto = new ProductoBean();
            producto.ID = (int)dataReader["@idProducto"];
            producto.nombre = (string)dataReader["@nombre"];
            producto.descripcion = (string)dataReader["@descripcion"];
            producto.estado = Convert.ToInt32(dataReader["@estado"]);
            sqlCon.Close();

            return producto;
        }
        public /*List<Producto>*/ void buscarProducto(string nombre)
        {

        }
        public void SeleccionarProducto(int ID)
        {
        }
        public void ActualizarProducto(ProductoBean producto)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Producto " +
                                    "SET nombre = '" + producto.nombre + "', descripcion = '" + producto.descripcion + "' " +
                                    "WHERE idProducto = " + producto.ID;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
        }
        public void EliminarProducto(int id)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Producto " +
                                    "SET estado = 0 WHERE idProducto = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
        }
    }
}