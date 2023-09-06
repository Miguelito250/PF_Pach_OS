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
    public class ProductosController : Controller
    {
        private readonly Pach_OSContext _context;

        public ProductosController(Pach_OSContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var pach_OSContext = await _context.Productos.ToListAsync();
            foreach (var pach in pach_OSContext)
            {
                if(pach.NomProducto == null)
                {
                    return RedirectToAction("Details", "Productos", new { pach.IdProducto, accion = "Create" });
                }
                if (pach.PrecioVenta == null)
                {
                    return RedirectToAction("Details", "Productos", new { pach.IdProducto, accion = "Create" });

                }
                if (pach.IdCategoria == null)
                {
                    return RedirectToAction("Details", "Productos", new { pach.IdProducto, accion = "Create" });

                }
                if (pach.IdCategoria == 1)
                {
                    if (pach.IdTamano == null)
                    {
                        return RedirectToAction("Details", "Productos", new { pach.IdProducto, accion = "Create" });

                    }
                }
            }

            
            return View(Enumerable.Reverse(pach_OSContext).ToList());
        }

        // GET: Productos/Details/5
        

        // GET: Productos/Create
        public void ProductoActivo (int id)
        {
            var productoActivo = _context.Productos.FirstOrDefault(p => p.IdProducto == id);

            if (productoActivo != null)
            {
                var categoriaActiva = _context.Categorias.FirstOrDefault(p => p.IdCategoria == productoActivo.IdCategoria);
                var tamanoActivo = _context.Tamanos.FirstOrDefault(p => p.IdTamano == productoActivo.IdTamano);

                ViewBag.ProductoActivo = productoActivo;
                if (categoriaActiva != null)
                {


                    ViewBag.SelectC = categoriaActiva.NomCategoria;
                    ViewBag.SelectCID = categoriaActiva.IdCategoria;
                }
                if (tamanoActivo != null)
                {
                    ViewBag.SelectT = tamanoActivo.NombreTamano;
                    ViewBag.SelectTID = tamanoActivo.IdTamano;
                }

            }

        }
        public IActionResult Details(int IdProducto, string accion)
        {
            if(accion != null)
            {
                ViewBag.Cancelar = 1;
            }
            else
            {
                ViewBag.Cancelar = 2;
            }
            ProductoActivo(IdProducto);
            var recetas = _context.Recetas.ToList();
            var insumos = _context.Insumos.ToList();

            var recetasConInsumos = recetas.Select(receta => new
            {
                IdReceta= receta.IdReceta,
                CantInsumo = receta.CantInsumo,
                NomInsumo = insumos.FirstOrDefault(i => i.IdInsumo == receta.IdInsumo)?.NomInsumo,
                IdProducto = receta.IdProducto
            }).ToList();

            ViewBag.RecetasConInsumos = recetasConInsumos.Cast<object>().ToList();
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NomCategoria");
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "NombreTamano");
            ViewBag.NombreTamano = new SelectList(_context.Tamanos, "IdTamano", "NombreTamano");
            ViewBag.Insumo = new SelectList(_context.Insumos, "IdInsumo", "NomInsumo");
            ViewBag.IdProducto = IdProducto;
            return View("Create");
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {   
                _context.Add(producto);
                await _context.SaveChangesAsync();

                // Redirige a la acción "Crear" con el IdProducto como parámetro en la URL
                ViewBag.IdProducto = producto.IdProducto;
                return RedirectToAction("Details", "Productos", new {producto.IdProducto, accion="Create" });
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NomCategoria");
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "NombreTamano");

            

            ViewBag.Insumo = new SelectList(_context.Insumos, "IdInsumo", "NomInsumo");
            return NotFound();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("IdProducto, NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto)
        {
            
            if (ModelState.IsValid)
            {

               
                    if (producto.Estado == 0)
                    {
                        producto.Estado = 0;
                    }
                    else
                    {
                        producto.Estado = 1;
                    }
                

                _context.Update(producto);
                await _context.SaveChangesAsync();
                ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", producto.IdCategoria);
                ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "nombre_tamano", producto.IdTamano);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Details", "Productos", new { producto.IdProducto });
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "IdTamano", producto.IdTamano);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "IdTamano", producto.IdTamano);
            return View(producto);
        }

    
        [HttpPost]
        public IActionResult Disable(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                producto.Estado = 1; // Deshabilitado
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Enable(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                producto.Estado = 0; // Habilitado
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        

        public async Task<IActionResult> Cancelar(int IdProducto)
        {
            if (_context.Recetas == null)
            {
                return Problem("Entity set 'Pach_OSContext.Recetas'  is null.");
            }
            var receta = await _context.Recetas
                .Where(r => r.IdProducto == IdProducto)
                .ToListAsync();
            if (receta != null)
            {
                foreach (var rec in receta)
                {
                    _context.Recetas.Remove(rec);

                }
            }
            var producto = await _context.Productos.FindAsync(IdProducto);
            if (producto != null) {
                _context.Productos.Remove(producto);
            }
            
            

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
