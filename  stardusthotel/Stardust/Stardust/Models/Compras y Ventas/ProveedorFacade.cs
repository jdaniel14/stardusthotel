using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorFacade
    {
        ProveedorService proveedorService = new ProveedorService();
        
        public List<ProveedorBean> ListarProveedor(String razonSocial)
        {
            return proveedorService.ListarProveedor(razonSocial);
        }

        public String RegistrarProveedor(ProveedorBean proveedor)
        {
            return proveedorService.RegistrarProveedor(proveedor);
        }

        public String ActualizarProveedor(ProveedorBean proveedor)
        {
            return proveedorService.ActualizarProveedor(proveedor);
        }

        public ProveedorBean GetProveedor(int idProveedor)
        {
            return proveedorService.GetProveedor(idProveedor);
        }

        public String EliminarProveedor(int idProveedor)
        {
            return proveedorService.EliminarProveedor(idProveedor);
        }

        /*----------producto----------------*/

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