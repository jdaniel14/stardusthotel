﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class ProveedorDAO
    {
        public List<ProveedorBean> ListarProveedor (String razonSocial, String contacto) {

            List<ProveedorBean> listaProveedor = new List<ProveedorBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            
            string commandString = "SELECT * FROM Proveedor WHERE estado=1";
            bool result1 = String.IsNullOrEmpty(razonSocial);//Nombre.Equals("") ;
            bool result2 = String.IsNullOrEmpty(contacto);// Contacto.Equals("");

            if (!result1)
                commandString = commandString + " AND UPPER(razonSocial) LIKE '%" + razonSocial.ToUpper() + "%'";

            if (!result2)
                commandString = commandString + " AND UPPER(contacto) LIKE '%" + contacto.ToUpper() + "%'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
                        
            while (dataReader.Read())
            {
                ProveedorBean proveedor = new ProveedorBean();

                
                proveedor.ID = (int)dataReader["idProveedor"];
                proveedor.razonSocial = (string)dataReader["razonSocial"];
                proveedor.contacto = (string)dataReader["contacto"];
                proveedor.emailContacto = (string)dataReader["emailContacto"];
                proveedor.cargoContacto = (string)dataReader["cargoContacto"];
                proveedor.ruc = (string)dataReader["ruc"];
                proveedor.web = (string)dataReader["web"];
                proveedor.telefono = (string)dataReader["telefono"];
                proveedor.direccion = (string)dataReader["direccion"];
                proveedor.observaciones = (string)dataReader["observaciones"];
                proveedor.estado = 1;
                listaProveedor.Add(proveedor);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaProveedor;
        }

        public String insertarProveedor(ProveedorBean proveedor) {
            String me = "";
            
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            proveedor.estado = 1;
            string commandString = "INSERT INTO Proveedor VALUES ('" + 
			       proveedor.razonSocial + "', '" + 
				   proveedor.contacto + "', '" + 
				   proveedor.emailContacto + "', '" + 
				   proveedor.cargoContacto + "', '" + 
				   proveedor.ruc + "', '" + 
				   proveedor.web + "', '" + 
				   proveedor.telefono + "', '" + 
				   proveedor.direccion + "', '" + 
				   proveedor.observaciones + "', '" + 
				   proveedor.estado + "')";
            
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();
         
            sqlCon.Close();
            return me;
        }

        public String ActualizarProveedor(ProveedorBean proveedor){
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString =  "UPDATE Proveedor " +
                                    "SET razonSocial = '" + proveedor.razonSocial + 
									"', contacto = '" +	proveedor.contacto + 
									"', emailContacto = '" + proveedor.emailContacto +
									"', cargoContacto = '" + proveedor.cargoContacto + 
									"', web = '" + proveedor.web +
									"', telefono = '" + proveedor.telefono + 
									"', direccion = '" + proveedor.direccion + 
									"', observaciones = '" + proveedor.observaciones + 
                                    "' WHERE idProveedor = " + proveedor.ID;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;        
        }

        public ProveedorBean SeleccionarProveedor(int idProveedor){
            ProveedorBean proveedor = new ProveedorBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Proveedor WHERE  idProveedor = " + idProveedor;
            //if (!Nombre.Equals(""))  commandString = commandString + "LIKE %"+Nombre+"%";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                proveedor.ID = (int)dataReader["idProveedor"];
                proveedor.razonSocial = (string)dataReader["razonSocial"];
                proveedor.contacto = (string)dataReader["contacto"];
                proveedor.emailContacto = (string)dataReader["emailContacto"];
                proveedor.cargoContacto = (string)dataReader["cargoContacto"];
                proveedor.ruc = (string)dataReader["ruc"];
                proveedor.web = (string)dataReader["web"];
                proveedor.telefono = (string)dataReader["telefono"];
                proveedor.direccion = (string)dataReader["direccion"];
                proveedor.observaciones = (string)dataReader["observaciones"];
                proveedor.estado = Convert.ToInt32(dataReader["estado"]);         
            }
            dataReader.Close();
            sqlCon.Close();

            return proveedor;
        }

        public String DeleteProveedor(int idProveedor){
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString =  "UPDATE Proveedor SET estado=0 WHERE idProveedor = " + idProveedor;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            return me;
        }

        /*--------Asignar Productos por Proveedor----*/

        public void InsertarProveedorxProducto( int idproveedor,ProductoxProveedorBean prod)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open(); 
            int i;
            for (i = 0; i < prod.listProdProv.Count; i++)
            {
                if (prod.listProdProv[i].precio>0)
                {
                   string commandString = "INSERT INTO ProductoXProveedor VALUES ('" +
                   idproveedor + "', '" +
                   prod.listProdProv[i].ID + "', '" +
                   prod.listProdProv[i].precio + "', '" +
                   prod.listProdProv[i].cantMaxima + "')";
                    SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                    sqlCmd.ExecuteNonQuery();
                }
            }

            sqlCon.Close();
        }

        public ProductoxProveedorBean obtenerlistaproductos(int idproveedor)
        {
            ProductoxProveedorBean prod = new ProductoxProveedorBean();
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            int i = 0;
            int idprove;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM ProductoXProveedor  WHERE idProveedor="+idproveedor;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            prod.listProdProv = new List<ProductoProveedor>();
            while (dataReader.Read())
            {
                ProductoProveedor prodProveedor = new ProductoProveedor();
                idprove= (int)dataReader["idProveedor"];
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

        public void ActualizarproductosxProveedor(int idproveedor, ProductoxProveedorBean prod)
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

        public string GetNombre(int id)
        {
            string nombre;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM Proveedor  WHERE idProveedor = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            dataReader.Read();
            
            nombre = (string)dataReader["razonSocial"];

            dataReader.Close();
            sqlCon.Close();

            return nombre;
        }

        public string GetNombreProducto(int id)
        {
            string nombre;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM Producto  WHERE idProducto = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            dataReader.Read();

            nombre = (string)dataReader["nombre"];

            dataReader.Close();
            sqlCon.Close();

            return nombre;
        }

        public OrdenCompras ObtenerOC(int id)
        {
            OrdenCompras OC = new OrdenCompras();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM OrdenCompra WHERE estado = 'Atendido' OR estado = 'Parcialmente_Atendido' AND idProveedor = "+id;
            //if (!Nombre.Equals(""))  commandString = commandString + "LIKE %"+Nombre+"%";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                OrdenCompra orden = new OrdenCompra();
                orden.id = (int)dataReader["idOrdenCompra"];
                orden.precio = (decimal)dataReader["precioTotal"];
                orden.fecha = Convert.ToString(dataReader["fechaPedido"]);
                orden.estado = (string)dataReader["estado"];
                OC.listaOC.Add(orden);
            }
            dataReader.Close();
            sqlCon.Close();

            return OC;
        }

        public OrdenCompras ListarOC(int id)
        {
            OrdenCompras OC = new OrdenCompras();
                  
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM OrdenCompraDetalle WHERE idOrdenCompra = " + id;
            //if (!Nombre.Equals(""))  commandString = commandString + "LIKE %"+Nombre+"%";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            decimal subtotal = 0;

            while (dataReader.Read())
            {
                OrdenCompraDetalle orden = new OrdenCompraDetalle();
                orden.idOC = (int)dataReader["idOrdenCompra"];
                orden.id = (int)dataReader["idProducto"];
                orden.precio = (decimal)dataReader["precio"];
                orden.cantidad = (int)dataReader["cantidad"];
                orden.nombre = GetNombreProducto(orden.id);
                subtotal += orden.precio;
                OC.listaOCDetalle.Add(orden);
            }
            dataReader.Close();
            sqlCon.Close();

            decimal igv = subtotal * 18 / 100;
            decimal total = subtotal + igv;

            OC.subtotal = subtotal;
            OC.igv = igv;
            OC.total = total;

            return OC;
        }

        public void RegistrarPagoContado(OrdenCompras OC)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "INSERT INTO Factura VALUES (" + OC.subtotal + " , " + OC.igv + " , "+ OC.total + " , 'Contado' , GETDATE() , " + OC.id + " , NULL , NULL)";
            
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion);
            sqlCon2.Open();

            string commandString2;

            if (OC.total == OC.pagado)
            {
                commandString2 = "UPDATE OrdenCompra SET estado = 'Cancelado' WHERE idOrdenCompra = " + OC.id;
            }
            else
            {
                commandString2 = "UPDATE OrdenCompra SET estado = 'Parcialmente_Atendido' WHERE idOrdenCompra = " + OC.id;
            }
            SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon2);
            sqlCmd2.ExecuteNonQuery();

            sqlCon2.Close();
        }

        public void RegistrarPagoCredito(OrdenCompras OC)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "INSERT INTO Factura VALUES (" + OC.subtotal + " , " + OC.igv + " , " + OC.total + " , 'Credito' , GETDATE() , " + OC.id + " , " + OC.interes + " , " + OC.numCoutas + ")";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            
                SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion);
                sqlCon2.Open();

                string commandString2;

                commandString2 = "UPDATE OrdenCompra SET estado = 'Cancelado' WHERE idOrdenCompra = " + OC.id;
                
                SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon2);
                sqlCmd2.ExecuteNonQuery();

                sqlCon2.Close();
        }
    }
}




