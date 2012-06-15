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
        public List<HabitacionReserva> listarNoDisponibles(int idHotel, String fechaIni, String fechaFin) {
            List<HabitacionReserva> listaHab = new List<HabitacionReserva>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query =  " SELECT DISTINCT rxh.idHabitacion " +
                            " FROM ReservaXHabitacion rXh, Habitacion h " +
                            " WHERE rXh.estado<3 and " +
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
                            " WHERE estado = 'ACTIVO' and tXh.idTipoHabitacion = h.idTipoHabitacion and tXh.idHotel = " + idHotel + " " + 
                            " ORDER BY idHabitacion";

            System.Diagnostics.Debug.WriteLine("TOTAL : "+ query);

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                HabitacionReserva hab = new HabitacionReserva();
                hab.idHab= (int)dataReader["idHabitacion"];
                hab.idTipoHabitacion = (int)dataReader["idTipoHabitacion"];
                System.Diagnostics.Debug.WriteLine("numero : " + (string)dataReader["numero"]);
                hab.numero = (string)dataReader["numero"];
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
                            " WHERE thXh.idHotel = 1 and th.idTipoHabitacion = thXh.idTipoHabitacion";
            
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


        public String deleteReserva(int idReserva)
        {
            String me = "";

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query1 = " UPDATE Reserva" +
                            " SET estado='CANCELADO'" +
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

        
        

        public List<CheckInBean> SelectDatosCheckIn(int idHotel, String nombre)
        {
            List<CheckInBean> listaReturn = new List<CheckInBean>();

            return new List<CheckInBean>();
        }

        public int registraCliente(ClienteReservaBean client){
        
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            //cliente.estado2 = 1;

            string commandString3 = "INSERT INTO Usuario VALUES (1," +
                     "''" + ", " +
                     "''" + ", " +
                     "'" + client.nomb+ "'" + ", " +
                     "'" + client.apell + "'" + ", " +
                     "''" + ", " +
                     "'" + client.email+"', " +//email
                     "'" + client.telf+"', " +//celular
                     "'" + client.tipDoc+ "'" + ", " +
                     "'" + client.nroDoc+ "'" + ", " +
                     "'" + client.razSoc+ "'" + ", " +
                     "'ACTIVO'" + ", " +//estado
                     "1" + ", " +//Distrito
                     "''" + ", " +//Direccion
                     "1" + ", " +//Provincia
                     "1" + //Departamento
                     ")"
                     ;


            SqlCommand sqlCmd3 = new SqlCommand(commandString3, sqlCon);
            sqlCmd3.ExecuteNonQuery();

            string commandString2 = "SELECT IDENT_CURRENT('" + "Usuario" + "') as lastId";
            SqlCommand sqlCmd2 = new SqlCommand(commandString2, sqlCon);
            SqlDataReader dataReader = sqlCmd2.ExecuteReader();

            int last_id = 0;
            if (dataReader.Read())
            {
                //last_id = (int)dataReader["lastId"];
                last_id = Int16.Parse(dataReader["lastId"].ToString()); 
                //listaClientes.Add(cliente);
            }
            dataReader.Close(); 
            
            System.Diagnostics.Debug.WriteLine("ultimo id "+last_id);

            string commandString1 = "INSERT INTO Cliente VALUES (" + last_id.ToString() + 
                     ", GETDATE()" + ", " +
                     "'ACTIVO'" + ", " +
                     "'" + client.tipoTarj+ "'" + ", " +
                     "'" + client.nroTarj + "'" + ")"
                     ;

            SqlCommand sqlCmd1 = new SqlCommand(commandString1, sqlCon);
            sqlCmd1.ExecuteNonQuery();

            sqlCon.Close();            

            return last_id ;
        
        
        
        }
        public int  resgitrarReserva(int idHotel, int idUsuario, String fechaIni, String fechaFin, String coment){


            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query = "INSERT INTO Reserva Values (convert(date,'" + fechaIni + "',103), convert(date,'" + fechaFin + "',103) ,NULL, 'POR CONFIRMAR', 0, 0, 0, " + idHotel.ToString() + ", " + idUsuario + ", GETDATE())";
            System.Diagnostics.Debug.WriteLine("query de RESERVA : " + query);

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            sqlCmd.ExecuteNonQuery();

            string query2 = "SELECT IDENT_CURRENT('" + "Reserva" + "') as lastId";
            SqlCommand sqlCmd2 = new SqlCommand(query2, sqlCon);
            SqlDataReader dataReader = sqlCmd2.ExecuteReader();

            int last_id = 0;
            if (dataReader.Read())
            {
                last_id = Int16.Parse(dataReader["lastId"].ToString());
            }
            dataReader.Close();
            sqlCon.Close();
            
            return last_id;
        }
        public String resgistrarHabitaciones(List<HabInsertBean> listTip, String fechaIni, String fechaFin, int idReserva) {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            System.Diagnostics.Debug.WriteLine("total : " + listTip.Count);
            for (int i = 0; i < listTip.Count; i++) {
                System.Diagnostics.Debug.WriteLine("--> " + listTip[i].cant);
                for (int j = 0; j < listTip[i].cant; j++) {
                    int hab = listTip[i].list[j].idHab;
                    String query = "INSERT INTO ReservaXHabitacion VALUES (" + idReserva + "," + hab + ",convert(date,'" + fechaIni + "',103),convert(date,'" + fechaFin + "', 103),2)";
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
                String query = "INSERT INTO ReservaXTipoHabitacionXHotel Values(" + idReserva + ", " + idHotel + ", " + listTip[i].tipo + ", " + listTip[i].cant + ")";
                System.Diagnostics.Debug.WriteLine("query TIPO--> " + query);
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.ExecuteNonQuery();
            }
            sqlCon.Close();
        }


        public DatosReservaBean consultarReserva(int idHotel, int idReserva, String documento)
        {
            DatosReservaBean consulta = new DatosReservaBean();
            

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query = " SELECT r.idReserva, r.fechaLlegada, r.fechaSalida, u.nroDocumento, (u.razonSocial+u.nombres+' '+u.apPat) as nombre"+
                           " FROM Reserva r, Usuario u " +
                           " WHERE r.idHotel = " + idHotel + " and r.idReserva = "+idReserva+ " and r.idUsuario = u.idUsuario and u.nroDocumento = '" + documento + "'";

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            consulta.me = "";
            if (dataReader.Read())
            {
                consulta.idReserva = (int)dataReader["idReserva"];
                consulta.fechaLlegada = ((DateTime)dataReader["fechaLlegada"]).ToString("dd-MM-yyyy");
                consulta.fechaSalida = ((DateTime)dataReader["fechaSalida"]).ToString("dd-MM-yyyy");
                consulta.doc = (String)dataReader["nroDocumento"];
                consulta.nomb= (String)dataReader["nombre"];   
            }
            else 
            {
                consulta.me = "No se encontro ninguna Reserva con esos datos";
            }
            dataReader.Close();
            sqlCon.Close();

            return consulta;
        }
        public List<TipoHabCant> listarTipHabReserva(int idReserva){
            List<TipoHabCant> lista = new List<TipoHabCant>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query = "SELECT t.nombre , r.cantidad FROM ReservaXTipoHabitacionXHotel r, TipoHabitacion t WHERE idReserva = " + idReserva + " and r.idTipoHabitacion = t.idTipoHabitacion";

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
                        
            while (dataReader.Read())
            {
                TipoHabCant tipo = new TipoHabCant();
                tipo.nombreTipoHab = (String)dataReader["nombre"];
                tipo.cant = (int)dataReader["cantidad"];
                lista.Add(tipo);
            }
            return lista;
        }



        public List<UbicacionClienteBean> ubicarPersona(String nombre)
        {
            List<UbicacionClienteBean> listClientes = new List<UbicacionClienteBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            String query = " SELECT rXhXc.idReserva, h.numero, h.piso, rXhXc.nombrYApell, rXhXc.dni" +
                            " FROM ReservaXHabitacionXCliente rXhXc, Reserva r, Habitacion h" +
                            " WHERE r.estado = 'EN CURSO' and rXhXc.nombrYApell LIKE '%" + nombre + "%'  and rXhXc.idHabitacion = h.idHabitacion";

            System.Diagnostics.Debug.WriteLine("Ubicacion " + query);

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                UbicacionClienteBean cliente = new UbicacionClienteBean();
                cliente.idReserva = (int)dataReader["idReserva"];
                cliente.numero = (String)dataReader["numero"];
                cliente.piso = (int)dataReader["piso"];
                cliente.nombYApell = (String)dataReader["nombrYApell"];
                cliente.dni = (String)dataReader["dni"];
                listClientes.Add(cliente);
            }
            dataReader.Close();
            sqlCon.Close();

            return listClientes;
        }
        //public String registrarServicio(Object datRec){}
    }
}
