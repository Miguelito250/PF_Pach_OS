// Autor: Juan Andres Navarro Herrera
// 09/10/2023
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class RolPermisosController : Controller
    {
        private readonly Pach_OSContext _context;

        public RolPermisosController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: RolPermisos
        public async Task<IActionResult> Index()
        {
            var pach_OSContext = _context.RolPermisos.Include(r => r.IdPermisoNavigation).Include(r => r.IdRolNavigation);
            return View(await pach_OSContext.ToListAsync());
        }

        // GET: RolPermisos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
        public IActionResult Create()
        {
            var permisos = _context.Permisos.ToList();
            

            var listarPermisos = permisos.Select(permisos => new
            {
                nom_permiso = permisos.NomPermiso,
                id_permiso = permisos.IdPermiso,
               
            }).ToList();

            Console.WriteLine("======================================================================");
            Console.WriteLine(listarPermisos);
            Console.WriteLine("======================================================================");
            ViewBag.Permisos = listarPermisos.Cast<object>().ToList();

               

            ViewData["IdPermiso"] = new SelectList(_context.Permisos, "IdPermiso", "IdPermiso");
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol");
            return View();
        }

        // POST: RolPermisos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRolPermisos,IdRol,IdPermiso")] RolPermiso rolPermiso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolPermiso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPermiso"] = new SelectList(_context.Permisos, "IdPermiso", "IdPermiso", rolPermiso.IdPermiso);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", rolPermiso.IdRol);
            return View(rolPermiso);
        }

        // GET: RolPermisos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RolPermisos == null)
            {
                return NotFound();
            }

            var rolPermiso = await _context.RolPermisos.FindAsync(id);
            if (rolPermiso == null)
            {
                return NotFound();
            }
            ViewData["IdPermiso"] = new SelectList(_context.Permisos, "IdPermiso", "IdPermiso", rolPermiso.IdPermiso);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", rolPermiso.IdRol);
            return View(rolPermiso);
        }

        // POST: RolPermisos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRolPermisos,IdRol,IdPermiso")] RolPermiso rolPermiso)
        {
            if (id != rolPermiso.IdRolPermisos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolPermiso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolPermisoExists(rolPermiso.IdRolPermisos))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPermiso"] = new SelectList(_context.Permisos, "IdPermiso", "IdPermiso", rolPermiso.IdPermiso);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", rolPermiso.IdRol);
            return View(rolPermiso);
        }

        // GET: RolPermisos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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
