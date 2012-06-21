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

        /* ======== EMPLEADO ======== */
        #region Empleado

        public EmpleadoBean getEmpleado(int id) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                    String command = "Select * from Empleado where idEmpleado = " + id;

                    SqlCommand query = new SqlCommand(command, sql);

                    SqlDataReader data = query.ExecuteReader();

                /*
                EmpleadoBean empleado = new EmpleadoBean();
            
                empleado.ID = (int)data.GetValue(0);
                //UsuarioFacade usuario = new UsuarioFacade();
                //UsuarioBean usuar = usuario.getUsuario(empleado.ID);
                //empleado.nombreEmpleado = usuar.nombres+" " + usuar.apMat + " "+usuar.apPat;
               
                UsuarioBean usuario = new UsuarioFacade().getUsuario(empleado.ID);
                empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;   
                //empleado.nombreEmpleado="EMPLEADO :)";
                empleado.fechaIngreso = (DateTime)data.GetValue(1);
                empleado.fechaSalida = (DateTime)data.GetValue(2);
                empleado.estado = Convert.ToString(data["estado"]);

                    data.Read();

                */
                    data.Read();
                    EmpleadoBean empleado = new EmpleadoBean();

                   
                    UsuarioFacade usuario = new UsuarioFacade();
                    UsuarioBean usuar = usuario.getUsuario(id);
                    empleado.nombreEmpleado = usuar.nombres + " " + usuar.apMat + " " + usuar.apPat;
                    empleado.ID = (int)data.GetValue(0);
                    empleado.fechaIngreso = (DateTime)data.GetValue(1);
                    empleado.fechaSalida = (DateTime)data.GetValue(2);
                    empleado.estado = Convert.ToString(data["estado"]);

                sql.Close();

                return empleado;
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public void registrarEmpleado(EmpleadoBean empleado) {
            SqlConnection sql = null;
            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                    DateTime date = empleado.fechaIngreso;

                    String fechaIngreso = date.Date.ToShortDateString();

                    String command = "Insert into Empleado ( idEmpleado , fechaIngreso , estado) values ( "
                                        + empleado.ID + ", '"
                                        + fechaIngreso + "', "
                                        + "'ACTIVO' )";

                    SqlCommand query = new SqlCommand(command, sql);

                    query.ExecuteNonQuery();

                sql.Close();
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public void modificarEmpleado(EmpleadoBean empleado) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

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
            finally {
                if (sql != null) sql.Close();
            }

        }

        public void eliminarEmpleado(int id) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                    DateTime date = new DateTime();

                    String fechaSalida = Utils.DateToString(date);

                    String command = "Update Empleado SET estado = 'INACTIVO' , fechaSalida = '" + fechaSalida +
                            "' WHERE idEmpleado = " + id;

                    SqlCommand query = new SqlCommand(command, sql);

                    query.ExecuteNonQuery();

                sql.Close();
            }
            finally {
                if (sql != null) sql.Close();
            }
        }

        public List<EmpleadoBean> listarEmpleados() {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

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
            finally {
                if (sql != null) sql.Close();
            }
        }

        public List<EmpleadoBean> buscarEmpleado(string nombre, string fechaInicio) {
            SqlConnection sql = null;

            try
            {
                sql = new SqlConnection(cadenaDB);

                sql.Open();

                    String command = "Select * from Empleado";
                    String mount = "";
                    if (!fechaInicio.Equals(""))
                    {
                        if (mount.Length > 0) mount += " and";
                        mount += " fechaIngreso >= '" + fechaInicio + "'";
                    }
                    if (mount.Length > 0)
                    {
                        command += " where";
                        command += mount;
                    }

                    SqlCommand query = new SqlCommand(command, sql);

                    SqlDataReader data = query.ExecuteReader();

                    List<EmpleadoBean> lista = new List<EmpleadoBean>();

                    while (data.Read())
                    {
                        EmpleadoBean empleado = new EmpleadoBean();

                        empleado.ID = Convert.ToInt32(data["idEmpleado"]);
                        UsuarioBean usuario = new UsuarioFacade().getUsuario(empleado.ID);
                        empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;

                        if (!empleado.nombreEmpleado.Contains(nombre)) continue;
                        //empleado.nombreEmpleado="EMPLEADO :)";
                        empleado.fechaIngreso = (DateTime)data["fechaIngreso"];
                        //empleado.fechaSalida = (DateTime)data.GetValue(2);
                        empleado.estado = Convert.ToString(data["estado"]);

                        lista.Add(empleado);
                    }

                sql.Close();

                return lista;
            }
            finally {
                if (sql != null) sql.Close();
            }
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


        public int asignarDetalle(HorarioDetalle horariod)
        {


            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

           // DateTime dateini = horariod.horasentrada;
          //  DateTime datefin = horariod.horasSalida;
           // String fechaIngreso = dateini.Date.ToShortDateString();
           // String fechaSalida = datefin.Date.ToShortDateString();

            if (this.comparadetalles(horariod)) return -1;
            if (this.comparahoras(horariod)) return 0;
            String command = "Insert into HorarioDetalle ( diaSemana , horaEntrada , horaSalida, idHorario) values ( '"
                               + horariod.diaSemana + "', '"
                               + horariod.horaEntrada+ "',  '"
                                + horariod.horaSalida + "', "
                                 + horariod.idHorario+ ")";

           


            SqlCommand query = new SqlCommand(command, sql);

            query.ExecuteNonQuery();

            sql.Close();
            return 1;
        }
        

        public List<HorarioDetalle> listarDetalle(int id)
        {

            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from HorarioDetalle WHERE idHorario=" + id;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            List<HorarioDetalle> lista = new List<HorarioDetalle>();

            while (data.Read())
            {
                HorarioDetalle horar = new HorarioDetalle();

                horar.idHorarioDetalle = (int)data.GetValue(0);


                horar.diaSemana = (String)data.GetValue(1);
                horar.horaEntrada = (String)data.GetValue(2);
                horar.horaSalida = (String)data.GetValue(3);
                horar.idHorario = (int)data.GetValue(4);

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

            String command = "Select * from HorarioDetalle where idHorarioDetalle = " + id;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();

            data.Read();

            HorarioDetalle hd = new HorarioDetalle();

            hd.idHorarioDetalle = (int)data.GetValue(0);
            hd.diaSemana = (String)data.GetValue(1);

            hd.horaEntrada = (String)data.GetValue(2);
            hd.horaSalida = (String)data.GetValue(3);
            hd.idHorario= (int)data.GetValue(4);

            //  EmpleadoBean empleado = new EmpleadoFacade().gethorario(h.idempleado);
            //  hd.nombreEmpleado = empleado.nombreEmpleado;

            // UsuarioBean usuario = new UsuarioFacade().getUsuario(h.idempleado);
            //  empleado.nombreEmpleado = usuario.nombres + " " + usuario.apPat + " " + usuario.apMat;



            sql.Close();

            return hd;
        }



        public int modificarHorarioDetalle(HorarioDetalle hd)
        {
            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();
            // String fechaini = h.fechainiciohorario.Year + "-" + h.fechafinhorario.Month + "-" + h.fechainiciohorario.Day;
            //  String fechafin = h.fechafinhorario.Year + "-" + h.fechafinhorario.Month + "-" + h.fechafinhorario.Day;
            // String a = h.fechainiciohorario;
           // System.Diagnostics.Debug.WriteLine(" DAOhorariodetalles =" + hd.horariodetalles);
           // System.Diagnostics.Debug.WriteLine(" DAO =" + hd.horaEntrada);
           // System.Diagnostics.Debug.WriteLine(" DAO =" + hd.horaSalida);
           // System.Diagnostics.Debug.WriteLine(" DAOiddetalles..este es =" + hd.idHorarioDetalle);
           // System.Diagnostics.Debug.WriteLine(" DAOhorario =" + hd.idHorario);

            if (this.comparahoras(hd)) return -1;
            String command = "Update HorarioDetalle SET "
                                + "horaEntrada = '" + hd.horaEntrada
                                + "', horaSalida = '" + hd.horaSalida
                //+ "' WHERE idEmpleado = " + h.idempleado
                //+" AND idHorario=" + h.ID;
                                + "' WHERE idHorarioDetalle = " + hd.idHorarioDetalle;
            SqlCommand query = new SqlCommand(command, sql);

            query.ExecuteNonQuery();

            sql.Close();
            return 0;
        }

        public bool comparadetalles(HorarioDetalle horariod) {

            List<HorarioDetalle> listHorariosdetalle = this.listarDetalle(horariod.idHorario);
            //System.Diagnostics.Debug.WriteLine("FechaINI = " + Utils.DateToString( fechaIni ) ) ;
            //System.Diagnostics.Debug.WriteLine("FechaFIN = " + Utils.DateToString( fechaFin ) ) ;
            for (int i = 0; i < listHorariosdetalle.Count; i++)
            {
                try
                {
                    HorarioDetalle horariodetalle = listHorariosdetalle.ElementAt(i);

                    if (horariodetalle.diaSemana == horariod.diaSemana) return true;

                    // if (fechaIni.CompareTo(ini) >= 0 && fechaIni.CompareTo(fin) <= 0) return true;
                    //  if (fechaFin.CompareTo(ini) >= 0 && fechaFin.CompareTo(fin) <= 0) return true;
                }
                catch {
                    return false;
                }
            }

            return false;
            
        }


        public bool comparahoras(HorarioDetalle horariod)
        {

          
            //System.Diagnostics.Debug.WriteLine("FechaINI = " + Utils.DateToString( fechaIni ) ) ;
          
               
                    DateTime iniprueba = Convert.ToDateTime(horariod.horaEntrada);
                    

                    DateTime ini = Convert.ToDateTime (horariod.horaEntrada);
                    DateTime fin = Convert.ToDateTime(horariod.horaSalida);
                  
                    if (ini> fin) return true;
                 
                    return false;
                
            


        }
        #endregion

        #region Asistencia

        public int compruebaasistencia(TomarAsistencia TA) { 
        
             int  idusuario=0;
             Horario horario=null;
             int iddetalle=0;
               if (this.ES_USUARIO(TA,ref idusuario)){  //verifica si es un usuario valido
                        // UsuarioBean usuario = new UsuarioFacade().getUsuario(idusuario);

                      if (this.ES_EMPLEADO(idusuario)){  //si es un empelado activo
                               EmpleadoFacade empleadofacade=new EmpleadoFacade();
                               EmpleadoBean empleado=empleadofacade.getEmpleado(idusuario);
                                
                                if (this.Es_EMPLEADOINACTIVO(empleado)){
                             
                                        if (this.Es_Contratoalafecha(empleado)){

                                             if (this.Es_Horarioalafecha(empleado,ref horario)){
                                                      
                                                    if (this.Es_Detallealafecha(horario,ref iddetalle)){
                                                           int valor= this.registraasistencia(iddetalle);                              
                                                               return valor;
                                                    }
                                                     else return 4;
                                              }
                                              else return 3;
                                            }
                                            else return 2;
                                    }
                                    else return 1;
                      
                      }
                      else return 0; 
               
               } 
               else return -1;
        
        
        }

        public bool ES_USUARIO(TomarAsistencia posibleusuario,ref int idusuario){

            SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from Usuario WHERE user_account = '" + posibleusuario.usuario+ "'AND pass='"+posibleusuario.pasword+"'";

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();
            data.Read();
                
           
            if (data.HasRows==true) {

                idusuario = (int)data.GetValue(0);


                sql.Close();
                return true;
            }
           // EmpleadoBean empleado = new EmpleadoBean();


            sql.Close();
            return false;

            
        }

         public bool ES_EMPLEADO(int idusuario){
         
                SqlConnection sql = new SqlConnection(cadenaDB);

            sql.Open();

            String command = "Select * from Empleado WHERE idEmpleado = " + idusuario ;

            SqlCommand query = new SqlCommand(command, sql);

            SqlDataReader data = query.ExecuteReader();
            data.Read();
                
           
            if (data.HasRows==true) {

                idusuario = (int)data.GetValue(0);


                sql.Close();
                return true;
            }
           // EmpleadoBean empleado = new EmpleadoBean();


            sql.Close();
            return false;
         }


         public bool Es_EMPLEADOINACTIVO(EmpleadoBean empleado) {


             if (empleado.estado == "ACTIVO") return true;
             else return false;
         
         
         }
         public bool Es_Contratoalafecha(EmpleadoBean empleado) { 
         
               DateTime dtsistema= DateTime.Now;
               if ((empleado.fechaIngreso <= dtsistema) && (dtsistema <= empleado.fechaSalida))
               {

                   return true;

               }
               else return false;
         }


         public bool Es_Horarioalafecha(EmpleadoBean empleado,ref Horario horario)
         {
         
              // List<Horario> listarHorario=
                 List<Horario> listHorarios = this.listarHorario( empleado.ID ) ;
            //System.Diagnostics.Debug.WriteLine("FechaINI = " + Utils.DateToString( fechaIni ) ) ;
            //System.Diagnostics.Debug.WriteLine("FechaFIN = " + Utils.DateToString( fechaFin ) ) ;
                 for (int i = 0; i < listHorarios.Count; i++)
                 {
                     Horario horarioaux = listHorarios.ElementAt(i);

                    // if (horarioaux.ID == empleado.ID) continue;

                     DateTime ini = horarioaux.fechaInicioHorario;
                     DateTime fin = horarioaux.fechaFinHorario;

                     DateTime dtsistema = DateTime.Now;
                     if ((ini <= dtsistema) && (dtsistema <= fin))
                     {
                         horario = horarioaux;
                         return true;
                     }
                 }
                 return false;
         }

         public bool Es_Detallealafecha(Horario horario,ref int iddetalle) {
             string dia="";
             List<HorarioDetalle> listDetalle = this.listarDetalle(horario.ID);

             for (int i = 0; i < listDetalle.Count; i++)
             {
               
                 HorarioDetalle horariodetalle = listDetalle.ElementAt(i);

                // if (horariodetalle.idHorario == horario.ID) continue;
                    
                    DateTime dt=DateTime.Now;

                    if (DayOfWeek.Monday == dt.DayOfWeek) dia = "Lunes";
                    if (DayOfWeek.Tuesday == dt.DayOfWeek) dia = "Martes";
                    if (DayOfWeek.Wednesday == dt.DayOfWeek) dia = "Miercoles";
                    if (DayOfWeek.Thursday == dt.DayOfWeek) dia = "Jueves";
                    if (DayOfWeek.Friday == dt.DayOfWeek) dia = "Viernes";
                    if (DayOfWeek.Saturday == dt.DayOfWeek) dia = "Sabado";
                    if (DayOfWeek.Sunday == dt.DayOfWeek) dia = "Domingo";

                    if (dia==horariodetalle.diaSemana) {
                        iddetalle = horariodetalle.idHorarioDetalle;
                        return true;
                    }
                   
             }
             return false;
         }

         public int registraasistencia(int iddetalle) {
             EmpleadoFacade EF=new EmpleadoFacade();
             HorarioDetalle hd = EF.gethorarioDetalle(iddetalle);
             Asistencia asistencia=new Asistencia();

             if (marcoasistenciaunavez(iddetalle) == 0) asistencia.TipoES = 'E';
             else if (marcoasistenciaunavez(iddetalle) == 1) asistencia.TipoES = 'S';
               else return 5;

             if (llegotarde(iddetalle, ref asistencia)) ;



             SqlConnection sql = new SqlConnection(cadenaDB);

             sql.Open();

         
             String command = "Insert into Asistencia ( horaMarcada , tipoES , estado, idHorarioDetalle) values ( '"
                                + asistencia.horaasistencia + "', '"
                                + asistencia.TipoES + "',  '"
                                 + asistencia.estado + "', "
                                  + asistencia.idHorarioDetalle + ")";




             SqlCommand query = new SqlCommand(command, sql);

             query.ExecuteNonQuery();

             sql.Close();
             return 6;
         }


         public int marcoasistenciaunavez(int iddetalle) {
              List<Asistencia>asistencias;
              int contador = 0;
             asistencias= this.ListarAsistenciasdeunidDetalle(iddetalle);
             for (int i = 0; i < asistencias.Count; i++)
             {

                 Asistencia a = asistencias.ElementAt(i);

                 if (a.horaasistencia.Day == DateTime.Now.Day) contador++ ;
                
             }

             return contador;
         }

       
         public List<Asistencia> ListarAsistenciasdeunidDetalle(int iddetalle) {



             SqlConnection sql = new SqlConnection(cadenaDB);

             sql.Open();

             String command = "Select * from Asistencia WHERE idHorarioDetalle=" + iddetalle;

             SqlCommand query = new SqlCommand(command, sql);

             SqlDataReader data = query.ExecuteReader();

             List<Asistencia> lista = new List<Asistencia>();

             while (data.Read())
             {
                 Asistencia asistencia = new Asistencia();

                 asistencia.idasistencia = (int)data.GetValue(0);


                 asistencia.horaasistencia = (DateTime)data.GetValue(1);
                 asistencia.stringtipoES = data.GetValue(2).ToString();
                 asistencia.estado = (String)data.GetValue(3);
                 asistencia.idHorarioDetalle = (int)data.GetValue(4);
                 asistencia.TipoES = Convert.ToChar(asistencia.stringtipoES);
                 lista.Add(asistencia);

                 
             }

             sql.Close();

             return lista;


         }

        public bool llegotarde(int iddetalle,ref Asistencia asistencia){

            DateTime horaactual = DateTime.Now;
            string hora = this.sacarhoradatetime(horaactual);
            EmpleadoFacade empleadofac=new EmpleadoFacade();
            HorarioDetalle hd=empleadofac.gethorarioDetalle(iddetalle);
            asistencia.idHorarioDetalle = iddetalle;
            asistencia.horaasistencia = DateTime.Now;
            if (asistencia.TipoES == 'E')
            {

                if ((hd.horaEntrada.CompareTo(hora)) < 0){ asistencia.estado="DESHORA"; return true;}
                else {asistencia.estado="ALAHORA"; return false;}
            }
            else {

                if ((hd.horaSalida.CompareTo(hora)) >= 0) { asistencia.estado = "ALAHORA"; return true; }
                else { asistencia.estado = "DESHORA"; return false; }
            
            
            }
        
        }

        string sacarhoradatetime(DateTime fecha) {
            string cadena = fecha.ToShortTimeString();
            char[] hora = new char[5];
            cadena.CopyTo(0, hora, 0, 5);
            string retorno = new string(hora);
            return retorno;
        }
        #endregion


    }
}