using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorFacade
    {
        ProveedorService proveedorService = new ProveedorService();
        
        public List<ProveedorBean> ListarProveedor(String razonSocial, String contacto)
        {
            return proveedorService.ListarProveedor(razonSocial, contacto);
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
        public void RegistrarproductosxProveedor(int idprove, ProductoxProveedorBean prod)
        {
            proveedorService.AsignarProductosxProveedor(idprove, prod);
        }
        public ProductoxProveedorBean obtenerlista(int id)
        {
            return proveedorService.obtenerlista(id);
        }
        public void ModificarproductosxProveedor(int idprove, ProductoxProveedorBean prod)
        {
            proveedorService.ModificarproductosxProveedor(idprove, prod);
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