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
        public int ID {get; set;}
        public string Nombre {get; set;}
    }
    public class Tipo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public Tipo(int ID, string Nombre)
        {
            this.ID = ID;
            this.Nombre = Nombre;
        }
    }

    public class PromocionBean
    {

        public int ID { get; set; }

        public int idPromocion { get; set; }
        
        public string nombre { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Es necesario ingresar una descripción")]
        public string descripcion { get; set; }

        [Display(Name = "Tipo de Descuento")]
        public int tipoDescuento { get; set; }

        public int tipo { get; set; }

        [Display(Name = "Tipo de Descuento")]
        public string descuento { get; set; }

        [Display(Name = "Hotel")]
        public string hotel { get; set; }

        [Display(Name = "Numero dias/Porc. Pago adelanto")]
        public int razon { get; set; }

        [Display(Name = "Numero de Dias")]
        [Range(1, 99, ErrorMessage = "El número mínimo de días es 1")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo números")]
        [Required(ErrorMessage = "Es necesario ingresar un número de días")]
        public int dias { get; set; }

        [Display(Name = "Porcentaje de Pago Adelantado")]
        public int adelanto { get; set;}
        [Required(ErrorMessage = "Debe ingresar el porcentaje de pago adelantado")]
        public string adelanto2{ get; set;}

        [Display(Name = "Porcentaje de Descuento")]
        public int porcDescontar { get; set; }
        [Required(ErrorMessage = "Debe ingresar el porcentaje de descuento")]
        public string porcDescontar2 { get; set; }

        [Display(Name = "Hotel")]
        public int idhotel { get; set; }

        public int estado { get; set; }        
    }
}