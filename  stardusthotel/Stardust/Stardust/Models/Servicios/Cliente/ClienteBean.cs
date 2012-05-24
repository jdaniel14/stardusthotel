using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Stardust.Models
{
    public class ClienteBean
    {
       //[Key]
        public int ID { get; set; }
        public string nombres { get; set; }
        public string apPat { get; set; }
        public string apMat { get; set; }
        public string razonSocial { get; set; }
        public string tipoDocumento { get; set; }//DNI, Carne de Extranjeria, RUC
        public string nroDocumento { get; set; }
        public string tipoTarjeta { get; set; }
        public string nroTarjeta { get; set; }

        /*
        [Display(Name = "Usuario")]
        public string user_account { get; set; }

        [Display(Name = "Contraseña")]
        public string pass { get; set; }

        [Display(Name = "Nombres")]
        public string nombres { get; set; }

        [Display(Name = "Apellido Paterno")]
        public string apPat { get; set; }
        
        [Display(Name = "Apellido Materno")]
        public string apMat { get; set; }

        [Display(Name = "Razón Social")]
        public string razonSocial { get; set; }
        
        [Display(Name = "Tipo de Documento")]
        public string tipoDocumento { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime fechaRegistro { get; set; }

        [Display(Name = "Numero de documento")]
        public string nroDocumento { get; set; }

       [Display(Name = "Correo electrónico")]
        public string email { get; set; }

       [Display(Name = "Teléfono")]
       public string telefono { get; set; }

       [Display(Name = "Teléfono")]
       public string direccion { get; set; }

       [Display(Name = "Celular")]
       public string celular { get; set; }

       [Display(Name = "Tipo Tarjeta")]
       public string tipoTajeta { get; set; }

       [Display(Name = "Número de Tarjeta")]
       public string nroTarjeta { get; set; }


       public string estado { get; set; }

        public int estado2 { get; set; }*/
    }
    }
