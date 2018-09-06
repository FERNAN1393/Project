using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SGAIJE.Models
{
    public class Cliente
    {
        public Guid GenClienteKey { get; set; }
        public Guid? GenTipoClienteLink { get; set; }
        [Display(Name = "Tipo Cliente")]
        public String DescripcionTipoCliente { get; set; }
        [Display(Name = "Nombre")]
        public String NombreCompleto { get; set; }
        [Required(ErrorMessage = "Falta agregar Nombre.")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "Falta agregar Apellido Paterno.")]
        [Display(Name ="Apellido Paterno")]
        public String ApellidoP { get; set; }
        [Required(ErrorMessage = "Falta agregar Apellido Materno.")]
        [Display(Name = "Apellido Materno")]
        public String ApellidoM { get; set; }
        public String RFC { get; set; }
        [Display(Name = "Domicilio")]
        public String DomicilioCompleto { get; set; }
        public String Calle { get; set; }
        //[Required(ErrorMessage = "Please enter how many Stream Entries are displayed per page.")]
        //[DataAnnotationsExtensions.Integer(ErrorMessage = "Please enter a valid number.")]
        [Display(Name = "No. Exterior")]
        public String NoExterior { get; set; }
        public String Colonia { get; set; }
        [Display(Name = "Codigo Postal")]
        public String CodigoPostal { get; set; }
        public String Telefono { get; set; }
        [Required(ErrorMessage = "Falta agregar Correo Electronico")]
        public String Correo { get; set; }
        [Required(ErrorMessage = "Falta seleccionar Tipo de Cliente.")]
        public virtual TipoCliente TipoCliente { get; set; }

    }
}