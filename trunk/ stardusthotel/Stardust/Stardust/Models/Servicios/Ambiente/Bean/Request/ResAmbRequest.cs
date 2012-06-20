using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ResAmbRequest
    {
        public String me { get; set; }
        public int cantDias { get; set; }
        public List<AmbienteBean> listaAmbientes { get; set; }
    }
}