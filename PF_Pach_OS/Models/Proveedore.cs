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
        [Display(Name = "Tipo de Documento")]
        public String? TipoDocumento { get; set; }
        public int IdProveedor { get; set; }
        public string? Nit { get; set; }
        public string? NomLocal { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
