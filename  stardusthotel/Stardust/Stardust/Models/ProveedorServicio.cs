using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorServicio
    {
        public string registrarProveedor(Proveedor prov)
        {
            ProveedorDAO proveedorDao = new ProveedorDAO();
            int res = proveedorDao.insertarProveedor(prov);
            string cadenares;
            if (res == 0) cadenares = "OK";
            else if (res == 1) cadenares = "Ya se encuentra registrado";
            else if (res == 2) cadenares = "Error en Conexion a BD";
            else cadenares = "Pantallazo Azul !!!!!!! :(";
            
            return cadenares;
        }

        public string asignarproductosxproveedor(Proveedor prov, List<Producto> prod)
        {
            string respuesta = "";

            return respuesta;
        }


        public Proveedor buscarproveedor(string razon_social, string contacto)
        {
            Proveedor prov= new Proveedor();
            
            return prov;

        }
        public string eliminarProveedor(Proveedor prov)
        {
            string respuesta = "";

            return respuesta;
        }


        


    }
}