using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class TipHabCheckInBean
    {
        public String nombTipHab { get; set; }
        public int nroPers { get; set; }
        public List <HabitacBean> lista{get; set;}
    }
}