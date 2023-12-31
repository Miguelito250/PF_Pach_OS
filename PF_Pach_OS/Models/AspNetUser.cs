﻿using System;
using System.Collections.Generic;

namespace PF_Pach_OS.Models
{
    public partial class AspNetUser
    {
        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Discriminator { get; set; } = null!;
        public string? DocumentNumber { get; set; }
        public string? DocumentType { get; set; }
        public DateTime? EntryDay { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? State { get; set; }
        public int? Id_Rol { get; set; }

        public virtual Role? IdRolNavigation { get; set; }
    }
}
