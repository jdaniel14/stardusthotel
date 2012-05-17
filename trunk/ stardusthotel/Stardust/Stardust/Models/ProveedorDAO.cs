using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorDAO
    {
        public int insertarProveedor(Proveedor prov)
        {
            return 1;
        }
        public /*List<Proveedor>*/ void buscarProveedor(string razon_social, string contacto)
        {
           // string res = "";
            //return 0;
        }
        public int eliminarProveedor(Proveedor prov)
        {
            return 1;
        }
        public List<Proveedor> listar()
        {
            List<Proveedor> lista = null ;
            Proveedor prov = new Proveedor();
            lista.Add(prov);
           
            return lista;
        }


    }
}