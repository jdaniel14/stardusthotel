using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorService
    {
        ProveedorDAO proveedorDAO = new ProveedorDAO();
        
        public List<ProveedorBean> ListarProveedor(String Nombre)
        {
            List<ProveedorBean> listaProveedor = proveedorDAO.ListarProveedor(Nombre);
            return listaProveedor;
        }

        public String RegistrarProveedor(ProveedorBean proveedor)
        {
            return proveedorDAO.InsertarProveedor(proveedor);
        }

        public String ActualizarProveedor(ProveedorBean proveedor)
        {
            return proveedorDAO.ActualizarProveedor(proveedor);
        }

        public ProveedorBean GetProveedor(int idProveedor)
        {
            return proveedorDAO.SeleccionarProveedor(idProveedor);
        }

        public String EliminarProveedor(int idProveedor)
        {
            return proveedorDAO.DeleteProveedor(idProveedor);
        }
    }
}