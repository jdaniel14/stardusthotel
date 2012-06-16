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

        public string Actualizarproducto(ProductoBean producto)
        {
            return (productoDAO.ActualizarProducto(producto));
        }

        public ProductoBean Getproducto(int idproducto)
        {
            return productoDAO.GetProducto(idproducto);
        }

        public string Eliminarproducto(int idproducto)
        {
            return (productoDAO.EliminarProducto(idproducto));
        }



         /* -----asignar productos a almacen------*/

        public string AsignarProductosxalmacen( ProductoXAlmacenBean prod)
        {

            return (productoDAO.InsertaralmacenxProducto(prod));
        }
        public ProductoXAlmacenBean obtenerlistaalmacen(int idalmacen)
        {
            return productoDAO.obtenerlistaproductos(idalmacen);
        }
        public string Modificarproductosxalmacen( ProductoXAlmacenBean prod)
        {
            return(productoDAO.Actualizarproductosxalmacen( prod));

        }

        public int obteneralmacen(int id) 
        {
            return productoDAO.obteneralmacen(id); //-1 error de conexion
        }
        
        /*--------------------actualizar stock--------------*/
        public string actualizarStock(ProductoXAlmacenBean prod)
        {
            return (productoDAO.actualizarStock(prod));
        }
    }
}