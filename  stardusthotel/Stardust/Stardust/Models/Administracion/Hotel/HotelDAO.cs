using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

using System.Data;

namespace Stardust.Models
{
    public class HotelDAO
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        
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
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
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

        //
        public void registrarTipoHabitacion(TipoHabitacionxHotel tipoHabitacion) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Insert into TipoHabitacionXHotel ( idHotel , idTipoHabitacion , precioBaseXDia ) values (" +
                                    tipoHabitacion.idHotel + ", " +
                                    tipoHabitacion.idTipoHabitacion + ", " +
                                    tipoHabitacion.precioBase + ")";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        //
        public List<TipoHabitacionxHotel> listarTipos(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from TipoHabitacionXHotel where idHotel = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<TipoHabitacionxHotel> lista = new List<TipoHabitacionxHotel>();

                TipoHabitacionFacade tipoFac = new TipoHabitacionFacade();

                while (data.Read()) {
                    TipoHabitacionxHotel tipo = new TipoHabitacionxHotel();

                    tipo.nombreTipoHabitacion = tipoFac.getTipo( (int)data.GetValue( 1 ) ).nombre ;
                    tipo.precioBase = (decimal)data.GetValue(2);
                    
                    lista.Add(tipo);
                }

            sql.Close();

            return lista;
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
    }
}