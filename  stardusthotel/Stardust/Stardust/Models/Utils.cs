﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using log4net;

namespace Stardust.Models
{
    public class Utils
    {
        public static String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(Utils));

        public static List<Departamento> listarDepartamentos() {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<Departamento> lstDepartamento = null;

                objDB.Open();
                String strQuery = "SELECT idDepartamento, nombre FROM Departamento";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);

                SqlDataReader objReader = objQuery.ExecuteReader();

                if (objReader.HasRows)
                {
                    lstDepartamento = new List<Departamento>();
                    while (objReader.Read())
                    {
                        Departamento departamento = new Departamento();

                        departamento.ID = Convert.ToInt32(objReader[0]);
                        departamento.nombre = Convert.ToString(objReader[1]);

                        lstDepartamento.Add(departamento);
                    }
                    return lstDepartamento;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("listarDepartamentos(EXCEPTION): ", ex);
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

        public static List<Provincia> listarProvincias(int idDepartamento) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<Provincia> lstProvincia = null;

                objDB.Open();
                String strQuery = "SELECT idProvincia, nombre FROM Provincia WHERE idDepartamento = @idDepartamento";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                agregarParametro(objQuery, "@idDepartamento", idDepartamento);

                SqlDataReader objReader = objQuery.ExecuteReader();

                if (objReader.HasRows)
                {
                    lstProvincia = new List<Provincia>();
                    while (objReader.Read())
                    {
                        Provincia provincia = new Provincia();

                        provincia.ID = Convert.ToInt32(objReader[0]);
                        provincia.nombre = Convert.ToString(objReader[1]);

                        lstProvincia.Add(provincia);
                    }
                    return lstProvincia;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("listarProvincias(EXCEPTION): ", ex);
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

        public static List<Distrito> listarDistritos(int idDepartamento, int idProvincia) {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<Distrito> lstDistrito = null;

                objDB.Open();
                String strQuery = "SELECT idDistrito, nombre FROM Distrito " +
                                    "WHERE idDepartamento = @idDepartamento AND idProvincia = @idProvincia";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                agregarParametro(objQuery, "@idDepartamento", idDepartamento);
                agregarParametro(objQuery, "@idProvincia", idProvincia);

                SqlDataReader objReader = objQuery.ExecuteReader();

                if (objReader.HasRows)
                {
                    lstDistrito = new List<Distrito>();
                    while (objReader.Read())
                    {
                        Distrito distrito = new Distrito();

                        distrito.ID = Convert.ToInt32(objReader[0]);
                        distrito.nombre = Convert.ToString(objReader[1]);

                        lstDistrito.Add(distrito);
                    }
                    return lstDistrito;
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error("listarDistritos(EXCEPTION): ", ex);
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

        public static string getNombreDepartamento(int idDepartamento)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "SELECT nombre FROM Departamento WHERE idDepartamento = @idDepartamento";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                agregarParametro(objQuery, "@idDepartamento", idDepartamento);

                SqlDataReader objReader = objQuery.ExecuteReader();
                if (objReader.HasRows)
                {
                    objReader.Read();
                    return Convert.ToString(objReader[0]);
                }
                return null;
                //return String.Empty; <-- podria ser, tal vez depende de lo que se quiera
            }
            catch (Exception ex)
            {
                log.Error("getNombreDepartamento(EXCEPTION): ", ex);
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

        public static string getNombreProvincia(int idDepartamento, int idProvincia)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "SELECT nombre FROM Provincia WHERE idDepartamento = @idDepartamento AND idProvincia = @idProvincia";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                agregarParametro(objQuery, "@idDepartamento", idDepartamento);
                agregarParametro(objQuery, "@idProvincia", idProvincia);

                SqlDataReader objReader = objQuery.ExecuteReader();
                if (objReader.HasRows)
                {
                    objReader.Read();
                    return Convert.ToString(objReader[0]);
                }
                return null;
                //return String.Empty; <-- podria ser, tal vez depende de lo que se quiera
            }
            catch (Exception ex)
            {
                log.Error("getNombreProvincia(EXCEPTION): ", ex);
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

        public static string getNombreDistrito(int idDepartamento, int idProvincia, int idDistrito)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);
                objDB.Open();

                String strQuery = "SELECT nombre FROM Distrito " + 
                                    " WHERE idDepartamento = @idDepartamento AND idProvincia = @idProvincia AND idDistrito = @idDistrito";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                agregarParametro(objQuery, "@idDepartamento", idDepartamento);
                agregarParametro(objQuery, "@idProvincia", idProvincia);
                agregarParametro(objQuery, "@idDistrito", idDistrito);

                SqlDataReader objReader = objQuery.ExecuteReader();
                if (objReader.HasRows)
                {
                    objReader.Read();
                    return Convert.ToString(objReader[0]);
                }
                return null;
                //return String.Empty; <-- podria ser, tal vez depende de lo que se quiera
            }
            catch (Exception ex)
            {
                log.Error("getNombreDistrito(EXCEPTION): ", ex);
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

        public static void agregarParametro(SqlCommand objQuery, String nombreParametro, object valorParametro)
        {
            try
            {
                SqlParameter objParametro = new SqlParameter();
                objParametro.ParameterName = nombreParametro;
                objParametro.Value = valorParametro ?? DBNull.Value;
                objQuery.Parameters.Add(objParametro);
            }
            catch (Exception ex)
            {
                log.Error("agregarParametro(EXCEPTION): ", ex);
            }
        }

        public static String DateToString(DateTime date) { 
            return date.Year + "-" +
                    (date.Month < 10 ? "0" + date.Month : "" + date.Month) + "-" +
                    (date.Day < 10 ? "0" + date.Day : "" + date.Day);
        }

        public static bool comprobarLogin(string usuario, string contrasenia)
        {
            SqlConnection objDB = null;
            try
            {
                objDB = new SqlConnection(cadenaDB);

                objDB.Open();
                String strQuery = "SELECT * FROM Usuario WHERE user_account = @usuario AND pass = @contrasenia AND estado = 'ACTIVO'";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                Utils.agregarParametro(objQuery, "@usuario", usuario);
                Utils.agregarParametro(objQuery, "@contrasenia", contrasenia);

                SqlDataReader objReader = objQuery.ExecuteReader();
                return objReader.HasRows;
            }
            catch (Exception ex)
            {
                log.Error("comprobarLogin(EXCEPTION): ", ex);
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
    }
}