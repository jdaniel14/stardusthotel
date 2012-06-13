using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class EmpleadoService
    {
        EmpleadoDAO empleadoDAO = new EmpleadoDAO();

        public EmpleadoBean getEmpleado(int id) {
            return empleadoDAO.getEmpleado(id);
        }

        public void registrarEmpleado(EmpleadoBean empleado) {
            empleadoDAO.registrarEmpleado(empleado);
        }

        public void modificarEmpleado(EmpleadoBean empleado) {
            empleadoDAO.modificarEmpleado(empleado);
        }

        public void eliminarEmpleado(int id) {
            empleadoDAO.eliminarEmpleado(id);
        }

        public List<EmpleadoBean> listarEmpleados() {
            return empleadoDAO.listarEmpleados();
        }

        public void asignarhorario(horario horar) {
            empleadoDAO.asignarhorario(horar);
            
        }

        public horario gethorario(int id) {
            return empleadoDAO.gethorario(id);
        
        }

        public void modificarHorario(horario h) {
            empleadoDAO.modificarHorario(h);
        }

        public List<horario> listarHorario(int id) {
            return empleadoDAO.listarHorario(id);
        }

       // public void asignarDetalle(int horariodetallebeam){
        
       //    return 
       // }
    }
}