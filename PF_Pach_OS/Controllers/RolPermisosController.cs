// Autor: Juan Andres Navarro Herrera
// 09/10/2023
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class RolPermisosController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        public readonly PermisosController _permisosController;




        public RolPermisosController(Pach_OSContext context, UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignInManager)
        {
            _context = context;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
            _permisosController = new PermisosController(_context, _UserManager, _SignInManager);
        }


       

        // GET: RolPermisos
        public async Task<IActionResult> Index()
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(7, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        // GET: RolPermisos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(7, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (id == null || _context.RolPermisos == null)
            {
                return NotFound();
            }

            var rolPermiso = await _context.RolPermisos
                .Include(r => r.IdPermisoNavigation)
                .Include(r => r.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdRolPermisos == id);
            if (rolPermiso == null)
            {
                return NotFound();
            }

            return View(rolPermiso);
        }

        // GET: RolPermisos/Create
        public IActionResult Crear()
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(7, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            var permisos = _context.Permisos.ToList();


            var listarPermisos = permisos.Select(permisos => new
            {
                nom_permiso = permisos.NomPermiso,
                id_permiso = permisos.IdPermiso,

            }).ToList();
            


            ViewBag.Permisos = listarPermisos.Cast<object>().ToList();

            return View("Create");
        }



        [HttpPost]
        private async void Crear(List<int> permisos, String nomRol)
        {
            var user = User;

           

            int id_Rol = 0;
            if (nomRol != null)
            {
                Role nuevo_Rol = new Role();
                nuevo_Rol.NomRol = nomRol;
                _context.Roles.Add(nuevo_Rol);
                _context.SaveChanges();
               

            }
            Role rol = _context.Roles.FirstOrDefault(p => p.NomRol == nomRol);
            if (rol != null)
            {
                id_Rol = rol.IdRol;
            }

            for (int i = 0; i < permisos.Count; i++)
            {
                

                var rolPermiso = new RolPermiso();
                rolPermiso.IdPermiso = permisos[i];
                rolPermiso.IdRol = id_Rol;
                _context.RolPermisos.Add(rolPermiso);

                _context.SaveChanges();

            }
            var datos = new { Nombre = "Ejemplo", Edad = 30 };
            
        }
        //Se crea el rol y se le asignan los permisos
        private async void Crear_rol(List<int> permisos, Role rol)
        {
            if (rol != null)
            {
                _context.Roles.Add(rol);
            }

            await _context.SaveChangesAsync();
        }
        // GET: RolPermisos/Edit/5
        public IActionResult Editar(int? id)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(7, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (id == null || _context.RolPermisos == null)
            {
                return NotFound();
            }

            var rolPermiso = _context.RolPermisos.Where(p=> p.IdRol== id).ToList();
            var rol = _context.Roles.FirstOrDefault(p => p.IdRol == id);
            if (rolPermiso == null)
            {
                return NotFound();
            }
            var permisos = _context.Permisos.ToList();
            var listarPermisos = permisos.Select(permisos => new
            {
                nom_permiso = permisos.NomPermiso,
                id_permiso = permisos.IdPermiso,

            }).ToList();
            ViewBag.rol = rol;
            ViewBag.PermisosUsados = rolPermiso.Cast<object>().ToList();
            ViewBag.Permisos = listarPermisos.Cast<object>().ToList();


            return View();
        }

        
        [HttpPost]
        private async void Editar(int id, List<int> permisos, String nomRol)
        {
            Console.WriteLine("=============================");
            Console.WriteLine("Entrada 1");
            Console.WriteLine("=============================");

            var rol = _context.Roles.Where(p=> p.IdRol== id).ToList();
            if (rol != null) {
                rol[0].NomRol = nomRol;
                _context.Roles.Update(rol[0]);
                _context.SaveChanges();
                Console.WriteLine("=============================");
                Console.WriteLine("Entrada 2");
                Console.WriteLine("=============================");

            }
            var permisos_Desactualizados = _context.RolPermisos.Where(p=> p.IdRol== id).ToList();
            foreach(var permiso_desactualizado in permisos_Desactualizados)
            {
                Console.WriteLine("=============================");
                Console.WriteLine("Entrada 3: " + permiso_desactualizado);
                Console.WriteLine("=============================");
                _context.RolPermisos.Remove(permiso_desactualizado);
                _context.SaveChanges();

            }
            foreach (var permiso in permisos)
            {
                Console.WriteLine("=============================");
                Console.WriteLine("Entrada 4: " + permiso);
                Console.WriteLine("=============================");
                RolPermiso rolPermiso = new RolPermiso();
                rolPermiso.IdRol = id;
                rolPermiso.IdPermiso = permiso;
                _context.RolPermisos.Add(rolPermiso);
                _context.SaveChanges();

            }
     
        }

        // GET: RolPermisos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(7, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (id == null || _context.RolPermisos == null)
            {
                return NotFound();
            }

            var rolPermiso = await _context.RolPermisos
                .Include(r => r.IdPermisoNavigation)
                .Include(r => r.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdRolPermisos == id);
            if (rolPermiso == null)
            {
                return NotFound();
            }

            return View(rolPermiso);
        }

        // POST: RolPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(7, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (_context.RolPermisos == null)
            {
                return Problem("Entity set 'Pach_OSContext.RolPermisos'  is null.");
            }
            var rolPermiso = await _context.RolPermisos.FindAsync(id);
            if (rolPermiso != null)
            {
                _context.RolPermisos.Remove(rolPermiso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolPermisoExists(int id)
        {
            return (_context.RolPermisos?.Any(e => e.IdRolPermisos == id)).GetValueOrDefault();
        }
    }
}
