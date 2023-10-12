using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class DetalleVenta
    {
        public DetalleVenta()
        {
            SaboresSeleccionados = new HashSet<SaboresSeleccionados>();
        }
        public int IdDetalleVenta { get; set; }
        public int? CantVendida { get; set; }
        public int? Precio { get; set; }
        public int? IdVenta { get; set; }
        public int? IdProducto { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Venta? IdVentaNavigation { get; set; }
        public virtual ICollection<SaboresSeleccionados> SaboresSeleccionados { get; set; }
    }
}
