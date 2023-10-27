using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using PF_Pach_OS.Models;
using System.Text;

namespace PF_Pach_OS.Controllers
{
    public class AccesoController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Pach_OSContext _context;
        public AccesoController(UserManager<IdentityUser> userManager, Pach_OSContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> ConfirmarCorreo(string idUsuario, string codigo)
        {
            if (idUsuario == null || codigo == null)
            {
                return RedirectToPage("/Index");
            }

            var usuario = _context.ApplicationUser
                .FirstOrDefault(u => u.Id == idUsuario);

            if(usuario == null)
            {
                return Json("No se encontro el usuario");
            }

            var token = usuario.EmailConfirmationToken;

            if(codigo == token)
            {
                usuario.EmailConfirmed = true;
                await _userManager.UpdateAsync(usuario);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
