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
        /**********-------------Orden Compra---------***************/
        ProductoDAO prod= new ProductoDAO();



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
                producto.idproducto = (int)dataReader["idProducto"];
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
            
            int cantidad = 0;
            for (int i = 0; i<producto.listaProducto.Count; i++)
            {
                if (producto.listaProducto[i].estadoguardar) cantidad++;
            }
            try
            {
                if (cantidad > 0)
                {
                    String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                    SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                    sqlCon.Open();

                    decimal total = 0; // decimal

                    for (int i = 0; i < producto.listaProducto.Count; i++)
                    {
                        if (producto.listaProducto[i].estadoguardar)
                        {
                            int valor = producto.listaProducto.ElementAt(i).cantidad;
                            decimal precio = producto.listaProducto.ElementAt(i).precio; // decimal
                            total += (valor * precio);
                        }
                    }

                    string commandString = "INSERT INTO OrdenCompra (fechaPedido, estado, precioTotal, idProveedor, idHotel) VALUES (GETDATE(), 'Tramite' , " + total + " , " + producto.id +","+producto.idhotel+ " )";//idproveedor

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
                        if (producto.listaProducto[i].estadoguardar)
                        {
                            decimal precio = 0; // decimal
                            Producto prod = producto.listaProducto.ElementAt(i);
                            precio = (prod.precio * prod.cantidad);
                            commandString = "INSERT INTO OrdenCompraDetalle (idProducto,idOrdenCompra,cantidad,precio) VALUES ( " + prod.idproducto + " , " + id + " , " + prod.cantidad + " , " + precio + " )";
                            SqlCommand sqlCmd3 = new SqlCommand(commandString, sqlCon2);
                            sqlCmd3.ExecuteNonQuery();
                        }
                    }

                    sqlCon2.Close();
                }

            }
            catch
            {

            }

        }

        public List<OrdenCompraBean> getlista(string nombre, string fecha1, string fecha2) //nombre proveedor
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
                DateTime date = new DateTime();
                OrdenCompraBean ord = new OrdenCompraBean();
                ord.nombreproveedor = nombre;
                ord.idproveedor = (int)dataReader2["idProveedor"];
                ord.estado = (string)dataReader2["estado"];
                ord.idOrdenCompra = (int)dataReader2["idOrdenCompra"];
                ord.idhotel = (int)dataReader2["idHotel"];
                date = Convert.ToDateTime(dataReader2["fechaPedido"]);
                ord.fecha = String.Format("{0:d/M/yyyy}",date);
                ord.preciototal = (decimal)dataReader2["preciototal"];

                orden.Add(ord);
                //idproveedor = (int)dataReader2["idProveedor"];
            }

            sqlCon2.Close();
            //orden = new List<OrdenCompraBean>();
            return orden;

        }

        public OrdenCompraBean buscarordencompra(int ordencompra)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM OrdenCompra WHERE idOrdenCompra = " + ordencompra;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            OrdenCompraBean orden = new OrdenCompraBean();

            while (dataReader.Read())
            {
                DateTime date = new DateTime();
                orden.idOrdenCompra = ordencompra;
                orden.estado = (string)dataReader["estado"];
                date = Convert.ToDateTime(dataReader["fechaPedido"]);
                orden.fecha = String.Format("{0:d/M/yyyy}",date);
                orden.preciototal = (decimal)dataReader["preciototal"];
                orden.idproveedor=(int)dataReader["idProveedor"];
                orden.idhotel = (int)dataReader["idHotel"];
            }
            sqlCon.Close();

            //detalle de orden compra

            //String cadenaConfiguracion2 = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            String cadenaConfiguracion2 = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion2);
            sqlCon2.Open();

            string commandString2 = "SELECT * FROM OrdenCompraDetalle WHERE idOrdenCompra = " + ordencompra;

            SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon2);
            SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

            //List<DetalleOrdenCompra> detalle = new List<DetalleOrdenCompra>();
            orden.detalle = new List<DetalleOrdenCompra>();
            while (dataReader2.Read())
            {
                DetalleOrdenCompra detalle = new DetalleOrdenCompra();
                detalle.ID=(int)dataReader2["idProducto"];
                detalle.Cantidad=(int)dataReader2["cantidad"];
                detalle.precio=(decimal)dataReader2["precio"];
                orden.detalle.Add(detalle);
            }
            sqlCon2.Close();
            return orden;
        }
        
        public void cambiarestado(int ordencompra, string estado)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE OrdenCompra " +
                                    "SET estado = '" + estado + "'" + 
                                    " WHERE idOrdenCompra = " + ordencompra;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
        }

        /*--------------nota de entrada -------*/


        public List<NotaEntradaBean> ListarNotasEntradas(int idordencompra)
        {
            // = new NotaEntradaBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM GuiaRemision WHERE idOrdenCompra = " + idordencompra;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            List<NotaEntradaBean> orden2 = new List<NotaEntradaBean>();

            while (dataReader.Read())
            {
                NotaEntradaBean orden = new NotaEntradaBean();
                orden.idguiaRemision = (int)dataReader["idGuiaRemision"];
                orden.idordencompra = idordencompra;                
                orden.fechaemitida = Convert.ToString(dataReader["fechaEntrega"]);
                orden2.Add(orden);
            }

            sqlCon.Close();


            return (orden2);
            
        }

        public void GuardarNotaEntrada(NotaEntradaBean nota, string estado)
        {
            int cantidad2 = 0;

            for (int i = 0; i < nota.detallenotaentrada.Count; i++)
            {
                if (nota.detallenotaentrada[i].cantidadentrante > 0) cantidad2++;
            }
            if (cantidad2 > 0)
            {

                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "INSERT INTO GuiaRemision (fechaEntrega, idOrdenCompra) VALUES  (GETDATE(), " + nota.idordencompra + " )";

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

                cambiarestado(nota.idordencompra, estado); // cambia de estado


                List<NotaEntradaBean> ordenes = ListarNotasEntradas(nota.idordencompra);

                int cantidad = ordenes.Count;

                nota.idguiaRemision = ordenes[cantidad - 1].idguiaRemision;


                String cadenaConfiguracion2 = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;



                for (int i = 0; i < nota.detallenotaentrada.Count; i++)
                {
                    if (nota.detallenotaentrada[i].cantidadentrante > 0)
                    {
                        SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion2);
                        sqlCon2.Open();

                        string commandString2 = "INSERT INTO GuiaRemisionDetalle (idProducto, idGuiaRemision, cantidadRecibida) VALUES  ( " + nota.detallenotaentrada[i].ID + ","
                                                 + nota.idguiaRemision + "," +
                                                 nota.detallenotaentrada[i].cantidadentrante + " )";

                        SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon2);
                        sqlCmd2.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                }


            }
        }


        public List<Notaentrada> obtenernotas(int idguiaremision)
        {
            List<Notaentrada> notas = new List<Notaentrada>();
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();
                string commandString = "SELECT * FROM GuiaRemisionDetalle WHERE idGuiaRemision = " + idguiaremision;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Notaentrada nota = new Notaentrada();
                    nota.ID = (int)dataReader["idProducto"];
                    nota.cantidadrecibida = (int)dataReader["cantidadRecibida"];

                    notas.Add(nota);

                }

                sqlCon.Close();
                
            }
            catch
            {
            }
            return notas;
        }
        
        public void actualizarstock(NotaEntradaBean nota)
        {
            //****actualizar stock en almacen.. 
            int idordencompra = nota.idordencompra;
            int idhotel = nota.idhotel;
            string error="";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM OrdenCompra WHERE idOrdenCompra = " + idordencompra;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            while (dataReader.Read())
            {
                idhotel= (int)dataReader["idHotel"];
            }

            sqlCon.Close();
            

            int idalmacen = prod.obteneralmacen(idhotel);
            if (idalmacen == -1) { error = "falla"; }
            else{
                
                //falta sumar...la cantidad actual con la entrante
                ProductoDAO orden = new ProductoDAO();
                ProductoXAlmacenBean productos = orden.obtenerlistaproductos(idalmacen);

                List<int> cantidades= new List<int>();
                for (int i = 0; i < nota.detallenotaentrada.Count; i++)
                {
                    for (int j = 0; j < productos.listProdalmacen.Count; j++)
                    {
                        if (nota.detallenotaentrada[i].ID == productos.listProdalmacen[j].ID)
                        {
                            int cant = nota.detallenotaentrada[i].cantidadentrante + productos.listProdalmacen[j].stockactual;
                            cantidades.Add(cant);
                        }
                    }
                }

                String cadenaConfiguracion2 = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                for (int i = 0; i < nota.detallenotaentrada.Count; i++)
                {
                    SqlConnection sqlCon2 = new SqlConnection(cadenaConfiguracion2);
                    sqlCon2.Open();

                    string commandString2 = "UPDATE ProductoXAlmacen  SET stockActual = " + cantidades[i] + " WHERE idAlmacen = " + idalmacen + " AND idProducto = "+ nota.detallenotaentrada[i].ID;

                    SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon2);
                    sqlCmd2.ExecuteNonQuery();
                    sqlCon.Close();
                }

                sqlCon.Close();
            }

        }


    }

}