using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;

namespace Stardust.Controllers
{
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/
        private CadenaHotelDB db = new CadenaHotelDB();

        public ViewResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Producto producto)
        {
            try
            {
                producto.estado = 1;
                string sql = "Insert into Productos ( nombre , descripcion, estado ) values ( {0} , {1} , {2} )";
                 db.Database.ExecuteSqlCommand(sql, 
                                                producto.nombre, 
                                                producto.descripcion, 
                                                producto.estado
                                                );
                db.SaveChanges();
                return RedirectToAction("../Home/Index");
            }
            catch (Exception e)
            {
                ViewBag.lol = e.Message;
                return View();
            }
        }

        public ActionResult Control()
        {
            //var ViewData.Model = db.Producto.ToList;
            //var model = from m in db.Productos select m;
            List<Producto> listaProducto = new List<Producto>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Productos ";
            //bool result = Nombre.Equals("");
            //if (!result) commandString = commandString + "WHERE UPPER(nombre) LIKE '%" + Nombre.ToUpper() + "%'";
            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            while (dataReader.Read())
            {
                Producto producto = new Producto();
                producto.ID = (int)dataReader["idProducto"];
                producto.nombre = (string)dataReader["nombre"];
                producto.descripcion = (string)dataReader["descripcion"];
                producto.estado = Convert.ToInt32(dataReader["estado"]);

                listaProducto.Add(producto);
            }
            return View(listaProducto);
        }

        public ActionResult Buscar() 
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(string nombre) {
            db.Productos.Find(nombre);
            if (nombre != "") 
            {

            }
            ViewData["nombre"]=nombre;
           
            return View();
        }
    }
}
