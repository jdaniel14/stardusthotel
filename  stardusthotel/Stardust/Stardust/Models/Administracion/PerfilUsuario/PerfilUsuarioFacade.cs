using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models
{
    public class PerfilUsuarioFacade
    {
        PerfilUsuarioService perfilServ = new PerfilUsuarioService();

        public void registrarPerfil( PerfilUsuarioBean perfil ){
            perfilServ.registrarPerfil(perfil);
        }

        public List<PerfilUsuarioBean> listarPerfiles() {
            return perfilServ.listarPerfiles();
        }

        public PerfilUsuarioBean getPerfil(int id) {
            return perfilServ.getPerfil( id );
        }

        public void actualizarPerfil(PerfilUsuarioBean perfil) {
            perfilServ.actualizarPerfil(perfil);
        }

        public void eliminarPerfil( int id ){
            perfilServ.eliminarPerfil( id );
        }

        public int getPerfilID(string nombre) {
            return perfilServ.getPerfilID(nombre);
        }

    }
}