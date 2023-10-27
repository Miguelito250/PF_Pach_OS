using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace PF_Pach_OS.Services
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ControlDeAccesos : AuthorizeAttribute, IAuthorizationFilter
    {

        private  SignInManager<ApplicationUser> SignInManager;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Pach_OSContext _context;





        private int idpermiso;

        public ControlDeAccesos(int idpermiso = 0)
        {
            this.idpermiso = idpermiso;
        }

        public ControlDeAccesos( UserManager<ApplicationUser> userManager, Pach_OSContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            
            _UserManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sesion = context.HttpContext.User;
            var nombre = sesion.Identity.Name;
            //var usuario = _context.ApplicationUser;
            //var producto = _context.Productos.ToList();
            if(idpermiso == 1) {
                Console.WriteLine("===========================================");
                Console.WriteLine("Entrada denegada");
                Console.WriteLine("===========================================");

            }




        }
    }
}