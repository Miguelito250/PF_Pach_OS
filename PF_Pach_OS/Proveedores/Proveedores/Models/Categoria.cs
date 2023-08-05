using System;
using System.Collections.Generic;

namespace Proveedores.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public byte IdCategoria { get; set; }
        public string NomCategoria { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
