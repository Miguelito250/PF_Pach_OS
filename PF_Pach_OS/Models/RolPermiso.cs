using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class RolPermiso
    {
        public int IdRolPermisos { get; set; }
        public int? IdRol { get; set; }
        public int? IdPermiso { get; set; }

        public virtual Permiso? IdPermisoNavigation { get; set; }
        public virtual Role? IdRolNavigation { get; set; }
    }
}
