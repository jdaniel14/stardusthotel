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


        public void InsertaralmacenxProducto(ProductoXAlmacenBean prod)
        {
            //String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            //SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            //sqlCon.Open();
            //int i;
            //for (i = 0; i < prod.listProdProv.Count; i++)
            //{
            //    if (prod.listProdProv[i].precio > 0)
            //    {
            //        string commandString = "INSERT INTO ProductoXProveedor VALUES ('" +
            //        idproveedor + "', '" +
            //        prod.listProdProv[i].ID + "', '" +
            //        prod.listProdProv[i].precio + "', '" +
            //        prod.listProdProv[i].cantMaxima + "')";
            //        SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            //        sqlCmd.ExecuteNonQuery();
            //    }
            //}

            //sqlCon.Close();
        }

        public ProductoXAlmacenBean obtenerlistaproductos(int idhotel)
        {
            ProductoxProveedorBean prod = new ProductoxProveedorBean();
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            int i = 0;
            int idprove;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM ProductoXAlmacen  WHERE idalmacen="  ;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            prod.listProdProv = new List<ProductoProveedor>();
            while (dataReader.Read())
            {
                ProductoProveedor prodProveedor = new ProductoProveedor();
                idprove = (int)dataReader["idProveedor"];
                prodProveedor.ID = (int)dataReader["idProducto"];
                prodProveedor.precio = (decimal)dataReader["precio"];
                prodProveedor.cantMaxima = (int)dataReader["cantPedidoMax"];
                i++;
                prod.listProdProv.Add(prodProveedor);
            }
            dataReader.Close();
            sqlCon.Close();
            ProveedorBean prov = SeleccionarProveedor(idproveedor);

            prod.Proveedor = prov.razonSocial;

            return prod;
        }

        public void Actualizarproductosxalmacen( ProductoXAlmacenBean prod)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            for (int i = 0; i < prod.listProdProv.Count; i++)
            {

                string commandString = "UPDATE ProductoXProveedor SET precio = " + prod.listProdProv[i].precio + " , cantPedidoMax = " + prod.listProdProv[i].cantMaxima +
                                " WHERE idProveedor = " + idproveedor + "AND idProducto = " + prod.listProdProv[i].ID;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

            }

            sqlCon.Close();
        }


    }
}