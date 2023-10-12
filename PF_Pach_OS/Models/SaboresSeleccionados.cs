using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PF_Pach_OS.Models
{
    public partial class SaboresSeleccionados
    {
        [Key]
        public int? IdSaborSeleccionado { get; set; }
        public int? IdProducto { get; set; }
        public int? IdDetalleVenta { get; set; }
        public virtual DetalleVenta? DetalleVentaNavigation { get; set; }
        public virtual Producto? ProductoNavigation { get; set; }
    }

}
