using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Stardust.Models
{
    public class TipoDocumento {
        public string nombre { get; set; }
        public TipoDocumento(string s) {
            this.nombre = s;
        }
    }

    public class UsuarioBean
    {
        public int ID { get; set; }

        [Display(Name = "Perfil Usuario")]
        public int idPerfilUsuario { get; set; }

        [Display(Name = "Perfil Usuario")]
        public string nombrePerfilUsuario { get; set; }

        [Display(Name = "Usuario")]
        public string user_account { get; set; }

        [Display(Name = "Password")]
        public string pass { get; set; }

        [Display(Name = "Nombre")]
        public string nombres { get; set; }

        [Display(Name = "Apellido Paterno")]
        public string apPat { get; set; }

        [Display(Name = "Apellido Materno")]
        public string apMat { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Teléfono")]
        public string celular { get; set; }

        [Display(Name = "Tipo de documento")]
        public string tipoDocumento { get; set; }

        [Display(Name = "Nro. de Documento")]
        public string nroDocumento { get; set; }

        [Display(Name = "Razón Social")]
        public string razonSocial { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Distrito")]
        public int idDistrito { get; set; }

        [Display(Name = "Distrito")]
        public string nombreDistrito { get; set; }

        [Display(Name = "Provincia")]
        public int idProvincia { get; set; }

        [Display(Name = "Provincia")]
        public string nombreProvincia { get; set; }

        [Display(Name = "Departamento")]
        public int idDepartamento { get; set; }

        [Display(Name = "Departamento")]
        public string nombreDepartamento { get; set; }
    }

    }