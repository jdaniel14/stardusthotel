using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class UsuarioService
    {
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        public UsuarioBean getLogin(string user, string pass) {
            return usuarioDAO.getLogin(user, pass);
        }

        public UsuarioBean getUsuario(int id)
        {
            return usuarioDAO.getUsuario(id);
        }

        public void registrarUsuario(UsuarioBean usuario)
        {
            usuarioDAO.registrarUsuario(usuario);
        }

        public void actualizarUsuario(UsuarioBean usuario)
        {
            usuarioDAO.actualizarUsuario(usuario);
        }

        public void eliminarUsuario(int id)
        {
            usuarioDAO.eliminarUsuario(id);
        }

        public List<UsuarioBean> listarUsuarios()
        {
            return usuarioDAO.listarUsuarios();
        }

        public List<UsuarioBean> buscarUsuario(string account ,string nombre, string apPat, string apMat,string tipoDocumento,string nroDocumento) {
            return usuarioDAO.buscarUsuario(account , nombre, apPat, apMat,tipoDocumento,nroDocumento);
        }
    }
}