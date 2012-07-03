using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using log4net;

namespace Stardust.Models
{
    public class PerfilUsuarioDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(PerfilUsuarioDAO));
        
        public void registrarPerfil(PerfilUsuarioBean perfil) {

            SqlConnection sql = null;
            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Insert into PerfilUsuario ( nombre , descripcion ) values ('" +
                                perfil.nombre + "', '" +
                                perfil.descripcion + "')";

                SqlCommand query = new SqlCommand(command, sql);
                query.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception e)
            {
                log.Error("registrarPerfil(EXCEPTION): ", e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public PerfilUsuarioBean getPerfil( int id ){
            SqlConnection sql = null;
            try
            {
                sql = new SqlConnection(cadenaDB);

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
            catch (Exception e)
            {
                log.Error("getPerfil(EXCEPTION): ", e);
                throw (e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public List<PerfilUsuarioBean> listarPerfiles(){
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from PerfilUsuario";

                SqlCommand query = new SqlCommand(command, sql);
                SqlDataReader data = query.ExecuteReader();

                List<PerfilUsuarioBean> lista = new List<PerfilUsuarioBean>();

                while (data.Read())
                {
                    PerfilUsuarioBean perfil = new PerfilUsuarioBean();

                    perfil.ID = Convert.ToInt32(data["idPerfilUsuario"]);
                    perfil.nombre = Convert.ToString(data["nombre"]);
                    perfil.descripcion = Convert.ToString(data["descripcion"]);

                    lista.Add(perfil);
                }

                sql.Close();

                return lista;
            }
            catch (Exception e)
            {
                log.Error("listarPerfiles(EXCEPTION): ", e);
                throw (e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public List<PerfilUsuarioBean> listarPerfilCliente()
        {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from PerfilUsuario WHERE nombre = @nombre";

                SqlCommand query = new SqlCommand(command, sql);

                Utils.agregarParametro(query, "nombre", "Cliente");

                SqlDataReader data = query.ExecuteReader();

                List<PerfilUsuarioBean> lista = new List<PerfilUsuarioBean>();

                while (data.Read())
                {
                    PerfilUsuarioBean perfil = new PerfilUsuarioBean();

                    perfil.ID = Convert.ToInt32(data["idPerfilUsuario"]);
                    perfil.nombre = Convert.ToString(data["nombre"]);
                    perfil.descripcion = Convert.ToString(data["descripcion"]);

                    lista.Add(perfil);
                }

                sql.Close();

                return lista;
            }
            catch (Exception e)
            {
                log.Error("listarPerfiles(EXCEPTION): ", e);
                throw (e);
            }
            finally
            {
                if (sql != null) sql.Close();
            }
        }

        public void actualizarPerfil(PerfilUsuarioBean perfil) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Update PerfilUsuario SET nombre = '" + perfil.nombre + "', " +
                                 "descripcion = '" + perfil.descripcion + "' where idPerfilUsuario = " + perfil.ID;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception e)
            {
                log.Error("actualizarPerfil(EXCEPTION): ", e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public void eliminarPerfil(int id) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Delete from PerfilUsuario where idPerfilUsuario = " + id;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception e)
            {
                log.Error("eliminarPerfil(EXCEPTION): ", e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public int getPerfilID( string nombre ){
            SqlConnection sql = null ;

            try{
                sql = new SqlConnection( cadenaDB ) ;

                sql.Open();

                String command = "Select * from PerfilUsuario WHERE nombre = @nombre" ;

                SqlCommand query = new SqlCommand( command , sql ) ;

                Utils.agregarParametro( query , "@nombre" , nombre ) ;

                SqlDataReader data =  query.ExecuteReader() ;

                if( data.HasRows ){
                    data.Read() ;
                    return Convert.ToInt32( data[ "idPerfilUsuario" ] ) ;
                }
                return -1 ;
            }catch( Exception e ){
                log.Error( "getPerfilID(EXCEPTION): " , e ) ;
                throw (e);
            }finally{
                if( sql != null ) sql.Close() ;
            }
        }
    }
}