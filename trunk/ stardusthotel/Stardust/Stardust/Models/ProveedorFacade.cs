using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorFacade
    {
        ProveedorServicio gestorproveedor;
        public string registrarproveedor(Proveedor prov)
        {
            gestorproveedor=new ProveedorServicio;
            string resp= gestorproveedor.registrarProveedor(prov);
            return resp;
        }
    }
}