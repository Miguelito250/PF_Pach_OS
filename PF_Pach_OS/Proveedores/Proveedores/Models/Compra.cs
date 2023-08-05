using System;
using System.Collections.Generic;

namespace Proveedores.Models
{
    public partial class Compra
    {
        public Compra()
        {
            DetallesCompras = new HashSet<DetallesCompra>();
        }

        public int IdCompra { get; set; }
        public DateTime? FechaCompra { get; set; }
        public int? Total { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdProveedor { get; set; }

        public virtual Proveedore? IdProveedorNavigation { get; set; }
        public virtual ICollection<DetallesCompra> DetallesCompras { get; set; }
    }
}
