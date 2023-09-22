using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class Roles : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public Roles(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        
        public IActionResult Index()
        {
            var roles = _roleManager.Roles
                .ToList();

            Tuple<IdentityRole, List<IdentityRole>> lista = new(new IdentityRole(), roles);
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(IdentityRole modelo) 
        {
            if (!_roleManager.RoleExistsAsync(modelo.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(modelo.Name)).GetAwaiter().GetResult(); 
            }
            return RedirectToAction("Index");
        }

    }
}
