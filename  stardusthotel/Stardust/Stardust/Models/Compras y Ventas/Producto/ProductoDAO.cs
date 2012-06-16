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
            try
            {
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
                sqlCon.Close();
            }
            catch
            {
                
            }
            
            return listaProducto;
        }

        public string RegistrarProducto(ProductoBean producto)
        {
            
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                producto.estado = 1;
                string commandString = "INSERT INTO Producto VALUES ('" + producto.nombre + "', '" + producto.descripcion + "', '" + producto.estado + "')";

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                sqlCon.Close();
                producto.conexion = "Bien";
            }
            catch(Exception e)
            {
                producto.conexion = "Falla en Conexión";
            }

            return producto.conexion;
            
        }
        public ProductoBean GetProducto(int id)
        {
            ProductoBean producto = new ProductoBean();
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "SELECT * FROM Producto WHERE idProducto = " + id;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                dataReader.Read();
                producto.ID = (int)dataReader["idProducto"];
                producto.nombre = (string)dataReader["nombre"];
                producto.descripcion = (string)dataReader["descripcion"];
                producto.estado = Convert.ToInt32(dataReader["estado"]);
                sqlCon.Close();
                producto.conexion = "Bien";
            }
            catch
            {
                producto.conexion = "no hay conexion";
            }
            
            return producto;
        }
        //public  void buscarProducto(string nombre)
        //{

        //}
        //public void SeleccionarProducto(int ID)
        //{
        //}
        public string ActualizarProducto(ProductoBean producto)
        {
            try
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
                producto.conexion = "Bien";
            }
            catch
            {
                producto.conexion = "no hay conexion";
            }

            return producto.conexion;
            
        }
        public string EliminarProducto(int id)
        {
            string conexion="";
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "UPDATE Producto " +
                                        "SET estado = 0 WHERE idProducto = " + id;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                conexion = "Bien";
            }
            catch
            {
                conexion = "no hay conexion";
            }

            return conexion;
        }

        /*----Asignar Productos a Almacen----*/

        public int obteneralmacen(int idhotel){
            
            int idalmacen=0;
            try
            {
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
            }
            catch
            {
                idalmacen = -1; // no hay conexion
            }
            return idalmacen;
        }
        public string InsertaralmacenxProducto(ProductoXAlmacenBean prod)
        {
            string conexion = "";
            try
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
                conexion = "Bien";
            }
            catch
            {

                conexion = "no hay conexion";
            }

            return conexion;
        }

        public ProductoXAlmacenBean obtenerlistaproductos(int idalmacen)
        {
            ProductoXAlmacenBean prod = new ProductoXAlmacenBean();
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            int i = 0;
            int idal;

            try
            {
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
                prod.conexion = "Bien";
            }
            catch 
            {
                prod.conexion = "Error";
            }
            return prod;
        }

        public string Actualizarproductosxalmacen( ProductoXAlmacenBean prod)
        {
            string conexion = "";
            try
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

                conexion = "Bien";
            }
            catch 
            {
                conexion = "Error";
            }

            return conexion;

        }

        public string actualizarStock (ProductoXAlmacenBean prod)
        {
            int stocksobrante = 0;
            string conexion = "";
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();
                
                for (int i = 0; i < prod.listProdalmacen.Count; i++)
                {
                    stocksobrante = prod.listProdalmacen[i].stockactual - prod.listProdalmacen[i].stocksaliente;
                    string commandString = "UPDATE ProductoXAlmacen SET stockActual = " + stocksobrante +
                                           " WHERE idAlmacen = " + prod.idalmacen + "AND idProducto = " + prod.listProdalmacen[i].ID;

                    SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                    sqlCmd.ExecuteNonQuery();

                }

                sqlCon.Close();
                conexion = "Bien";
            }
            catch
            {
                conexion = "Error";
            }

            return conexion;
        }
    }
}