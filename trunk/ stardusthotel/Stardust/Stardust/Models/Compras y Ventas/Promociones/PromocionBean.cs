using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
namespace Stardust.Models
{
    public class PromocionBean
    {

        public int ID { get; set; }

        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Tipo de Descuento")]
        public int tipoDescuento { get; set; }

        [Display(Name = "Porcentaje de Descuento")]
        public int porcDescontar { get; set; }

        [Display(Name = "Hotel")]
        public int idhotel { get; set; }

        public int estado { get; set; }
    }
}