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
        public Producto GetProducto(int id)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM ProductoXAlmacen WHERE stockMinimo = stockActual and idProducto = "+id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                Producto producto = new Producto();
                producto.id = Convert.ToString(dataReader["idProducto"]);
                producto.stockActual = (int)dataReader["stockActual"];
                producto.stockMaximo = (int)dataReader["stockMaximo"];
                producto.stockMinimo = (int)(dataReader["stockMinimo"]);

                sqlCon.Close();
                return producto;
            }
            else
            {
                sqlCon.Close();
                return null;
            }            
        }

        public void GuardarOrdenCompra(OrdenProducto producto)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            decimal total=0;

            for (int i = 0; i < producto.listaProducto.Count;i++ )
            {
                int valor = producto.listaProducto.ElementAt(i).cantidad;
                decimal precio = producto.listaProducto.ElementAt(i).precio;
                if (valor > 0)
                    total += (valor*precio);
            }

            string commandString = "INSERT INTO OrdenCompra VALUES (GETDATE(), 'Registrado' , " + producto.id + " , " + total+" )";
            
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            commandString = "SELECT * FROM OrdenCompra";

            SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd2.ExecuteReader();

            int id = 0;

            while (dataReader.Read())
            {
                id = (int)dataReader["idOrdenCompra"];
            }

            sqlCon.Close();

            String cadenaConfiguracion2 = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion2);
            sqlCon2.Open();

            for (int i = 0; i < producto.listaProducto.Count; i++)
            {
                Producto prod = producto.listaProducto.ElementAt(i);
                prod.precio = (prod.precio * prod.cantidad);
                commandString = "INSERT INTO OrdenCompraDetalle VALUES ( " + prod.id +" , "+ id + " , "+ prod.cantidad + " , "+ prod.precio +" )";
                SqlCommand sqlCmd3 = new SqlCommand(commandString, sqlCon2);
                sqlCmd3.ExecuteNonQuery();
            }

            sqlCon2.Close();
        }

        public List<OrdenCompraBean> getlista(string nombre, string fecha1, string fecha2)
        {


            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();


            string comand2;//="SELECT * FROM Proveedor WHERE UPPER(razonSocial) LIKE "+ 
            bool result1 = String.IsNullOrEmpty(nombre);
            bool result2 = String.IsNullOrEmpty(fecha1);
            bool result3 = String.IsNullOrEmpty(fecha2);

            int idproveedor = 0;
            if (!result1)
            {//saca el id del proveedor
                comand2 = "SELECT * FROM Proveedor WHERE  UPPER(razonSocial) LIKE '%" + nombre.ToUpper() + "%'";

                SqlCommand sqlCmd = new SqlCommand(comand2, sqlCon);
                SqlDataReader dataReader = sqlCmd.ExecuteReader();
                while (dataReader.Read())
                {
                    idproveedor = (int)dataReader["idProveedor"];
                }


            }

            sqlCon.Close();

            // if (!result2)  commandString = commandString + " AND UPPER(fecha) LIKE '%" + fecha1.ToUpper() + "%'";
            String cadenaConfiguracion2 = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion2);
            sqlCon2.Open();

            string commandString = "SELECT * FROM OrdenCompra WHERE idProveedor = " + idproveedor;

            SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon2);
            SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

            List<OrdenCompraBean> orden = new List<OrdenCompraBean>();
            while (dataReader2.Read())
            {
                OrdenCompraBean ord = new OrdenCompraBean();
                ord.nombreproveedor = nombre;
                ord.idproveedor = (int)dataReader2["idProveedor"];
                ord.estado = (string)dataReader2["estado"];
                ord.idOrdenCompra = (int)dataReader2["idOrdenCompra"];
                ord.fecha = Convert.ToString(dataReader2["fechaPedido"]);
                ord.preciototal = (decimal)dataReader2["preciototal"];

                orden.Add(ord);
                //idproveedor = (int)dataReader2["idProveedor"];
            }

            sqlCon2.Close();
            //orden = new List<OrdenCompraBean>();
            return orden;

        }

    
    

        /*--------------nota de entrada -------*/

        public NotaEntradaBean RegistrarNotaEntrada(int idordencompra)
        {
            NotaEntradaBean orden;// = new NotaEntradaBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM OrdenCompra WHERE idOrdenCompra = " + idordencompra;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            
            while (dataReader.Read())
            {
                
                //OrdenCompraBean ord = new OrdenCompraBean();
                //ord.nombreproveedor = nombre;
                //ord.idproveedor = (int)dataReader2["idProveedor"];
                //ord.estado = (string)dataReader2["estado"];
                //ord.idOrdenCompra = (int)dataReader2["idOrdenCompra"];
                //ord.fecha = Convert.ToString(dataReader2["fechaPedido"]);
                //ord.preciototal = (decimal)dataReader2["preciototal"];

                //orden.Add(ord);
                //idproveedor = (int)dataReader2["idProveedor"];
            }


            return (orden);
            
        }
    
    
    }

}