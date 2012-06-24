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

        public ReservaCheckOut GetReserva2(int id)
        {
            ReservaCheckOut reserva = new ReservaCheckOut();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Reserva WHERE estado = 4 AND idReserva = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            DateTime fechaIni = new DateTime();
            DateTime fechaHoy = DateTime.Now;

            if (dataReader.Read())
            {
                reserva.id = (int)dataReader["idReserva"];
                fechaIni = (DateTime)dataReader["fechaLlegada"];
                reserva.fechaIni = fechaIni.ToString("dd-MM-yyyy");
                reserva.fechaFin = ((DateTime)dataReader["fechaSalida"]).ToString("dd-MM-yyyy");
                reserva.fechaHoy = fechaHoy.ToString("dd-MM-yyyy");
                int idU = (int)dataReader["idUsuario"];
                UsuarioBean usuario = GetNombreUsuario(idU);
                reserva.tipoDoc = usuario.tipoDocumento;
                reserva.dni = usuario.nroDocumento;
                reserva.nombre = usuario.nombres;
            }

            dataReader.Close();

            if (fechaIni <= fechaHoy)
            {
                commandString = "SELECT * FROM DocumentoPago WHERE idReserva = " + id;

                SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

                TimeSpan dias = fechaHoy - fechaIni;

                int idDoc = 0;

                if (dataReader2.Read())
                {
                    reserva.faltante = (decimal)dataReader2["montoFaltante"];
                    reserva.idDocPago = (int)dataReader2["idDocPago"];
                    idDoc = (int)dataReader2["idDocPago"];
                }

                reserva.listaDetalles = ListaDetalle(idDoc, dias.Days);

                for (int i = 0; i < reserva.listaDetalles.Count; i++)
                    reserva.subTotal += reserva.listaDetalles.ElementAt(i).totalDet;

                reserva.IGV = reserva.subTotal * 18 / 100;
                reserva.total = reserva.IGV + reserva.subTotal;

                reserva.montPagado = reserva.total - reserva.faltante;

                sqlCon.Close();
            }

            return reserva;
        }

        public ReservaCheckOut GetReserva(int id)
        {
            ReservaCheckOut reserva = new ReservaCheckOut();
            
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            sqlCon.Open();

            string commandString = "SELECT * FROM Reserva WHERE estado = 3 AND idReserva = "+id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();  
            DateTime fechaIni = new DateTime();
            DateTime fechaHoy = DateTime.Now;

            if (dataReader.Read())
            {                
                reserva.id = (int)dataReader["idReserva"];  
                fechaIni = (DateTime)dataReader["fechaLlegada"];
                reserva.fechaIni  = fechaIni.ToString("dd-MM-yyyy");                
                reserva.fechaFin = ((DateTime)dataReader["fechaSalida"]).ToString("dd-MM-yyyy");
                reserva.fechaHoy = fechaHoy.ToString("dd-MM-yyyy");
                int idU = (int)dataReader["idUsuario"];
                UsuarioBean usuario = GetNombreUsuario(idU);
                reserva.tipoDoc = usuario.tipoDocumento;
                reserva.dni = usuario.nroDocumento;
                reserva.nombre = usuario.nombres;                
            }

            dataReader.Close();

            if (fechaIni <= fechaHoy)
            {
                commandString = "SELECT * FROM DocumentoPago WHERE idReserva = " + id;

                SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

                TimeSpan dias = fechaHoy - fechaIni;

                int idDoc = 0;

                if (dataReader2.Read())
                {
                    reserva.faltante = (decimal)dataReader2["montoFaltante"];
                    reserva.idDocPago = (int)dataReader2["idDocPago"];
                    idDoc = (int)dataReader2["idDocPago"];
                }

                reserva.listaDetalles = ListaDetalle(idDoc, dias.Days);

                for (int i = 0; i < reserva.listaDetalles.Count; i++)
                    reserva.subTotal += reserva.listaDetalles.ElementAt(i).totalDet;

                reserva.IGV = reserva.subTotal * 18 / 100;
                reserva.total = reserva.IGV + reserva.subTotal;

                reserva.montPagado = reserva.total - reserva.faltante;

                sqlCon.Close();
            }

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

                string commandString; //= "UPDATE DocumentoPago SET montoTotal = " + reserva.total + " , montoFaltante = " + faltante + " , subTotal = " + reserva.subTotal + " , igv = " + reserva.IGV + " , estado = 3 WHERE idDocPago = " + reserva.idDocPago;

                //SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                //sqlCmd.ExecuteNonQuery();

                if (reserva.faltante > 0)
                {                    
                    commandString = "INSERT INTO Pagos VALUES ( " + reserva.faltante + " , NULL , GETDATE() , " + reserva.idDocPago;

                    if (reserva.tipoDoc.Equals("DNI"))
                        commandString += " , 'Boleta' )";
                    else
                        commandString += " , 'Factura' )";

                    SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                    sqlCmd2.ExecuteNonQuery();
                }

                commandString = "UPDATE Reserva SET estado = 4 , fechaCheckOut = GETDATE() WHERE idReserva = " + reserva.id;
                SqlCommand sqlCmd3 = new SqlCommand(commandString, sqlCon);
                sqlCmd3.ExecuteNonQuery();

                mensaje.me = ActualizarHabitacion(id);

                mensaje.me = ActualizarPagoDetalle(reserva.listaDetalles);

                //mensaje.id = id;
                
                return mensaje;
            }
            catch (Exception e)
            {
                mensaje.me = e.ToString();
                return mensaje;
            }           
        }

        public string ActualizarPagoDetalle(List<TipoDetalle> listaDetalle)
        {            
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                //for (int i = 0; i < listaDetalle.Count; i++)
                //{
                //    string commandString = "UPDATE DocumentoPago_Detalle SET total = "+listaDetalle.ElementAt(i).totalDet+" WHERE idDocPagoDetalle = " + listaDetalle.ElementAt(i).idDetalle;

                //    SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                //    sqlCmd.ExecuteNonQuery();
                //}

                return "";
            }
            catch (Exception e)
            {
                return e.ToString();
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

                return mensaje = "";
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

            string commandString = "SELECT * FROM Promocion WHERE tipo = 1 AND razon <= " + dias + " ORDER BY razon DESC";

            SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

            int porc = 0;

            if (dataReader2.Read())
            {
                porc = 100 - (int)dataReader2["porcDescontar"];
            }

            dataReader2.Close();

            commandString = "SELECT * FROM DocumentoPago_Detalle WHERE idDocPago = " + id + " ORDER BY es_habitacion";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                TipoDetalle detalle = new TipoDetalle();
                detalle.id = (int)dataReader["idDocPago"];
                detalle.idDetalle = (int)dataReader["idDocPagoDetalle"];
                detalle.detalle = (string)dataReader["detalle"];
                detalle.cantidad = (int)dataReader["cantidad"];
                detalle.pUnit = (decimal)dataReader["precioUnitario"];
                detalle.estado = (int)dataReader["es_habitacion"];
                if (detalle.estado == 1)
                {
                    if (porc > 0 && porc < 100)
                        detalle.totalDet = detalle.cantidad * detalle.pUnit * dias * porc / 100;
                    else
                        detalle.totalDet = detalle.cantidad * detalle.pUnit * dias;
                    
                }
                else
                    detalle.totalDet = (decimal)dataReader["total"];
                listaDetalle.Add(detalle);
            }

            dataReader.Close();
            sqlCon.Close();

            return listaDetalle;
        }

        public PagoAdelantadoBean PagoAdelantado(RequestPagoAde request)
        {
            PagoAdelantadoBean pago = new PagoAdelantadoBean();

            try
            {            
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

                sqlCon.Open();

                string commandString;

                if (request.flag == 0)
                {
                    commandString = "SELECT u.nombres as nombre , u.idUsuario as id , p.montoFaltante as montoFaltante , p.montoTotal as montoTotal , r.pagoInicial as pagoInicial  FROM Usuario u , Reserva r , DocumentoPago p WHERE u.nroDocumento = " + request.doc +" AND r.idReserva = "+ request.id +" AND u.idUsuario = p.idUsuario";
                    
                    SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                    SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

                    if (!dataReader2.HasRows)
                    {
                        pago.mensaje = "No se encuentra los datos registrados";
                        return pago;
                    }

                    if (dataReader2.Read())
                    {
                        pago.data = Convert.ToString(dataReader2["id"]);
                        pago.doc = request.doc;
                        pago.mensaje = "";
                        pago.estado = 1;
                        pago.nom = (string)dataReader2["nombre"];
                        pago.montoInicial = (decimal)dataReader2["pagoInicial"];
                        pago.montoTotal = (decimal)dataReader2["montoTotal"];
                    }
                }
                else
                {
                    commandString = "SELECT * FROM Evento WHERE estado = 1 AND idCliente = ";// + id;
                    SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                    SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

                    if (dataReader2.Read())
                    {
                        pago.data = Convert.ToString(dataReader2["nombre"]);
                        pago.doc = request.doc;
                        pago.montoInicial = (decimal)dataReader2["pagoInicial"];
                    }
                }

                pago.mensaje = "";
            }
            catch (Exception e)
            {
                pago.mensaje = e.ToString();
            }

            return pago;
        }

        public List<ListaHabitacion> listaHabitacion(int idHotel,string fechaIni, string fechaFin)
        {
            List<ListaHabitacion> listaHab = new List<ListaHabitacion>();

            DateTime fechaI = new DateTime();
            DateTime fechaF = new DateTime();

            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

                sqlCon.Open();

                fechaI = DateTime.ParseExact(fechaIni,"dd-MM-yyyy",null);
                fechaF = DateTime.ParseExact(fechaFin, "dd-MM-yyyy", null);

                TimeSpan dif = fechaF - fechaI;

                int dias = dif.Days;

                string commandString = "SELECT * FROM Habitacion WHERE idHotel = " + idHotel + " ORDER BY idHabitacion";

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                List<ListaHabitacionEstado> listaDetalle = new List<ListaHabitacionEstado>();

                for (int i = 0; i < dias; i++)
                {
                    ListaHabitacionEstado estado = new ListaHabitacionEstado();
                    estado.idReserva = 0;
                    estado.estado = "Libre";
                    listaDetalle.Add(estado);
                }

                    while (dataReader.Read())
                    {
                        ListaHabitacion hab = new ListaHabitacion();
                        hab.idHabit = (int)dataReader["idHabitacion"];
                        int idTipo = (int)dataReader["idTipoHabitacion"];

                        commandString = "SELECT * FROM TipoHabitacion WHERE idTipoHabitacion = " + idTipo;

                        SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                        SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

                        if (dataReader2.Read())
                            hab.nHabit = (string)dataReader2["nombre"];

                        listaHab.Add(hab);
                    }

                for (int i = 0; i < listaHab.Count; i++)
                {
                    commandString = "SELECT * FROM ReservaXHabitacion WHERE idHabitacion = "+listaHab.ElementAt(i).idHabit+" fechaIni BETWEEN " + fechaI + " AND " + fechaF + " AND fechaFin BETWEEN " + fechaI + " AND " + fechaF +" ORDER BY fechaIni";

                    SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                    SqlDataReader dataReader2 = sqlCmd2.ExecuteReader();

                    while (dataReader2.Read())
                    {                   
                        DateTime fechaInicio = (DateTime)dataReader2["fechaIni"];
                        DateTime fechaFinal = (DateTime)dataReader2["fechaFin"];
                        TimeSpan dife = fechaI - fechaInicio;
                        int a = dife.Days;
                        dife = fechaI - fechaFinal;
                        int b = dife.Days;
                        for (int j = a; j <= b; j++)
                        {
                            listaHab.ElementAt(i).listaFechas.ElementAt(j).idReserva = (int)dataReader["idReserva"];
                            int est = (int)dataReader2["estado"];
                            switch (est)
                            {
                                case 1: listaHab.ElementAt(i).listaFechas.ElementAt(j).estado = "Por Confirmar";
                                        break;
                                case 2: listaHab.ElementAt(i).listaFechas.ElementAt(j).estado = "Confirmado";
                                        break;
                                case 3: listaHab.ElementAt(i).listaFechas.ElementAt(j).estado = "En Curso";
                                        break;
                                case 4: listaHab.ElementAt(i).listaFechas.ElementAt(j).estado = "Libre";
                                        break;
                            }
                        }                      
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return listaHab;
        }

        public MensajeBean RegistrarPagoAdelantado(RequestResHab request)
        {
            MensajeBean mensaje = new MensajeBean();

            return mensaje;
        }
    }
}
