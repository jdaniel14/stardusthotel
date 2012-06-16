using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class CheckInBean
    {
        public String me{get; set;}
        public String doc{get; set;}
        public String nomb{get; set;}
        public String fechaReg{get; set;}
        public String fechaLleg{get; set;}
        public List<TipHabCheckInBean> lista { get; set; } 
    }
}