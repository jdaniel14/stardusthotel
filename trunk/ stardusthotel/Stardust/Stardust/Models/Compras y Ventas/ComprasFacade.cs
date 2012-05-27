using System;
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

        /**----- Orden de compra(registrar, buscar)  2----------*/

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