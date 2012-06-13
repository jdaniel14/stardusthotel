using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Stardust.Models
{
    public class EmpleadoDAO
    {
        String cadenaDB = ConfigurationManager.ConnectionStrings["CadenaHotelDB"].ConnectionString;
        
        public EmpleadoBean getEmpleado(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Empleado where idEmpleado = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                EmpleadoBean empleado = new EmpleadoBean();

                 
                empleado.ID = (int)data.GetValue(0);

                UsuarioBean usuario = new UsuarioFacade().getUsuario(empleado.ID);
                empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;   

                empleado.fechaIngreso = (DateTime)data.GetValue(1);
                //empleado.fechaSalida = (DateTime)data.GetValue(2);
                empleado.estado = (string)data.GetValue(3);

            sql.Close();

            return empleado;
        }

        public void registrarEmpleado(EmpleadoBean empleado) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            DateTime date = empleado.fechaIngreso;

            String fechaIngreso = date.Date.ToShortDateString() ;

            String command = "Insert into Empleado ( idEmpleado , fechaIngreso , estado) values ( "
                                + empleado.ID + ", '"
                                + fechaIngreso + "', "
                                + "'ACTIVO' )";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public void modificarEmpleado(EmpleadoBean empleado) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Update Empleado SET "
                                    + "fechaSalida = '" + empleado.fechaSalida
                                    + "', estado = '" + empleado.estado
                                    + "' WHERE idEmpleado = " + empleado.ID;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();

        }

        public void eliminarEmpleado(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                DateTime date = new DateTime();

                String fechaSalida = date.Date.ToShortDateString();

                String command = "Update Empleado SET estado = 'INACTIVO' , fechaSalida = '" + fechaSalida + 
                        "' WHERE idEmpleado = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public List<EmpleadoBean> listarEmpleados() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Empleado ORDER BY fechaIngreso";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<EmpleadoBean> lista = new List<EmpleadoBean>();
            
                while (data.Read()) {
                    EmpleadoBean empleado = new EmpleadoBean();

                    empleado.ID = (int)data.GetValue(0);
                    UsuarioBean usuario = new UsuarioFacade().getUsuario( empleado.ID ) ;
                    empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;
                    empleado.fechaIngreso = (DateTime)data.GetValue(1);
                    //var aux = data.GetValue(2);
                    //if (aux != null)
                    //    empleado.fechaSalida = (DateTime)aux;
                    // NO FUNCIONA U_U
                    empleado.estado = (string)data.GetValue(3);

                    lista.Add(empleado);
                }

            sql.Close();

            return lista;



        }

        //horarios

        public void asignarhorario(horario horario) {


            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            DateTime dateini = horario.fechainiciohorario;
            DateTime datefin = horario.fechafinhorario;
            String fechaIngreso = dateini.Date.ToShortDateString();
            String fechaSalida = datefin.Date.ToShortDateString();
            

            String command = "Insert into Horario (  fechaIni , fechaFin, idEmpleado) values ( '"
                               
                               + horario.fechainiciohorario + "',  '"
                                + horario.fechafinhorario + "', "
                                 + horario.idempleado+  ")";
                              

            SqlCommand query = new SqlCommand(command, sql);

            query.ExecuteNonQuery();

            sql.Close();
        
        }


        public horario gethorario(int id)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from Horario where idHorario = " + id;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            data.Read();

            horario h = new horario();

            h.ID = (int)data.GetValue(0);
            h.fechainiciohorario = (DateTime)data.GetValue(1);
            
            h.fechafinhorario = (DateTime)data.GetValue(2);
            h.idempleado = (int)data.GetValue(3);
            
              EmpleadoBean empleado = new EmpleadoFacade().getEmpleado(h.idempleado);
              h.nombreEmpleado = empleado.nombreEmpleado;

           // UsuarioBean usuario = new UsuarioFacade().getUsuario(h.idempleado);
          //  empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;

           

            sql.Close();

            return h;
        }


        public void modificarHorario(horario h)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();
            String fechaini=h.fechainiciohorario.Year +"-"+h.fechafinhorario.Month+"-"+h.fechainiciohorario.Day;
            String fechafin = h.fechafinhorario.Year + "-" + h.fechafinhorario.Month + "-" + h.fechafinhorario.Day;
           // String a = h.fechainiciohorario;

            String command = "Update Horario SET "
                                + "fechaIni = '" + h.fechainiciohorario
                                + "', fechaFin = '" + h.fechafinhorario
                //+ "' WHERE idEmpleado = " + h.idempleado
                //+" AND idHorario=" + h.ID;
                                + "' WHERE idHorario = " + h.ID;
            SqlCommand query = new SqlCommand(command, sql);

            query.ExecuteNonQuery();

            sql.Close();

        }

        public void eliminarHorario(int id)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            DateTime date = new DateTime();

            String fechaSalida = date.Date.ToShortDateString();

            String command = "Update Empleado SET estado = 'INACTIVO' , fechaSalida = '" + fechaSalida +
                    "' WHERE idEmpleado = " + id;

            SqlCommand query = new SqlCommand(command, sql);

            query.ExecuteNonQuery();

            sql.Close();
        }

        public List<horario> listarHorario(int id)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from Horario  ORDER BY idHorario";
           
            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            List<horario> lista = new List<horario>();

            while (data.Read())
            {
                horario horar = new horario();

                horar.ID = (int)data.GetValue(0);
                            
            
                horar.fechainiciohorario = (DateTime)data.GetValue(1);
                horar.fechafinhorario = (DateTime)data.GetValue(2);
                horar.idempleado = (int)data.GetValue(3);

                EmpleadoBean empleado = new EmpleadoFacade().getEmpleado(horar.idempleado);
                horar.nombreEmpleado = empleado.nombreEmpleado;
                lista.Add(horar);
            }

            sql.Close();

            return lista;



        }


        //detalle
        /*

        public void asignarhorario(horariodetalle horariod)
        {


            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            DateTime dateini = horariod.horasentrada.;
            DateTime datefin = horariod.fechafinhorario;
            String fechaIngreso = dateini.Date.ToShortDateString();
            String fechaSalida = datefin.Date.ToShortDateString();


            String command = "Insert into Horario ( idHorario , fechaIni , fechaFin, idEmpleado) values ( "
                               + horario.ID + ", '"
                               + horario.fechainiciohorario + "',  '"
                                + horario.fechafinhorario + "', "
                                 + horario.idempleado + ")";


            SqlCommand query = new SqlCommand(command, sql);

            query.ExecuteNonQuery();

            sql.Close();

        }
        */
    }

}