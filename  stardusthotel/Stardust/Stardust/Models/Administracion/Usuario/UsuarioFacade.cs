using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class UsuarioFacade
    {
        UsuarioService usuarioServ = new UsuarioService();

        public UsuarioBean getLogin(string user, string pass) {
            return usuarioServ.getLogin(user, pass);
        }

        public void marcarOnline(int id) {
            usuarioServ.marcarOnline(id);
        }

        public void logout(int id) {
            usuarioServ.logout(id);
        }

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

        public List<UsuarioBean> buscarUsuario(string account , string nombre, string apPat, string apMat , string tipoDocumento , string nroDocumento) {
            if (account == null) account = "";
            if (nombre == null) nombre = "";
            if (apPat == null) apPat = "";
            if (apMat == null) apMat = "";
            if (tipoDocumento == null) tipoDocumento = "";
            if (nroDocumento == null) nroDocumento = "";
            if (account.Equals( nombre ) && nombre.Equals(apPat) && apPat.Equals(apMat) && apMat.Equals("") &&tipoDocumento.Equals(apMat) &&nroDocumento.Equals(nombre)) return new List<UsuarioBean>();
            return usuarioServ.buscarUsuario(account , nombre, apPat, apMat,tipoDocumento,nroDocumento);
        }
    }
}