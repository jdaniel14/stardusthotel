using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class EmpleadoFacade
    {
        EmpleadoService empleadoServ = new EmpleadoService();

        public EmpleadoBean getEmpleado(int id) {
            return empleadoServ.getEmpleado(id);
        }

        public void registrarEmpleado(EmpleadoBean empleado) {
            empleadoServ.registrarEmpleado(empleado);
        }

        public void modificarEmpleado(EmpleadoBean empleado) {
            empleadoServ.modificarEmpleado(empleado);
        }

        public void eliminarEmpleado(int id) {
            empleadoServ.eliminarEmpleado(id);
        }

        public List<EmpleadoBean> listarEmpleados() {
            return empleadoServ.listarEmpleados();
        }
    }
}