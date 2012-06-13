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

        public void asignarhorario(horario horar)
        {
            empleadoServ.asignarhorario(horar);

        }

        public horario gethorario(int id)
        {
            return empleadoServ.gethorario(id);

        }

        public void modificarHorario(horario h)
        {
            empleadoServ.modificarHorario(h);
        }

        public List<horario> listarHorario(int id)
        {
            return empleadoServ.listarHorario(id);
        }
    }
}