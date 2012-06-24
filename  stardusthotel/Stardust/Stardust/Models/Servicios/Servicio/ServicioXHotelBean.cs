using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class ServicioXHotelBean
    {
        [Display(Name = "Hotel")]
        public String hotel { get; set; }
        public int idhotel { get; set; }
        public List<ServicioHotel> listServHot { get; set; }
    }
}