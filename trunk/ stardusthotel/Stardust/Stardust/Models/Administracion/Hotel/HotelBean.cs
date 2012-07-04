using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Stardust.Models
{
    public class HotelViewModelCreate
    {
        [Display(Name="Nombre")]
        [Required(ErrorMessage = "Debe ingresar el nombre del Hotel")]
        public string nombre { get; set; }

        [Display(Name = "Razón Social")]
        public string razonSocial { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar la dirección del Hotel")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono Principal")]
        [Required(ErrorMessage = "Debe ingresar el teléfono")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Debe ingresar 9 dígitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe tener la sintaxis de un telefóno")]
        public string tlf1 { get; set; }

        [Display(Name = "Teléfono Secundario")]
        [Remote("ValidaFonoNoRequerido", "Validation")]
        public string tlf2 { get; set; }

        [Display(Name = "E-mail")]
        [Remote("ValidaEmail", "Validation")]
        public string email { get; set; }

        [Display(Name = "Número de pisos")]
        [Required(ErrorMessage = "Debe indicar el número de pisos")]
        [Range(0, 999, ErrorMessage = "El número de pisos es incorrecto")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int? nroPisos { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Debe seleccionar un departamento")]
        public int idDepartamento { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Debe seleccionar una provincia")]
        public int idProvincia { get; set; }

        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "Debe seleccionar un distrito")]
        public int idDistrito { get; set; }

        public List<Departamento> Departamentos { get; set; }
        
        public AlmacenBean Almacen { get; set; }
    }

    public class HotelViewModelDetails
    {
        [Key]
        public int ID { get; set; } //Para que pueda ir de la pantalla Details a la pantalla Edit

        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Razón Social")]
        public string razonSocial { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono Principal")]
        public string tlf1 { get; set; }

        [Display(Name = "Teléfono Secundario")]
        public string tlf2 { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Número de pisos")]
        public int? nroPisos { get; set; }

        [Display(Name = "Departamento")]
        public string nombreDepartamento { get; set; }

        [Display(Name = "Provincia")]
        public string nombreProvincia { get; set; }

        [Display(Name = "Distrito")]
        public string nombreDistrito { get; set; }

        //con estos 3 datos, se obtiene el nombre del departamentos, provincia o distrito pero
        //no son parte visible en el view
        public int idDepartamento { get; set; }
        public int idProvincia { get; set; }
        public int idDistrito { get; set; }
    }

    public class HotelViewModelEdit
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar el nombre del Hotel")]
        public string nombre { get; set; }

        [Display(Name = "Razón Social")]
        public string razonSocial { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar la dirección del Hotel")]
        public string direccion { get; set; }

        [Display(Name = "Teléfono Principal")]
        [Required(ErrorMessage = "Debe ingresar el teléfono")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Debe ingresar 9 dígitos")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe tener la sintaxis de un telefóno")]
        public string tlf1 { get; set; }

        [Display(Name = "Teléfono Secundario")]
        [Remote("ValidaFonoNoRequerido", "Hotel")]
        public string tlf2 { get; set; }

        [Display(Name = "E-mail")]
        [Remote("ValidaEmail", "Hotel")]
        public string email { get; set; }

        [Display(Name = "Número de pisos")]
        [Required(ErrorMessage = "Debe indicar el número de pisos")]
        [Range(0, 999, ErrorMessage = "El número de pisos es incorrecto")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int? nroPisos { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Debe seleccionar un departamento")]
        public int idDepartamento { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "Debe seleccionar una provincia")]
        public int idProvincia { get; set; }

        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "Debe seleccionar un distrito")]
        public int idDistrito { get; set; }

        public List<Departamento> Departamentos { get; set; }
        public List<Provincia> Provincias { get; set; }
        public List<Distrito> Distritos { get; set; }

    }

    public class HotelViewModelList
    {
        [Key]
        public int ID { get; set; } //Para que pueda ir de la pantalla Details a la pantall Edit
        public string nombre { get; set; }
        public string razonSocial { get; set; }

        //public string direccion { get; set; }
        //public string tlf1 { get; set; }
        //public string tlf2 { get; set; }
        //public string email { get; set; }
        //public int? nroPisos { get; set; }

        //con estos 3 datos, se obtiene el nombre del departamentos, provincia o distrito pero
        //no son parte visible en el view
        public int idDepartamento { get; set; }
        public int idProvincia { get; set; }
        public int idDistrito { get; set; }
        
        public string nombreDepartamento { get; set; }
        public string nombreProvincia { get; set; }
        public string nombreDistrito { get; set; }
    }

    public class HotelViewModelDelete
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Razón Social")]
        public string razonSocial { get; set; }

        [Display(Name = "Dirección")]
        public string direccion { get; set; }

        [Display(Name = "Departamento")]
        public string nombreDepartamento { get; set; }

        [Display(Name = "Provincia")]
        public string nombreProvincia { get; set; }

        [Display(Name = "Distrito")]
        public string nombreDistrito { get; set; }

        // INFORMACIÓN PARA QUE LA PIENSE EN CASO DESEE DESACTIVAR EL HOTEL xD!
        // --------------------------------------------------------------------
        [Display(Name = "N° de Tipos de Habitaciones")]
        public int nTipoHabitacion { get; set; }

        [Display(Name = "N° de Habitaciones")]
        public int nHabitacion { get; set; }

        [Display(Name = "N° de Ambientes")]
        public int nAmbientes { get; set; }

        [Display(Name = "N° de Servicios")]
        public int nServicios { get; set; }

        [Display(Name = "N° de Promociones")]
        public int nPromociones { get; set; }

        [Display(Name = "N° de Almacenes")]
        public int nAlmacenes { get; set; }
        // --------------------------------------------------------------------

        //con estos 3 datos, se obtiene el nombre del departamentos, provincia o distrito pero
        //no son parte visible en el view
        public int idDepartamento { get; set; }
        public int idProvincia { get; set; }
        public int idDistrito { get; set; }
    }

    public class HotelBean
    {
        [Key]
        public int ID { get; set; }

        //[Display( Name = "Nombre" ) ]
        //[Required (ErrorMessage="Debe ingresar el nombre del Hotel")]
        //[RegularExpression("^([a-zA-Z-9ÑñáéíóúÁÉÍÓÚ]*\\w*\\d*\\s*)*$", ErrorMessage="El nombre ingresado no es válido")]
        public string nombre { get; set; }

        //[Display( Name = "Razón Social" ) ]
        //[RegularExpression("^([a-zA-Z-9ÑñáéíóúÁÉÍÓÚ]*\\w*\\d*\\s*)*$", ErrorMessage = "La razón social no es válida")]
        public string razonSocial { get; set; }

        //[Display( Name = "Dirección" ) ]
        //[Required(ErrorMessage = "Debe ingresar la dirección del Hotel")]
        //[RegularExpression("^([a-zA-Z-9ÑñáéíóúÁÉÍÓÚ]*\\w*\\d*\\s*)*$", ErrorMessage = "La dirección no es válida")]
        public string direccion { get; set; }

        //[Display( Name = "Teléfono Principal" ) ]
        //[Required(ErrorMessage="Debe ingresar el teléfono")]
        //[StringLength(9, MinimumLength = 9, ErrorMessage = "Debe ingresar 9 dígitos")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe tener la sintaxis de un telefóno")]
        public string tlf1 { get; set; }

        //[Display( Name = "Teléfono Secundario" ) ]
        //[Remote("ValidaFonoNoRequerido","Hotel")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public string tlf2 { get; set; }

        //[Display( Name = "E-mail") ]
        //[Remote("ValidaEmail", "Hotel")]
        //[RegularExpression(@"([a-z0-9!#$%&'*+/=?^_`B|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|pe)\b){0,1}", ErrorMessage = "El email ingresado es incorrecto")]
        //Problema: Ver que la expresion regular pueda aceptar email vacio al no ser required
        public string email { get; set; }

        //[Display( Name = "Número de pisos" ) ]
        //[Required(ErrorMessage = "Debe indicar el número de pisos")]
        //[Range( 0 , 999, ErrorMessage = "El número de pisos es incorrecto")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "El valor ingresado debe ser un número positivo")]
        public int? nroPisos { get; set; }

        //[Display( Name = "Departamento" ) ]
        //[Required(ErrorMessage = "Debe seleccionar un departamento")]
        public int idDepartamento { get; set; }

        //[Display( Name = "Provincia" )]
        //[Required(ErrorMessage = "Debe seleccionar una provincia")]
        public int idProvincia { get; set; }

        //[Display( Name = "Distrito") ]
        //[Required(ErrorMessage = "Debe seleccionar un distrito")]
        public int idDistrito { get; set; }

        public bool estado { get; set; }
        
        //public string nombreDepartamento { get; set; }
        //public string nombreProvincia { get; set; }
        //public string nombreDistrito { get; set; }

        //public List<Departamento> Departamentos { get; set; }
        //public List<Provincia> Provincias { get; set; }
        //public List<Distrito> Distritos { get; set; }

        //public AlmacenBean Almacen { get; set; }
    }


    /*
     * Podria ser necesario que esta clase (u objeto) tambien necesite su ViewModelCreate, Details, Delete, pero eso se tiene que
     * hacer corrigiendo todos los que depende de este OJO!!
     */
    public class AlmacenBean
    {
        [Display(Name="Descripcion del Almacen")]
        public string descripcion { get; set; }

        [Display(Name = "Cantidad Máxima del Almacen")]
        [Required(ErrorMessage="Es necesario ingresar la cantidad máxima del almacen que contendra este hotel")]
        public int cantidad { get; set; }
    }
}