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
    public class Proveedor
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
    }
    
    public class OrdenCompraBean
    {
        public int idOrdenCompra { get; set; }
        public int idproveedor { get; set; }

        [Display(Name = "Proveedor")]
        public string idProv { get; set; } 
        
        [Display(Name = "Proveedor")]
        public string nombreproveedor { get; set; }

        [Display(Name = "Producto")]
        public string idProd { get; set; } 
        
        [Display(Name = "Estado")]
        public string estado { get; set; }
        
        [Display(Name = "Fecha")]
        public string fecha { get; set; }

        [Display(Name = "Precio Total")]
        public decimal preciototal { get; set; }

        List<DetalleOrdenCompra> detalles { get; set; }

        public IEnumerable<Proveedor> getProveedor() 
        {
            List<Proveedor> listaProveedor = new List<Proveedor>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Proveedor";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                Proveedor proveedor = new Proveedor();
                proveedor.ID = Convert.ToString(dataReader["idProveedor"]);
                proveedor.Nombre = (string)dataReader["razonSocial"];

                listaProveedor.Add(proveedor);
            }

            return listaProveedor;
        }

        public List<Producto> productoList { get; set; }

        public SelectList proveedorList { get; set; }
        
        public List<DetalleOrdenCompra> detalle { get; set; }

        public OrdenCompraBean()
        {
            proveedorList = new SelectList(getProveedor(), "ID", "Nombre");
        }
    }
}