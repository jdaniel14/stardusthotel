﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/* 
 * Orden de compra(registrar, buscar)        2 iteraccion
 * Asignar productos a almacen               2 iteraccion
 * Recepcion de producto(notas de entrada)   2 iteraccion
 * Reporte de compras                        3 iteraccion     

 */
namespace Stardust.Models
{
    public class ComprasFacade
    {

        ProductoService produservice = new ProductoService();
        OrdenCompraService ordenCompraService = new OrdenCompraService();

        /**----- Orden de compra(registrar, buscar)  2----------*/
        public Producto GetProducto(int id)
        {
            return ordenCompraService.GetProducto(id);
        }

        public void GuardarOrdenCompra(OrdenProducto producto)
        {
            ordenCompraService.GuardarOrdenCompra(producto);
        }

        public List<OrdenCompraBean> buscarOrdenes(string nombre1, string fecha1, string fecha2)
        {
            return ordenCompraService.getordencompra(nombre1, fecha1, fecha2);
        }

        public OrdenCompraBean buscarOrdenes(int idorden)
        {
            return ordenCompraService.buscarordencompra(idorden);
        }

        public void modificarestadoordencompra(int idordencompra, string estado)
        {
            ordenCompraService.Guardarestadoordencompra(idordencompra, estado);
        }

        /**------ Asignar productos a almacen  2 -----*/
        public string RegistrarproductosxAlmacen( ProductoXAlmacenBean prod)
        {
            return(produservice.AsignarProductosxalmacen( prod));
        }
        public ProductoXAlmacenBean obtenerlistadAlmacen(int id)
        {
            return produservice.obtenerlistaalmacen(id);
        }
        public string  modificarproductosxalmacen( ProductoXAlmacenBean prod)
        {
            return (produservice.Modificarproductosxalmacen(prod));
        }

        public int obteneralmacen(int id)
        {
            return produservice.obteneralmacen(id);
        }

        /** ---------Recepcion de producto(notas de entrada) 2 --------*/

        public List<NotaEntradaBean> listarnotasentrada(int ordencompra)
        {
            return ordenCompraService.listarnotas(ordencompra);
        }

        public void guardarnotaentrada(NotaEntradaBean nota, string estado)
        {
            ordenCompraService.guardarnotaentrada(nota, estado);
        }

        public List<Notaentrada> obtenernotas(int idgui){

            return (ordenCompraService.obtenernotas(idgui));
        }

        public void actualizarstock(NotaEntradaBean nota)
        {
            ordenCompraService.actualizarstock(nota);
        }

        public string actualizarStock(ProductoXAlmacenBean prod)
        {
            return (produservice.actualizarStock(prod));
        }

       

    }
}