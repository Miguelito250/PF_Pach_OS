using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class DetalleVentasController : Controller
    {
        private readonly Pach_OSContext _context;

        public DetalleVentasController(Pach_OSContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Funcion para ir agregando los detalles de venta a la factura
        public async Task<IActionResult> AgregarDetalle([Bind("CantVendida,Precio,IdVenta,IdProducto")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                
                var precioProducto = _context.Productos
                    .Where(detalle => detalle.IdProducto == detalleVenta.IdProducto)
                    .Select(detalle => detalle.PrecioVenta)
                    .FirstOrDefault();
                detalleVenta.Precio = precioProducto;

                
                var productoExistente = _context.DetalleVentas
                    .Where(detalle => detalle.IdVenta == detalleVenta.IdVenta && detalle.IdProducto == detalleVenta.IdProducto)
                    .FirstOrDefault();

                if (productoExistente == null)
                {
                    _context.Add(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var detalleActualizar = _context.DetalleVentas.Find(productoExistente.IdDetalleVenta);
                    if (detalleActualizar == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        detalleActualizar.CantVendida += detalleVenta.CantVendida;
                    }

                    _context.Update(detalleActualizar);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Crear", "Ventas", new { detalleVenta.IdVenta });
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto", detalleVenta.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", detalleVenta.IdVenta);
            return View(detalleVenta);
        }

        public async Task<object> ConsultarMaximoSabores(byte IdProducto)
        {
            if (IdProducto == null)
            {
                return NotFound();
            }
            var consultarMaximoSabores = await _context.Tamanos.FindAsync(IdProducto);

            return consultarMaximoSabores;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var detalleVentas = await _context.DetalleVentas.FindAsync(id);
            _context.DetalleVentas.Remove(detalleVentas);
            await _context.SaveChangesAsync();
            return RedirectToAction("Crear", "Ventas", new { detalleVentas.IdVenta });
        }

        private bool DetalleVentaExists(int id)
        {
            return (_context.DetalleVentas?.Any(e => e.IdDetalleVenta == id)).GetValueOrDefault();
        }
    }
}
