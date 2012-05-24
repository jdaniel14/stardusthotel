using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class HotelDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        
        public HotelBean getHotel(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Hotel where idHotel = " + id;
                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

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

                hotel.distrito = this.getNombreDistrito(idDepartamento, idProvincia, idDistrito);
                hotel.provincia = this.getNombreProvincia(idDepartamento, idProvincia);
                hotel.departamento = this.getNombreDepartamento(idDepartamento);

            sql.Close();

            return hotel;
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
                //int idDepartamento = this.getIdDepartamento(hotel.departamento);
                //int idProvincia = this.getIdProvincia(hotel.provincia , idDepartamento );
                //int idDistrito = this.getIdDistrito(hotel.distrito , idProvincia , idDepartamento );
    
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

                    hotel.distrito = this.getNombreDistrito(idDepartamento, idProvincia, idDistrito);
                    hotel.provincia = this.getNombreProvincia(idDepartamento, idProvincia);
                    hotel.departamento = this.getNombreDepartamento(idDepartamento);

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

        public int getIdDistrito(string s , int idProvincia , int idDepartamento) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select idDistrito from Distrito where nombre = '" + s + "' and idDepartamento = " + idDepartamento + "and idProvincia = " + idProvincia ;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                int idDistrito = (int)data.GetValue(0);

            sql.Close();

            return idDistrito;
        }

        public int getIdProvincia(string s ,int idDepartamento ) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select idProvincia from Provincia where nombre = '" + s + "'and idDepartamento = " + idDepartamento ;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                int idProvincia = (int)data.GetValue(0);

            sql.Close();

            return idProvincia;
        }

        public int getIdDepartamento(string s) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select idDepartamento from Departamento where nombre = '" + s + "'";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                int idDepartamento = (int)data.GetValue(0);

            sql.Close();

            return idDepartamento ;
        }

        public void registrarTipoHabitacion(TipoHabitacionxHotel tipoHabitacion) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                HabitacionDAO habitacionDAO = new HabitacionDAO() ;
                int idHotel = habitacionDAO.getIdHotel( tipoHabitacion.hotel ) ;
                int idTipoHabitacion = habitacionDAO.getIdTipoHabitacion(tipoHabitacion.tipoHabitacion);

                String command = "Insert into TipoHabitacionXHotel ( idHotel , idTipoHabitacion , precioBaseXDia ) values (" +
                                    idHotel + ", " +
                                    idTipoHabitacion + ", " +
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

                HabitacionDAO habitacionDAO = new HabitacionDAO();
                while (data.Read()) {
                    TipoHabitacionxHotel tipo = new TipoHabitacionxHotel();

                    tipo.hotel = habitacionDAO.getNombreHotel((int)data.GetValue(0));
                    tipo.tipoHabitacion = habitacionDAO.getTipoHabitacion((int)data.GetValue(1));
                    tipo.precioBase = (decimal)data.GetValue(2);

                    lista.Add(tipo);
                }

            sql.Close();

            return lista;
        }
    }
}