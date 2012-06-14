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

        public List<EmpleadoBean> buscarEmpleado(string nombre, string fechaInicio) {
            return empleadoDAO.buscarEmpleado(nombre, fechaInicio);
        }

        public int asignarHorario(Horario horario) {
            return empleadoDAO.asignarHorario(horario);
        }

        public Horario getHorario(int id) {
            return empleadoDAO.getHorario(id);
        }

        public int modificarHorario(Horario horario) {
            return empleadoDAO.modificarHorario(horario);
        }

        public List<Horario> listarHorario(int id) {
            return empleadoDAO.listarHorario(id);

        }

        public void asignarDetalle(HorarioDetalle horariod) {
            empleadoDAO.asignarDetalle(horariod);
        }
        public List<HorarioDetalle> listarDetalle(int id) {
            return empleadoDAO.listarDetalle(id);
        }

        public HorarioDetalle gethorarioDetalle(int id) {
            return empleadoDAO.gethorarioDetalle(id);
        }

        public void modificarHorarioDetalle(HorarioDetalle hd) {

            empleadoDAO.modificarHorarioDetalle(hd);
        }
    }
}