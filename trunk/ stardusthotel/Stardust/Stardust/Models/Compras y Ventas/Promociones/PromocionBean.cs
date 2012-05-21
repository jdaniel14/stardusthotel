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
    public class Hoteles
    {
        public string ID {get; set;}
        public string Nombre {get; set;}
    }
    public class PromocionBean
    {

        public string ID { get; set; }

        public string nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Tipo de Descuento")]
        public int tipoDescuento { get; set; }

        public int razon { get; set; }

        [Display(Name = "Numero de Dias")]
        public int dias { get; set; }

        [Display(Name = "Porcentaje de Pago Adelantado")]
        public int adelanto { get; set;}

        [Display(Name = "Porcentaje de Descuento")]
        public int porcDescontar { get; set; }

        [Display(Name = "Hotel")]
        public int idhotel { get; set; }

        public int estado { get; set; }

        public IEnumerable<Hoteles> getHoteles()
        {
            List<Hoteles> listaHotel = new List<Hoteles>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Hotel";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                Hoteles hotel = new Hoteles();
                hotel.ID = Convert.ToString(dataReader["idHotel"]);
                hotel.Nombre = (string)dataReader["nombre"];

                listaHotel.Add(hotel);
            }
            return listaHotel;
        }
        public SelectList hotelList { get; set; }

        public PromocionBean()
        {
            hotelList = new SelectList(getHoteles(), "ID", "Nombre");
        }
    }
}