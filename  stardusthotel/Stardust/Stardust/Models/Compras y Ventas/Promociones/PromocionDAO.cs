using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using Stardust.Controllers;
using log4net;



namespace Stardust.Models
{
    public class PromocionDAO
    {
        private static ILog log = LogManager.GetLogger(typeof(HotelController));
        
        public void RegistrarPromocion(PromocionBean promocion)
        {
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                promocion.estado = 1;

                string commandString = "INSERT INTO Promocion (descripcion, razon, procDescontar,estado,tipo,idHotel) VALUES ('" + promocion.descripcion + "', " + promocion.razon + " ," + promocion.porcDescontar + ", " + promocion.estado + " , " + promocion.tipoDescuento + " ," + promocion.idhotel + ")";

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                log.Error("RegistrarPromocion", ex);

            }
        }

        public List<PromocionBean> ListarPromocion(int id,int hotel)
        {
            List<PromocionBean> listaPromocion = new List<PromocionBean>();
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "";

                commandString = "SELECT * FROM Promocion WHERE estado = 1";

                if (hotel >= 1)
                    commandString += " AND idHotel = " + hotel;
                if (id > 1)
                {
                    id--;
                    commandString += " AND tipo = " + id;
                }

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    PromocionBean promociones = new PromocionBean();

                    promociones.idPromocion = (int)dataReader["idPromocion"];
                    promociones.descripcion = (string)dataReader["descripcion"];
                    promociones.razon = (int)dataReader["razon"];
                    promociones.porcDescontar = (int)dataReader["porcDescontar"];
                    promociones.tipoDescuento = (int)dataReader["tipo"];
                    promociones.idhotel = (int)dataReader["idHotel"];
                    promociones.estado = 1;

                    listaPromocion.Add(promociones);
                }
                dataReader.Close();
                sqlCon.Close();

                
            }
            catch (Exception ex)
            {
                log.Error("listarPromocion", ex);

            }
            return listaPromocion;
        }

        public PromocionBean GetPromocion(int id)
        {
            PromocionBean promocion = new PromocionBean();
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "SELECT * FROM Promocion WHERE idPromocion = " + id;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                dataReader.Read();

                promocion.idPromocion = (int)dataReader["idPromocion"];
                promocion.descripcion = (string)dataReader["descripcion"];
                promocion.razon = (int)dataReader["razon"];
                promocion.porcDescontar = (int)dataReader["porcDescontar"];
                promocion.tipo = (int)dataReader["tipo"];
                promocion.idhotel = (int)dataReader["idHotel"];

                if (promocion.tipo == 1)
                    promocion.descuento = "Numero de Dias de Reserva";
                else
                    promocion.descuento = "Porcentaje Pago de Adelanto";

                List<Hoteles> pro = getHoteles(2);

                for (int i = 0; i < pro.Count(); i++)
                {
                    int ID = Convert.ToInt32(pro.ElementAt(i).ID);
                    if (ID == promocion.idhotel)
                        promocion.hotel = pro.ElementAt(i).Nombre;
                }
                sqlCon.Close();

                
            }
            catch (Exception ex)
            {
                log.Error("getpromocion", ex);

            }
            return promocion;
        }

        public void ActualizarPromocion(PromocionBean promocion)
        {
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                promocion.idhotel = Convert.ToInt32(promocion.ID);

                string commandString = "UPDATE Promocion " +
                                        "SET descripcion = '" + promocion.descripcion + "', razon = " + promocion.razon + " , porcDescontar = " + promocion.porcDescontar;

                if (promocion.tipoDescuento > 1)
                {
                    promocion.tipoDescuento--;
                    commandString += " , tipo = " + promocion.tipoDescuento;
                }

                if (promocion.idhotel >= 1)
                    commandString += " , idHotel = " + promocion.idhotel;

                commandString += " WHERE idPromocion = " + promocion.idPromocion;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                log.Error("actualizarpromocion", ex);

            }
        }

        public void EliminarPromocion(int id)
        {
            try
            {
                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "UPDATE Promocion " +
                                        "SET estado = 0 WHERE idPromocion = " + id;

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                sqlCmd.ExecuteNonQuery();

                sqlCon.Close();
            }
            catch (Exception ex)
            {
                log.Error("deletepromocion", ex);

            }

        }

        public List<Tipo> getTipo(int i)
        {
            List<Tipo> listaTipo = new List<Tipo>();

            try
            {
                if (i == 1)
                {
                    Tipo tipo = new Tipo(1, "Todo");
                    listaTipo.Add(tipo);
                }

                Tipo tipo1 = new Tipo(2, "Dias de Reserva");
                Tipo tipo2 = new Tipo(3, "Pago Adelantado");

                listaTipo.Add(tipo1);
                listaTipo.Add(tipo2);
            }
            catch (Exception ex)
            {
                log.Error("gettipo(EXCEPTION): ", ex);
                throw ex;
            }
            return listaTipo;
        }

        public List<Hoteles> getHoteles(int i)
        {
            try
            {
                List<Hoteles> listaHotel = new List<Hoteles>();

                String cadenaConfiguracion = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;

                SqlConnection sqlCon = new SqlConnection(cadenaConfiguracion);
                sqlCon.Open();

                string commandString = "SELECT * FROM Hotel";

                SqlCommand sqlCmd = new SqlCommand(commandString, sqlCon);
                SqlDataReader dataReader = sqlCmd.ExecuteReader();

                if (i == 1)
                {
                    Hoteles hoteles = new Hoteles();
                    hoteles.ID = 0;
                    hoteles.Nombre = "Todo";
                    listaHotel.Add(hoteles);
                }

                while (dataReader.Read())
                {
                    Hoteles hotel = new Hoteles();
                    hotel.ID = (int)dataReader["idHotel"];
                    hotel.Nombre = (string)dataReader["nombre"];

                    listaHotel.Add(hotel);
                }
                return listaHotel;
            }
            catch (Exception ex)
            {
                log.Error("gethoteles(EXCEPTION): ", ex);
                throw ex;
            }
        }
    }
}