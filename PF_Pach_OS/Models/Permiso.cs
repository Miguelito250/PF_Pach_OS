using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class Permiso
    {
        public Permiso()
        {
            RolPermisos = new HashSet<RolPermiso>();
        }

        public int IdPermiso { get; set; }
        public string? NomPermiso { get; set; }

        public virtual ICollection<RolPermiso> RolPermisos { get; set; }
    }
}
