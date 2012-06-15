using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class CheckInBean
    {
        public int idTipHab { get; set; }
        public int nroPers { get; set; }
        public List <ReservaHabitacionBean> listHab {get; set;}
    }
}