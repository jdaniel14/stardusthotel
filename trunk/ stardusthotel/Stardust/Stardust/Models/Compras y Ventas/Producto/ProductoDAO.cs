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
            producto.ID = (int)dataReader["idProducto"];
            producto.nombre = (string)dataReader["nombre"];
            producto.descripcion = (string)dataReader["descripcion"];
            producto.estado = Convert.ToInt32(dataReader["estado"]);
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

        /*----Asignar Productos a Almacen----*/

        public int obteneralmacen(int idhotel){
            
            int idalmacen=0;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM Almacen  WHERE idHotel=" + idhotel;
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            
            while (dataReader.Read())
            {
               idalmacen = (int)dataReader["idalmacen"];
            }
            dataReader.Close();
            sqlCon.Close();


            return idalmacen;
        }
        public void InsertaralmacenxProducto(ProductoXAlmacenBean prod)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            int i;
            for (i = 0; i < prod.listProdalmacen.Count; i++)
            {
                if (!(prod.listProdalmacen[i].stockminimo == 0 && prod.listProdalmacen[i].stockactual == 0 && prod.listProdalmacen[i].stockmaximo == 0))
                {
                    string commandString = "INSERT INTO ProductoXAlmacen VALUES ('" +
                    prod.idalmacen + "', '" +
                    prod.listProdalmacen[i].ID + "', '" +
                    prod.listProdalmacen[i].stockminimo + "', '" +
                    prod.listProdalmacen[i].stockmaximo + "', '" +
                    prod.listProdalmacen[i].stockactual + "')";
                    SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                    sqlCmd.ExecuteNonQuery();
                }
            }

            sqlCon.Close();
        }

        public ProductoXAlmacenBean obtenerlistaproductos(int idalmacen)
        {
            ProductoXAlmacenBean prod = new ProductoXAlmacenBean();
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            int i = 0;
            int idal;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM ProductoXAlmacen  WHERE idalmacen=" + idalmacen;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            prod.listProdalmacen = new List<ProductoAlmacen>();
            
            while (dataReader.Read())
            {
                ProductoAlmacen prodalmacen = new ProductoAlmacen();
                
                idal = (int)dataReader["idalmacen"];
                prodalmacen.ID = (int)dataReader["idProducto"];
                prodalmacen.stockminimo = (int)dataReader["stockMinimo"];
                prodalmacen.stockactual = (int)dataReader["stockActual"];
                prodalmacen.stockmaximo = (int)dataReader["stockMaximo"];
                i++;
                prod.listProdalmacen.Add(prodalmacen);
            }
            dataReader.Close();
            sqlCon.Close();

            return prod;
        }

        public void Actualizarproductosxalmacen( ProductoXAlmacenBean prod)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            for (int i = 0; i < prod.listProdalmacen.Count; i++)
            {

                string commandString = "UPDATE ProductoXAlmacen SET stockMinimo = " + prod.listProdalmacen[i].stockminimo + 
                                        " , stockActual = " + prod.listProdalmacen[i].stockactual +
                                        " , stockMaximo= " + prod.listProdalmacen[i].stockmaximo +
                                      " WHERE idAlmacen = " + prod.idalmacen + "AND idProducto = " + prod.listProdalmacen[i].ID;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

            }

            sqlCon.Close();
        }


    }
}