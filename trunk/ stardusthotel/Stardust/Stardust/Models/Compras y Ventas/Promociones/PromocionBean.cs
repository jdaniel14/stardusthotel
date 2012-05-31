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
    public class Tipo
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public Tipo(string ID, string Nombre)
        {
            this.ID = ID;
            this.Nombre = Nombre;
        }
    }

    public class PromocionBean
    {

        public string ID { get; set; }

        public int idPromocion { get; set; }

        public string nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Tipo de Descuento")]
        public int tipoDescuento { get; set; }

        public string tipo { get; set; }

        [Display(Name = "Tipo de Descuento")]
        public string descuento { get; set; }

        [Display(Name = "Hotel")]
        public string hotel { get; set; }

        [Display(Name = "Numero dias/Porcentaje Pago adelanto")]
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

        public IEnumerable<Tipo> getTipo(int i)
        {
            List<Tipo> listaTipo = new List<Tipo>();

            if (i == 1)
            {
                Tipo tipo = new Tipo("1", "Todo");
                listaTipo.Add(tipo);
            }

            Tipo tipo1 = new Tipo("2", "Dias de Reserva");
            Tipo tipo2 = new Tipo("3", "Pago Adelantado");
            
            listaTipo.Add(tipo1);
            listaTipo.Add(tipo2);

            return listaTipo;
        }

        public IEnumerable<Hoteles> getHoteles(int i)
        {
            List<Hoteles> listaHotel = new List<Hoteles>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Hotel";

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            if (i == 1)
            {
                Hoteles hoteles = new Hoteles();
                hoteles.ID = "1";
                hoteles.Nombre = "Todo";
                listaHotel.Add(hoteles);
            }                    

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
        public SelectList tipoList { get; set; }

        public PromocionBean()
        {
        }

        public PromocionBean(int i)
        {
            if (i == 1)
            {
                hotelList = new SelectList(getHoteles(1), "ID", "Nombre");
                tipoList = new SelectList(getTipo(1), "ID", "Nombre");
            }
            else
            {
                hotelList = new SelectList(getHoteles(2), "ID", "Nombre");
                tipoList = new SelectList(getTipo(2), "ID", "Nombre");
            }
        }
    }
}