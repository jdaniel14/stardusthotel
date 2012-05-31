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
        public IEnumerable<Producto> GetProducto(int id)
        {
            //return ordenCompraService.GetProducto(id);
            var model = new List<Producto>();
            return model;
        }

        /**------ Asignar productos a almacen  2 -----*/
        public void RegistrarproductosxAlmacen( ProductoXAlmacenBean prod)
        {
            produservice.AsignarProductosxalmacen( prod);
        }
        public ProductoXAlmacenBean obtenerlistadAlmacen(int id)
        {
            return produservice.obtenerlistaalmacen(id);
        }
        public void modificarproductosxalmacen( ProductoXAlmacenBean prod)
        {
            produservice.Modificarproductosxalmacen(prod);
        }

        public int obteneralmacen(int id)
        {
            return produservice.obteneralmacen(id);
        }

        /** ---------Recepcion de producto(notas de entrada) 2 --------*/


        /**----------- Reporte de compras 3------------*/

    }
}