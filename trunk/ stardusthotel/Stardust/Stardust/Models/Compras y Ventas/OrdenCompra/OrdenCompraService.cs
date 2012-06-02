using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class OrdenCompraService
    {
        OrdenCompraDAO ordenCompraDAO = new OrdenCompraDAO();

        public Producto GetProducto(int id)
        {
            return ordenCompraDAO.GetProducto(id);
        }

        public void GuardarOrdenCompra(OrdenProducto producto)
        {
            ordenCompraDAO.GuardarOrdenCompra(producto);
        }
        public List<OrdenCompraBean> getordencompra(string nombre1, string fecha1, string fecha2){
        
            return ordenCompraDAO.getlista(nombre1, fecha1,fecha2);
        }
        public OrdenCompraBean buscarordencompra(int idordenCompra)
        {
            return ordenCompraDAO.buscarordencompra(idordenCompra);
        }
        //notas de entrada

        public List<NotaEntradaBean> listarnotas(int idorden)
        {
            return ordenCompraDAO.ListarNotasEntradas(idorden);
        }



    }
}