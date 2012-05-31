using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;


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
                DAO.agregarParametro(objquery, "idHotel", id);

                SqlDataReader objDataReader = objquery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    hotel = new HotelBean();

                    hotel.ID = (int)objDataReader.GetValue(0);
                    hotel.nombre = (string)objDataReader.GetValue(1);
                    hotel.razonSocial = (string)objDataReader.GetValue(2);
                    hotel.direccion = (string)objDataReader.GetValue(3);
                    hotel.tlf1 = (string)objDataReader.GetValue(4);
                    hotel.tlf2 = (string)objDataReader.GetValue(5);
                    hotel.email = (string)objDataReader.GetValue(6);
                    hotel.nroPisos = (int)objDataReader.GetValue(7);
                    hotel.idDistrito = Convert.ToInt32(objDataReader["idDistrito"]);
                    hotel.idProvincia = Convert.ToInt32(objDataReader["idProvincia"]);
                    hotel.idDepartamento = Convert.ToInt32(objDataReader["idDepartamento"]);

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
                List<HotelBean> listaHoteles = null;

                objDB.Open();
                String strQuery = "SELECT * FROM Hotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    listaHoteles = new List<HotelBean>();
                    while (objDataReader.Read())
                    {
                        HotelBean hotel = new HotelBean();
                        hotel.ID = Convert.ToInt32(objDataReader[0]);
                        hotel.nombre = Convert.ToString(objDataReader[1]);
                        hotel.razonSocial = Convert.ToString(objDataReader[2]);
                        hotel.direccion = Convert.ToString(objDataReader[3]);
                        hotel.tlf1 = Convert.ToString(objDataReader[4]);
                        hotel.tlf2 = Convert.ToString(objDataReader[5]);
                        hotel.email = Convert.ToString(objDataReader[6]);
                        hotel.direccion = Convert.ToString(objDataReader[7]);
                        hotel.nroPisos = Convert.ToInt32(objDataReader[8]);

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
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();


                //int idDepartamento = this.getIdDepartamento(hotel.departamento);
                //int idProvincia = this.getIdProvincia(hotel.provincia , idDepartamento );
                //int idDistrito = this.getIdDistrito(hotel.distrito , idProvincia , idDepartamento );

                // LUEGO CAMBIAR ESTOS VALORES
                int idDepartamento = 15; 
                int idProvincia = 1;
                int idDistrito = 22;



                String command = "Insert into Hotel ( nombre , razonSocial , direccion , tlf1 , tlf2 , email , nroPisos , idDistrito , idProvincia , idDepartamento ) values " +
                                    "(' " + hotel.nombre + "', '" +
                                    hotel.razonSocial + "', '" +
                                    hotel.direccion + "', '" +
                                    hotel.tlf1 + "', '" +
                                    hotel.tlf2 + "', '" +
                                    hotel.email + "', " +
                                    hotel.nroPisos + ", " +
                                    idDistrito + ", " +
                                    idProvincia + ", " +
                                    idDepartamento + ")";
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void actualizarHotel(HotelBean hotel) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                //int idDepartamento = this.getIdDepartamento(hotel.departamento);
                //int idProvincia = this.getIdProvincia(hotel.provincia , idDepartamento );
                //int idDistrito = this.getIdDistrito(hotel.distrito , idProvincia , idDepartamento );

                int idDepartamento = 15;
                int idProvincia = 1;
                int idDistrito = 22;

                String command = "Update Hotel SET " +
                                "nombre = '" + hotel.nombre + "', " +
                                "razonSocial = '" + hotel.razonSocial + "', " +
                                "tlf1 = '" + hotel.tlf1 + "', " +
                                "tlf2 = '" + hotel.tlf2 + "', " +
                                "email = '" + hotel.email + "', " +
                                "nroPisos = " + hotel.nroPisos + ", " +
                                "idDistrito = " + idDistrito + ", " +
                                "idProvincia = " + idProvincia + ", " +
                                "idDepartamento = " + idDepartamento +
                                "where idHotel = " + hotel.ID;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void eliminarHotel(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Delete from Hotel where idHotel = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public List<HotelBean> listarHoteles() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Hotel";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<HotelBean> lista = new List<HotelBean>();

                while (data.Read()) {
                    HotelBean hotel = new HotelBean();
                    hotel.ID = (int)data.GetValue(0);
                    hotel.nombre = (string)data.GetValue(1);
                    hotel.razonSocial = (string)data.GetValue(2);
                    hotel.direccion = (string)data.GetValue(3);
                    hotel.tlf1 = (string)data.GetValue(4);
                    hotel.tlf2 = (string)data.GetValue(5);
                    hotel.email = (string)data.GetValue(6);
                    hotel.nroPisos = (int)data.GetValue(7);

                    int idDistrito = (int)data.GetValue(8);
                    int idProvincia = (int)data.GetValue(9);
                    int idDepartamento = (int)data.GetValue(10);

                    //hotel.idDistrito = this.getNombreDistrito(idDepartamento, idProvincia, idDistrito);
                    //hotel.idProvincia = this.getNombreProvincia(idDepartamento, idProvincia);
                    //hotel.idDepartamento = this.getNombreDepartamento(idDepartamento);

                    lista.Add(hotel);
                }

            sql.Close();

            return lista;
        }

        public string getNombreDistrito(int idDepartamento , int idProvincia , int idDistrito ) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select nombre from Distrito where idDepartamento = " + idDepartamento + "and idProvincia = " + idProvincia + "and idDistrito = " + idDistrito ;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                String distrito = (string)data.GetValue(0);

            sql.Close();

            return distrito;
        }

        public string getNombreProvincia(int idDepartamento , int idProvincia ) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select nombre from Provincia  where idDepartamento = " + idDepartamento + "and idProvincia = " + idProvincia ;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                String provincia = (string)data.GetValue(0);

            sql.Close();

            return provincia;
        }

        public string getNombreDepartamento(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select nombre from Departamento where idDepartamento = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                String idDepartamento = (string)data.GetValue(0);

            sql.Close();

            return idDepartamento;
        }

        //public int getIdDistrito(string s , int idProvincia , int idDepartamento) {
        //    SqlConnection sql = new SqlConnection(cadenaDB);

        //    sql.Open();

        //        String command = "Select idDistrito from Distrito where nombre = '" + s + "' and idDepartamento = " + idDepartamento + "and idProvincia = " + idProvincia ;

        //        SqlCommand query = new SqlCommand(command, sql);

        //        SqlDataReader data = query.ExecuteReader();

        //        data.Read();

        //        int idDistrito = (int)data.GetValue(0);

        //    sql.Close();

        //    return idDistrito;
        //}

        //public int getIdProvincia(string s ,int idDepartamento ) {
        //    SqlConnection sql = new SqlConnection(cadenaDB);

        //    sql.Open();

        //        String command = "Select idProvincia from Provincia where nombre = '" + s + "'and idDepartamento = " + idDepartamento ;

        //        SqlCommand query = new SqlCommand(command, sql);

        //        SqlDataReader data = query.ExecuteReader();

        //        data.Read();

        //        int idProvincia = (int)data.GetValue(0);

        //    sql.Close();

        //    return idProvincia;
        //}

        //public int getIdDepartamento(string s) {
        //    SqlConnection sql = new SqlConnection(cadenaDB);

        //    sql.Open();

        //        String command = "Select idDepartamento from Departamento where nombre = '" + s + "'";

        //        SqlCommand query = new SqlCommand(command, sql);

        //        SqlDataReader data = query.ExecuteReader();

        //        data.Read();

        //        int idDepartamento = (int)data.GetValue(0);

        //    sql.Close();

        //    return idDepartamento ;
        //}

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
    }
}