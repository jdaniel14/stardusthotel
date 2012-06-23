using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class PerfilUsuarioDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        
        public void registrarPerfil(PerfilUsuarioBean perfil) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Insert into PerfilUsuario ( nombre , descripcion ) values ('" +
                                perfil.nombre + "', '" +
                                perfil.descripcion + "')";

                SqlCommand query = new SqlCommand( command , sql ) ;
                query.ExecuteNonQuery();
            
            sql.Close();
        }

        public PerfilUsuarioBean getPerfil( int id ){
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from PerfilUsuario where idPerfilUsuario = " + id;

                SqlCommand query = new SqlCommand(command, sql);
                SqlDataReader data = query.ExecuteReader();

                data.Read();

                PerfilUsuarioBean perfil = new PerfilUsuarioBean();
                perfil.ID = Convert.ToInt32(data["idPerfilUsuario"]);
                perfil.nombre = Convert.ToString(data["nombre"]);
                perfil.descripcion = Convert.ToString(data["descripcion"]);
                perfil.token = Convert.ToString(data["token"]);

            sql.Close();

            return perfil;
        }

        public List<PerfilUsuarioBean> listarPerfiles(){
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from PerfilUsuario";

                SqlCommand query = new SqlCommand(command, sql);
                SqlDataReader data = query.ExecuteReader();

                List<PerfilUsuarioBean> lista = new List<PerfilUsuarioBean>();

                while (data.Read()) {
                    PerfilUsuarioBean perfil = new PerfilUsuarioBean();

                    perfil.ID = Convert.ToInt32( data[ "idPerfilUsuario" ] ) ;
                    perfil.nombre = Convert.ToString(data["nombre"]);
                    perfil.descripcion = Convert.ToString(data["descripcion"]);

                    lista.Add(perfil);
                }

            sql.Close();

            return lista;
        }

        public void actualizarPerfil(PerfilUsuarioBean perfil) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update PerfilUsuario SET nombre = '" + perfil.nombre + "', " +
                                 "descripcion = '" + perfil.descripcion + "' where idPerfilUsuario = " + perfil.ID;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void eliminarPerfil(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Delete from PerfilUsuario where idPerfilUsuario = " + id;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }
    }
}