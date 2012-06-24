using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

using System.Data;
using log4net;

namespace Stardust.Models
{
    public class HotelDAO
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(HotelDAO));

        public HotelBean getHotel(int id) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                HotelBean hotel = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Hotel WHERE idHotel = @idHotel";
                SqlCommand objquery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objquery, "@idHotel", id);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    hotel = new HotelBean();

                    hotel.ID = Convert.ToInt32(objDataReader[0]);//muy importante llenar este campo
                    hotel.nombre = Convert.ToString(objDataReader[1]);
                    hotel.razonSocial = Convert.ToString(objDataReader[2]);
                    hotel.direccion = Convert.ToString(objDataReader[3]);
                    hotel.tlf1 = Convert.ToString(objDataReader[4]);
                    hotel.tlf2 = Convert.ToString(objDataReader[5]);
                    hotel.email = Convert.ToString(objDataReader[6]);
                    hotel.nroPisos = Convert.ToInt32(objDataReader[7]);
                    hotel.idDistrito = Convert.ToInt32(objDataReader["idDistrito"]);
                    hotel.idProvincia = Convert.ToInt32(objDataReader["idProvincia"]);
                    hotel.idDepartamento = Convert.ToInt32(objDataReader["idDepartamento"]);
                    hotel.estado = Convert.ToBoolean(objDataReader["estado"]);
                }
                return hotel;
            }
            catch (Exception ex)
            {
                log.Error("getHotel(EXCEPTION): ", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public List<HotelBean> ListarHotel(string nombre){

        List<HotelBean> listaHotel = new List<HotelBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            
            sqlCon.Open();
            
            string commandString = "SELECT * FROM Hotel ";
            bool result1 = String.IsNullOrEmpty(nombre);//Nombre.Equals("") ;
            

            if (!result1);
                commandString = commandString + " WHERE UPPER(nombre) LIKE '%" + nombre.ToUpper() + "%'"; 
                        

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
                        
            while (dataReader.Read())
            {
                HotelBean hotel = new HotelBean();

                hotel.ID = Convert.ToInt32(dataReader[0]);//muy importante llenar este campo
                hotel.nombre = Convert.ToString(dataReader[1]);
                hotel.razonSocial = Convert.ToString(dataReader[2]);
                hotel.direccion = Convert.ToString(dataReader[3]);
                hotel.tlf1 = Convert.ToString(dataReader[4]);
                hotel.tlf2 = Convert.ToString(dataReader[5]);
                hotel.email = Convert.ToString(dataReader[6]);
                hotel.nroPisos = Convert.ToInt32(dataReader[7]);
                hotel.idDistrito = Convert.ToInt32(dataReader["idDistrito"]);
                hotel.idProvincia = Convert.ToInt32(dataReader["idProvincia"]);
                hotel.idDepartamento = Convert.ToInt32(dataReader["idDepartamento"]);
                hotel.estado = Convert.ToBoolean(dataReader["estado"]);
                listaHotel.Add(hotel);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaHotel;
        }


        /*--------Asignar Servicios por Hotel----*/

        public void InsertarHotelxServicio(int idhotel, ServicioXHotelBean serv)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            int i;
            for (i = 0; i < serv.listServHot.Count; i++)
            {
                if (serv.listServHot[i].precio > 0)
                {
                    string commandString = "INSERT INTO ServicioXHotel(idServicio,idHotel,precioBase) VALUES ('" +
                    serv.listServHot[i].id + "', '" +
                    idhotel+ "', '" +                    
                    serv.listServHot[i].precio + "')";
                    SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                    sqlCmd.ExecuteNonQuery();
                }
            }

            sqlCon.Close();
        }
        public ServicioXHotelBean obtenerlistaservicios(int idhotel)
        {
            ServicioXHotelBean serv = new ServicioXHotelBean();
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
            int i = 0;
            int idhot;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();
            string commandString = "SELECT * FROM ServicioXHotel  WHERE idHotel=" + idhotel;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            serv.listServHot = new List<ServicioHotel>();
            while (dataReader.Read())
            {
                ServicioHotel servHotel = new ServicioHotel();
                idhot = (int)dataReader["idHotel"];
                servHotel.id = (int)dataReader["idServicio"];
                servHotel.precio = (Decimal)dataReader["precioBase"];                ;
                i++;
                serv.listServHot.Add(servHotel);
            }
            dataReader.Close();
            sqlCon.Close();
            HotelBean hotel = getHotel(idhotel);

            serv.hotel = hotel.nombre;

            return serv;
        }

        public List<HotelBean> getHoteles()
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<HotelBean> listaHoteles = new List<HotelBean>();

                objDB.Open();
                String strQuery = "SELECT * FROM Hotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    //listaHoteles = new List<HotelBean>();
                    while (objDataReader.Read())
                    {
                        HotelBean hotel = new HotelBean();

                        hotel.ID = Convert.ToInt32(objDataReader["idHotel"]);
                        hotel.nombre = Convert.ToString(objDataReader["nombre"]);
                        hotel.razonSocial = Convert.ToString(objDataReader["razonSocial"]);
                        hotel.direccion = Convert.ToString(objDataReader["direccion"]);
                        hotel.tlf1 = Convert.ToString(objDataReader["tlf1"]);
                        hotel.tlf2 = Convert.ToString(objDataReader["tlf2"]);
                        hotel.email = Convert.ToString(objDataReader["email"]);
                        hotel.nroPisos = Convert.ToInt32(objDataReader["nroPisos"]);
                        hotel.idDepartamento = Convert.ToInt32(objDataReader["idDepartamento"]);
                        hotel.idProvincia = Convert.ToInt32(objDataReader["idProvincia"]);
                        hotel.idDistrito = Convert.ToInt32(objDataReader["idDistrito"]);
                        hotel.estado = Convert.ToBoolean(objDataReader["estado"]);

                        listaHoteles.Add(hotel);
                    }
                }

                return listaHoteles;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public List<HotelBean> getHotelesActivos()
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<HotelBean> listaHoteles = new List<HotelBean>();

                objDB.Open();
                String strQuery = "SELECT * FROM Hotel WHERE estado = 1"; //obtener todos los hoteles activos
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    //listaHoteles = new List<HotelBean>();
                    while (objDataReader.Read())
                    {
                        HotelBean hotel = new HotelBean();

                        hotel.ID = Convert.ToInt32(objDataReader["idHotel"]);
                        hotel.nombre = Convert.ToString(objDataReader["nombre"]);
                        hotel.razonSocial = Convert.ToString(objDataReader["razonSocial"]);
                        hotel.direccion = Convert.ToString(objDataReader["direccion"]);
                        hotel.tlf1 = Convert.ToString(objDataReader["tlf1"]);
                        hotel.tlf2 = Convert.ToString(objDataReader["tlf2"]);
                        hotel.email = Convert.ToString(objDataReader["email"]);
                        hotel.nroPisos = Convert.ToInt32(objDataReader["nroPisos"]);
                        hotel.idDepartamento = Convert.ToInt32(objDataReader["idDepartamento"]);
                        hotel.idProvincia = Convert.ToInt32(objDataReader["idProvincia"]);
                        hotel.idDistrito = Convert.ToInt32(objDataReader["idDistrito"]);
                        hotel.estado = Convert.ToBoolean(objDataReader["estado"]);

                        listaHoteles.Add(hotel);
                    }
                }

                return listaHoteles;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public void registrarHotel(HotelBean hotel) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "Insert into Hotel (nombre, razonSocial, direccion, tlf1, tlf2, email, nroPisos, idDistrito, idProvincia, idDepartamento, estado) values " +
                                    "(@nombre, @razonSocial, @direccion, @tlf1, @tlf2, @email, @nroPisos, @idDistrito, @idProvincia, @idDepartamento, @estado)";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@nombre", hotel.nombre);
                Utils.agregarParametro(objQuery, "@razonSocial", hotel.razonSocial);
                Utils.agregarParametro(objQuery, "@direccion", hotel.direccion);
                Utils.agregarParametro(objQuery, "@tlf1", hotel.tlf1);
                Utils.agregarParametro(objQuery, "@tlf2", hotel.tlf2);
                Utils.agregarParametro(objQuery, "@email", hotel.email);
                Utils.agregarParametro(objQuery, "@nroPisos", hotel.nroPisos);
                Utils.agregarParametro(objQuery, "@idDistrito", hotel.idDistrito);
                Utils.agregarParametro(objQuery, "@idProvincia", hotel.idProvincia);
                Utils.agregarParametro(objQuery, "@idDepartamento", hotel.idDepartamento);
                Utils.agregarParametro(objQuery, "@estado", hotel.estado);

                objQuery.ExecuteNonQuery();
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public int buscarHotel(HotelBean hotel)// buscar a un hotel con características determinadas
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                //La exactitud de la búsqueda va dentro del método, debe ser inherente a la llamada en los parámetros
                String strQuery = "SELECT idHotel FROM Hotel WHERE nombre = @nombre AND razonSocial = @razonSocial";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "@nombre", hotel.nombre);
                DAO.agregarParametro(objQuery, "@razonSocial", hotel.razonSocial);

                SqlDataReader data = objQuery.ExecuteReader();

                if (data.HasRows)
                {
                    data.Read();
                    return Convert.ToInt32(data.GetValue(0));
                }
                return -1; // en caso no encuentre el hotel que esta buscando con las caracteristicas pasadas por parametro
            }
            //catch (Exception ex)
            //{

            //}
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public void actualizarHotel(HotelBean hotel) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                
                objDB.Open();
                String strQuery = "UPDATE Hotel SET nombre = @nombre, razonSocial = @razonSocial, direccion = @direccion, " +
                                    "tlf1 = @tlf1, tlf2 = @tlf2, email = @email, nroPisos = @nroPisos, " +
                                    "idDepartamento = @idDepartamento, idProvincia = @idProvincia, idDistrito = @idDistrito " +
                                    "WHERE idHotel = @idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                Utils.agregarParametro(objQuery, "@idHotel", hotel.ID);
                Utils.agregarParametro(objQuery, "@nombre", hotel.nombre);
                Utils.agregarParametro(objQuery, "@razonSocial", hotel.razonSocial);
                Utils.agregarParametro(objQuery, "@direccion", hotel.direccion);
                Utils.agregarParametro(objQuery, "@tlf1", hotel.tlf1);
                Utils.agregarParametro(objQuery, "@tlf2", hotel.tlf2);
                Utils.agregarParametro(objQuery, "@email", hotel.email);
                Utils.agregarParametro(objQuery, "@nroPisos", hotel.nroPisos);
                Utils.agregarParametro(objQuery, "@idDepartamento", hotel.idDepartamento);
                Utils.agregarParametro(objQuery, "@idProvincia", hotel.idProvincia);
                Utils.agregarParametro(objQuery, "@idDistrito", hotel.idDistrito);

                objQuery.ExecuteNonQuery();

            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        /*
         * la eliminación del hotel es solo un cambio de estado porque en verdad no se debe poder eliminar la información
         * porque si no se pierde referencia de las Promociones, Tipos de Habitación por Hotel, Almacenes,
         * Servicios, Ambientes y Habitaciones
         */
        public void desactivarHotel(int idHotel) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                //this.eliminarAlmacen(id);

                objDB.Open();
                String strQuery = "UPDATE Hotel SET estado = 0 WHERE idHotel = @idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                objQuery.ExecuteNonQuery();
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void activarHotel(int idHotel)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                //this.eliminarAlmacen(id);

                objDB.Open();
                String strQuery = "UPDATE SET estado = 1 WHERE idHotel = @idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                objQuery.ExecuteNonQuery();

            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }



        public void registrarAlmacen(int idHotel, AlmacenBean almacen) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                
                objDB.Open();
                String strQuery = "INSERT INTO Almacen ( descripcion , capacidad , idHotel ) " + 
                                    "values ( @descripcion , @capacidad , @idHotel )";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "@descripcion", almacen.descripcion);
                DAO.agregarParametro(objQuery, "@capacidad", almacen.cantidad);
                DAO.agregarParametro(objQuery, "@idHotel", idHotel);

                objQuery.ExecuteNonQuery();
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        //
        public void eliminarAlmacen(int idHotel) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Delete from Almacen where idHotel = " + idHotel;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        //
        public int getAlmacen(int idHotel) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select idAlmacen from Almacen where idHotel = " + idHotel;
            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            data.Read();

            int resp = (int)data.GetValue(0);

            sql.Close();

            return resp;
        }

        //Parte para dar información antes de desactivar un Hotel
        //--------------------------------------------------------
        public int getNHabitacionesXHotel(int idHotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT COUNT(*) " + 
                                "FROM Hotel A, TipoHabitacionXHotel B, Habitacion C " +
                                "WHERE A.idHotel = @idHotel and A.idHotel = B.idHotel and " +
                                "B.idHotel = C.idHotel and B.idTipoHabitacion = C.idTipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                SqlDataReader objReader = objQuery.ExecuteReader();

                objReader.Read();
                return Convert.ToInt32(objReader[0]);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

            //preguntar si tiene habitaciones
            //en caso no tenga, se pregunta si tiene ambientes
            //en caso no tenga, se pregunta si tiene servicios
        }

        public int getNTipoHabitacionesXHotel(int idHotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT COUNT(*) FROM Hotel A, TipoHabitacionXHotel B " +
                                "WHERE A.idHotel = @idHotel and A.idHotel = B.idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                SqlDataReader objReader = objQuery.ExecuteReader();

                objReader.Read();
                return Convert.ToInt32(objReader[0]);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public int getNAmbientesXHotel(int idHotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT COUNT(*) FROM Hotel A, Ambiente B " +
                                "WHERE A.idHotel = @idHotel and A.idHotel = B.idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                SqlDataReader objReader = objQuery.ExecuteReader();

                objReader.Read();
                return Convert.ToInt32(objReader[0]);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public int getNServiciosXHotel(int idHotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT COUNT(*) FROM Hotel A, ServicioXHotel B " +
                                "WHERE A.idHotel = @idHotel and A.idHotel = B.idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                SqlDataReader objReader = objQuery.ExecuteReader();

                objReader.Read();
                return Convert.ToInt32(objReader[0]);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public int getNPromocionesXHotel(int idHotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT COUNT(*) FROM Hotel A, Promocion B " +
                                "WHERE A.idHotel = @idHotel and A.idHotel = B.idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                SqlDataReader objReader = objQuery.ExecuteReader();

                objReader.Read();
                return Convert.ToInt32(objReader[0]);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public int getNAlmacenesXHotel(int idHotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT COUNT(*) FROM Hotel A, Almacen B " +
                                "WHERE A.idHotel = @idHotel and A.idHotel = B.idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                SqlDataReader objReader = objQuery.ExecuteReader();

                objReader.Read();
                return Convert.ToInt32(objReader[0]);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }
        //--------------------------------------------------------

        public bool existeTipoHabitacion_Hotel(TipoHabitacionXHotel tipoHabitacion)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT * FROM TipoHabitacionXHotel " +
                    "WHERE idHotel = @idHotel AND idTipoHabitacion = @idTipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", tipoHabitacion.idHotel);
                Utils.agregarParametro(objQuery, "@idTipoHabitacion", tipoHabitacion.idTipoHabitacion);

                SqlDataReader objReader = objQuery.ExecuteReader();
                return objReader.HasRows;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void registrarTipoHabitacion(TipoHabitacionXHotel tipoHabitacionXhotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "INSERT INTO TipoHabitacionXHotel (idHotel, idTipoHabitacion, precioBaseXDia, nroPersonas)" +
                                " VALUES (@idHotel, @idTipoHabitacion, @precio, @nroPersonas)";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", tipoHabitacionXhotel.idHotel);
                Utils.agregarParametro(objQuery, "@idTipoHabitacion", tipoHabitacionXhotel.idTipoHabitacion);
                Utils.agregarParametro(objQuery, "@precio", tipoHabitacionXhotel.precio);
                Utils.agregarParametro(objQuery, "@nroPersonas", tipoHabitacionXhotel.nroPersonas);

                objQuery.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Error("registrarTipoHabitacion(EXCEPTION): ", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();

                }
            }
        }

        public void actualizarTipoHabitacion(TipoHabitacionXHotel tipoHabitacionXhotel)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "UPDATE TipoHabitacionXHotel SET precioBaseXDia = @precio, nroPersonas = @nroPersonas " +
                                    "WHERE idHotel = @idHotel AND idTipoHabitacion = @idTipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", tipoHabitacionXhotel.idHotel);
                Utils.agregarParametro(objQuery, "@idTipoHabitacion", tipoHabitacionXhotel.idTipoHabitacion);
                Utils.agregarParametro(objQuery, "@precio", tipoHabitacionXhotel.precio);
                Utils.agregarParametro(objQuery, "@nroPersonas", tipoHabitacionXhotel.nroPersonas);

                objQuery.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Error("actualizarTipoHabitacion(EXCEPTION): ", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public List<TipoHabitacionXHotelViewModelList> getTipoHabitacionXHotel(int idHotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);
                //lista de Tipos de Habitacion por Hotel
                List<TipoHabitacionXHotelViewModelList> lstTHXH_VML = new List<TipoHabitacionXHotelViewModelList>();

                objDB.Open();
                String strQuery = "SELECT * FROM TipoHabitacionXHotel A, TipoHabitacion B " +
                                "WHERE A.idHotel = @idHotel and A.idTipoHabitacion = B.idTipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);

                SqlDataReader objReader = objQuery.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        TipoHabitacionXHotelViewModelList tipoHabitacion = new TipoHabitacionXHotelViewModelList();

                        tipoHabitacion.idTipoHabitacion = Convert.ToInt32(objReader["idTipoHabitacion"]);
                        tipoHabitacion.nombre = Convert.ToString(objReader["nombre"]);
                        tipoHabitacion.descripcion = Convert.ToString(objReader["descripcion"]);
                        tipoHabitacion.precio = Convert.ToDecimal(objReader["precioBaseXDia"]);
                        tipoHabitacion.nroPersonas = Convert.ToInt32(objReader["nroPersonas"]);

                        lstTHXH_VML.Add(tipoHabitacion);
                    }
                }

                return lstTHXH_VML;
            }
            catch (Exception ex)
            {
                log.Error("getTipoHabitacionXHotel(EXCEPTION)", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public TipoHabitacionXHotelViewModelEdit getTipoHabitacionXHotel(int idHotel, int idTipoHabitacion)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                TipoHabitacionXHotelViewModelEdit objReturn = null;

                objDB.Open();
                String strQuery = "SELECT * FROM TipoHabitacionXHotel A, TipoHabitacion B " +
                                "WHERE A.idTipoHabitacion = B.idTipoHabitacion AND " +
                                "A.idHotel = @idHotel AND B.idTipoHabitacion = @idTipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);
                Utils.agregarParametro(objQuery, "@idTipoHabitacion", idTipoHabitacion);

                SqlDataReader objReader = objQuery.ExecuteReader();
                if (objReader.HasRows)
                {
                    objReturn = new TipoHabitacionXHotelViewModelEdit();
                    objReader.Read();

                    objReturn.idTipoHabitacion = idTipoHabitacion;
                    objReturn.idHotel = idHotel;
                    objReturn.nroPersonas = Convert.ToInt32(objReader["nroPersonas"]);
                    objReturn.precio = Convert.ToDecimal(objReader["precioBaseXDia"]);
                }

                return objReturn;
            }
            catch (Exception ex)
            {
                log.Error("getTipoHabitacionXHotel(EXCEPTION): ", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public List<TipoHabitacion> getTipoHabitaciones()
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<TipoHabitacion> lstTipoHabitacion = new List<TipoHabitacion>();

                objDB.Open();
                String strQuery = "SELECT * FROM TipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objReader = objQuery.ExecuteReader();

                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        TipoHabitacion tipoHabitacionAux = new TipoHabitacion();

                        tipoHabitacionAux.idTipoHabitacion = Convert.ToInt32(objReader[0]);
                        tipoHabitacionAux.nombre = Convert.ToString(objReader[1]);
                        tipoHabitacionAux.descripcion = Convert.ToString(objReader[2]);

                        lstTipoHabitacion.Add(tipoHabitacionAux);
                    }
                }
                return lstTipoHabitacion;
            }
            catch (Exception ex)
            {
                log.Error("getTipoHabitaciones(EXCEPTION): ", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public decimal getPrecioTipoHabitacionXHotel(int idHotel, int idTipoHabitacion)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT precioBaseXDia FROM TipoHabitacionXHotel " +
                                "WHERE idHotel = @idHotel AND idTipoHabitacion = @idTipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@idHotel", idHotel);
                Utils.agregarParametro(objQuery, "@idTipoHabitacion", idTipoHabitacion);

                SqlDataReader objReader = objQuery.ExecuteReader();

                if (objReader.HasRows)
                {
                    objReader.Read();
                    return Convert.ToDecimal(objReader[0]);
                }
                return -1;//en caso no encuentre el precio del tipo de habitacion del hotel que estoy buscando

            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }
    }
}