using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    [AllowAnonymous]
    public class AuthApiController : ControllerBase
    {
        private readonly Pach_OSContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public readonly PermisosController _permisosController;


        public AuthApiController(Pach_OSContext context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _permisosController = new PermisosController(_context, _userManager, _signInManager);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ApplicationUser model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return Unauthorized(new { Message = "Credenciales incorrectas" });
            }
            bool permisoInformes = _permisosController.tintoMovil(1, model.UserName);
            bool permisoVentas = _permisosController.tintoMovil(2, model.UserName);
            bool permisoCompras = _permisosController.tintoMovil(5, model.UserName);
            if (user.State != 1)
            {
                return Unauthorized(new { Message = "Usuario Deshabilitado" });
            }
            else if (permisoInformes != true && permisoVentas != true && permisoCompras != true)
            {
                return Unauthorized(new { Message = "Su rol no tiene suficientes permisos para ingresar" });
            }
            else if (user != null && await _userManager.CheckPasswordAsync(user, model.PasswordHash))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { Message = "Inicio de sesión exitoso"});
            }

            return Unauthorized(new { Message = "Credenciales incorrectas" });
        }
    }
}
