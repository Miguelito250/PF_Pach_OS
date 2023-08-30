using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PF_Pach_OS.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Compras = new HashSet<Compra>();
        }

        public int IdProveedor { get; set; }

        [CustomUniqueNit(ErrorMessage = "El NIT ya está registrado.")]
        public string? Nit { get; set; }
        public string? NomLocal { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
    public class CustomUniqueNitAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (Pach_OSContext)validationContext.GetService(typeof(Pach_OSContext));
            var nit = value as string;

            if (context.Proveedores.Any(p => p.Nit == nit))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}
