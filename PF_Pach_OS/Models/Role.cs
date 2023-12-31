﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PF_Pach_OS.Models
{
    public partial class Role
    {
        public Role()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            RolPermisos = new HashSet<RolPermiso>();
        }

        public int IdRol { get; set; }
        public string? NomRol { get; set; }
        
        public byte? Estado { get; set; }
        
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<RolPermiso> RolPermisos { get; set; }
    }
}
