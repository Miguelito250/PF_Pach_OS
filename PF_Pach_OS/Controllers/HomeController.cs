using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;
using System.Diagnostics;

namespace PF_Pach_OS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly ILogger<HomeController> _logger;
        public readonly PermisosController _permisosController;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, Pach_OSContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _permisosController = new PermisosController(_context, _userManager, _signInManager);
        }
        public async Task <IActionResult> Index()
        {
            var rolUsuario = _userManager.GetUserAsync(User).Result.Id_Rol;

            var permisos = await _context.RolPermisos
                .Where(r => r.IdRol == rolUsuario)
                .ToListAsync();
            for (int i = 1; i <= permisos.Count(); i ++) 
            {
                int recorrer = (int)permisos[i].IdPermiso;
                if (recorrer == 1)
                {
                    return RedirectToAction("Index", "Estadisticas");
                }
                else if (recorrer == 2)
                {
                    return RedirectToAction("Index", "Ventas");
                }
                else if (recorrer == 3)
                {
                    return RedirectToAction("Index", "Productos");
                }
                else if (recorrer == 4)
                {
                    return RedirectToAction("Index", "Insumos");
                }
                else if (recorrer == 5)
                {
                    return RedirectToAction("Index", "Compras");
                }
                else if (recorrer == 6)
                {
                    return RedirectToAction("Index", "Proveedores");
                }
                else if (recorrer == 7)
                {
                    return RedirectToAction("Index", "RolPermisos");
                }
                else if (recorrer == 8)
                {
                    return RedirectToAction("Index", "AspNetUsers");
                }
            }


            return Content("No asigno ningun permiso");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}