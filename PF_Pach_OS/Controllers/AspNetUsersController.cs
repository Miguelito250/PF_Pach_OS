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
    public class AspNetUsersController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AspNetUsersController(Pach_OSContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AspNetUsers
        public async Task<IActionResult> Index()
        {

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
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AspNetUser == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }




        // GET: AspNetUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
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
            if (id != applicationUser.Id)
            {
                return NotFound();
            }


            try
            {
              
                var user = await _userManager.FindByIdAsync(applicationUser.Id);


                user.Email = applicationUser.Email;
                user.DocumentNumber = applicationUser.DocumentNumber;
                user.DocumentType = applicationUser.DocumentType;
                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.Id_Rol = applicationUser.Id_Rol;

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

        // GET: AspNetUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AspNetUser == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AspNetUser == null)
            {
                return Problem("Entity set 'Pach_OSContext.AspNetUser'  is null.");
            }
            var aspNetUser = await _context.AspNetUser.FindAsync(id);
            if (aspNetUser != null)
            {
                _context.AspNetUser.Remove(aspNetUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserExists(string id)
        {
            return (_context.AspNetUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
