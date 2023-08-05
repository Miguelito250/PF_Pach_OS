using System;
using System.Collections.Generic;

namespace Proveedores.Models
{
    public partial class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int? CantVendida { get; set; }
        public int? Precio { get; set; }
        public int? IdVenta { get; set; }
        public int? IdProducto { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
        public virtual Venta? IdVentaNavigation { get; set; }
    }
}
