﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 
 * administrar Productos(buscar, registrar y eliminar)  1 iteraccion
 * Administrar Proveedor(buscar, registrar y eliminar)  1 iteraccion
 * Asignar productos a Proveedor                        1 iteraccion
 * Registrar pago a proveedor                           2 iteraccion
 * Registrar datos de la factura                        3 iteraccion

 */
namespace Stardust.Models
{
    public class ProveedorFacade
    {
        
        /*---------Administrar Proveedor-1--------*/
        ProveedorService proveedorService = new ProveedorService();
        ProductoService productoService = new ProductoService();

        public List<ProveedorBean> ListarProveedor(String razonSocial, String contacto)
        {
            return proveedorService.ListarProveedor(razonSocial, contacto);
        }

        public ProveedorList ListarProveedor2()
        {
            return proveedorService.ListarProveedor2();
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

        public Boolean existeproveedor(string razonsocial)
        {
            return proveedorService.existeproveedor(razonsocial);
        }

        public Boolean existeproveedor2(string ruc)
        {
            return proveedorService.existeproveedor2(ruc);
        }

        /*--------Administrar Producto-1------------*/

        

        public List<ProductoBean> ListarProducto(String nombre)
        {
            return productoService.Listarproducto(nombre);
        }

        public string Registrarproducto(ProductoBean producto)
        {
            return (productoService.Registrarproducto(producto));
        }

        public string ActualizarProducto(ProductoBean producto)
        {
            return (productoService.Actualizarproducto(producto));
        }

        public ProductoBean Getproducto(int idProducto)
        {
            return productoService.Getproducto(idProducto);
        }

        public string Eliminarproducto(int idproducto)
        {
            return(productoService.Eliminarproducto(idproducto));
        }


        /*-----Asignar Productos a Proveedor-1------*/

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
        
        

        /*-----Registrar Pago del Proveedor-2------*/

        public OrdenCompras ObtenerOC(int id)
        {
            return proveedorService.ObtenerOC(id);
        }

        public string GetNombre(int id)
        {
            return proveedorService.GetNombre(id);
        }

        public OrdenCompras ListarOC(int id)
        {
            return proveedorService.ListarOC(id);
        }

        public void RegistrarPagoContado(OrdenCompras OC)
        {
            proveedorService.RegistrarPagoContado(OC);
        }

        public void RegistrarPagoCredito(OrdenCompras OC)
        {
            proveedorService.RegistrarPagoCredito(OC);
        }

        public List<Proveedors> GetList()
        {
            return proveedorService.GetList();
        }

        public OrdenCompras GetOC(int id)
        {
            return proveedorService.GetOC(id);
        }

        public string GetNombreProducto(int id)
        {
            return proveedorService.GetNombreProducto(id);
        }

        /*-----------------------------*/

    }
}