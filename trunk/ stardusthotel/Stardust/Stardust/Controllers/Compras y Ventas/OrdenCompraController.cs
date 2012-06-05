using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;

namespace Stardust.Controllers
{
    public class OrdenCompraController : Controller
    {
        ComprasFacade comprasFacade = new ComprasFacade();
        ProveedorFacade proveedorFacade = new ProveedorFacade();
        HotelFacade hoteles = new HotelFacade();
        
        /*--------Orden de Compra----------*/

        public ActionResult Index()
        {
            List<HotelBean> hotel = hoteles.getHoteles();    
            return View(hotel);
        }

        public ActionResult Buscar()
        {
            PagoProveedorBean pagoProveedor = new PagoProveedorBean();
            return View(pagoProveedor);
            //return View();
        }

        [HttpPost]
        public ActionResult Buscar(PagoProveedorBean pago)
        {
            int ID = Convert.ToInt32(pago.ID);
            ProveedorBean prove = proveedorFacade.GetProveedor(ID);
            string nombre = prove.razonSocial;

            return RedirectToAction("Buscar2", new { nombre = nombre });
        }


        public ActionResult Buscar2(string nombre)
        {
            string fecha1 = "";
            string fecha2 = "";
            return View(comprasFacade.buscarOrdenes(nombre, fecha1, fecha2));
        }

        public ActionResult Lista(OrdenCompraBean orden)
        {

            return View(orden);
        }


        public ActionResult Registrar(int id)
        {

            OrdenCompraBean ordenCompra = new OrdenCompraBean();
            ordenCompra.idhotel=id;
            return View(ordenCompra);
        }

        [HttpPost]
        public ActionResult Registrar(OrdenCompraBean ordenCompra)
        {
            int ID = Convert.ToInt32(ordenCompra.idProv);
            return RedirectToAction("Registrar2", new { id = ID,idhotel=ordenCompra.idhotel});
        }

        public ViewResult Registrar2(int id, int idhotel)
        {
            OrdenProducto prod = new OrdenProducto();
            ProveedorBean prov = proveedorFacade.GetProveedor(id);
            List<Producto> produ = new List<Producto>();

            ProductoxProveedorBean productos = proveedorFacade.obtenerlista(id);

            for (int i = 0; i < productos.listProdProv.Count; i++)
            {
                Producto producto = comprasFacade.GetProducto(productos.listProdProv.ElementAt(i).ID);
                if (producto != null)
                {
                    producto.precio = productos.listProdProv.ElementAt(i).precio;
                    produ.Add(producto);//lista de los productos en la tabla productoxproveedor                
                }
            }

            
            prod.listaProducto = produ;
            prod.proveedor = prov.razonSocial;
            prod.id = id;
            prod.idhotel = idhotel;
            HotelBean hotel = hoteles.getHotel(idhotel);
            prod.nombrehotel = hotel.nombre;
            //lista de productos en la tabla de productoxproveedor
            for (int i = 0; i < prod.listaProducto.Count; i++)
            {
                ProductoBean producto = proveedorFacade.Getproducto(Convert.ToInt32(prod.listaProducto[i].id));
                prod.listaProducto[i].Nombre = producto.nombre;
            }

            return View(prod);
        }

        
        public ActionResult RegistrarOC(OrdenProducto producto)
        {
            comprasFacade.GuardarOrdenCompra(producto);
            return RedirectToAction("Buscar");
        }

        public ActionResult DetalleOrdenC(int id) 
        {
            OrdenCompraBean ordencompra = comprasFacade.buscarOrdenes(id);
            ProveedorBean proveedor = proveedorFacade.GetProveedor(ordencompra.idproveedor);
            ordencompra.nombreproveedor = proveedor.razonSocial;

            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                ProductoBean produc = proveedorFacade.Getproducto(ordencompra.detalle[i].ID);
                ordencompra.detalle[i].nombre = produc.nombre;
            }
            ProductoxProveedorBean productos = proveedorFacade.obtenerlista(ordencompra.idproveedor);
            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                for (int j = 0; j < productos.listProdProv.Count; j++)
                {
                    if (ordencompra.detalle[i].ID == productos.listProdProv[j].ID)
                    {
                        ordencompra.detalle[i].preciounitario = productos.listProdProv[j].precio;
                    }
                }
            }

            return View(ordencompra);
        }

        public ActionResult ModificarOrdenC(int id) //idordencompra
        {
            OrdenCompraBean ordencompra = comprasFacade.buscarOrdenes(id);
            ProveedorBean proveedor = proveedorFacade.GetProveedor(ordencompra.idproveedor);
            ordencompra.nombreproveedor = proveedor.razonSocial;

            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                ProductoBean produc = proveedorFacade.Getproducto(ordencompra.detalle[i].ID);
                ordencompra.detalle[i].nombre = produc.nombre;
            }
            ProductoxProveedorBean productos = proveedorFacade.obtenerlista(ordencompra.idproveedor);
            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                for (int j = 0; j < productos.listProdProv.Count; j++)
                {
                    if (ordencompra.detalle[i].ID == productos.listProdProv[j].ID)
                    {
                        ordencompra.detalle[i].preciounitario = productos.listProdProv[j].precio;
                    }
                }
            }

            return View(ordencompra);

        }


        public ActionResult GuardarestadoOrdenC(OrdenCompraBean orden)
        {
            //guardar estado de la orden de compra
            comprasFacade.modificarestadoordencompra(orden.idOrdenCompra, orden.estado);
            return RedirectToAction("Buscar");
        }
        /*--------Notas de Entrada----------*/

        public ActionResult ListarNotaEntrada(int id) 
        {
            List<NotaEntradaBean> notas = comprasFacade.listarnotasentrada(id);//idordencompra

            OrdenCompraBean ordencompra = comprasFacade.buscarOrdenes(id);
            ordencompra.notasentrada = new List<NotaEntradaBean>();
            
            for (int i = 0; i < notas.Count; i++)
            {
                ordencompra.notasentrada.Add(notas[i]);
            }
            
            ProveedorBean proveedor = proveedorFacade.GetProveedor(ordencompra.idproveedor);
            ordencompra.nombreproveedor = proveedor.razonSocial;

            return View(ordencompra);
        }

        public ActionResult RegistrarNotaEntrada(int id) //idordencompra
        {
            NotaEntradaBean notaentrada = new NotaEntradaBean();
            
            OrdenCompraBean ordencompra = comprasFacade.buscarOrdenes(id);
            ProveedorBean proveedor = proveedorFacade.GetProveedor(ordencompra.idproveedor);
            ordencompra.nombreproveedor = proveedor.razonSocial;
            
            notaentrada.nombreproveedor = proveedor.razonSocial;
            notaentrada.idordencompra = id;
            notaentrada.idproveedor = ordencompra.idproveedor;
            notaentrada.fechaRegistradaOrdenCompra = ordencompra.fecha;
            notaentrada.detallenotaentrada = new List<Notaentrada>();
            notaentrada.estado = ordencompra.estado;


            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                ProductoBean produc = proveedorFacade.Getproducto(ordencompra.detalle[i].ID);
                ordencompra.detalle[i].nombre = produc.nombre;
            }

            for (int i = 0; i < ordencompra.detalle.Count; i++)
            {
                Notaentrada notas = new Notaentrada();
                notas.cantidadsolicitada = ordencompra.detalle[i].Cantidad;
                notas.ID = ordencompra.detalle[i].ID;
                notas.nombre = ordencompra.detalle[i].nombre;

                notaentrada.detallenotaentrada.Add(notas);

            }
          
            //falta cantidad faltante
            List<NotaEntradaBean> notas2 = comprasFacade.listarnotasentrada(id); // lista de notas de entrada de uan orden de compra

            for (int i = 0; i < notas2.Count; i++)
            {
                List<Notaentrada> detallenotaentrada = comprasFacade.obtenernotas(notas2[i].idguiaRemision);
                for (int j = 0; j < detallenotaentrada.Count; j++)
                {
                    for (int k=0;k<notaentrada.detallenotaentrada.Count;k++)
                    {
                        if (notaentrada.detallenotaentrada[k].ID == detallenotaentrada[j].ID) 
                        {
                            notaentrada.detallenotaentrada[k].cantidadfaltante += detallenotaentrada[j].cantidadrecibida;
                        }

                    }
                    
                }
            }

           return View(notaentrada);

        }

        public ActionResult guardarNotaentrad(NotaEntradaBean not) //nueva nota de entrada
        {

            comprasFacade.guardarnotaentrada(not);
            return RedirectToAction("Buscar");
        }

        public ActionResult DetallenotaEntrada(int id) // idguiaremision
        {

            List<Notaentrada> detallenotaentrada = comprasFacade.obtenernotas(id);
            return View(detallenotaentrada);

        }
        
    }
}
