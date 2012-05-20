using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Administracion
{
    public class UsuarioFacade
    {
        UsuarioService usrServ = new UsuarioService();
        
        public void registrarUsuario(UsuarioBean usuario) {
            usrServ.registrarUsuario( usuario ) ;
        }
    }
}