using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ListaServiciosResponseBean
    {
        public List<ServiciosBean> lista { get; set; }
        public String me { get; set; }
    }
}