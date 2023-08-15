using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PF_Pach_OS.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Compras = new HashSet<Compra>();
        }
        public int IdProveedor { get; set; }
        public string? Nit { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? NomLocal { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Correo { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
