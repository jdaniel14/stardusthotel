using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class Provincia
    {
        public int ID { get; set; }
        public string nombre { get; set; }
        public int idDepartamento { get; set; }
    }
}