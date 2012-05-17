﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class ServiciosDAO
    {
        public List<ServiciosBean> ListarServicios( String Nombre) {

            List<ServiciosBean> listaServicios = new List<ServiciosBean>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["PruebaDBContext"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Servicio";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
                        
            while (dataReader.Read())
            {
                ServiciosBean servicio = new ServiciosBean();
                servicio.id = (int)dataReader["nombre"];
                servicio.nombre = (string)dataReader["nombre"];
                servicio.descripcion = (string)dataReader["descripcion"];

                listaServicios.Add(servicio);
            }
            dataReader.Close();
            sqlCon.Close();

            return listaServicios;
        }
    }
}