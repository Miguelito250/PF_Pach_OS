using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Inicializador
{
    public class DBInicializar : Inicializador
    {
        private readonly Pach_OSContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DBInicializar(Pach_OSContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void Inicializar()
        {
            try
            {
                if(_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }
            string superAdmin = "pachitoche2501259@gmail.com";
            if (_context.ApplicationUser.Any(a => a.Email == superAdmin)) return;

            var usuarioNuevo = new ApplicationUser {
                Email = superAdmin,
                UserName = superAdmin,
                EmailConfirmed = true,
                DocumentType = "NIT",
                DocumentNumber = "1152706792",
                FirstName = "Pachito-ché",
                LastName = "",
                State = 1,
                Id_Rol = 1
            };

            _userManager.CreateAsync(usuarioNuevo, "Admin123.").GetAwaiter().GetResult();
        }
    }
    public interface Inicializador
    {
        void Inicializar();
        
    }
}
