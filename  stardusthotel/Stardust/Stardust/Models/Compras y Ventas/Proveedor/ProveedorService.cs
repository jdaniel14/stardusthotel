﻿using System;
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

        public ProveedorList ListarProveedor2()
        {
            return proveedorDAO.ListarProveedor2();
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

        public Boolean existeproveedor(string razonsocial)
        {
            return proveedorDAO.existeproveedor(razonsocial);
        }

        public Boolean existeproveedor2(string ruc)
        {
            return proveedorDAO.existeproveedor2(ruc);
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

        public OrdenCompras ListarOC(int id)
        {
            return proveedorDAO.ListarOC(id);
        }

        public void RegistrarPagoContado(OrdenCompras OC)
        {
            proveedorDAO.RegistrarPagoContado(OC);
        }

        public void RegistrarPagoCredito(OrdenCompras OC)
        {
            proveedorDAO.RegistrarPagoCredito(OC);
        }

        public List<Proveedors> GetList()
        {
            return proveedorDAO.GetList();
        }

        public OrdenCompras GetOC(int id)
        {
            return proveedorDAO.GetOC(id);
        }

        public string GetNombreProducto(int id)
        {
            return proveedorDAO.GetNombreProducto(id);
        }
    }
}
