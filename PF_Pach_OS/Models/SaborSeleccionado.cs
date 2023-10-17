using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class SaborSeleccionado
    {
        public int? IdSaborSeleccionado { get; set; }
        public int? IdProducto { get; set; }
        public int? IdDetalleVenta { get; set; }

        public virtual DetalleVenta? IdDetalleVentaNavigation { get; set; }
        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
