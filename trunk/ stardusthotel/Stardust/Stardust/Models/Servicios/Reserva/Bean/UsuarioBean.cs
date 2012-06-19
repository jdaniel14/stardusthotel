using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stardust.Models.Servicios
{
    public class UsuarioBean
    {   
        public int idUsuario {get; set;}
        public int idPerfilUsuario {get; set;}
        public String uset_account {get; set;}
        public String pass {get; set;}
        public String nombres {get; set;}
        public String apPat {get; set;}
        public String apMat {get; set;}
        public String email {get; set;}
        public String celular {get; set;}
        public String tipoDocumento {get; set;}
        public String nroDocumento {get; set;}
        public String razonSocial {get; set;}
        public String estado {get; set;}
        public String direccion {get; set;}        
        public int idDistrito {get; set;}
        public int idProvincia {get; set;}
        public int idDepartamento {get; set;}
    }
}