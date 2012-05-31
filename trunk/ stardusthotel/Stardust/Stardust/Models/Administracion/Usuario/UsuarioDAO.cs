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
            usuario.direccion = (string)data.GetValue(14);

            int idDistrito = (int)data.GetValue(13);
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
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select nombre from Distrito where idDepartamento = " + idDepartamento + " AND idProvincia = " + idProvincia + " AND idDistrito = " + idDistrito;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            data.Read();

            String distrito = (string)data.GetValue(0);

            sql.Close();

            return distrito;
        }

        public String getProvincia(int idDepartamento, int idProvincia)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select nombre from Provincia where idDepartamento = " + idDepartamento + " AND idProvincia = " + idProvincia;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            data.Read();

            String provincia = (string)data.GetValue(0);

            sql.Close();

            return provincia;
        }

        public String getDepartamento(int idDepartamento)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select nombre from Departamento where idDepartamento = " + idDepartamento;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            data.Read();

            String departamento = (string)data.GetValue(0);

            sql.Close();

            return departamento;
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

            int idDistrito = 22;
            int idProvincia = 1;
            int idDepartamento = 15;

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
                                idDistrito + ", " +
                                idProvincia + ", " +
                                idDepartamento + ") ";
            //usuario.idDistrito + ", " +
            //usuario.idProvincia + ", " +
            //usuario.idDepartamento + ") ";

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
                                "razonSocial = '" + usuario.razonSocial + "', " +
                                "estado = '" + usuario.estado + "', " +
                                "idDistrito = 22 , idProvincia = 1 , idDepartamento = 15 " +
                                "WHERE idUsuario = " + usuario.ID;

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
    }
}