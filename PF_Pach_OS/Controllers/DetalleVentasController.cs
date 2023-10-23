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

        //Miguel 22/10/2023: Funcion para ir agregando los detalles de venta a la factura
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AgregarDetalle([Bind("CantVendida,Precio,IdVenta,IdProducto,Estado")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                if (detalleVenta.CantVendida <= 0 || detalleVenta.IdProducto == 0)
                {
                    return RedirectToAction("Crear", "Ventas", new { detalleVenta.IdVenta });
                }

                bool insumosSuficientes = InsumosSuficientes(detalleVenta.IdProducto, detalleVenta.CantVendida);

                if (!insumosSuficientes)
                {
                    return RedirectToAction("Crear", "Ventas", new { detalleVenta.IdVenta });
                }
                else
                {

                    var precioProducto = _context.Productos
                        .Where(detalle => detalle.IdProducto == detalleVenta.IdProducto)
                        .Select(detalle => detalle.PrecioVenta)
                        .FirstOrDefault();

                    detalleVenta.Precio = precioProducto;
                    detalleVenta.Estado = "Sin Descontar";


                    var productoExistente = _context.DetalleVentas
                        .FirstOrDefault(detalle => detalle.IdVenta == detalleVenta.IdVenta && detalle.IdProducto == detalleVenta.IdProducto && detalle.Estado != "Descontado");

                    //var productoDescontado = _context.DetalleVentas
                    //    .FirstOrDefault(d => d.IdVenta == detalleVenta.IdVenta && d.IdProducto == detalleVenta.IdProducto);

                    if (productoExistente == null || productoExistente.Estado == "Descontado")
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
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto", detalleVenta.IdProducto);
            ViewData["IdVenta"] = new SelectList(_context.Ventas, "IdVenta", "IdVenta", detalleVenta.IdVenta);
            return RedirectToAction("Crear", "Ventas", new { detalleVenta.IdVenta });
        }

        //Miguel 22/10/2023: Función para consultar con AJAX el maximo de sabores de una pizza
        public async Task<object> ConsultarMaximoSabores(byte IdProducto)
        {
            if (IdProducto == null)
            {
                return NotFound();
            }
            var consultarMaximoSabores = await _context.Tamanos.FindAsync(IdProducto);

            return consultarMaximoSabores;
        }

        //Miguel 22/10/2023: Función para eliminar un detalle de venta en especifico
        public async Task<IActionResult> EliminarDetalle(int? id)
        {
            var detalleVentas = await _context.DetalleVentas.FindAsync(id);
            _context.DetalleVentas.Remove(detalleVentas);
            await _context.SaveChangesAsync();
            return RedirectToAction("Crear", "Ventas", new { detalleVentas.IdVenta });
        }

        //Miguel 22/10/2023: Función para consultar si hay ingredientes suficientes para agregar un detalle 
        public bool InsumosSuficientes(int? idProducto, int? cantidadVender)
        {
            bool insumosSuficientes = true;
            var producto = _context.Productos
                .Where(r => r.IdProducto == idProducto)
                .FirstOrDefault();

            if (producto == null)
            {
                return false;
            }

            var recetaProducto = _context.Recetas
                .Where(r => r.IdProducto == producto.IdProducto)
                .ToList();

            foreach (var receta in recetaProducto)
            {
                var insumosExistentes = _context.Insumos
                    .SingleOrDefault(i => i.IdInsumo == receta.IdInsumo);

                int? cantidadTotal = receta.CantInsumo * cantidadVender;

                if (cantidadTotal > insumosExistentes.CantInsumo)
                {
                    insumosSuficientes = false;
                    break;
                }
                else
                {
                    insumosSuficientes = true;
                }


            }
            return insumosSuficientes;
        }

        //Miguel 22/10/2023: Función para organizar los detalles en caso de que la venta sea cuenta abierta y se agregue el mismo producto
        public void OrganizarDetalles(int? idVenta, int? idProducto)
        {
            var primerDetalle = _context.DetalleVentas
                .Where(d => d.IdVenta == idVenta && d.IdProducto == idProducto)
                .OrderBy(d => d.IdDetalleVenta)
                .FirstOrDefault();

            if (primerDetalle != null)
            {
                var detallesRepetidos = _context.DetalleVentas
                    .Where(d => d.IdVenta == idVenta && d.IdProducto == idProducto && d.IdDetalleVenta != primerDetalle.IdDetalleVenta)
                    .ToList();

                foreach (var detalleRepetido in detallesRepetidos)
                {
                    primerDetalle.CantVendida += detalleRepetido.CantVendida;
                }

                foreach (var detalleRepetido in detallesRepetidos)
                {
                    _context.DetalleVentas.Remove(detalleRepetido);
                }

                _context.SaveChanges();
            }
        }

        //Miguel 22/10/2023: Función para conultar los detalles de las ventas en el index de ventas
        public async Task<IActionResult> DetallesVentas(int? IdVenta)
        {
            if (IdVenta == null)
            {
                return NotFound();
            }

            var detallesVentas = await _context.DetalleVentas
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(d => d.IdVenta == IdVenta);

            var listaDetalles = _context.DetalleVentas
                .Where(d => d.IdVenta == IdVenta)
                .Include(d => d.IdProductoNavigation)
                .ToList();

            List<DetalleVenta> detallesLista = listaDetalles.ToList();

            int cambio = (int)(detallesVentas.IdVentaNavigation.Pago - detallesVentas.IdVentaNavigation.TotalVenta);
            ViewBag.Cambio = cambio;
            ViewBag.listaDetalles = detallesLista;

            if (detallesVentas == null)
            {
                return NotFound();
            }
            return View(detallesVentas);
        }

    }
}
