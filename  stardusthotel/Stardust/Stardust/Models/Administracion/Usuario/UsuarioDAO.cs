﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using log4net;
using System.Web.Configuration;

namespace Stardust.Models
{
    public class UsuarioDAO
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(UsuarioDAO));

        public UsuarioBean getLogin(string user, string pass) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from Usuario where user_account = @user_account AND pass = @pass AND estado = @estado";

                SqlCommand query = new SqlCommand(command, sql);

                Utils.agregarParametro(query, "user_account", user);
                Utils.agregarParametro(query, "pass", pass);
                Utils.agregarParametro(query, "estado", "ACTIVO");

                SqlDataReader data = query.ExecuteReader();

                int idPerfil = 1;
                UsuarioBean usuario = null;

                if (data.HasRows)
                {
                    data.Read();
                    idPerfil = Convert.ToInt32(data["idPerfilUsuario"]);
                    int idUsuario = Convert.ToInt32(data["idUsuario"]);
                    usuario = this.getUsuario(idUsuario);
                    //return Convert.ToString(data["idUsuario"]); // <------------- comentar ... =)
                }

                sql.Close();

                String permisosPerfil = new PerfilUsuarioFacade().getPerfil(idPerfil).token;

                usuario.estado = permisosPerfil;

                return usuario ;
            }
            catch( Exception e ){
                log.Error("getLogin(EXCEPTION): " + e);
                return null;
            }
            finally
            {
                if (sql != null) sql.Close();
            }
        }

        public void marcarOnline(int id) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                    String command = "Update Usuario SET estado = @estado " +
                                        "WHERE idUsuario = @idUsuario";
                    SqlCommand query = new SqlCommand(command, sql);

                    Utils.agregarParametro(query, "estado", "ONLINE");
                    Utils.agregarParametro(query, "idUsuario", id);

                    query.ExecuteNonQuery();

                sql.Close();
            }
            catch( Exception e )
            {
                log.Error("marcarOnline(EXCEPTION): " + e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public void logout(int id) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                    String command = "Update Usuario SET estado = @estado " +
                                        "WHERE idUsuario = @idUsuario";
                    SqlCommand query = new SqlCommand(command, sql);

                    Utils.agregarParametro(query, "estado", "ACTIVO");
                    Utils.agregarParametro(query, "idUsuario", id);

                    query.ExecuteNonQuery();

                sql.Close();
            }
            catch( Exception e )
            {
                log.Error("logout(EXCEPTION): " + e);
            }
            finally
            {
                if (sql != null) sql.Close();
            }
        }
        
        public UsuarioBean getUsuario(int id)
        {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from Usuario where idUsuario = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                UsuarioBean usuario = null;

                if (data.HasRows)
                {
                    data.Read();

                    usuario = new UsuarioBean();

                    usuario.ID = Convert.ToInt32(data["idUsuario"]);
                    usuario.idPerfilUsuario = Convert.ToInt32(data["idPerfilUsuario"]);
                    
                    usuario.user_account = Convert.ToString(data["user_account"]);
                    usuario.pass = Convert.ToString(data["pass"]);
                    usuario.nombres = Convert.ToString(data["nombres"]);
                    usuario.apPat = Convert.ToString(data["apPat"]);
                    usuario.apMat = Convert.ToString(data["apMat"]);
                    usuario.email = Convert.ToString(data["email"]);
                    usuario.celular = Convert.ToString(data["celular"]);
                    usuario.tipoDocumento = Convert.ToString(data["tipoDocumento"]);
                    usuario.nroDocumento = Convert.ToString(data["nroDocumento"]);
                    usuario.razonSocial = Convert.ToString(data["razonSocial"]);
                    usuario.estado = Convert.ToString(data["estado"]);
                    usuario.direccion = Convert.ToString(data["direccion"]);
                    int idDistrito = usuario.idDistrito = Convert.ToInt32(data["idDistrito"]);
                    int idProvincia = usuario.idProvincia = Convert.ToInt32(data["idProvincia"]);
                    int idDepartamento = usuario.idDepartamento = Convert.ToInt32(data["idDepartamento"]);
                    
                    usuario.nombrePerfilUsuario = this.getNombrePerfilUsuario(usuario.idPerfilUsuario);

                    usuario.nombreDistrito = this.getDistrito(idDepartamento, idProvincia, idDistrito);
                    usuario.nombreProvincia = this.getProvincia(idDepartamento, idProvincia);
                    usuario.nombreDepartamento = this.getDepartamento(idDepartamento);

                }

                return usuario;
            }
            catch( Exception e ){
                log.Error("getUsuario(EXCEPTION): " + e);
                throw e;
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public String getDistrito(int idDepartamento, int idProvincia, int idDistrito)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();

                //String command = "SELECT nombre FROM Distrito WHERE idDepartamento = " + idDepartamento + " AND idProvincia = " + idProvincia + " AND idDistrito = " + idDistrito;
                String strQuery = "SELECT nombre FROM Distrito WHERE " +
                                    "idDepartamento = @idDepartamento AND idProvincia = @idProvincia AND idDistrito = @idDistrito";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "@idDepartamento", idDepartamento);
                DAO.agregarParametro(objQuery, "@idProvincia", idProvincia);
                DAO.agregarParametro(objQuery, "@idDistrito", idDistrito);

                SqlDataReader data = objQuery.ExecuteReader();
                if (data.HasRows)
                {
                    data.Read();
                    return Convert.ToString(data.GetValue(0));
                }
                return null;
            }
            catch(Exception e ){
                log.Error("getDistrito(EXCEPTION): " + e);
                throw e ;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public String getProvincia(int idDepartamento, int idProvincia)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();

                String strQuery = "SELECT nombre FROM Provincia WHERE " +
                                    "idDepartamento = @idDepartamento AND idProvincia = @idProvincia";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "@idDepartamento", idDepartamento);
                DAO.agregarParametro(objQuery, "@idProvincia", idProvincia);

                SqlDataReader data = objQuery.ExecuteReader();
                if (data.HasRows)
                {
                    data.Read();
                    return Convert.ToString(data.GetValue(0));
                }
                return null;
            }catch( Exception e )
            {
                log.Error("getProvincia(EXCEPTION): " + e);
                throw e ;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }

        }

        public String getDepartamento(int idDepartamento)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();

                String strQuery = "SELECT nombre FROM Departamento WHERE idDepartamento = @idDepartamento";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "@idDepartamento", idDepartamento);

                SqlDataReader data = objQuery.ExecuteReader();
                if (data.HasRows)
                {
                    data.Read();
                    return Convert.ToString(data.GetValue(0));
                }
                return null;
            }
            catch (Exception e) {
                log.Error("getDepartamento(EXCEPTION): " + e);
                throw e ;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public String getNombrePerfilUsuario(int idPerfil)
        {
            try
            {
                PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
                return perfilFac.getPerfil(idPerfil).nombre;
            }
            catch(Exception e ) {
                log.Error("getNombrePerfil(EXCEPTION): " + e);
                throw e ;
            }
        }

        public void registrarUsuario(UsuarioBean usuario)
        {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Insert into Usuario ( idPerfilUsuario , user_account , pass , nombres , apPat , apMat , email , celular , tipoDocumento , nroDocumento , razonSocial , estado , direccion , idDistrito , idProvincia , idDepartamento ) values (" +
                                    usuario.idPerfilUsuario + ", '" +
                                    usuario.user_account + "', '" +
                                    usuario.pass + "', '" +
                                    usuario.nombres + "', '" +
                                    usuario.apPat + "', '" +
                                    usuario.apMat + "', '" +
                                    usuario.email + "', '" +
                                    usuario.celular + "', '" +
                                    usuario.tipoDocumento + "', '" +
                                    usuario.nroDocumento + "', '" +
                                    usuario.razonSocial + "', '" +
                                    usuario.estado + "', '" +
                                    usuario.direccion + "', " +
                                    usuario.idDistrito + ", " +
                                    usuario.idProvincia + ", " +
                                    usuario.idDepartamento + ") ";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception e) {
                log.Error("registrarUsuario(EXCEPTION): " + e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public void actualizarUsuario(UsuarioBean usuario)
        {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Update Usuario SET idPerfilUsuario = @idPerfilUsuario , " +
                                    "nombres = @nombres , " +
                                    "apPat = @apPat , " +
                                    "apMat = @apMat , " +
                                    "email = @email , " +
                                    "celular = @celular , " +
                                    "tipoDocumento = @tipoDocumento , " +
                                    "nroDocumento = @nroDocumento , " +
                                    "razonSocial = @razonSocial , " +
                                    "idDistrito = @idDistrito , " +
                                    "idProvincia = @idProvincia , " +
                                    "idDepartamento = @idDepartamento " +
                                    "WHERE idUsuario = @idUsuario";
                SqlCommand query = new SqlCommand(command, sql);

                Utils.agregarParametro(query, "@idPerfilUsuario", usuario.idPerfilUsuario);
                Utils.agregarParametro(query, "@nombres", usuario.nombres);
                Utils.agregarParametro(query, "@apPat", usuario.apPat);
                Utils.agregarParametro(query, "@apMat", usuario.apMat);
                Utils.agregarParametro(query, "@email", usuario.email);
                Utils.agregarParametro(query, "@celular", usuario.celular);
                Utils.agregarParametro(query, "@tipoDocumento", usuario.tipoDocumento);
                Utils.agregarParametro(query, "@nroDocumento", usuario.nroDocumento);
                Utils.agregarParametro(query, "@razonSocial", usuario.razonSocial);
                Utils.agregarParametro(query, "@idUsuario", usuario.ID);
                Utils.agregarParametro(query, "@idDistrito", usuario.idDistrito);
                Utils.agregarParametro(query, "@idProvincia", usuario.idProvincia);
                Utils.agregarParametro(query, "@idDepartamento", usuario.idDepartamento);

                query.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception e) {
                log.Error("actualizarUsuario(EXCEPTION): " + e);
                throw e;
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public void eliminarUsuario(int id)
        {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Update Usuario SET estado = 'INACTIVO' WHERE idUsuario = @idUsuario";

                SqlCommand query = new SqlCommand(command, sql);

                Utils.agregarParametro(query, "idUsuario", id);

                query.ExecuteNonQuery();

                sql.Close();
            }
            catch( Exception e ) {
                log.Error("eliminar(EXCEPTION): " + e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public List<UsuarioBean> listarUsuarios()
        {
            SqlConnection sql = null;
            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from Usuario";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<UsuarioBean> lista = new List<UsuarioBean>();
                
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        UsuarioBean usuario = new UsuarioBean();

                        usuario.ID = Convert.ToInt32(data["idUsuario"]);
                        usuario.idPerfilUsuario = Convert.ToInt32(data["idPerfilUsuario"]);
                        usuario.user_account = Convert.ToString(data["user_account"]);
                        usuario.pass = Convert.ToString(data["pass"]);
                        usuario.nombres = Convert.ToString(data["nombres"]);
                        usuario.apPat = Convert.ToString(data["apPat"]);
                        usuario.apMat = Convert.ToString(data["apMat"]);
                        usuario.email = Convert.ToString(data["email"]);
                        usuario.celular = Convert.ToString(data["celular"]);
                        usuario.tipoDocumento = Convert.ToString(data["tipoDocumento"]);
                        usuario.nroDocumento = Convert.ToString(data["nroDocumento"]);
                        usuario.razonSocial = Convert.ToString(data["razonSocial"]);
                        usuario.estado = Convert.ToString(data["estado"]);
                        usuario.direccion = Convert.ToString(data["direccion"]);
                        usuario.idDistrito = Convert.ToInt32(data["idDistrito"]);
                        usuario.idProvincia = Convert.ToInt32(data["idProvincia"]);
                        usuario.idDepartamento = Convert.ToInt32(data["idDepartamento"]);

                        usuario.nombrePerfilUsuario = this.getNombrePerfilUsuario(Convert.ToInt32(data["idPerfilUsuario"]));

                        int idDistrito = Convert.ToInt32(data["idDistrito"]);
                        int idProvincia = Convert.ToInt32(data["idProvincia"]);
                        int idDepartamento = Convert.ToInt32(data["idDepartamento"]);

                        usuario.nombreDistrito = this.getDistrito(idDepartamento, idProvincia, idDistrito);
                        usuario.nombreProvincia = this.getProvincia(idDepartamento, idProvincia);
                        usuario.nombreDepartamento = this.getDepartamento(idDepartamento);

                        lista.Add(usuario);
                    }
                }

                return lista;
            }
            catch (Exception e) {
                log.Error("listarUsuarios(EXCEPTION): " + e);
                throw e ;
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public List<UsuarioBean> buscarUsuario(string account , string nombre, string apPat, string apMat , string tipoDocumento , string nroDocumento ) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from Usuario";
                String mount = "";
                if (!nombre.Equals(""))
                {
                    mount += " nombres = '" + nombre + "'";
                }
                if (!account.Equals(""))
                {
                    if (mount.Length > 0) mount += " and";
                    mount += " user_account = '" + account + "'";
                }
                if (!apPat.Equals(""))
                {
                    if (mount.Length > 0) mount += " and";
                    mount += " apPat = '" + apPat + "'";
                }
                if (!apMat.Equals(""))
                {
                    if (mount.Length > 0) mount += " and";
                    mount += " apMat = '" + apMat + "'";
                }
                if (!tipoDocumento.Equals(""))
                {
                    if (mount.Length > 0) mount += " and";
                    mount += " tipoDocumento = '" + tipoDocumento + "'";
                }
                if (!nroDocumento.Equals(""))
                {
                    if (mount.Length > 0) mount += " and";
                    mount += " nroDocumento = '" + nroDocumento + "'";
                }
                if (mount.Length > 0)
                {
                    command += " where";
                    command += mount;
                }

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<UsuarioBean> lista = new List<UsuarioBean>();

                while (data.Read())
                {
                    UsuarioBean usuario = new UsuarioBean();

                    usuario.ID = Convert.ToInt32(data["idUsuario"]);
                    usuario.nombrePerfilUsuario = this.getNombrePerfilUsuario(Convert.ToInt32(data["idPerfilUsuario"]));
                    usuario.user_account = Convert.ToString(data["user_account"]);
                    usuario.pass = Convert.ToString(data["pass"]);
                    usuario.nombres = Convert.ToString(data["nombres"]);
                    usuario.apPat = Convert.ToString(data["apPat"]);
                    usuario.apMat = Convert.ToString(data["apMat"]);
                    usuario.email = Convert.ToString(data["email"]);
                    usuario.celular = Convert.ToString(data["celular"]);
                    usuario.tipoDocumento = Convert.ToString(data["tipoDocumento"]);
                    usuario.nroDocumento = Convert.ToString(data["nroDocumento"]);
                    usuario.razonSocial = Convert.ToString(data["razonSocial"]);
                    usuario.estado = Convert.ToString(data["estado"]);
                    usuario.direccion = Convert.ToString(data["direccion"]);

                    int idDistrito = Convert.ToInt32(data["idDistrito"]);
                    int idProvincia = Convert.ToInt32(data["idProvincia"]);
                    int idDepartamento = Convert.ToInt32(data["idDepartamento"]);

                    usuario.nombreDistrito = this.getDistrito(idDepartamento, idProvincia, idDistrito);
                    usuario.nombreProvincia = this.getProvincia(idDepartamento, idProvincia);
                    usuario.nombreDepartamento = this.getDepartamento(idDepartamento);

                    lista.Add(usuario);
                }

                sql.Close();

                return lista;
            }
            catch (Exception e) {
                log.Error("buscarUsuario(EXCEPTION): " + e);
                throw e ;
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public bool yaExisteUsuario(string user_account)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT * FROM Usuario WHERE user_account = @user_account";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@user_account", user_account);

                SqlDataReader objReader = objQuery.ExecuteReader();
                return objReader.HasRows;
            }
            catch (Exception ex)
            {
                log.Error("yaExisteUsuario(EXCEPTION): ", ex);
                throw ex;
            }
            finally
            {
                if (objDB != null) objDB.Close();
            }
        }
    }
}