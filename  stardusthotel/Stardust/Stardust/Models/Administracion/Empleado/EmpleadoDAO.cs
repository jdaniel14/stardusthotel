﻿using System;
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

        /* ======== EMPLEADO ======== */
        #region Empleado

        public EmpleadoBean getEmpleado(int id) {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Empleado where idEmpleado = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                EmpleadoBean empleado = new EmpleadoBean();
            
                empleado.ID = (int)data.GetValue(0);

                //UsuarioBean usuario = new UsuarioFacade().getUsuario(empleado.ID);
                //empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;   
                empleado.nombreEmpleado="EMPLEADO :)";
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

                String fechaSalida = Utils.DateToString(empleado.fechaSalida);

                String command = "Update Empleado SET "
                                    + "fechaSalida = '" + fechaSalida
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

                String fechaSalida = Utils.DateToString(date);

                String command = "Update Empleado SET estado = 'INACTIVO' , fechaSalida = '" + fechaSalida + 
                        "' WHERE idEmpleado = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();
        }

        public List<EmpleadoBean> listarEmpleados() {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from Empleado WHERE estado = 'ACTIVO' ORDER BY fechaIngreso";

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<EmpleadoBean> lista = new List<EmpleadoBean>();

                while (data.Read())
                {
                    EmpleadoBean empleado = new EmpleadoBean();

                    empleado.ID = (int)data.GetValue(0);
                    UsuarioBean usuario = new UsuarioFacade().getUsuario(empleado.ID);
                    empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;
                    empleado.fechaIngreso = (DateTime)data.GetValue(1);
                    empleado.estado = (string)data.GetValue(3);

                    lista.Add(empleado);
                }
            sql.Close();

            return lista;
        }
        #endregion

        /* ======== HORARIOS ======== */
        #region Horario
        public int asignarHorario(Horario horario){

            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String fechaIni = Utils.DateToString(horario.fechaInicioHorario);
                String fechaFin = Utils.DateToString(horario.fechaFinHorario);

                if ( existeHorario( horario.ID , horario.idEmpleado , horario.fechaInicioHorario , horario.fechaFinHorario ) ) return -1;

                String command = "Insert into Horario ( fechaIni , fechaFin, idEmpleado) values ( '"
                                   + fechaIni + "',  '"
                                   + fechaFin + "', "
                                   + horario.idEmpleado + ")";

                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();

            return 0;
        }

        public Horario getHorario(int id)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Horario where idHorario = " + id;

                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                data.Read();

                Horario h = new Horario();

                h.ID = (int)data.GetValue(0);
                h.fechaInicioHorario = (DateTime)data.GetValue(1);
            
                h.fechaFinHorario = (DateTime)data.GetValue(2);
                h.idEmpleado = (int)data.GetValue(3);
            
                EmpleadoBean empleado = new EmpleadoFacade().getEmpleado(h.idEmpleado);
                h.nombreEmpleado = empleado.nombreEmpleado;

            sql.Close();

            return h;
        }

        public int modificarHorario(Horario horario)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String fechaIni = Utils.DateToString(horario.fechaInicioHorario);
                String fechaFin = Utils.DateToString(horario.fechaFinHorario);

                if (existeHorario(horario.ID , horario.idEmpleado, horario.fechaInicioHorario, horario.fechaFinHorario)) return -1;

                String command = "Update Horario SET "
                                    + "fechaIni = '" + fechaIni
                                    + "', fechaFin = '" + fechaFin
                                    + "' WHERE idHorario = " + horario.ID;
                SqlCommand query = new SqlCommand(command, sql);

                query.ExecuteNonQuery();

            sql.Close();

            return 0;
        }

        public List<Horario> listarHorario(int id)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

                String command = "Select * from Horario WHERE idEmpleado = " + id + " ORDER BY idHorario";
           
                SqlCommand query = new SqlCommand(command, sql);

                SqlDataReader data = query.ExecuteReader();

                List<Horario> lista = new List<Horario>();

                while (data.Read())
                {
                    Horario horar = new Horario();

                    horar.ID = (int)data.GetValue(0);
            
                    horar.fechaInicioHorario = (DateTime)data.GetValue(1);
                    horar.fechaFinHorario = (DateTime)data.GetValue(2);
                    horar.idEmpleado = (int)data.GetValue(3);

                    EmpleadoBean empleado = new EmpleadoFacade().getEmpleado(horar.idEmpleado);
                    horar.nombreEmpleado = empleado.nombreEmpleado;
                    lista.Add(horar);
                }

            sql.Close();

            return lista;
        }

        public bool existeHorario(int idHorario , int idEmpleado, DateTime fechaIni, DateTime fechaFin) {
            List<Horario> listHorarios = this.listarHorario( idEmpleado ) ;
            //System.Diagnostics.Debug.WriteLine("FechaINI = " + Utils.DateToString( fechaIni ) ) ;
            //System.Diagnostics.Debug.WriteLine("FechaFIN = " + Utils.DateToString( fechaFin ) ) ;
            for (int i = 0; i < listHorarios.Count; i++) {
                Horario horario = listHorarios.ElementAt( i ) ;

                if (horario.ID == idHorario) continue;

                DateTime ini = horario.fechaInicioHorario;
                DateTime fin = horario.fechaFinHorario;

                //System.Diagnostics.Debug.WriteLine("INI = " + Utils.DateToString( ini ) );
                //System.Diagnostics.Debug.WriteLine("FIN = " + Utils.DateToString( fin ) ) ;

                if (fechaIni.CompareTo(ini) >= 0 && fechaIni.CompareTo(fin) <= 0) return true;
                if (fechaFin.CompareTo(ini) >= 0 && fechaFin.CompareTo(fin) <= 0) return true;
            }
            return false;
        }
        #endregion


        #region HorarioDetalle
        //detalle


        public void asignarDetalle(HorarioDetalle horariod)
        {


            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

           // DateTime dateini = horariod.horasentrada;
          //  DateTime datefin = horariod.horasSalida;
           // String fechaIngreso = dateini.Date.ToShortDateString();
           // String fechaSalida = datefin.Date.ToShortDateString();



            String command = "Insert into HorarioDetalle ( diaSemana , horaEntrada , horaSalida, idHorario) values ( '"
                               + horariod.diaSemana + "', '"
                               + horariod.horaEntrada+ "',  '"
                                + horariod.horaSalida + "', "
                                 + horariod.idHorario+ ")";

           


            SqlCommand query = new SqlCommand(command, sql);

            query.ExecuteNonQuery();

            sql.Close();

        }


        public List<HorarioDetalle> listarDetalle(int id)
        {

            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from HorarioDetalle WHERE idHorarioDetalle=" + id + " ORDER BY idHorario";

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            List<HorarioDetalle> lista = new List<HorarioDetalle>();

            while (data.Read())
            {
                HorarioDetalle horar = new HorarioDetalle();

                horar.idHorarioDetalle = (int)data.GetValue(0);


                horar.diaSemana = (String)data.GetValue(1);
                horar.horaEntrada = (DateTime)data.GetValue(2);
                horar.horaSalida = (DateTime)data.GetValue(3);
                horar.idHorario = (int)data.GetValue(3);

                EmpleadoBean empleado = new EmpleadoFacade().getEmpleado(getHorario(horar.idHorario).idEmpleado);
                horar.nombreEmpleado = empleado.nombreEmpleado;
                lista.Add(horar);
            }

            sql.Close();

            return lista;



        }

        public HorarioDetalle gethorarioDetalle(int id)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from HorarioDetalle where idHorario = " + id;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            data.Read();

            HorarioDetalle hd = new HorarioDetalle();

            hd.idHorarioDetalle = (int)data.GetValue(0);
            hd.diaSemana = (String)data.GetValue(1);

            hd.horaEntrada = (DateTime)data.GetValue(2);
            hd.horaSalida = (DateTime)data.GetValue(3);
            hd.idHorario= (int)data.GetValue(4);

            //  EmpleadoBean empleado = new EmpleadoFacade().gethorario(h.idempleado);
            //  hd.nombreEmpleado = empleado.nombreEmpleado;

            // UsuarioBean usuario = new UsuarioFacade().getUsuario(h.idempleado);
            //  empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;



            sql.Close();

            return hd;
        }



        public void modificarHorarioDetalle(HorarioDetalle hd)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();
            // String fechaini = h.fechainiciohorario.Year + "-" + h.fechafinhorario.Month + "-" + h.fechainiciohorario.Day;
            //  String fechafin = h.fechafinhorario.Year + "-" + h.fechafinhorario.Month + "-" + h.fechafinhorario.Day;
            // String a = h.fechainiciohorario;

            String command = "Update HorarioDetalle SET "
                                + "fechaIni = '" + hd.horaEntrada
                                + "', fechaFin = '" + hd.horaSalida
                //+ "' WHERE idEmpleado = " + h.idempleado
                //+" AND idHorario=" + h.ID;
                                + "' WHERE idHorarioDetalle = " + hd.idHorarioDetalle;
            SqlCommand query = new SqlCommand(command, sql);

            query.ExecuteNonQuery();

            sql.Close();

        }

        #endregion
    }
}