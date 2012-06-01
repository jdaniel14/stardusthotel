using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class Utils
    {
        String cadenaDB = WebConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

        public List<Departamento> listarDepartamentos() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Departamento";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<Departamento> lista = new List<Departamento>();
            
                while (data.Read()) {
                    Departamento dep = new Departamento();

                    dep.ID = (int)data.GetValue(0);
                    dep.nombre = (string)data.GetValue(1);

                    lista.Add(dep);
                }

            sql.Close();

            return lista;
        }

        public List<Provincia> listarProvincias(int idDepartamento) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Provincia where idDepartamento = @idDepartamento";

                SqlCommand query = new SqlCommand( command , sql ) ;

                this.agregarParametro(query, "idDepartamento", idDepartamento);

                SqlDataReader data = query.ExecuteReader();

                List<Provincia> lista = new List<Provincia>();

                while (data.Read()) {
                    Provincia prov = new Provincia();

                    prov.ID = (int)data.GetValue(1);
                    prov.nombre = (string)data.GetValue(2);

                    lista.Add(prov);
                }

            sql.Close();

            return lista;
        }

        public List<Distrito> listarDistritos(int idDepartamento, int idProvincia) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from Distrito where idDepartamento = @idDepartamento AND idProvincia = @idProvincia";

            SqlCommand query = new SqlCommand(command, sql);

            this.agregarParametro(query, "idDepartamento", idDepartamento);
            this.agregarParametro(query, "idProvincia", idProvincia);

            SqlDataReader data = query.ExecuteReader();

            List<Distrito> lista = new List<Distrito>();

            while (data.Read()) {
                Distrito distrito = new Distrito();

                distrito.ID = (int)data.GetValue(2);
                distrito.nombre = (string)data.GetValue(3);

                lista.Add(distrito);
            }

            sql.Close();

            return lista;
        }

        public void agregarParametro(SqlCommand objQuery, String nombreParametro, object valorParametro)
        {
            SqlParameter objParametro = new SqlParameter();
            objParametro.ParameterName = nombreParametro;
            objParametro.Value = valorParametro;
            objQuery.Parameters.Add(objParametro);
        }
    }
}