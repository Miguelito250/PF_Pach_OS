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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthApiController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ApplicationUser model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.PasswordHash))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { Message = "Inicio de sesión exitoso", User = user });
            }

            return Unauthorized(new { Message = "Credenciales incorrectas" });
        }
    }
}
