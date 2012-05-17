using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorFacade
    {
        ProveedorServicio gestorproveedor;
        ProductoServicio gestorproducto;

        public string registrarproveedor(Proveedor prov)
        {
            gestorproveedor=new ProveedorServicio();
            string resp= gestorproveedor.registrarProveedor(prov);
            return resp;
        }
        
        public string BuscarProveedor(string razonsocial, string contacto)
        {
            gestorproveedor = new ProveedorServicio();
            Proveedor prov = gestorproveedor.buscarproveedor(razonsocial, contacto);
            string cadena = "";
            return cadena;
        }

        public string EliminarProveedor(Proveedor prov)
        {
            gestorproveedor = new ProveedorServicio();
            string res = gestorproveedor.eliminarProveedor(prov);
            return res;
        }

        public string Asignarproveedorxproducto(Proveedor prov, List<Producto> prod)
        {
            gestorproveedor = new ProveedorServicio();
            string res=gestorproveedor.asignarproductosxproveedor(prov, prod);
            return res;
        }

        /*----------producto----------------*/
        
        public string registrarproducto(Producto prod)
        {
            gestorproducto = new ProductoServicio();
            string resp = gestorproducto.registrarProducto(prod);
            return resp;
        }

        public string BuscarProducto(string nombre)
        {
            gestorproducto = new ProductoServicio();
            Producto prod = gestorproducto.buscarproducto(nombre);
            string cadena = "";
            return cadena;
        }

        public string EliminarProducto(Producto prod)
        {
            gestorproducto = new ProductoServicio();
            string res = gestorproducto.eliminarProducto(prod);
            return res;
        }

        public string Asignarproductoxalmacen ( List<Producto> prod, Almacen almacen)
        {
            gestorproducto = new ProductoServicio();
            string res = gestorproducto.asignarproductosxalmacen(prod, almacen);
            return res;
        }
        public List<Proveedor> listar() 
        {
            gestorproveedor = new ProveedorServicio();
            return gestorproveedor.Listar();
            
        }


    }
}