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

        public string Registrarproducto(ProductoBean producto)
        {
            return(productoDAO.RegistrarProducto(producto));
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



         /* -----asignar productos a almacen------*/

        public void AsignarProductosxalmacen( ProductoXAlmacenBean prod)
        {

            productoDAO.InsertaralmacenxProducto(prod);
        }
        public ProductoXAlmacenBean obtenerlistaalmacen(int idalmacen)
        {
            return productoDAO.obtenerlistaproductos(idalmacen);
        }
        public void Modificarproductosxalmacen( ProductoXAlmacenBean prod)
        {
            productoDAO.Actualizarproductosxalmacen( prod);

        }

        public int obteneralmacen(int id)
        {
            return productoDAO.obteneralmacen(id);
        }
        
        /*--------------------actualizar stock--------------*/
        public void actualizarStock(ProductoXAlmacenBean prod)
        {
            productoDAO.actualizarStock(prod);
        }
    }
}