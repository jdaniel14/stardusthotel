﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class PerfilUsuarioService
    {
        PerfilUsuarioDAO perfilDAO = new PerfilUsuarioDAO();

        public void registrarPerfil(PerfilUsuarioBean perfil) {
            perfilDAO.registrarPerfil(perfil);
        }

        public List<PerfilUsuarioBean> listarPerfiles() {
            return perfilDAO.listarPerfiles();
        }

        public void actualizarPerfil(PerfilUsuarioBean perfil) {
            perfilDAO.actualizarPerfil( perfil );
        }

        public PerfilUsuarioBean getPerfil(int id) {
            return perfilDAO.getPerfil( id ) ;
        }

        public void eliminarPerfil(int id) {
            perfilDAO.eliminarPerfil(id);
        }
    }
}