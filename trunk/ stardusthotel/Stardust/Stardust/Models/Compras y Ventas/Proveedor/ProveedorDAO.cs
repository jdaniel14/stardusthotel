using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;

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
                proveedor.telefonocontacto = (string)dataReader["telefonocontacto"];
                proveedor.estado = 1;
                listaProveedor.Add(proveedor);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaProveedor;
        }

        public ProveedorList ListarProveedor2()
        {

            ProveedorList listaProveedor = new ProveedorList();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Proveedor WHERE estado=1";

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
                proveedor.telefonocontacto = (string)dataReader["telefonocontacto"];
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
                   proveedor.estado + "', '" + 
				   proveedor.telefonocontacto + "')";
            
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
                                    "', telefonocontacto = '" + proveedor.telefonocontacto + 
                                    "' WHERE idProveedor = " + proveedor.ID;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;        
        }

        public Boolean existeproveedor(string razonsocial)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM Proveedor WHERE estado=1 AND UPPER(razonSocial) LIKE '%" + razonsocial.ToUpper() + "%'";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                ProveedorBean proveedor = new ProveedorBean();
                proveedor.razonSocial = (string)dataReader["razonSocial"];
                proveedor.ruc = (string)dataReader["ruc"];
                return true;

            }
            else { return false; }


            
        }

        public Boolean existeproveedor2(string ruc)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM Proveedor WHERE estado=1 AND UPPER(razonSocial) LIKE '%" + ruc.ToUpper() + "%'";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                ProveedorBean proveedor = new ProveedorBean();
                proveedor.razonSocial = (string)dataReader["razonSocial"];
                proveedor.ruc = (string)dataReader["ruc"];
                return true;

            }
            else { return false; }

        }

        public ProveedorBean SeleccionarProveedor(int idProveedor){
            ProveedorBean proveedor = new ProveedorBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Proveedor WHERE  idProveedor = " + idProveedor;
            
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
                proveedor.telefonocontacto = (string)dataReader["telefonocontacto"];
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
                prodProveedor.precio =(Decimal) dataReader["precio"];
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

        public string GetEstado(int id)
        {
            string estado = null;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM OrdenCompra WHERE idOrdenCompra = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                estado = (string)dataReader["estado"];
            }

            return estado;
        }

        public OrdenCompras ObtenerOC(int id)
        {
            OrdenCompras OC = new OrdenCompras();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM OrdenCompra WHERE estado = 'Atendido' OR estado = 'Parcialmente Atendido' OR estado = 'Parcialmente Pagada' AND idProveedor = " + id;
            
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

            OC.estado = GetEstado(id);

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM OrdenCompraDetalle WHERE idOrdenCompra = " + id;

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
                orden.precio1 = Convert.ToString(orden.precio);
            }
            dataReader.Close();
            sqlCon.Close();

            if (OC.estado.Equals("Parcialmente Pagada"))
            {
                String cadenaConfiguracion2 = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
                SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion2);
                sqlCon2.Open();

                string commandString2 = "SELECT * FROM Factura WHERE idOrdenCompra = " + id;

                SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon2);
                SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

                if (dataReader2.Read())
                {
                    OC.subtotal = (decimal)dataReader2["subTotal"];
                    OC.igv = (decimal)dataReader2["impuesto"];
                    OC.paga = (decimal)dataReader2["total"];                    
                    OC.total = OC.subtotal + OC.igv;
                    OC.pagar = OC.total - OC.paga;                    
                }
            }
            else
            {
                decimal igv = subtotal * 18 / 100;
                decimal total = subtotal + igv;

                OC.subtotal = subtotal;
                OC.igv = igv;
                OC.total = total;
                OC.pagar = OC.total;
            }

           

            return OC;
        }

        public void RegistrarPagoContado(OrdenCompras OC)
        {
            OC.pagado = OC.pagado;
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "INSERT INTO Factura VALUES (" + OC.subtotal + " , " + OC.igv + " , " + OC.pagado + " , 'Contado' , GETDATE() , NULL, NULL, " + OC.id + " )";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion);
            sqlCon2.Open();

            string commandString2;

            if (OC.total == (OC.pagado+OC.paga))
            {
                commandString2 = "UPDATE OrdenCompra SET estado = 'Cancelado' WHERE idOrdenCompra = " + OC.id;
            }
            else
            {
                commandString2 = "UPDATE OrdenCompra SET estado = 'Parcialmente Pagada' WHERE idOrdenCompra = " + OC.id;
            }
            SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon2);
            sqlCmd2.ExecuteNonQuery();

            sqlCon2.Close();
        }

        public void RegistrarPagoCredito(OrdenCompras OC)
        {
            OC.pagado = OC.pagado;
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "INSERT INTO Factura VALUES (" + OC.subtotal + " , " + OC.igv + " , " + OC.pagado + " , 'Credito' , GETDATE() , " + OC.interes + " , " + OC.numCuotas + " , " + OC.id + ")";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
                        
            SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion);
            sqlCon2.Open();

            string commandString2;

            if (OC.total == (OC.pagado + OC.paga))
            {
                commandString2 = "UPDATE OrdenCompra SET estado = 'Cancelado' WHERE idOrdenCompra = " + OC.id;
            }
            else
            {
                commandString2 = "UPDATE OrdenCompra SET estado = 'Parcialmente Pagada' WHERE idOrdenCompra = " + OC.id;
            }

            SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon2);
            sqlCmd2.ExecuteNonQuery();

            sqlCon2.Close();
        }

        public List<Proveedors> GetList()
        {
            List<Proveedors> list = new List<Proveedors>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Proveedor WHERE estado=1";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            //list.Add(new Proveedors { id = "0",name = "Todo"});

            while (dataReader.Read())
            {
                Proveedors proveedor = new Proveedors();


                proveedor.id = Convert.ToString(dataReader["idProveedor"]);
                proveedor.name = (string)dataReader["razonSocial"];

                list.Add(proveedor);
            }
            dataReader.Close();
            sqlCon.Close();

            return list;
        }

        public OrdenCompras GetOC(int id)
        {
            OrdenCompras OC = new OrdenCompras();

            OC.listaOC = new List<OrdenCompra>();

            OC.nombre = GetNombre(id);
            OC.id = id;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM OrdenCompra ";

            if (id > 0)
                commandString += "WHERE idProveedor = " + id;

            commandString += " ORDER BY estado";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                DateTime date = new DateTime();
                OrdenCompra Orden = new OrdenCompra();
                Orden.id = (int)dataReader["idOrdenCompra"];
                date = (DateTime)dataReader["fechaPedido"];
                Orden.fecha = String.Format("{0:d/M/yyyy}", date);
                Orden.estado = (string)dataReader["estado"];
                Orden.precio = (decimal)dataReader["precioTotal"];
                OC.listaOC.Add(Orden);
            }

            return OC;
        }
    }
}




