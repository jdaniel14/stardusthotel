using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models.Servicios
{
    public class ReservaHabitacionDAO
    {
        #region DIPONIBLES
        public List<HabitacionReserva> listarNoDisponibles(int idHotel, String fechaIni, String fechaFin) {
            List<HabitacionReserva> listaHab = new List<HabitacionReserva>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query =  " SELECT DISTINCT rxh.idHabitacion " +
                            " FROM ReservaXHabitacion rXh, Habitacion h " +
                            " WHERE rXh.idHotel = " + idHotel + " and h.idHotel = rXh.idHotel and  rXh.estado<3 and " +
                            " ((rXh.fechaFin between convert(datetime,'" + fechaIni + "',103)" + " and  convert(datetime,'" + fechaFin + "',103)" + ")  OR (rXh.fechaIni between  convert(datetime,'" +  fechaIni + "',103) and  convert(datetime,'" + fechaFin + "',103))) AND rxH.idHabitacion = h.idHabitacion" +
                            " ORDER BY idHabitacion";
            System.Diagnostics.Debug.WriteLine("NO DISPONIBLES : " + query);
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                HabitacionReserva hab = new HabitacionReserva();
                hab.idHab= (int)dataReader["idHabitacion"];
                hab.idTipoHabitacion = 0;
                hab.numero = "";
                hab.piso = 0;
                listaHab.Add(hab);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaHab;
        }

        public List<HabitacionReserva> listarHabitaciones(int idHotel)
        {
            List<HabitacionReserva> listaHab = new List<HabitacionReserva>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query =  " SELECT h.idHabitacion, h.idTipoHabitacion, h.numero, h.piso " +
                            " FROM Habitacion h, TipoHabitacionXHotel tXh " +
                            " WHERE estado = 'ACTIVO' and tXh.idTipoHabitacion = h.idTipoHabitacion and tXh.idHotel = " + idHotel + " and tXh.idHotel = h.idHotel " + 
                            " ORDER BY idHabitacion";

            System.Diagnostics.Debug.WriteLine("TOTAL : "+ query);

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                HabitacionReserva hab = new HabitacionReserva();
                hab.idHab= (int)dataReader["idHabitacion"];
                hab.idTipoHabitacion = (int)dataReader["idTipoHabitacion"];
                System.Diagnostics.Debug.WriteLine("numero : " + Convert.ToString(dataReader["numero"]));
                hab.numero = Convert.ToString(dataReader["numero"]);
                hab.piso = (int)dataReader["piso"];
                listaHab.Add(hab);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaHab;
        }

        public List<ReservaTipoHabitacion> listaTipoHabitacion(int idHotel)
        {
            List<ReservaTipoHabitacion> listTipHab = new List<ReservaTipoHabitacion>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query =  " SELECT thXh.idTipoHabitacion, th.nombre, thXh.precioBaseXDia,  th.descripcion " +
                            " FROM TipoHabitacionXHotel thXh, TipoHabitacion th " + 
                            " WHERE thXh.idHotel = "+ idHotel +" and th.idTipoHabitacion = thXh.idTipoHabitacion";
            
            System.Diagnostics.Debug.WriteLine("TIPO HABITACION " + query);

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                ReservaTipoHabitacion tipHab = new ReservaTipoHabitacion();
                tipHab.idTipoHabitacion = (int)dataReader["idTipoHabitacion"];
                tipHab.nombre = (String)dataReader["nombre"];
                tipHab.precioBaseXDia = decimal.Parse(dataReader["precioBaseXDia"].ToString());                
                tipHab.descripcion = (String)dataReader["descripcion"];
                listTipHab.Add(tipHab);
            }
            dataReader.Close();
            sqlCon.Close();

            return listTipHab;
        }
        #endregion

        #region CONSULTA_ELIMINAR_RESERVA

        public DatosReservaBean consultarReserva(int idHotel, int idReserva, String documento)
        {
            DatosReservaBean consulta = new DatosReservaBean();


            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query = " SELECT r.idReserva, r.fechaLlegada, r.fechaSalida, u.nroDocumento, (u.razonSocial+u.nombres+' '+u.apPat) as nombre" +
                           " FROM Reserva r, Usuario u " +
                           " WHERE r.idHotel = " + idHotel + " and r.idReserva = " + idReserva + " and r.idUsuario = u.idUsuario and u.nroDocumento = '" + documento + "'";

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            consulta.me = "";
            if (dataReader.Read())
            {
                consulta.idReserva = (int)dataReader["idReserva"];
                consulta.fechaLlegada = ((DateTime)dataReader["fechaLlegada"]).ToString("dd-MM-yyyy");
                consulta.fechaSalida = ((DateTime)dataReader["fechaSalida"]).ToString("dd-MM-yyyy");
                consulta.doc = (String)dataReader["nroDocumento"];
                consulta.nomb = (String)dataReader["nombre"];
            }
            else
            {
                consulta.me = "No se encontro ninguna Reserva con esos datos";
            }
            dataReader.Close();
            sqlCon.Close();

            return consulta;
        }

        public String deleteReserva(int idReserva)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query1 = " UPDATE Reserva" +
                            " SET estado= 5 " +
                            " WHERE idReserva=" + idReserva.ToString();
            SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
            sqlCmd1.ExecuteNonQuery();

            String query2 = " UPDATE ReservaXHabitacion " +
                            " SET estado=4" +
                            " WHERE idReserva=" + idReserva.ToString();
            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
            sqlCmd2.ExecuteNonQuery();

            me = "Su reserva fue anulada satisfactoriamente";

            sqlCon.Close();



            return me;
        }
        #endregion
        
        #region REGISTRAR_RESERVA

        public UsuarioResBean login(String mail, String pass)
        {
            UsuarioResBean usuario = new UsuarioResBean();
            usuario.me = "";
            String query = "SELECT * FROM Usuario WHERE user_account = '"+mail+"' and pass='" + pass+ "'";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            SqlDataReader dataReader;
            sqlCon.Open();
            SqlCommand sqlCmd2 = new SqlCommand(query, sqlCon);
            dataReader = sqlCmd2.ExecuteReader();
                       

            int res = 0;
            if (dataReader.Read()) {

                res = 1;
                usuario.idUsuario = (int)dataReader["idUsuario"];
                usuario.tipoDocumento = (String)dataReader["tipoDocumento"];
                usuario.nroDocumento = (String)dataReader["nroDocumento"];
                usuario.nombres = (String)dataReader["razonSocial"] + (String)dataReader["nombres"] + "___" + (String)dataReader["apPat"];
                usuario.email = (String)dataReader["email"];
                usuario.celular = (String)dataReader["celular"];
                dataReader.Close();

                String queryTarj = "SELECT nroTarjeta FROM Cliente WHERE idCliente = " + usuario.idUsuario;
                
                sqlCmd2 = new SqlCommand(queryTarj, sqlCon);
                try
                {
                    dataReader = sqlCmd2.ExecuteReader();
                    if (dataReader.Read())
                    {
                        usuario.nroTarjeta = (String)dataReader["nroTarjeta"];
                    }
                }
                catch (Exception e) {
                    usuario.nroTarjeta = e.Message;
                }
            }
            usuario.me = res.ToString();
            return usuario;
        }

        public HotelResponse listarHoteles()
        {
            HotelResponse response = new HotelResponse();
            response.me = "";

            List<HotelBean> list = new List<HotelBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            String query = "SELECT idHotel, nombre FROM Hotel WHERE estado = 1";

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            SqlDataReader dataReader;

            try
            {
                sqlCon.Open();
            }
            catch (Exception e) {
                response.me = "Error en conexion a Base de Datos";
                return response;
            }

            SqlCommand sqlCmd2 = new SqlCommand(query, sqlCon);

            try
            {
                dataReader = sqlCmd2.ExecuteReader();
                while (dataReader.Read())
                {
                    HotelBean hotel = new HotelBean();
                    hotel.ID = (int)dataReader["idHotel"];
                    hotel.nombre = (String)dataReader["nombre"];
                    System.Diagnostics.Debug.WriteLine("Hotel :  " + hotel.nombre);
                    list.Add(hotel);
                }
                dataReader.Close();
                response.lista = list;
            }
            catch (Exception e) {
                response.me = "Error al ejecutar query : " + e.Message;
            }
           
            return response;
        }

        public UsuarioResBean registraCliente(ClienteReservaBean client, String pass){

            UsuarioResBean usuario = new UsuarioResBean();
            usuario.me = "";
            
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);

            try
            {
                sqlCon.Open();
            }catch(Exception e){
                usuario.me = "Error en conexion a base de datos";
                return usuario;
            }


            String consultaUser = "SELECT COUNT(*) as res FROM Usuario WHERE user_account = '"+ client.email +"' OR  nroDocumento = '" + client.nroDoc + "'";

            SqlCommand sqlCmdUser = new SqlCommand(consultaUser, sqlCon);
            SqlDataReader dataReaderUser;
            try
            {
                dataReaderUser = sqlCmdUser.ExecuteReader();
            }
            catch (Exception e)
            {
                usuario.me = "Error en BD";
                return usuario;
            }
            if (dataReaderUser.Read())
            {
                
                int count = (int)dataReaderUser["res"];
                dataReaderUser.Close();
                if (count == 0)
                {
                    string commandString3 = "INSERT INTO Usuario VALUES (1," +
                     "'" + client.email + "', " +
                     "'" + pass + "', " +
                     "'" + client.nomb + "'" + ", " +
                     "'" + client.apell + "'" + ", " +
                     "''" + ", " +
                     "'" + client.email + "', " +//email
                     "'" + client.telf + "', " +//celular
                     "'" + client.tipoDoc + "'" + ", " +
                     "'" + client.nroDoc + "'" + ", " +
                     "'" + client.razSoc + "'" + ", " +
                     "'ACTIVO'" + ", " +//estado
                     "1" + ", " +//Distrito
                     "''" + ", " +//Direccion
                     "1" + ", " +//Provincia
                     "1" + //Departamento
                     ")"
                     ;


                    SqlCommand sqlCmd3 = new SqlCommand(commandString3, sqlCon);
                    try
                    {
                        sqlCmd3.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        usuario.me = "Error al registrar el Usuario : " + e.Message;
                        return usuario;
                    }


                    string commandString2 = "SELECT IDENT_CURRENT('" + "Usuario" + "') as lastId";
                    SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon);
                    SqlDataReader dataReader;
                    try
                    {
                        dataReader = sqlCmd2.ExecuteReader();
                    }
                    catch (Exception e)
                    {
                        usuario.me = "Error al encontrar al id del Usuario";
                        return usuario;
                    }

                    int last_id = 0;
                    if (dataReader.Read())
                    {
                        //last_id = (int)dataReader["lastId"];
                        last_id = Int16.Parse(dataReader["lastId"].ToString());
                        //listaClientes.Add(cliente);
                    }

                    dataReader.Close();

                    System.Diagnostics.Debug.WriteLine("ultimo id " + last_id);

                    string commandString1 = "INSERT INTO Cliente VALUES (" + last_id.ToString() +
                             ", GETDATE()" + ", " +
                             "'ACTIVO'" + ", " +
                             "'" + client.tipoTarj + "'" + ", " +
                             "'" + client.nroTarj + "'" + ")"
                             ;

                    SqlCommand sqlCmd1 = new SqlCommand(commandString1, sqlCon);
                    try
                    {
                        sqlCmd1.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        usuario.me = "Error al registrar el usuario : " + e.Message;
                        return usuario;
                    }

                    sqlCon.Close();

                    usuario.idUsuario = last_id;
                    usuario.tipoDocumento = client.tipoDoc;
                }
                else
                {
                    usuario.me = "El correo ya se encuentra registrado.";
                }
                
            }
            else {
                usuario.me = "Error en Lectura";
            }            
            return usuario;
        }

        public ReservaResBean resgitrarReserva(int idHotel, int idUsuario, String fechaIni, String fechaFin, Decimal total, String coment){

            ReservaResBean reserva = new ReservaResBean();
            reserva.me = "";

            System.Diagnostics.Debug.WriteLine("total a pagar : " + total);
            int reservaEstado = 1;//POR CONFIRMAR
            int reservaPago = 1;//CERO PAGO
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            try
            {
                sqlCon.Open();
            }catch(Exception e){
                reserva.me = "Error en conexion a base de datos";
                return reserva;     
            }

            String query = "SELECT porcAdelanto FROM Politica";
            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

            SqlDataReader ReaderPorc;
            try
            {
                ReaderPorc = sqlCmd.ExecuteReader();
            }catch(Exception e){
                reserva.me = "Error al consultar a la tabla Politica";
                return reserva;
            }

            int porc = 0;
            if (ReaderPorc.Read()) {
                porc = (int)ReaderPorc["porcAdelanto"];
            }
            ReaderPorc.Close();
            System.Diagnostics.Debug.WriteLine("porcentaje : " + porc);

            Decimal adelanto = (porc * total) / 100;
            String query1 = "INSERT INTO Reserva Values (convert(date,'" + fechaIni + "',103), convert(date,'" + fechaFin + "',103) ,NULL, "+reservaEstado+" , " + adelanto + ", " + total + ", 0, " + idHotel.ToString() + ", " + idUsuario + ", GETDATE() , "+reservaPago+")";
            System.Diagnostics.Debug.WriteLine("query de RESERVA : " + query1);

            reserva.montoTotal = total;
            reserva.pagoInicial = adelanto;
            SqlCommand sqlCmd1 = new SqlCommand(query1, sqlCon);
            try
            {
                sqlCmd1.ExecuteNonQuery();
            }catch(Exception e){
                reserva.me = "Error al insertar la Reserva : " + e.Message;
                return reserva;
            }

            string query2 = "SELECT IDENT_CURRENT('" + "Reserva" + "') as lastId";
            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
            
            SqlDataReader dataReader;
            try{
                dataReader = sqlCmd2.ExecuteReader();
            }catch (Exception e){
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
            reserva.idReserva = last_id;


            //String query_Factura = "INSERT INTO DocumentoPago VALUES("+idUsuario+")";
            return reserva;
        }

        public DocumentoPagoBean registrarDocumentoPago(UsuarioResBean  usuario, ReservaResBean reserva){

            DocumentoPagoBean documento = new DocumentoPagoBean ();
            documento.me = "";
            Decimal igv  = reserva.montoTotal * 18/100;
            Decimal montoTotal = reserva.montoTotal + igv;
            String tipoDocPago = "";
            bool result;
            result = usuario.tipoDocumento.Equals("DNI");
            if (result)
            {
                tipoDocPago = "Boleta";
            }
            else {
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

            String queryIns =   "INSERT INTO DocumentoPago " +
                                "VALUES("+usuario.idUsuario+" , " + montoTotal + " , " + montoTotal + " , GETDATE() , " + reserva.montoTotal + " , " + igv + " , NULL , NULL , '" + tipoDocPago + "' , 1 , " + reserva.idReserva + " , NULL )" ;

            SqlCommand sqlCmd = new SqlCommand(queryIns , sqlCon);

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
            SqlDataReader dataReader ;
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
                //last_id = (int)dataReader["lastId"];
                last_id = Int16.Parse(dataReader["lastId"].ToString());
                //listaClientes.Add(cliente);
            }
            documento.idDocPago = last_id;

            return documento;
        }

        public String registrarDetalleFactura(int idDocPago, List<HabInsertBean> list, int z) {
            
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

            for (int i = 0; i < list.Count; i++) {
                HabInsertBean tipHab = list[i];
                Decimal total = tipHab.cant*tipHab.precUnit*z;
                String query = "INSERT INTO DocumentoPago_Detalle VALUES ( " + idDocPago + " , '" + tipHab.nombTipo + "' , " + tipHab.cant + " , " + tipHab.precUnit + " , " + total + " , 1)";
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

        public String resgistrarHabitaciones(int idHotel, List<HabInsertBean> listTip, String fechaIni, String fechaFin, int idReserva) {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            System.Diagnostics.Debug.WriteLine("total : " + listTip.Count);
            for (int i = 0; i < listTip.Count; i++) {
                System.Diagnostics.Debug.WriteLine("--> " + listTip[i].cant);
                for (int j = 0; j < listTip[i].cant; j++) {
                    int hab = listTip[i].list[j].idHab;

                    String query = "INSERT INTO ReservaXHabitacion VALUES (" + idReserva + "," + hab + ",convert(date,'" + fechaIni + "',103),convert(date,'" + fechaFin + "', 103), 1,"+idHotel+")";
                    System.Diagnostics.Debug.WriteLine("query--> " + query);
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.ExecuteNonQuery();

                }
            }
            sqlCon.Close();
            return "" ;
        }
        public void registrarXtipoHabitacion(int idReserva, int idHotel, List<HabInsertBean> listTip)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            for (int i = 0; i < listTip.Count; i++) {
                String query = "";
                if (listTip[i].cant > 0)
                {
                    query = "INSERT INTO ReservaXTipoHabitacionXHotel Values(" + idReserva + ", " + idHotel + ", " + listTip[i].tipo + ", " + listTip[i].cant + ")";
                    System.Diagnostics.Debug.WriteLine("query TIPO--> " + query);
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            sqlCon.Close();
        }
        #endregion

        #region CHECK IN

        public DatosReservaBean SelectDatosCheckIn(int idHotel, int idReserva)
        {
            DatosReservaBean reserva = new DatosReservaBean();
            int estado_confirmado = 2;

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query =  " SELECT u.nroDocumento as doc , (u.razonSocial + u.nombres + ' ' + u.apPat) as nomb, r.fechaRegistro as fechaReg, r.fechaLlegada as fechaLleg, r.estado " +
                            " FROM Reserva r, Usuario u " +
                            " WHERE r.idHotel = " + idHotel + " and r.idReserva = " + idReserva + " and r.idUsuario = u.idUsuario ";
                            //" WHERE r.estado = "+ estado_confirmado + " and  r.idHotel = " + idHotel + " and r.idReserva = " + idReserva + " and r.idUsuario = u.idUsuario ";

            System.Diagnostics.Debug.WriteLine("Query Check IN " + query);

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            reserva.me = "";
            int estadoReserva = 0;
            if (dataReader.Read())
            {
                reserva.doc = (String)dataReader["doc"];
                reserva.nomb = (String)dataReader["nomb"];
                DateTime fechaLlegada = (DateTime)dataReader["fechaLleg"];
                reserva.fechaRegistro = ((DateTime)dataReader["fechaReg"]).ToString("dd-MM-yyyy");
                reserva.fechaLlegada = fechaLlegada.ToString("dd-MM-yyyy");
                estadoReserva = (int)dataReader["estado"];

                if (estadoReserva == 1)
                    reserva.me = "Aun no se ha pagado la cuota inicial de la Reserva";
                else if (estadoReserva == 2)
                {
                    if (fechaLlegada > DateTime.Now)
                    {
                        reserva.me = "Aun no  se puede realizar el check in su Reserva se hara efectiva a partir del : " + fechaLlegada;
                    }
                }
                else {
                    reserva.me = "Reserva fuera de alcance";
                }
            }
            else
            {
                reserva.me = "No se encontro dicha Reserva";
            }
            dataReader.Close();
            sqlCon.Close();

            return reserva;
        }

        public List<TipHabCheckInBean> listarTipHabReserva(int idReserva)
        {
            List<TipHabCheckInBean> lista = new List<TipHabCheckInBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query = " SELECT t.nombre , tXh.nroPersonas, r.idTipoHabitacion " +
                            " FROM ReservaXTipoHabitacionXHotel r, TipoHabitacion t , TipoHabitacionXHotel tXh " +
                            " WHERE idReserva = " + idReserva + " and r.idHotel = tXh.idHotel and  r.idTipoHabitacion = t.idTipoHabitacion and tXh.idTipoHabitacion = t.idTipoHabitacion";

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                TipHabCheckInBean tipo = new TipHabCheckInBean();
                tipo.idTipHab = (int)dataReader["idTipoHabitacion"];
                tipo.nombTipHab = (String)dataReader["nombre"];
                tipo.nroPers = (int)dataReader["nroPersonas"];                
                lista.Add(tipo);
            }

            dataReader.Close();

            String queryHab =   " SELECT rXh.idHabitacion, h.idTipoHabitacion, h.numero " +
                                " FROM ReservaXHabitacion rXh, Habitacion h " +
                                " WHERE rXh.idReserva = " + idReserva + " AND rXh.idHabitacion = h.idHabitacion " +
                                " ORDER BY idTipoHabitacion";

            SqlCommand sqlCmdHab = new SqlCommand(queryHab, sqlCon);
            SqlDataReader dataReaderHab = sqlCmdHab.ExecuteReader();

            if (dataReaderHab.Read())
            {
                //bool sale = false;
                for (int i = 0; i < lista.Count; i++)
                {
                    int tipoHab = lista[i].idTipHab;
                    List<HabitacBean> listaHabitac = new List<HabitacBean>();
                    System.Diagnostics.Debug.WriteLine("TipoHab " + tipoHab);


                    while (tipoHab == (int)dataReaderHab["idTipoHabitacion"])
                    {
                        System.Diagnostics.Debug.WriteLine("--> " + (int)dataReaderHab["idHabitacion"]);
                        HabitacBean habitac = new HabitacBean();
                        habitac.idHab = (int)dataReaderHab["idHabitacion"];
                        habitac.numero = (String)dataReaderHab["numero"];
                        listaHabitac.Add(habitac);
                        if(!dataReaderHab.Read()){
                            //sale = true;
                            break;
                        }
                    }
                    lista[i].lista = listaHabitac;
                    //if (sale) break;
                }
            }
            dataReaderHab.Close();
            sqlCon.Close();
            return lista;
        }

        public String InsertClientesHabitacion(List<ClienteHabBean> listClientHab)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            for (int i = 0; i < listClientHab.Count; i++)
            {
                ClienteHabBean cliente = listClientHab[i];

                if (cliente.dni != null && cliente.nombresYAp != null)
                {
                    String query = "INSERT INTO ReservaXHabitacionXCliente VALUES ( " + cliente.idReserva + " , " + cliente.idHab + " , '" + cliente.nombresYAp + "' , '" + cliente.dni + "' )";

                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                    sqlCmd.ExecuteNonQuery();
                }
            }
            
            sqlCon.Close();
            return me;
        }

        public string ActualizarReserva(List<ClienteHabBean> listClientHab)
        {
            try
            {
                int idReserva = listClientHab.Last().idReserva;

                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "UPDATE Reserva SET estado = 3 WHERE idReserva = " + idReserva;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                for (int i = 0; i < listClientHab.Count; i++)
                {
                    commandString = "UPDATE ReservaXHabitacion SET estado = 3 WHERE idReserva = "+listClientHab.ElementAt(i).idReserva+" AND idHabitacion = "+listClientHab.ElementAt(i).idHab;

                    SqlCommand sqlCmd2 = new SqlCommand(commandString, sqlCon);
                    sqlCmd2.ExecuteNonQuery();
                }

                return "Ok";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        #endregion

        #region BUSCARPERSONA
        public UbicacPersResponse ubicarPersona(int idHotel, String nombre)
        {
            UbicacPersResponse ubic = new UbicacPersResponse();
            ubic.me = "";
            List<UbicacionPersonaBean> listClientes = new List<UbicacionPersonaBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            try
            {
                sqlCon.Open();
            }
            catch (Exception e) {
                ubic.me = "Error en conexion a Base de Datos";
                return ubic;
            }

            String query = " SELECT rXhXc.idReserva, h.numero, h.piso, rXhXc.nombre_apellidos, rXhXc.dni" +
                            " FROM ReservaXHabitacionXCliente rXhXc, Reserva r, Habitacion h" +
                            " WHERE r.idHotel = " + idHotel + " and r.estado = 3 and rXhXc.nombre_apellidos LIKE '%" + nombre + "%'  and rXhXc.idHabitacion = h.idHabitacion";

            System.Diagnostics.Debug.WriteLine("Ubicacion " + query);

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader;

            try
            {
                dataReader = sqlCmd.ExecuteReader();
                while (dataReader.Read())
                {
                    UbicacionPersonaBean cliente = new UbicacionPersonaBean();
                    cliente.reserva = (int)dataReader["idReserva"];
                    cliente.nroHab = (String)dataReader["numero"];
                    cliente.piso = (int)dataReader["piso"];
                    cliente.nomb = (String)dataReader["nombre_apellidos"];
                    cliente.dni = (String)dataReader["dni"];
                    listClientes.Add(cliente);
                }
                dataReader.Close();
            }catch(Exception e){
                ubic.me = "Error al buscar al Cliente xD (oo): " + e.Message;
                return ubic;
            }
            
            sqlCon.Close();

            ubic.lista =  listClientes;
            return ubic;
        }
        #endregion
    }
}
