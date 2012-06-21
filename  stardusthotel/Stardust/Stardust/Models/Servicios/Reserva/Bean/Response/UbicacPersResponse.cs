using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class UbicacPersResponse
    {
        public String me { get; set; }
        public List<UbicacionPersonaBean> lista { get; set; }
    }
}