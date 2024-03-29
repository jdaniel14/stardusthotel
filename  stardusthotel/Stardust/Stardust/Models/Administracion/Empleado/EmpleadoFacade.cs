﻿using System;
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

        public List<EmpleadoBean> buscarEmpleado(string nombre, string fechaInicio) {
            if (nombre == null) nombre = "";
            if (fechaInicio == null) fechaInicio = "";
            if (nombre.Equals("") && fechaInicio.Equals("")) return null;
            return empleadoServ.buscarEmpleado(nombre, fechaInicio);
        }

        public int asignarHorario(Horario horario){
            return empleadoServ.asignarHorario(horario);
        }

        public Horario getHorario(int id){
            return empleadoServ.getHorario(id);
        }

        public int modificarHorario(Horario horario){
            return empleadoServ.modificarHorario( horario ) ;
        }

        public List<Horario> listarHorario(int id){
            return empleadoServ.listarHorario( id ) ;
        }

        public int asignarDetalle(HorarioDetalle horariod)
        {
            return empleadoServ.asignarDetalle(horariod);
        }
        public List<HorarioDetalle> listarDetalle(int id)
        {
            return empleadoServ.listarDetalle(id);
        }

        public HorarioDetalle gethorarioDetalle(int id)
        {
            return empleadoServ.gethorarioDetalle(id);
        }

        public int modificarHorarioDetalle(HorarioDetalle hd)
        {

            return empleadoServ.modificarHorarioDetalle(hd);
        }

        public bool comparahoras(HorarioDetalle horariod)
        {

            return empleadoServ.comparahoras(horariod);
        }
        public int compruebaasistencia(TomarAsistencia TA)
        {
            return empleadoServ.compruebaasistencia(TA);

        }

        public List<Horario> listarReporte(int codigoempleado,EmpleadoBean rango)
        {
            return empleadoServ.listarReporte(codigoempleado,rango);
        }
        public List<EmpleadoBean> listartodoempleado(EmpleadoBean rango) {
            return empleadoServ.listartodoempleado(rango);
        } 
    }
}