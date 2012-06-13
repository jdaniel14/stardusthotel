using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class UsuarioFacade
    {
        UsuarioService usuarioServ = new UsuarioService();

        public UsuarioBean getUsuario(int id)
        {
            return usuarioServ.getUsuario(id);
        }

        public void registrarUsuario(UsuarioBean usuario)
        {
            usuarioServ.registrarUsuario(usuario);
        }

        public void actualizarUsuario(UsuarioBean usuario)
        {
            usuarioServ.actualizarUsuario(usuario);
        }

        public void eliminarUsuario(int id)
        {
            usuarioServ.eliminarUsuario(id);
        }

        public List<UsuarioBean> listarUsuarios()
        {
            return usuarioServ.listarUsuarios();
        }

        public List<UsuarioBean> buscarUsuario(string nombre, string apPat, string apMat) {
            if (nombre == null) nombre = "";
            if (apPat == null) apPat = "";
            if (apMat == null) apMat = "";
            if (nombre.Equals(apPat) && apPat.Equals(apMat) && apMat.Equals("")) return new List<UsuarioBean>();
            return usuarioServ.buscarUsuario(nombre, apPat, apMat);
        }
    }
}