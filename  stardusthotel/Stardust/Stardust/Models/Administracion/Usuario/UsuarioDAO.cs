using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class UsuarioDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

        public UsuarioBean getUsuario(int id)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Usuario where idUsuario = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                UsuarioBean usuario = new UsuarioBean();

                data.Read();
                usuario.ID = (int)data.GetValue(0);
                usuario.idPerfilUsuario = (int)data.GetValue(1);
                usuario.nombrePerfilUsuario = this.getNombrePerfil(usuario.idPerfilUsuario);
                usuario.user_account = (string)data.GetValue(2);
                usuario.pass = (string)data.GetValue(3);
                usuario.nombres = (string)data.GetValue(4);
                usuario.apPat = (string)data.GetValue(5);
                usuario.apMat = (string)data.GetValue(6);
                usuario.email = (string)data.GetValue(7);
                usuario.celular = (string)data.GetValue(8);
                usuario.tipoDocumento = (string)data.GetValue(9);
                usuario.nroDocumento = (string)data.GetValue(10);
                usuario.razonSocial = (string)data.GetValue(11);
                usuario.estado = (string)data.GetValue(12);
                usuario.direccion = (string)data.GetValue(13);

                int idDistrito = (int)data.GetValue(14);
                int idProvincia = (int)data.GetValue(15);
                int idDepartamento = (int)data.GetValue(16);

                usuario.nombreDistrito = this.getDistrito(idDepartamento, idProvincia, idDistrito);
                usuario.nombreProvincia = this.getProvincia(idDepartamento, idProvincia);
                usuario.nombreDepartamento = this.getDepartamento(idDepartamento);

            sql.Close();

            return usuario;
        }

        public String getDistrito(int idDepartamento, int idProvincia, int idDistrito)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();

                //String command = "SELECT nombre FROM Distrito WHERE idDepartamento = " + idDepartamento + " AND idProvincia = " + idProvincia + " AND idDistrito = " + idDistrito;
                String strQuery =    "SELECT nombre FROM Distrito WHERE " +
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
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public String getNombrePerfil(int idPerfil)
        {
            PerfilUsuarioFacade perfilFac = new PerfilUsuarioFacade();
            return perfilFac.getPerfil(idPerfil).nombre;
        }

        public void registrarUsuario(UsuarioBean usuario)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

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
                                    usuario.razonSocial + "', " +
                                    "'ACTIVO' , '" +
                                    usuario.direccion + "', " +
                                    usuario.idDistrito + ", " +
                                    usuario.idProvincia + ", " +
                                    usuario.idDepartamento + ") ";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void actualizarUsuario(UsuarioBean usuario)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update Usuario SET idPerfilUsuario = " + usuario.idPerfilUsuario +
                                    ", nombres = '" + usuario.nombres + "', " +
                                    "apPat = '" + usuario.apPat + "', " +
                                    "apMat = '" + usuario.apMat + "', " +
                                    "email = '" + usuario.email + "', " +
                                    "celular = '" + usuario.email + "', " +
                                    "tipoDocumento = '" + usuario.tipoDocumento + "', " +
                                    "nroDocumento = '" + usuario.nroDocumento + "', " +
                                    "razonSocial = '" + usuario.razonSocial + //"', " +
                                    //"estado = '" + usuario.estado + "' " +
                                    " WHERE idUsuario = " + usuario.ID;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void eliminarUsuario(int id)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update Usuario SET estado = 'INACTIVO' WHERE idUsuario = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public List<UsuarioBean> listarUsuarios()
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Usuario";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<UsuarioBean> lista = new List<UsuarioBean>();

                while (data.Read())
                {
                    UsuarioBean usuario = new UsuarioBean();

                    usuario.ID = (int)data.GetValue(0);
                    usuario.nombrePerfilUsuario = this.getNombrePerfil((int)data.GetValue(1));
                    usuario.user_account = (string)data.GetValue(2);
                    usuario.pass = (string)data.GetValue(3);
                    usuario.nombres = (string)data.GetValue(4);
                    usuario.apPat = (string)data.GetValue(5);
                    usuario.apMat = (string)data.GetValue(6);
                    usuario.email = (string)data.GetValue(7);
                    usuario.celular = (string)data.GetValue(8);
                    usuario.tipoDocumento = (string)data.GetValue(9);
                    usuario.nroDocumento = (string)data.GetValue(10);
                    usuario.razonSocial = (string)data.GetValue(11);
                    usuario.estado = (string)data.GetValue(12);
                    usuario.direccion = (string)data.GetValue(14);

                    int idDistrito = (int)data.GetValue(13);
                    int idProvincia = (int)data.GetValue(15);
                    int idDepartamento = (int)data.GetValue(16);

                    usuario.nombreDistrito = this.getDistrito(idDepartamento, idProvincia, idDistrito);
                    usuario.nombreProvincia = this.getProvincia(idDepartamento, idProvincia);
                    usuario.nombreDepartamento = this.getDepartamento(idDepartamento);

                    lista.Add(usuario);
                }

                sql.Close();

            return lista;
        }

        public List<UsuarioBean> buscarUsuario(string nombre, string apPat, string apMat) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from Usuario";
            String mount = "";
            if (!nombre.Equals("")){
                mount += " nombres = '" + nombre + "'";
            }
            if (!apPat.Equals("")){
                if (mount.Length > 0) mount += " and";
                mount += " apPat = '" + apPat + "'";
            }
            if (!apMat.Equals("")){
                if (mount.Length > 0) mount += " and";
                mount += " apMat = '" + apMat + "'" ;
            }
            if (mount.Length > 0) {
                command += " where";
                command += mount;
            }

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            List<UsuarioBean> lista = new List<UsuarioBean>();

            while (data.Read()) {
                UsuarioBean usuario = new UsuarioBean();
                usuario.ID = (int)data.GetValue(0);
                usuario.idPerfilUsuario = (int)data.GetValue(1);
                usuario.nombrePerfilUsuario = this.getNombrePerfil(usuario.idPerfilUsuario);
                usuario.user_account = (string)data.GetValue(2);
                usuario.pass = (string)data.GetValue(3);
                usuario.nombres = (string)data.GetValue(4);
                usuario.apPat = (string)data.GetValue(5);
                usuario.apMat = (string)data.GetValue(6);
                usuario.email = (string)data.GetValue(7);
                usuario.celular = (string)data.GetValue(8);
                usuario.tipoDocumento = (string)data.GetValue(9);
                usuario.nroDocumento = (string)data.GetValue(10);
                usuario.razonSocial = (string)data.GetValue(11);
                usuario.estado = (string)data.GetValue(12);
                usuario.direccion = (string)data.GetValue(14);

                int idDistrito = (int)data.GetValue(13);
                int idProvincia = (int)data.GetValue(15);
                int idDepartamento = (int)data.GetValue(16);

                usuario.nombreDistrito = this.getDistrito(idDepartamento, idProvincia, idDistrito);
                usuario.nombreProvincia = this.getProvincia(idDepartamento, idProvincia);
                usuario.nombreDepartamento = this.getDepartamento(idDepartamento);

                lista.Add(usuario);
            }

            sql.Close();

            return lista;
        }
    }
}