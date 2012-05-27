using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class OrdenCompraService
    {
        OrdenCompraDAO ordenCompraDAO = new OrdenCompraDAO();

        public IEnumerable<Producto> GetProducto(int id)
        {
            return ordenCompraDAO.GetProducto(id);
        }


    }
}