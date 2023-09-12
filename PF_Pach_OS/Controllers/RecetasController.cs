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
    public class RecetasController : Controller
    {
        private readonly Pach_OSContext _context;

        public RecetasController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: Recetas
        public async Task<IActionResult> Index()
        {
            var pach_OSContext = _context.Recetas.Include(r => r.IdInsumoNavigation).Include(r => r.IdProductoNavigation);
            return View(await pach_OSContext.ToListAsync());
        }

        // GET: Recetas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recetas == null)
            {
                return NotFound();
            }

            var receta = await _context.Recetas
                .Include(r => r.IdInsumoNavigation)
                .Include(r => r.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdReceta == id);
            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        // POST: Recetas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReceta,CantInsumo,IdProducto,IdInsumo")] Receta receta, [Bind("IdProducto,NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto)
        {
            bool exite = false;
            var recetaActiva = new Receta();
            
            _context.Update(producto);
            await _context.SaveChangesAsync();

            if (!ProductoExists(producto.IdProducto))
            {
                return NotFound();
            }
           
            var recetasExistente = _context.Recetas.ToList();
            foreach (var rec in recetasExistente)
            {
                if (rec.IdProducto == receta.IdProducto && rec.IdInsumo == receta.IdInsumo)
                {
                    recetaActiva = rec;
                    exite = true;
                    break;
                }
            }
            if (exite)
            {
                recetaActiva.CantInsumo += receta.CantInsumo;
                _context.Update(recetaActiva);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Productos", new { receta.IdProducto });
            }
            else
            {
                _context.Add(receta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Productos", new { receta.IdProducto });

            }
        }


        // GET: Recetas/Delete/5

        public async Task<IActionResult> Delete(int? id, int IdProducto)
        {
            if (_context.Recetas == null)
            {
                return Problem("Entity set 'Pach_OSContext.Recetas'  is null.");
            }
            var receta = await _context.Recetas.FindAsync(id);
            if (receta != null)
            {
                _context.Recetas.Remove(receta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Productos", new { IdProducto });
        }

        private bool RecetaExists(int id)
        {
            return (_context.Recetas?.Any(e => e.IdReceta == id)).GetValueOrDefault();
        }
        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
