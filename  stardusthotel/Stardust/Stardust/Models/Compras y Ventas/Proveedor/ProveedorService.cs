using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class ProveedorService
    {
        ProveedorDAO proveedorDAO = new ProveedorDAO();

        public List<ProveedorBean> ListarProveedor(String Nombre, String contacto)
        {
            return proveedorDAO.ListarProveedor(Nombre, contacto);
        }

        public String RegistrarProveedor(ProveedorBean proveedor)
        {
            return proveedorDAO.insertarProveedor(proveedor);
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

        /*productos a proveedor*/
        public void AsignarProductosxProveedor(int idprove, ProductoxProveedorBean prod)
        {
            proveedorDAO.InsertarProveedorxProducto(idprove, prod);
        }
        public ProductoxProveedorBean obtenerlista(int idproveedor)
        {
            return proveedorDAO.obtenerlistaproductos(idproveedor);
        }
        public void ModificarproductosxProveedor(int idprove, ProductoxProveedorBean prod)
        {
            proveedorDAO.ActualizarproductosxProveedor(idprove, prod);
        }

        /*Pago a proveedor*/

        public OrdenCompras ObtenerOC(int id)
        {
            return proveedorDAO.ObtenerOC(id);
        }

        public string GetNombre(int id)
        {
            return proveedorDAO.GetNombre(id);
        }
    }
}
