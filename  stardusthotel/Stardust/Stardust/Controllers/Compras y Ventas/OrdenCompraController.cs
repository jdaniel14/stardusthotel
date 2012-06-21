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

            List<OrdenCompraBean> orden= comprasFacade.buscarOrdenes(nombre, fecha1, fecha2);

            for (int i = 0; i < orden.Count; i++)
            {
                if (orden[i].estado == "Tramite" || orden[i].estado == "Cancelado") orden[i].estado2 = true;
                else orden[i].estado2 = false;

            }
            return View(orden);
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

        public ViewResult Registrar2(int id, int idhotel) //registrar orden compra.......idproveedor y id hotel
        {
            OrdenProducto prod = new OrdenProducto();
            ProveedorBean prov = proveedorFacade.GetProveedor(id);
            

            ProductoxProveedorBean productosprov = proveedorFacade.obtenerlista(id); // de la tabla productoxpreoveedor
            int idalmacen = comprasFacade.obteneralmacen(idhotel);
            ProductoXAlmacenBean productosalmacen = comprasFacade.obtenerlistadAlmacen(idalmacen); // de la tabla productoxalmacen


            List<Producto> produ = new List<Producto>();

            for (int i = 0; i < productosalmacen.listProdalmacen.Count; i++)
            {
                for (int j = 0; j < productosprov.listProdProv.Count; j++)
                {
                    if (productosalmacen.listProdalmacen[i].ID == productosprov.listProdProv[j].ID)
                    {
                        Producto produc = new Producto();
                        produc.idproducto = productosalmacen.listProdalmacen[i].ID;
                        produc.Nombre = proveedorFacade.GetNombreProducto(productosalmacen.listProdalmacen[i].ID);
                        produc.precio = productosprov.listProdProv[j].precio;
                        produc.stockActual = productosalmacen.listProdalmacen[i].stockactual;
                        produc.stockMinimo = productosalmacen.listProdalmacen[i].stockminimo;
                        produc.stockMaximo = productosalmacen.listProdalmacen[i].stockmaximo;
                        if (produc.stockActual <= produc.stockMinimo) { produc.estado = true; }
                        else { produc.estado = false; }
                        produ.Add(produc);
                    }
                    
                }
            }
            
            prod.listaProducto = produ;
            prod.proveedor = prov.razonSocial;
            prod.id = id;//idproveedor
            prod.idhotel = idhotel;
            HotelBean hotel = hoteles.getHotel(idhotel);
            prod.nombrehotel = hotel.nombre;
            return View(prod);
        }

        
        public ActionResult RegistrarOC(OrdenProducto producto)
        {
            
            for (int i = 0; i < producto.listaProducto.Count; i++)
            {
                if (producto.listaProducto[i].cantidad > 0)
                {
                    producto.listaProducto[i].estadoguardar = true;
                    
                }
                else
                {
                    producto.listaProducto[i].estadoguardar = false;
                }
            }

            comprasFacade.GuardarOrdenCompra(producto);
            return RedirectToAction("Buscar");
        }

        public ActionResult DetalleOrdenC(int id) //id orden compra
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

            List<NotaEntradaBean> notas = comprasFacade.listarnotasentrada(id);
            for (int i = 0; i < notas.Count; i++)
            {
                int idguiaremision = notas[i].idguiaRemision;
                List<Notaentrada> not2 = comprasFacade.obtenernotas(idguiaremision);
                for (int j=0; j < not2.Count; j++)
                {
                    for (int k = 0; k < ordencompra.detalle.Count(); k++)
                    {
                        if (ordencompra.detalle[k].ID == not2[j].ID)
                        {
                            ordencompra.detalle[k].Cantidadentrante+=not2[j].cantidadrecibida;
                        }
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
                        ordencompra.detalle[i].preciounitario =  productos.listProdProv[j].precio;
                    }
                }
            }

            return View(ordencompra);

        }


        public ActionResult GuardarestadoOrdenC(OrdenCompraBean orden) 
        {
            // guarda el estado de la orden de compra a registrado o cancelado
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
            
            notaentrada.idhotel=ordencompra.idhotel;
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
                            notaentrada.detallenotaentrada[k].cantidadrecibida += detallenotaentrada[j].cantidadrecibida;
                        }

                    }
                    
                }
            }
            for (int i = 0; i < notaentrada.detallenotaentrada.Count; i++)
            {
                notaentrada.detallenotaentrada[i].cantidadfaltante = notaentrada.detallenotaentrada[i].cantidadsolicitada - notaentrada.detallenotaentrada[i].cantidadrecibida;
                if (notaentrada.detallenotaentrada[i].cantidadfaltante == 0)
                {
                    notaentrada.detallenotaentrada[i].estado = true;
                }
            }

           return View(notaentrada);

        }

        [HttpPost]
        public ActionResult RegistrarNotaEntrada(NotaEntradaBean not) //nueva nota de entrada
        {
            for (int i = 0; i < not.detallenotaentrada.Count; i++)
            {
                if (not.detallenotaentrada[i].cantidadentrante > not.detallenotaentrada[i].cantidadfaltante)
                {
                    ViewBag.error = "la cantidad debe ser menor a la cantidad faltante";
                    not.detallenotaentrada[i].cantidadentrante = 0;
                    return View(not);
                }
            }

            for (int i = 0; i < not.detallenotaentrada.Count; i++)
            {
                int cantidad = not.detallenotaentrada[i].cantidadentrante - not.detallenotaentrada[i].cantidadfaltante;
                if (cantidad== 0)
                {
                    not.detallenotaentrada[i].estado = true;
                }
            }

            string estado = ""; // verificar las cantidades

            Boolean estado2 = true;
            for (int i = 0; i < not.detallenotaentrada.Count; i++)
            {
                estado2 = not.detallenotaentrada[i].estado && estado2;
            }

            if (estado2) estado = "Atendido"; else estado = "Parcialmente Atendido";
            
            comprasFacade.guardarnotaentrada(not, estado);
            comprasFacade.actualizarstock(not);//.. cambiar stock de producto
            return RedirectToAction("ListarNotaEntrada/" + not.idordencompra, "OrdenCompra"); 
        }

        public ActionResult DetallenotaEntrada(int id, int id2) // idguiaremision , idordencompra
        {
            NotaEntradaBean nota = new NotaEntradaBean();
            nota.detallenotaentrada= comprasFacade.obtenernotas(id);
            nota.idguiaRemision = id;
            nota.idordencompra = id2;

            List<NotaEntradaBean> notas = comprasFacade.listarnotasentrada(id2);

            for (int i = 0; i < notas.Count; i++)
            {
                if (notas[i].idguiaRemision == id)
                {
                    nota.fechaemitida = notas[i].fechaemitida;
                }
            }

            OrdenCompraBean orden= comprasFacade.buscarOrdenes(id2);

            nota.idproveedor = orden.idproveedor;

            ProveedorBean proveedor = proveedorFacade.GetProveedor(nota.idproveedor);
            nota.nombreproveedor = proveedor.razonSocial;

            for (int i = 0; i < nota.detallenotaentrada.Count; i++)
            {
                ProductoBean producto = proveedorFacade.Getproducto(nota.detallenotaentrada[i].ID);
                nota.detallenotaentrada[i].nombre = producto.nombre;
            }
            return View(nota);

        }
        
    }
}
