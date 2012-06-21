using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Stardust.Models
{
    public class AmbienteDAO
    {
        public String getNombreHotel(int id)
        {
            String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select nombre from Hotel where idHotel = " + id;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            data.Read();

            String hotel = (string)data.GetValue(0);

            sql.Close();

            return hotel;
        }
        
        
        
        public List<AmbienteBean> ListarAmbiente(int idHotel,String Nombre, String estado, float precio_menor, float precio_mayor)
        {

            List<AmbienteBean> listaAmbientes = new List<AmbienteBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Ambiente ";
            bool result;

            //result = estado.Equals("");
            //if (!result) 
            commandString = commandString + "WHERE estado = 'ACTIVO'";

            if (idHotel != 0)
            {
                commandString += " AND idHotel = @idHotel ";
            }

            result = Nombre.Equals("");
            if (!result) commandString = commandString + "AND  UPPER(nombre) LIKE '%" + Nombre.ToUpper() + "%' ";

            if (precio_menor > 0) commandString = commandString + " AND precioXhora  >= " + precio_menor;
            if (precio_mayor > 0) commandString = commandString + " AND precioXhora  <= " + precio_mayor;

            commandString += "ORDER BY idHotel,Nombre ";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);

            if (idHotel != 0)
            {
                Utils.agregarParametro(sqlCmd, "idHotel", idHotel);
            }

            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                AmbienteBean ambiente = new AmbienteBean();
                ambiente.id = (int)dataReader["idAmbiente"];
                ambiente.nombre = (string)dataReader["nombre"];
                ambiente.descripcion = (string)dataReader["descripcion"];
                ambiente.cap_maxima = (int)dataReader["capacMaxima"];
                //ambiente.largo  = (float)dataReader.GetDouble(dataReader.GetOrdinal("largo"));
                //ambiente.ancho = (float)dataReader.GetDouble(dataReader.GetOrdinal("ancho"));
                ambiente.largo = decimal.Parse(dataReader["largo"].ToString());
                ambiente.ancho = decimal.Parse(dataReader["ancho"].ToString());
                //ambiente.precioXhora = (decimal)dataReader.GetDouble(dataReader.GetOrdinal("precioXHora"));
                ambiente.precioXhora = decimal.Parse(dataReader["precioXHora"].ToString()); 
                ambiente.piso = (int)dataReader["piso"];
                ambiente.estado = (string)dataReader["estado"];
                ambiente.nombreHotel = this.getNombreHotel(ambiente.idHotel);
                listaAmbientes.Add(ambiente);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaAmbientes;
        }

        public String insertarAmbiente(AmbienteBean ambiente)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "INSERT INTO Ambiente" +
                                    "( nombre , descripcion , capacMaxima, largo , ancho , precioXHora , piso , estado , idHotel ) " +
                                    "VALUES (@nombre, @descripcion, @capacMaxima, @largo, @ancho, @precioXHora, @piso, @estado,  @idHotel )";

                     
           // VALUES ('" + ambiente.nombre + "', '" + ambiente.descripcion + "', " + ambiente.cap_maxima + ", " + ambiente.largo + ", " + ambiente.ancho + ", " + ambiente.precioXhora + ", " + ambiente.piso + ", 'ACTIVO', 1)";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            DAO.agregarParametro(sqlCmd, "nombre", ambiente.nombre);
            DAO.agregarParametro(sqlCmd, "descripcion", ambiente.descripcion);
            DAO.agregarParametro(sqlCmd, "capacMaxima", ambiente.cap_maxima);
            DAO.agregarParametro(sqlCmd, "largo", ambiente.largo);
            DAO.agregarParametro(sqlCmd, "ancho", ambiente.ancho);
            DAO.agregarParametro(sqlCmd, "precioXHora", ambiente.precioXhora);
            DAO.agregarParametro(sqlCmd, "piso", ambiente.piso);
            DAO.agregarParametro(sqlCmd, "estado", "ACTIVO");
            DAO.agregarParametro(sqlCmd, "idHotel", ambiente.id);
            
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;
        }
        public String ActualizarAmbiente(AmbienteBean ambiente)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            //string commandString = "UPDATE Ambiente " +
            //                        "SET  nombre = '" + ambiente.nombre + "', descripcion = '" + ambiente.descripcion + "', capacMaxima = " + ambiente.cap_maxima + ", largo = " + ambiente.largo + ", ancho = " + ambiente.ancho + ", precioXHora= " + ambiente.precioXhora + ", piso = " + ambiente.piso + " " +
            //                        //"SETEA estado = 'INACTIVO' " +
            //                        "WHERE idAmbiente = " + ambiente.id.ToString();

            String commandString= "UPDATE Ambiente SET " +
                                   "nombre = @nombre," +
                                   "descripcion = @descripcion," +
                                   "capacMaxima = @capacMaxima," +
                                   "largo = @largo," +
                                   "ancho = @ancho," +
                                   "precioXHora = @precioXHora," +
                                   "piso = @piso," +
                                   "estado =@estado," +
                                   "idHotel = @idHotel," +                                   
                                   "WHERE idAmbiente = @idAmbiente";
          
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            DAO.agregarParametro(sqlCmd, "nombre", ambiente.nombre);
            DAO.agregarParametro(sqlCmd, "descripcion", ambiente.descripcion);
            DAO.agregarParametro(sqlCmd, "capacMaxima", ambiente.cap_maxima);
            DAO.agregarParametro(sqlCmd, "largo", ambiente.largo);
            DAO.agregarParametro(sqlCmd, "ancho", ambiente.ancho);
            DAO.agregarParametro(sqlCmd, "precioXHora", ambiente.precioXhora);
            DAO.agregarParametro(sqlCmd, "piso", ambiente.piso);
            DAO.agregarParametro(sqlCmd, "estado", ambiente.estado);
            DAO.agregarParametro(sqlCmd, "idHotel", ambiente.idHotel);
            DAO.agregarParametro(sqlCmd, "idAmbiente", ambiente.id); 
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return me;
        }

        public AmbienteBean SeleccionarAmbiente(int id)
        {
            AmbienteBean ambiente = new AmbienteBean();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Ambiente WHERE  idAmbiente = @idAmbiente ";            
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            DAO.agregarParametro(sqlCmd, "idAmbiente", id);

            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (dataReader.HasRows)/*(dataReader.Read())*/
            {
                //ambiente.id = (int)dataReader["idAmbiente"];
                //ambiente.nombre = (string)dataReader["nombre"];
                //ambiente.descripcion = (string)dataReader["descripcion"];
                //ambiente.cap_maxima = (int)dataReader["capacMaxima"];
                ////ambiente.largo = (float)dataReader.GetDouble(dataReader.GetOrdinal("largo"));
                ////ambiente.ancho = (float)dataReader.GetDouble(dataReader.GetOrdinal("ancho"));
                //ambiente.largo = decimal.Parse(dataReader["largo"].ToString());
                //ambiente.ancho = decimal.Parse(dataReader["ancho"].ToString());
                ////ambiente.precioXhora = (decimal)dataReader.GetDouble(dataReader.GetOrdinal("precioXHora"));
                //ambiente.precioXhora = decimal.Parse(dataReader["precioXHora"].ToString()); 
                //ambiente.piso = (int)dataReader["piso"];
                //ambiente.estado = (string)dataReader["estado"];

                dataReader.Read();

                ambiente.id = (int)dataReader.GetValue(0);
                ambiente.nombre = (string)dataReader.GetValue(1);
                ambiente.descripcion = (string)dataReader.GetValue(2);
                ambiente.cap_maxima = (int)dataReader.GetValue(3);
                ambiente.largo = (decimal)dataReader.GetValue(4);
                ambiente.ancho = (decimal)dataReader.GetValue(5);
                ambiente.precioXhora = (decimal)dataReader.GetValue(6);
                ambiente.piso = (int)dataReader.GetValue(7);
                ambiente.estado = (string)dataReader.GetValue(8);
                ambiente.idHotel = (int)dataReader.GetValue(9);
                
            }
            dataReader.Close();
            sqlCon.Close();

            return ambiente;
        }

        public String DeleteAmbiente(int id)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Ambiente " +
                                    "SET estado = 'INACTIVO' " +
                                    "WHERE idAmbiente = " + id.ToString();

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            return me;
        }

        public List<AmbienteBean> listarNodisponibles(int idHotel, String fechaIni, String fechaFin)
        {
            List<AmbienteBean> listaNoDisp = new List<AmbienteBean>();
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            String query =  " SELECT DISTINCT idAmbiente " +
                            " FROM AmbienteXEvento aXe " + 
                            " WHERE idHotel = " + idHotel +
                            " AND aXe.estado < 3 "+
                            " AND ((aXe.fechaFin between convert(datetime,'" + fechaIni + "',103)" + " and  convert(datetime,'" + fechaFin + "',103)" + ")  OR (aXe.fechaIni between  convert(datetime,'" + fechaIni + "',103) and  convert(datetime,'" + fechaFin + "',103))) " +
                            " ORDER BY idAmbiente";
            System.Diagnostics.Debug.WriteLine("query Ambiente : " + query);
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                AmbienteBean amb = new AmbienteBean();
                amb.id = (int)dataReader["idAmbiente"];
                listaNoDisp.Add(amb);
            }

            return listaNoDisp;
        }

        public List<AmbienteBean> listarTodas(int idHotel)
        {
            List < AmbienteBean >  listTotal = new List<AmbienteBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query =  " SELECT idAmbiente, nombre, capacMaxima, precioXHora " + 
                            " FROM Ambiente " + 
                            " WHERE idHotel = " + idHotel + " AND estado='ACTIVO'";

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                AmbienteBean amb = new AmbienteBean();
                amb.id = (int)dataReader["idAmbiente"];
                amb.nombre = (String)dataReader["nombre"];
                amb.cap_maxima = (int)dataReader["capacMaxima"];
                amb.precioXhora = decimal.Parse(dataReader["precioXHora"].ToString());
                listTotal.Add(amb);
            }

            return listTotal;
        }

        public ReservaAmbBean registrarEvento(EventoResBean evento, int idHotel, int idUsuario, String fechaIni, String fechaFin, Decimal total, String coment)
        {

            ReservaAmbBean reserva = new ReservaAmbBean();
            reserva.me = "";

            System.Diagnostics.Debug.WriteLine("total a pagar : " + total);
            int reservaEstado = 1;//POR CONFIRMAR
            int reservaPago = 1;//CERO PAGO
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            try
            {
                sqlCon.Open();
            }
            catch (Exception e)
            {
                reserva.me = "Error en conexion a base de datos";
                return reserva;
            }

            String query = "SELECT porcAdelanto FROM Politica";
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

            SqlDataReader ReaderPorc;
            try
            {
                ReaderPorc = sqlCmd.ExecuteReader();
            }
            catch (Exception e)
            {
                reserva.me = "Error al consultar a la tabla Politica";
                return reserva;
            }

            int porc = 0;
            if (ReaderPorc.Read())
            {
                porc = (int)ReaderPorc["porcAdelanto"];
            }
            ReaderPorc.Close();
            System.Diagnostics.Debug.WriteLine("porcentaje : " + porc);

            Decimal adelanto = (porc * total) / 100;
            String query1 = "INSERT INTO Evento Values ('" + evento.nomb + "', '" + evento.descripcion + "' , convert(date,'" + fechaIni + "',103), convert(date,'" + fechaFin + "',103) ," + evento.nroParticipantes + " , " + idUsuario + " , NULL, NULL, " + reservaEstado + " , " + reservaPago + " , " + total + ", " + adelanto + " , " + idHotel.ToString() + ", GETDATE())";
            System.Diagnostics.Debug.WriteLine("query de EVENTO : " + query1);

            reserva.nombre = evento.nomb;
            reserva.descripcion = evento.descripcion;
            reserva.montoTotal = total;
            reserva.pagoInicial = adelanto;
            SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
            try
            {
                sqlCmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                reserva.me = "Error al insertar la Reserva : " + e.Message;
                return reserva;
            }

            string query2 = "SELECT IDENT_CURRENT('" + "Evento" + "') as lastId";
            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);

            SqlDataReader dataReader;
            try
            {
                dataReader = sqlCmd2.ExecuteReader();
            }
            catch (Exception e)
            {
                reserva.me = "Error al insertar a pedir el id de la Reserva";
                return reserva;
            }

            int last_id = 0;
            if (dataReader.Read())
            {
                last_id = Int16.Parse(dataReader["lastId"].ToString());
            }
            dataReader.Close();
            sqlCon.Close();

            reserva.idHotel = idHotel;
            reserva.idEvento= last_id;


            //String query_Factura = "INSERT INTO DocumentoPago VALUES("+idUsuario+")";
            return reserva;
        }

        public DocumentoPagoBean registrarDocumentoPago(UsuarioResBean usuario, ReservaAmbBean reserva)
        {
            DocumentoPagoBean documento = new DocumentoPagoBean();
            documento.me = "";
            Decimal igv = reserva.montoTotal * 18 / 100;
            Decimal montoTotal = reserva.montoTotal + igv;
            String tipoDocPago = "";
            bool result;
            result = usuario.tipoDocumento.Equals("DNI");
            if (result)
            {
                tipoDocPago = "Boleta";
            }
            else
            {
                tipoDocPago = "Factura";
            }


            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            try
            {
                sqlCon.Open();
            }
            catch (Exception e)
            {
                documento.me = "Error en conexion a base de datos";
                return documento;
            }

            String queryIns = "INSERT INTO DocumentoPago " +
                               "VALUES(" + usuario.idUsuario + " , " + montoTotal + " , " + montoTotal + " , GETDATE() , " + reserva.montoTotal + " , " + igv + " , NULL , NULL , '" + tipoDocPago + "' , 1 , NULL, " + reserva.idEvento + " )";
            SqlCommand sqlCmd = new SqlCommand(queryIns, sqlCon);

            try
            {
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                documento.me = "Error al registrar el Documento de Pago: " + e.Message;
                return documento;
            }

            String queryLastId = "SELECT IDENT_CURRENT('" + "DocumentoPago" + "') as lastId";
            SqlCommand sqlCmd2 = new SqlCommand(queryLastId, sqlCon);
            SqlDataReader dataReader;
            try
            {
                dataReader = sqlCmd2.ExecuteReader();
            }
            catch (Exception e)
            {
                documento.me = "Error al encontrar al id del Documento de Pago";
                return documento;
            }

            int last_id = 0;
            if (dataReader.Read())
            {
                last_id = Int16.Parse(dataReader["lastId"].ToString());
            }
            documento.idDocPago = last_id;

            return documento;
        }

        public String resgistrarAmbientes(List<AmbienteBean> lista, String fechaIni, String fechaFin, int idEvento)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            try
            {
                sqlCon.Open();
            }
            catch (Exception e) {
                return "Error en conexion a Base de Datos";
            }

            System.Diagnostics.Debug.WriteLine("total : " + lista.Count);
            int tam = lista.Count;
            for (int i = 0; i < tam ; i++)
            {
                int amb = lista[i].id;
                String query = "INSERT INTO AmbienteXEvento VALUES (" + idEvento + "," + amb + ",convert(date,'" + fechaIni + "',103),convert(date,'" + fechaFin + "', 103),1)";
                System.Diagnostics.Debug.WriteLine("query--> " + query);
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                try
                {
                    sqlCmd.ExecuteNonQuery();
                }catch(Exception e){
                    return "Error en insertar Ambientes para el Evento : "+ e.Message;
                }
            }
            sqlCon.Close();
            return "";
        }

        public String registrarDetalleFactura(int idDocPago, List<AmbienteBean> list, int z)
        {

            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            try
            {
                sqlCon.Open();
            }
            catch (Exception e)
            {
                me = "Error en conexion a base de datos";
                return me;
            }

            for (int i = 0; i < list.Count; i++)
            {
                AmbienteBean amb = list[i];
                Decimal total = amb.precioXhora*z;
                String query = "INSERT INTO DocumentoPago_Detalle VALUES ( " + idDocPago + " , '" + amb.nombre+ "' , 1 , " + amb.precioXhora + " , " + total + " , 0)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                try
                {
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    me = "Error al registrar el Detalle del Documento de Pago: " + e.Message;
                    return me;
                }
            }
            return me;
        }
    }
}