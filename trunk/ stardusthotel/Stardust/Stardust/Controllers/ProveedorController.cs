using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Stardust.Controllers
{
    public class ProveedorController : Controller
    {
        //
        // GET: /Proveedores/
        private CadenaHotelDB db = new CadenaHotelDB();
        public ViewResult Index()
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor("");
            return View(listaProveedor);
        }

        [HttpPost]
        public ViewResult Index(List<ProveedorBean> listaProveedor)
        {
            return View(listaProveedor);
        }

        public ActionResult RegistrarProveedor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarProveedor(ProveedorBean proveedor)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.RegistrarProveedor(proveedor);
            return RedirectToAction("../Home/Index");

        }

        public ActionResult ModificarProveedor(int idProveedor)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            ProveedorBean item = proveedorFacade.GetProveedor(idProveedor);
            return View(item);
        }

        [HttpPost]
        public ActionResult ModificarProveedor(ProveedorBean item)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.ActualizarProveedor(item);
            return RedirectToAction("MostrarProveedores");
        }

        public ActionResult EliminarProveedor(int idProveedor)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            proveedorFacade.EliminarProveedor(idProveedor);
            return RedirectToAction("BuscarProveedor");
        }

        public ActionResult BuscarProveedor(string razon,string contacto)
        {
            List<Proveedor> listaProveedor = new List<Proveedor>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "";

            ViewBag.resp = "";
            if ((String.IsNullOrEmpty(razon)) && (String.IsNullOrEmpty(contacto)))
            {
                commandString = "SELECT * FROM Proveedor WHERE estado = 1 ";
            }
            else
                if ((!String.IsNullOrEmpty(razon)) && (String.IsNullOrEmpty(contacto)))
                {                
                    commandString = "SELECT * FROM Proveedor WHERE estado = 1 AND UPPER(razonSocial) LIKE '%" + razon.ToUpper() + "%'";
                    ViewBag.resp += "1";
                }
                else
                {
                    if ((String.IsNullOrEmpty(razon)) && (!String.IsNullOrEmpty(contacto)))
                    {
                        commandString = "SELECT * FROM Proveedor WHERE estado = 1 AND UPPER(contacto) LIKE '%" + contacto.ToUpper() + "%'";
                        ViewBag.resp += "1";
                    }
                    else
                    {
                        commandString = "SELECT * FROM Proveedor WHERE estado = 1 AND UPPER(razonSocial) LIKE '%" + razon.ToUpper() + "%' AND UPPER(contacto) LIKE '%"+ contacto.ToUpper()+"%'";
                        ViewBag.resp += "1";
                    }
                }

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                Proveedor proveedor = new Proveedor();
                proveedor.idProveedor = (int)dataReader["idProveedor"];
                proveedor.razonSocial = (string)dataReader["razonSocial"];
                proveedor.contacto = (string)dataReader["contacto"];
                proveedor.estado = Convert.ToInt32(dataReader["estado"]);
                proveedor.emailContacto = (string)dataReader["emailContacto"];
                proveedor.cargoContacto = (string)dataReader["cargoContacto"];
                proveedor.ruc = (string)dataReader["ruc"];
                proveedor.web = (string)dataReader["web"];
                proveedor.telefono = (string)dataReader["telefono"];
                proveedor.direccion = (string)dataReader["direccion"];
                proveedor.Observaciones = (string)dataReader["observaciones"];

                listaProveedor.Add(proveedor);
            }
            return View(listaProveedor);
        }

        public ActionResult MostrarProveedores(ProveedorBean item)
        {
            ProveedorFacade proveedorFacade = new ProveedorFacade();
            List<ProveedorBean> listaProveedor = proveedorFacade.ListarProveedor(item.razonSocial);
            return View(listaProveedor);
        }
    }
}

