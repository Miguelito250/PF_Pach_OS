using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PF_Pach_OS.Models;
using PF_Pach_OS.Services;
using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.ExternalLoginModel;

namespace PF_Pach_OS.Controllers
{
    [Authorize]
    public class AspNetUsersController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        public readonly PermisosController _permisosController;
        private readonly IEmailSender _emailSender;
        public AspNetUsersController(Pach_OSContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _SignInManager = signInManager;
            _emailSender = emailSender;
            _permisosController = new PermisosController(_context, _userManager, _SignInManager);
        }
        [BindProperty]
        public InputModel Input { get; set; }

        // GET: AspNetUsers
        public async Task<IActionResult> Index()
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(8, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (_context.ApplicationUser != null)
            {
                var usuarios = await _context.ApplicationUser.ToListAsync();
                var roles = _context.Roles.ToList();
                ViewBag.roles = roles;

                return View(usuarios.ToList());
            }
            return View();

        }

        // GET: AspNetUsers/Details/5
        public async Task<IActionResult> Detalles(string id)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(8, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (id == null || _context.ApplicationUser == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.ApplicationUser.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            var permiso = await _context.Roles.ToListAsync();
            var permisoActivo = await _context.Roles.FindAsync(aspNetUser.Id_Rol);

            ViewBag.NombrePermisoActivo = permisoActivo.NomRol;

            ViewBag.Permisos = permiso;
            return View(aspNetUser);
        }




        // GET: AspNetUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(8, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (id == null || _context.ApplicationUser == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.ApplicationUser.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            var permiso = await _context.Roles.Where(p=> p.Estado != 0).ToListAsync();
            var permisoActivo = await _context.Roles.FindAsync(aspNetUser.Id_Rol);
            ViewBag.IdPermisoActivo = permisoActivo.IdRol;
            ViewBag.NombrePermisoActivo = permisoActivo.NomRol;

            ViewBag.Permisos = permiso;
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Email,DocumentNumber,DocumentType,FirstName,LastName,Id_Rol")] ApplicationUser applicationUser)
        {
            

            bool tine_permiso = _permisosController.tinto(8, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (id != applicationUser.Id)
            {
                return NotFound();
            }


            try
            {

                var user = await _userManager.FindByIdAsync(applicationUser.Id);

               

                user.DocumentNumber = applicationUser.DocumentNumber;
                user.DocumentType = applicationUser.DocumentType;
                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.Id_Rol = applicationUser.Id_Rol;

               

                if (user.Email != applicationUser.Email)
                {
                    user.EmailConfirmed = false;
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    user.EmailConfirmationToken = code;
                    var callbackUrl = Url.Action("ConfirmarCorreo", "Acceso", new { idUsuario = user.Id, codigo = code }, Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirma tu email",
                        $"Por favor confirma tu nuevo correo <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>presionando aqui</a>.");
                    user.Email = applicationUser.Email;
                }

                // Actualiza el usuario en la base de datos
                var result = await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(applicationUser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "AspNetUsers");
        }

        public IActionResult Deshabilitar(string id)
        {
            

            bool tine_permiso = _permisosController.tinto(8, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (id == null || _context.ApplicationUser == null)
            {
                return NotFound();
            }

            var aspNetUser = _context.ApplicationUser.Find(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            aspNetUser.State = 0;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        //Deshabilita un producto que este habilitado 
        public IActionResult Habilitar(string id)
        {
            bool tine_permiso = _permisosController.tinto(8, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (id == null || _context.ApplicationUser == null)
            {
                return NotFound();
            }

            var aspNetUser = _context.ApplicationUser.Find(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            aspNetUser.State = 1;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        private bool AspNetUserExists(string id)
        {
            return (_context.AspNetUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult VistaCambiarContraseña()
        {
            
            return View("CambiarContraseña");

        }

        [HttpPost]
        public async Task<bool> CambiarContraseña(string AntiguaContraseña, string NuevaContraseña)
        {

            var user = await _userManager.GetUserAsync(User);
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, AntiguaContraseña, NuevaContraseña);
            if (!changePasswordResult.Succeeded)
            {

                return false;
            }
            await _SignInManager.RefreshSignInAsync(user);


            return true;

        }
       
        public IActionResult ConfirmarCambiarContraseña()
        {
           
            return View("ConfirmarCambiarContraseña");
        }

        public async  Task<IActionResult> Rol(string idUsario)
        {

            
            var aspNetUser = await _context.ApplicationUser.FindAsync(idUsario);
            int? idrol_Usuario = aspNetUser.Id_Rol;
            var rol = _context.Roles.FirstOrDefault(p=> p.IdRol == idrol_Usuario);
            if(rol.Estado == 0)
            {
                return Json(true);
            }
            
            return Json(false);
        }

        public async Task<IActionResult> CorreoDuplicado(string Coreo)
        {
           
            var EsDuplicaddo = _context.ApplicationUser.Any(x => x.Email == Coreo);

            return Json(EsDuplicaddo);
        }

        public async Task<IActionResult> DocumentoDuplicado(string documento)
        {

            var EsDuplicaddo = _context.ApplicationUser.Any(x => x.DocumentNumber == documento);

            return Json(EsDuplicaddo);
        }
    }
}

