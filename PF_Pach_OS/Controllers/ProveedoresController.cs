using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;


namespace Pach_OS.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly Pach_OSContext _context;

        public ProveedoresController(Pach_OSContext context)
        {
            _context = context;
        }

    
        public async Task<IActionResult> Index()
        {
              return _context.Proveedores != null ?
                          View(await _context.Proveedores.OrderByDescending(p => p.IdProveedor).ToListAsync()) :
                          Problem("Entity set 'Pach_OSContext.Proveedores'  is null.");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }


        public IActionResult Create()
        {
            return View();
        }


        public IActionResult CheckNitAvailability(string nit)
        {
            bool isAvailable = !_context.Proveedores.Any(p => p.Nit == nit);
            return Json(isAvailable);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProveedor,Nit,NomLocal,Direccion,Telefono,Correo,TipoDocumento")] Proveedore proveedore)
        {

            if (ModelState.IsValid)
            {
                if (proveedore.TipoDocumento == "NIT")
                {
                    var proveedorConNit = new Proveedore
                    {
                        Nit = proveedore.Nit,
                        NomLocal = proveedore.NomLocal,
                        Direccion = proveedore.Direccion,
                        Telefono = proveedore.Telefono,
                        Correo = proveedore.Correo,
                        TipoDocumento = proveedore.TipoDocumento,
                        Estado = 1
                    };
                    _context.Add(proveedorConNit);
                }
                else if (proveedore.TipoDocumento == "Cédula")
                {
                    var proveedorConCedula = new Proveedore
                    {
                        Nit = proveedore.Nit, 
                        NomLocal = proveedore.NomLocal,
                        Direccion = proveedore.Direccion,
                        Telefono = proveedore.Telefono,
                        Correo = proveedore.Correo,
                        TipoDocumento = proveedore.TipoDocumento,
                        Estado = 1
                    };
                    _context.Add(proveedorConCedula);
                }
                TempData["SuccessMessage"] = "Proveedor creado exitosamente";
                await _context.SaveChangesAsync();
                return Content("success");
            }
            return Content("success");
        }

        [HttpPost]
        public IActionResult IsNitAvailable(string Nit)
        {
            bool isAvailable = !_context.Proveedores.Any(p => p.Nit == Nit);
            return Json(isAvailable);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore == null)
            {
                return NotFound();
            }
            return View(proveedore);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProveedor,NomLocal,Direccion,Telefono,Correo,TipoDocumento,Nit")] Proveedore proveedore)
        {
            if (id != proveedore.IdProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingProveedor = await _context.Proveedores.FindAsync(id);
                    if (existingProveedor != null)
                    {
                        existingProveedor.NomLocal = proveedore.NomLocal;
                        existingProveedor.Direccion = proveedore.Direccion;
                        existingProveedor.Telefono = proveedore.Telefono;
                        existingProveedor.Correo = proveedore.Correo;
                        if (existingProveedor.TipoDocumento != proveedore.TipoDocumento)
                        {
                            existingProveedor.TipoDocumento = proveedore.TipoDocumento;

                            existingProveedor.Nit = proveedore.Nit;
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoreExists(proveedore.IdProveedor))
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
            return View(proveedore);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'Pach_OSContext.Proveedores'  is null.");
            }
            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore != null)
            {
                _context.Proveedores.Remove(proveedore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoreExists(int id)
        {
          return (_context.Proveedores?.Any(e => e.IdProveedor == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult Disable(int id)
        {
            var proveedor = _context.Proveedores.Find(id);
            if (proveedor != null)
            {
                proveedor.Estado = 1; 
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Enable(int id)
        {
            var proveedor = _context.Proveedores.Find(id);
            if (proveedor != null)
            {
                proveedor.Estado = 0; 
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }



    }
}
