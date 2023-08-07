﻿using System;
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

            var pach_OSContext = _context.Ventas.Include(v => v.IdEmpleadoNavigation);
            return View(await pach_OSContext.ToListAsync());
        }
        
        public IActionResult Crear(int IdVenta)
        {
            var DetallesVentas = _context.DetalleVentas
                .Where(d => d.IdVenta == IdVenta)
                .ToList();

            ViewData["DetallesVentas"] = DetallesVentas;

            ViewBag.IdVenta = IdVenta;
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta")] Venta venta)
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
        public async Task<IActionResult> ConfirmarVenta([Bind("IdVenta,FechaVenta,TotalVenta,TipoPago,Pago,PagoDomicilio,IdEmpleado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction("Index", "Ventas", new {venta.IdVenta});
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", venta.IdEmpleado);
            return View(venta);
        }

        private bool VentaExists(int id)
        {
          return (_context.Ventas?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }
    }
}
