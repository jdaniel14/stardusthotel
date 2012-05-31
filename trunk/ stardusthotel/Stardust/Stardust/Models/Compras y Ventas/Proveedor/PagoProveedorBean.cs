using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;

namespace Stardust.Models
{
    public class Proveedores
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
    }

    public class PagoProveedorBean
    {
        public string ID { get; set; }
        public int idPago { get; set; }

        public IEnumerable<Proveedores> getProveedores()
        {
            List<Proveedores> listaProveedor = new List<Proveedores>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Proveedor";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            /*if (i == 1)
            {
                Proveedores proveedores = new Proveedores();
                proveedores.ID = "1";
                proveedores.Nombre = "Todo";
                listaProveedor.Add(proveedores);
            }*/                   

            while (dataReader.Read())
            {
                Proveedores proveedor = new Proveedores();
                proveedor.ID = Convert.ToString(dataReader["idProveedor"]);
                proveedor.Nombre = (string)dataReader["nombre"];
                listaProveedor.Add(proveedor);
            }
            return listaProveedor;
        }
        public SelectList proveedorList { get; set; }

        public PagoProveedorBean()
        {
            proveedorList = new SelectList(getProveedores(), "ID", "Nombre");
        }
    }
}
