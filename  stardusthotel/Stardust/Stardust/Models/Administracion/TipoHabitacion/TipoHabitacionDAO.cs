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
    public class TipoHabitacionDAO
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        private static ILog log = LogManager.GetLogger(typeof(TipoHabitacionDAO));

        public TipoHabitacionBean getTipoHabitacion(int id) {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);
                TipoHabitacionBean tipoHabitacion = null;

                objDB.Open();
                String strQuery = "SELECT * FROM TipoHabitacion WHERE idTipoHabitacion = @idTipoHabitacion";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "idTipoHabitacion", id);

                SqlDataReader objDataReader = objQuery.ExecuteReader();

                if (objDataReader.HasRows)
                {
                    objDataReader.Read();
                    tipoHabitacion = new TipoHabitacionBean();

                    tipoHabitacion.ID = Convert.ToInt32(objDataReader["idTipoHabitacion"]);
                    tipoHabitacion.nombre = Convert.ToString(objDataReader["nombre"]);
                    tipoHabitacion.descripcion = Convert.ToString(objDataReader["descripcion"]);
                }

                return tipoHabitacion;
            }
            catch (Exception e)
            {
                log.Error("getTipoHabitacion(EXCEPTION): " + e);
                throw e;
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public IEnumerable<TipoHabitacionBean> getTipoHabitacionXHotel(int idHotel)
        {
            SqlConnection objDB = null;

            try
            {
                objDB = new SqlConnection(cadenaDB);
                List<TipoHabitacionBean> listaTipoHabitacion = new List<TipoHabitacionBean>();

                objDB.Open();
                String strQuery = "SELECT A.* " +
                                    "FROM TipoHabitacion A, TipoHabitacionXHotel B " +
                                    "WHERE A.idTipoHabitacion = B.idTipoHabitacion and B.idHotel = @idHotel";
                SqlCommand objQuery = new SqlCommand(strQuery, objDB);
                DAO.agregarParametro(objQuery, "idHotel", idHotel);

                SqlDataReader objDataReader = objQuery.ExecuteReader();
                if (objDataReader.HasRows)
                {
                    while (objDataReader.Read())
                    {
                        TipoHabitacionBean tipoHabitacion = new TipoHabitacionBean();

                        tipoHabitacion.ID = Convert.ToInt32(objDataReader["idTipoHabitacion"]);
                        tipoHabitacion.nombre = Convert.ToString(objDataReader["nombre"]);
                        tipoHabitacion.descripcion = Convert.ToString(objDataReader["descripcion"]);

                        listaTipoHabitacion.Add(tipoHabitacion);
                    }
                }
                // en caso de no leer tipos de habitaciones, devolverla una lista vacia
                return listaTipoHabitacion;
            }
            catch (Exception e) {
                log.Error("getTipoHabitacionXHotel(EXCEPTION): " + e);
                throw (e);
            }
            finally
            {
                if (objDB != null)
                {
                    objDB.Close();
                }
            }
        }

        public void registrarTipoHabitacion(TipoHabitacionBean tipo) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Insert into TipoHabitacion ( nombre , descripcion ) values ('" +
                                    tipo.nombre + "', '" +
                                    tipo.descripcion + "')";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception e) {
                log.Error("registrarTipoHabitacion(EXCEPTION): " + e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public void actualizarTipoHabitacion(TipoHabitacionBean tipo) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Update TipoHabitacion SET " +
                                    "nombre = '" + tipo.nombre + "', " +
                                    "descripcion = '" + tipo.descripcion + "' " +
                                    "where idTipoHabitacion = " + tipo.ID;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception e) {
                log.Error("actualizarTipoHabitacion(EXCEPTION): " + e);               
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public void eliminarTipoHabitacion(int id) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Delete from TipoHabitacion where idTipoHabitacion = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

                sql.Close();
            }
            catch (Exception e) {
                log.Error("eliminarTipoHabitacion(EXCEPTION): " + e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public List<TipoHabitacionBean> listarTipoHabitacion() {
            SqlConnection sql = null;
            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                String command = "Select * from TipoHabitacion";
                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<TipoHabitacionBean> lista = new List<TipoHabitacionBean>();

                while (data.Read())
                {
                    TipoHabitacionBean tipo = new TipoHabitacionBean();

                    tipo.ID = (int)data.GetValue(0);
                    tipo.nombre = (string)data.GetValue(1);
                    tipo.descripcion = (string)data.GetValue(2);

                    lista.Add(tipo);
                }

                sql.Close();

                return lista;
            }
            catch (Exception e) {
                log.Error("listarTipoHabitacion(EXCEPTION): " + e);
                throw (e);
            }
            finally {
                if (sql != null) sql.Close();
            }
        }
    }
}