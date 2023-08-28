using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{

    public class VentasController : Controller
    {
        private readonly Pach_OSContext _context;

        public VentasController(Pach_OSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto");

            var pach_OSContext = _context.DetalleVentas
                .Include(d => d.IdVentaNavigation)
                .Include(d => d.IdProductoNavigation);
            return View(await pach_OSContext.ToListAsync());
        }
        
        public IActionResult Crear(int IdVenta)
        {
            var DetallesVentas = _context.DetalleVentas
                .Where(d => d.IdVenta == IdVenta)
                .ToList();

            ViewBag.IdVenta = IdVenta;
            ViewData["DetallesVentas"] = DetallesVentas;
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto");

            Tuple<DetalleVenta, Venta> Venta_Detalle = new(new DetalleVenta(), new Venta());
            return View(Venta_Detalle);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearVenta([Bind("IdVenta")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();

                return RedirectToAction("Crear", "Ventas", new { venta.IdVenta});
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", venta.IdEmpleado);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarVenta([Bind("IdVenta,FechaVenta,TotalVenta,TipoPago,Pago,PagoDomicilio,IdEmpleado,Estado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    venta.Estado = "Pendiente";
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVenta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Ventas");
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", venta.IdEmpleado);
            return View(venta);
        }

        public async Task<IActionResult> DetallesVentas(int? IdVenta)
        {
            if (IdVenta == null)
            {
                return NotFound();
            }

            var detallesVentas = await _context.Ventas
                .FirstOrDefaultAsync(v => v.IdVenta == IdVenta);

            if (detallesVentas == null)
            {
                return NotFound();
            }
            return View(detallesVentas);
            
        }

        public async Task<IActionResult> CambiarEstado(int IdVenta)
        {
            var cambioEstado = "";

            var estadoVenta = _context.Ventas
                .FirstOrDefault(d => d.IdVenta == IdVenta);

            if (estadoVenta.Estado == "Pendiente")
            {
                cambioEstado = "Entregado";
            }
            else
            {
                cambioEstado = "Pendiente";
            }

            estadoVenta.Estado = cambioEstado;
            _context.Update(estadoVenta); 
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ventas");
        }
        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }
    }
}
