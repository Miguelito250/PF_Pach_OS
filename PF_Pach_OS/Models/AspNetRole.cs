﻿using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class AspNetRole
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
