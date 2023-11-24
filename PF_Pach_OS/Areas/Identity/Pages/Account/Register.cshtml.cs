using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PF_Pach_OS.Models;
using PF_Pach_OS.Services;
using Microsoft.AspNetCore.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using NuGet.Packaging.Signing;
using PF_Pach_OS.Controllers;

namespace PF_Pach_OS.Areas.Identity.Pages.Account
{
    [Authorize]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly Pach_OSContext _contex;
        public readonly PermisosController _permisosController;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            Pach_OSContext contex)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _contex = contex;

            _permisosController = new PermisosController(_contex, _userManager, _signInManager);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El campo es obligatorio")]
            [Display(Name = "Tipo de documento *")]
            public string DocumentType { get; set; }

            [Required(ErrorMessage = "El campo es obligatorio")]
            [Display(Name = "N° documento *")]
            public string DocumentNumber { get; set; }

            [Required(ErrorMessage = "El campo es obligatorio")]
            [Display(Name = "Nombre *")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "El campo es obligatorio")]
            [Display(Name = "Apellido *")]
            public string LastName { get; set; }


            [Required(ErrorMessage = "El campo es obligatorio")]
            [Display(Name = "Dia de Entrada")]
            public DateTime EntryDay { get; set; }

            [Required(ErrorMessage = "El campo es obligatorio")]
            [EmailAddress(ErrorMessage = "Por favor, ingrese una dirección de correo electrónico válida.")]
            [Display(Name = "Correo *")]
            public string Email { get; set; }

            [Required(ErrorMessage = "El campo es obligatorio")]
            [StringLength(50, ErrorMessage = "La Contraseña debe tener al menos 8 caracteres y maximo 50", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña *")]
            public string Password { get; set; }

            [Required(ErrorMessage = "El campo es obligatorio")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "Las constraseñas no coinciden")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "El campo es obligatorio")]
            [Display(Name = "Rol *")]
            public string Role { get; set; }

            [ValidateNever]

            public IEnumerable<SelectListItem> RoleList { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            bool tine_permiso = _permisosController.tinto(8, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            Input = new InputModel()
            {
                RoleList = _contex.Roles
                 .Where(role => role.Estado != 0)
                .Select(role => new SelectListItem{
                    Text = role.NomRol,
                    Value = role.IdRol.ToString()
                }).ToList()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (!ModelState.IsValid)
            {
                int IdRol = 0; 
                if (!string.IsNullOrEmpty(Input.Role))
                {
                    if (int.TryParse(Input.Role, out int parsedId))
                    {
                        IdRol = parsedId;
                    }
                }
                DateTime Fecha_Entrada = DateTime.Today;
                var user = new ApplicationUser
                {
                    DocumentType = Input.DocumentType,
                    DocumentNumber = Input.DocumentNumber,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Email,
                    Email = Input.Email,                
                    State = 1,
                    EntryDay = Fecha_Entrada,
                    Id_Rol = IdRol

                };
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                user.EmailConfirmationToken = code;

                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                   

                    var callbackUrl = Url.Action("ConfirmarCorreo", "Acceso", new {idUsuario = user.Id, codigo = code}, Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirma tu email",
                        $"Por favor confirma tu cuenta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clickeando aqui</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction ("index" , "AspNetUsers");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError(string.Empty, "El correo electrónico ya está en uso.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            Input = new InputModel()
            {
                RoleList = _contex.Roles
                .Where(role => role.Estado != 0)
               .Select(role => new SelectListItem
               {
                   Text = role.NomRol,
                   Value = role.IdRol.ToString()
               }).ToList()
            };

            return Page();
        }
    }
}
