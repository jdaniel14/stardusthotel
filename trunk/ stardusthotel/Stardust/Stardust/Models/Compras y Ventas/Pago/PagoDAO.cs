﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

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

            if (dataReader.Read())
            {                
                reserva.id = (int)dataReader["idReserva"];
                reserva.fechaIni = (DateTime)dataReader["fechaLlegada"];
                reserva.fechaFin = (DateTime)dataReader["fechaSalida"];
                reserva.fechaHoy = DateTime.Now;
                int idU = (int)dataReader["idUsuario"];
                UsuarioBean usuario = GetNombreUsuario(idU);
                reserva.dni = usuario.nroDocumento;
                reserva.nombre = usuario.nombres;                
            }

            dataReader.Close();

            commandString = "SELECT * FROM DocumentoPago WHERE idReserva = " + id;

            SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

            TimeSpan dias = reserva.fechaHoy - reserva.fechaIni;

            int idDoc=0;

            if (dataReader2.Read())
            {               
                reserva.faltante = (decimal)dataReader2["montoFaltante"];
                reserva.total = (decimal)dataReader2["montoTotal"];
                reserva.subTotal = (decimal)dataReader2["subTotal"];
                decimal faltante = (decimal)dataReader2["igv"];
                reserva.montPagado = reserva.total - faltante;
                idDoc = (int)dataReader2["idDocPago"];
            }

            reserva.listaDetalles = ListaDetalle(idDoc,dias.Days);

            sqlCon.Close();

            return reserva;
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
