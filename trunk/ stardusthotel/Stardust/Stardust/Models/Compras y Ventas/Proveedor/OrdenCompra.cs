using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class OrdenCompraDetalle
    {
        public int id { get; set; }
        public int idOC { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public int preciouni { get; set; }
        public string nombre { get; set; }
        public string precio1 { get; set; }
    }
    public class OrdenCompra
    {
        public int id { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        public decimal precio { get; set; }
        public int idProv { get; set; }
    }
    public class OrdenCompras
    {
        public int id { get; set; }        
        public string nombre { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        [Display(Name = "Interes (%)")]
        public int interes { get; set; }
        [Display(Name = "Numero de Coutas")]
        public int numCuotas { get; set; }
        [Display(Name = "Ingrese el monto")]
        public decimal pagado { get; set; }
        [Display(Name = "Monto pagado")]
        public decimal paga { get; set; }
        [Display(Name = "Monto a Pagar")]
        public decimal pagar { get; set; }
        public string estado { get; set; }
        public string pagado1 { get; set; }
        public string paga1 { get; set; }
        public string pagar1 { get; set; }
        public string subtotal1 { get; set; }
        public string igv1 { get; set; }
        public string total1 { get; set; }
        public string interes1 { get; set; }
        public string numCuotas1 { get; set; }
        public List<OrdenCompra> listaOC { get; set; }
        public List<OrdenCompraDetalle> listaOCDetalle { get; set; }

        public OrdenCompras()
        {
            listaOC = new List<OrdenCompra>();
            listaOCDetalle = new List<OrdenCompraDetalle>();
        }
    }
}