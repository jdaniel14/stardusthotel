using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        public string nombres { get; set; }

        public string user_1 { get; set; }

        public string pass { get; set; }

        public string apPat { get; set; }

        public string apMat { get; set; }

        public string dni { get; set; }

        public string pasaporte { get; set; }

        public string direccion { get; set; }

        public string email { get; set; }

        public string ruc { get; set; }

        public string telefono { get; set; }

        public string celular { get; set; }

        public string razonSocial { get; set; }

        public string estado { get; set; }
    }
}