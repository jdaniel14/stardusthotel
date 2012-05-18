using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProductoFacade
    {
        ProductoService productoService = new ProductoService();

        public List<ProductoBean> ListarProducto(String nombre)
        {
            return productoService.Listarproducto(nombre);
        }

        public void Registrarproducto(ProductoBean producto)
        {
            productoService.Registrarproducto(producto);
        }

        public void ActualizarProducto(ProductoBean producto)
        {
            productoService.Actualizarproducto(producto);
        }

        public ProductoBean Getproducto(int idProducto)
        {
            return productoService.Getproducto(idProducto);
        }

        public void Eliminarproducto(int idproducto)
        {
            productoService.Eliminarproducto(idproducto);
        }
    }
}