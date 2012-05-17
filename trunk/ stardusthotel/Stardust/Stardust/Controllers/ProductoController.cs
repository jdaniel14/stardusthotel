using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stardust.Models;
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
                return RedirectToAction("Buscar");//("../Home/Index");
            }
            catch (Exception e)
            {
                ViewBag.lol = e.Message;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Productos WHERE idProducto = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            dataReader.Read();

            Producto producto = new Producto();
            producto.ID = (int)dataReader["idProducto"];
            producto.nombre = (string)dataReader["nombre"];
            producto.descripcion = (string)dataReader["descripcion"];
            producto.estado = Convert.ToInt32(dataReader["estado"]);
            sqlCon.Close();

            return View(producto);
        }

        [HttpPost]
        public ActionResult Edit(Producto producto)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Productos " +
                                    "SET nombre = '" + producto.nombre + "', descripcion = '" + producto.descripcion + "' " +
                                    "WHERE idProducto = " + producto.ID;  

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();

            return RedirectToAction("Buscar");
        }

        public ActionResult Delete(int id)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "SELECT * FROM Productos WHERE idProducto = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();

            dataReader.Read();

            Producto producto = new Producto();
            producto.ID = (int)dataReader["idProducto"];
            producto.nombre = (string)dataReader["nombre"];
            producto.descripcion = (string)dataReader["descripcion"];
            producto.estado = Convert.ToInt32(dataReader["estado"]);
            sqlCon.Close();

            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();

            string commandString = "UPDATE Productos " +
                                    "SET estado = 0 WHERE idProducto = " + id;

            SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
            sqlCmd.ExecuteNonQuery();

            sqlCon.Close();
            return RedirectToAction("Buscar");
        }

        public ActionResult Buscar(string nombre)
        {
            List<Producto> listaProducto = new List<Producto>();

            String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

            SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
            sqlCon.Open();                       

            string commandString = "";

            ViewBag.resp = "";
            if (!String.IsNullOrEmpty(nombre))
            {
                commandString = "Select * FROM Productos WHERE estado = 1 AND UPPER(nombre) = '" + nombre.ToUpper() + "'";
                ViewBag.resp += "1";
            }
            else
            {
                commandString = "SELECT * FROM Productos WHERE estado = 1 ";
            }

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
    }
}
