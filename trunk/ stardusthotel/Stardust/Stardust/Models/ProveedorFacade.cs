using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorFacade
    {
        ProveedorService proveedorService = new ProveedorService();
        
        public List<ProveedorBean> ListarProveedores(String razonSocial)
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

        public ProveedorBean GetProveedor(int id)
        {
            return proveedorService.GetProveedor(id);
        }

        public String EliminarServicio(int id)
        {
            return proveedorService.EliminarProveedor(id);
        }

        /*----------producto----------------*/
        
        public string registrarproducto(ProductoBean prod)
        {
            gestorproducto = new ProductoServicio();
            string resp = gestorproducto.registrarProducto(prod);
            return resp;
        }

        public string BuscarProducto(string nombre)
        {
            gestorproducto = new ProductoServicio();
            ProductoBean prod = gestorproducto.buscarproducto(nombre);
            string cadena = "";
            return cadena;
        }

        public string EliminarProducto(ProductoBean prod)
        {
            gestorproducto = new ProductoServicio();
            string res = gestorproducto.eliminarProducto(prod);
            return res;
        }

        public string Asignarproductoxalmacen ( List<ProductoBean> prod, Almacen almacen)
        {
            gestorproducto = new ProductoServicio();
            string res = gestorproducto.asignarproductosxalmacen(prod, almacen);
            return res;
        }
        public List<ProveedorBean> listar() 
        {
            gestorproveedor = new ProveedorService();
            return gestorproveedor.Listar();
            
        }


    }
}