using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using Stardust.Models.Servicios;

namespace Stardust.Models
{
    public class PagoDAO
    {
        public int GetID(string doc)
        {
            int id=0;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Usuario WHERE nroDocumento = '"+doc+"'";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                id = (int)dataReader["idUsuario"];
            }

            sqlCon.Close();

            return id;
        }

        public ReservaCheckOut GetReserva(int id)
        {
            ReservaCheckOut reserva = new ReservaCheckOut();
            
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Reserva WHERE idReserva = "+id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();  
            DateTime fechaIni = new DateTime();
            DateTime fechaHoy = new DateTime();

            if (dataReader.Read())
            {                
                reserva.id = (int)dataReader["idReserva"];  
                fechaIni = (DateTime)dataReader["fechaLlegada"];
                reserva.fechaIni  = fechaIni.ToString("dd-MM-yyyy");                
                reserva.fechaFin = ((DateTime)dataReader["fechaSalida"]).ToString("dd-MM-yyyy");
                fechaHoy = DateTime.Now;
                reserva.fechaHoy = fechaHoy.ToString("dd-MM-yyyy");
                int idU = (int)dataReader["idUsuario"];
                UsuarioBean usuario = GetNombreUsuario(idU);
                reserva.dni = usuario.nroDocumento;
                reserva.nombre = usuario.nombres;                
            }

            dataReader.Close();

            commandString = "SELECT * FROM DocumentoPago WHERE idReserva = " + id;

            SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

            TimeSpan dias = fechaHoy - fechaIni;

            int idDoc=0;

            if (dataReader2.Read())
            {               
                reserva.faltante = (decimal)dataReader2["montoFaltante"];
                reserva.idDocPago = (int)dataReader2["idDocPago"];                
                idDoc = (int)dataReader2["idDocPago"];
            }

            reserva.listaDetalles = ListaDetalle(idDoc,dias.Days);

            for (int i = 0; i < reserva.listaDetalles.Count; i++)
                reserva.subTotal += reserva.listaDetalles.ElementAt(i).totalDet;

            reserva.IGV = reserva.subTotal * 18 / 100;
            reserva.total = reserva.IGV + reserva.subTotal;

            reserva.montPagado = reserva.total - reserva.faltante;

            sqlCon.Close();

            return reserva;
        }

        public MensajeBean RegistrarCheckOut(int id)
        {
            MensajeBean mensaje = new MensajeBean();

            try
            {
                ReservaCheckOut reserva = GetReserva(id);

                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                decimal faltante = 0;

                string commandString = "UPDATE DocumentoPago SET montoTotal = " + reserva.total + " , montoFaltante = " + faltante + " , subTotal = " + reserva.subTotal + " , igv = " + reserva.IGV + " , estado = 3 WHERE idDocPago = " + reserva.idDocPago;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                if (reserva.faltante > 0)
                {
                    commandString = "INSERT INTO Pagos VALUES ( " + reserva.faltante + " , NULL , GETDATE() , " + reserva.idDocPago + " )";

                    SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                    sqlCmd2.ExecuteNonQuery();
                }

                commandString = "UPDATE Reserva SET estado = 4 , fechaCheckOut = GETDATE() WHERE idReserva = " + reserva.id;
                SqlCommand sqlCmd3 = new SqlCommand(commandString, sqlCon);
                sqlCmd3.ExecuteNonQuery();

                mensaje.me = ActualizarHabitacion(id);              
                
                return mensaje;
            }
            catch (Exception e)
            {
                mensaje.me = e.ToString();
                return mensaje;
            }           
        }

        public string ActualizarHabitacion(int id)
        {
            string mensaje;

            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "UPDATE ReservaXHabitacion SET estado = 4 WHERE idReserva = "+id;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                return mensaje = "Se registro satsfactoriamente";
            }
            catch(Exception e)
            {
                mensaje = e.ToString();
                return mensaje;
            }
        }

        public UsuarioBean GetNombreUsuario(int id)
        {
            UsuarioBean usuario = new UsuarioBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Usuario WHERE idUsuario = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                usuario.nombres = (string)dataReader["nombres"];
                usuario.nroDocumento = (string)dataReader["nroDocumento"];
                usuario.tipoDocumento = (string)dataReader["tipoDocumento"];
            }

            dataReader.Close();
            sqlCon.Close();

            return usuario;
        }

        public string GetNombreHotel(int id)
        {
            string nombre = null;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Hotel WHERE idHotel = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                nombre = (string)dataReader["nombre"];
            }

            dataReader.Close();
            sqlCon.Close();

            return nombre;
        }

        public string GetNombreTipoHab(int id)
        {
            string nombre = null;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM TipoHabitacion WHERE idTipoHabitacion = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                nombre = (string)dataReader["nombre"];
            }

            dataReader.Close();
            sqlCon.Close();

            return nombre;
        }

        public decimal GetPrecioTipoHab(int id)
        {
            decimal precio = 0;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM TipoHabitacionXHotel WHERE idTipoHabitacion = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.Read())
            {
                precio = (decimal)dataReader["precioBaseXDia"];
            }

            dataReader.Close();
            sqlCon.Close();

            return precio;
        }

        public List<TipoDetalle> ListaDetalle(int id, int dias)
        {
            List<TipoDetalle> listaDetalle = new List<TipoDetalle>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM DocumentoPago_Detalle WHERE idDocPago = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                TipoDetalle detalle = new TipoDetalle();
                detalle.id = (int)dataReader["idDocPago"];
                detalle.detalle = (string)dataReader["detalle"];
                detalle.cantidad = (int)dataReader["cantidad"];
                detalle.pUnit = (decimal)dataReader["precioUnitario"];
                detalle.totalDet = detalle.cantidad * detalle.pUnit * dias;
                listaDetalle.Add(detalle);
            }

            dataReader.Close();
            sqlCon.Close();

            return listaDetalle;
        }

        //public ReservaBean ObtenerReserva(int id)
        //{
        //    ReservaBean reserva = new ReservaBean();

        //    String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

        //    SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

        //    sqlCon.Open();

        //    string commandString = "SELECT * FROM Reserva WHERE idReserva = " + id;

        //    SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
        //    SqlDataReader dataReader = sqlCmd.ExecuteReader();

        //    while (dataReader.Read())
        //    {
        //        reserva.id = (int)dataReader["idReserva"];
        //        reserva.pagoIni = (decimal)dataReader["pagoInicial"];
        //        reserva.subTotal = (decimal)dataReader["montoTotal"]; //Sin IGV :S
        //        reserva.idHotel = (int)dataReader["idHotel"];
        //        reserva.idUsuario = (int)dataReader["idUsuario"];
        //        reserva.estado = (int)dataReader["estado"];
        //        reserva.estadoPago = (int)dataReader["estadoPago"];
        //        reserva.fechaLlegada = (DateTime)dataReader["fechaLlegada"];
        //        reserva.fechaSalida = (DateTime)dataReader["fechaSalida"];
        //    }

        //    dataReader.Close();

        //    if (reserva.estadoPago == 2 || reserva.estadoPago == 3)
        //    {
        //        commandString = "SELECT * FROM DocumentoPago WHERE idReserva = "+reserva.id;
        //        SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
        //        SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();
        //        if (dataReader2.Read())
        //        {
        //            reserva.pago = (int)dataReader2["montoTotal"];
        //        }
        //    }
                        
        //    decimal total=0;

        //    int dia = reserva.fechaSalida.Day - reserva.fechaLlegada.Day;

        //    reserva.listaHab = ListaHab(reserva.id);
        //    for (int i = 0; i < reserva.listaHab.Count; i++)
        //    {
        //        TipoHab hab = reserva.listaHab.ElementAt(i);

        //    }

        //    decimal subtotal = reserva.subTotal;
        //    decimal igv = subtotal * 18 / 100;

        //    reserva.igv = igv;
        //    reserva.total = total;
        //    reserva.hotel = GetNombreHotel(reserva.idHotel);
        //    UsuarioBean usuario = GetNombreUsuario(reserva.idUsuario);
        //    reserva.usuario = usuario.nombres;
        //    reserva.numDoc = usuario.nroDocumento;
        //    reserva.tipoDoc = usuario.tipoDocumento;
        //    reserva.apagar = reserva.total - reserva.pago;
            
        //    sqlCon.Close();

        //    return reserva;
        //}

    //    public string RegistrarPagoContado(Reserva reserva)
    //    {
    //        string conexion = null;
    //        try
    //        {   
    //            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

    //            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
    //            sqlCon.Open();

    //            string commandString = null;

    //            if (reserva.tipoDoc.Equals("RUC"))
    //            {
    //                commandString = "INSERT INTO DocumentoPago VALUES ('" + reserva.numDoc + "', " + reserva.pagado + " , GETDATE() , NULL , " + reserva.igv + " , NULL , " + reserva.subTotal + " , 'Contado' , '" + reserva.tipoDoc + "' , 'Factura' , "+reserva.id+" , NULL)";
    //            }
    //            else
    //            {
    //                commandString = "INSERT INTO DocumentoPago VALUES ('" + reserva.numDoc + "', " + reserva.pagado + " , GETDATE() , NULL , " + reserva.igv + " , NULL , " + reserva.subTotal + " , 'Contado' , '" + reserva.tipoDoc + "' , 'Boleta' , "+reserva.id+" , NULL)";
    //            }

    //            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
    //            sqlCmd.ExecuteNonQuery();

    //            commandString = "SELECT * FROM DocumentoPago WHERE idReserva = " + reserva.id; Registrar el detalle del pago :S

    //            SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
    //            SqlDataReader dataReader = sqlCmd2.ExecuteReader();

    //            int id = 0;

    //            if (dataReader.Read())
    //                id = (int)dataReader["idDocumentoPago"];

    //            commandString = "INSERT INTO DocumentoPago_Detalle VALUES ( "++" )"

    //            if (reserva.estadoPago == 1) //Pago cero
    //            {
    //                if (reserva.pagado < reserva.total)
    //                    commandString = "UPDATE Reserva SET estado = 2 , estadoPago = 2 WHERE idReserva = " + reserva.id;
    //                else
    //                    commandString = "UPDATE Reserva SET estado = 2 , estadoPago = 4 WHERE idReserva = " + reserva.id;
    //            }
    //            else
    //            {
    //                if (reserva.estadoPago == 2) //Inicial
    //                {
    //                    if (reserva.pagado < reserva.total)
    //                        commandString = "UPDATE Reserva SET estadoPago = 3 WHERE idReserva = " + reserva.id;
    //                    else
    //                        commandString = "UPDATE Reserva SET estadoPago = 4 WHERE idReserva = " + reserva.id;
    //                }
    //                else
    //                {
    //                    if (reserva.estadoPago == 3) //Parcial Pagado
    //                    {
    //                        if (reserva.pagado == reserva.total)
    //                            commandString = "UPDATE Reserva SET estadoPago = 4 WHERE idReserva = " + reserva.id;
    //                    }
    //                }
    //            }

    //            SqlCommand sqlCmd3 = new SqlCommand(commandString, sqlCon);
    //            sqlCmd3.ExecuteNonQuery();

    //            sqlCon.Close();        
    //        }
    //        catch
    //        {
    //            conexion = "Falla en Conexión";
    //        }

    //        return conexion;
    //    }
    }
}
