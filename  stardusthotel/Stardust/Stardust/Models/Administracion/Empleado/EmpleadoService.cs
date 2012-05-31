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
    }
}