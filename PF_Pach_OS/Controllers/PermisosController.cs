using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Versioning;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{

    
    public class PermisosController : Controller
    {
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly Pach_OSContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;


        public PermisosController(Pach_OSContext context , UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignInManager)
        {
            _context = context;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
        }

        public bool tinto(int permiso, ClaimsPrincipal User)
        {
            var user = User;
            var Id_rol = _UserManager.GetUserAsync(User).Result.Id_Rol;
            var permisos_rol = _context.RolPermisos.Where(p=> p.IdRol == Id_rol).ToList();
            foreach(var permiso_rol in permisos_rol)
            {
                if(permiso_rol.IdPermiso == permiso)
                {
                    return true;
                    
                }
            }

            return false;
        }

        public bool tintoMovil(int permiso, string email)
        {
            var user = User;
            var Id_rol = _UserManager.FindByEmailAsync(email).Result.Id_Rol;
            var permisos_rol = _context.RolPermisos.Where(p => p.IdRol == Id_rol).ToList();
            foreach (var permiso_rol in permisos_rol)
            {
                if (permiso_rol.IdPermiso == permiso)
                {
                    return true;

                }
            }

            return false;
        }
    }
}
