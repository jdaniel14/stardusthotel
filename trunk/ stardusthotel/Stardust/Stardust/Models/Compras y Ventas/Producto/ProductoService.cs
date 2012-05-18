using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProductoService
    {
        ProductoDAO productoDAO = new ProductoDAO();

        public List<ProductoBean> Listarproducto(String Nombre)
        {
            List<ProductoBean> listaproducto = productoDAO.ListarProducto(Nombre);
            return listaproducto;
        }

        public void Registrarproducto(ProductoBean producto)
        {
            productoDAO.RegistrarProducto(producto);
        }

        public void Actualizarproducto(ProductoBean producto)
        {
            productoDAO.ActualizarProducto(producto);
        }

        public ProductoBean Getproducto(int idproducto)
        {
            return productoDAO.GetProducto(idproducto);
        }

        public void Eliminarproducto(int idproducto)
        {
            productoDAO.EliminarProducto(idproducto);
        }
    }
}