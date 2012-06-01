using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class NotaEntradaBean
    {
        int idordencompra { get; set; }

        string nombreproveedor { get; set; }
        
        int idproveedor { get; set; }
        List<Notaentrada> notas { get; set; }
    }
}