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

        //Vista para listar la informacion de ventas
        public async Task<IActionResult> Index()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto");
            var ventasNulas = _context.Ventas.FirstOrDefault(v => v.TipoPago == null);

            if (ventasNulas != null)
            {
                CancelarVenta(ventasNulas.IdVenta);
            }

            var pach_OSContext = _context.Ventas
                .OrderByDescending(v => v.FechaVenta);

            return View(await pach_OSContext.ToListAsync());
        }

        //Funcion para redirigir con los datos necesarios a la vista de registrar los detalles de venta
        public IActionResult Crear(int IdVenta)
        {
            var DetallesVentas = _context.DetalleVentas
                .Where(d => d.IdVenta == IdVenta)
                .ToList();

            ViewBag.IdVenta = IdVenta;
            ViewData["DetallesVentas"] = DetallesVentas;
            ViewData["IdProducto"] = new SelectList(_context.Productos.
                Where(p => p.Estado == 1),
                "IdProducto", "NomProducto");

            Tuple<DetalleVenta, Venta> Venta_Detalle = new(new DetalleVenta(), new Venta());
            return View(Venta_Detalle);
        }

        //Funcion para crear la venta vacia para poder asignarle su detalle de venta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearVenta([Bind("IdVenta")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();

                return RedirectToAction("Crear", "Ventas", new { venta.IdVenta });
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", venta.IdEmpleado);
            return NotFound();
        }


        //Funcion para confirmar la venta y hacer la disminuicion de insumos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarVenta([Bind("IdVenta,FechaVenta,TotalVenta,TipoPago,Pago,PagoDomicilio,IdEmpleado,Estado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var ventaVacia = _context.DetalleVentas
                        .SingleOrDefault(d => d.IdVenta == venta.IdVenta);

                    if(ventaVacia == null)
                    {
                        return RedirectToAction("Crear", "Ventas", new { venta.IdVenta });
                    }
                    

                    if (venta.Pago < venta.TotalVenta || venta.Pago == null)
                    {
                        ViewBag.ValidacionPago = "El pago debe de ser mayor al total de la venta";
                        return RedirectToAction("Crear", "Ventas", new { venta.IdVenta });
                    }
                    else
                    {

                        venta.Estado = "Pendiente";
                        _context.Update(venta);

                        var detallesVenta = _context.DetalleVentas
                            .Where(v => v.IdVenta == venta.IdVenta)
                            .ToList();

                        
                        foreach (var detalle in detallesVenta)
                        {
                            int? cantidadDisminuir = 0;
                            var recetaProducto = _context.Recetas
                                .Where(r => r.IdProducto == detalle.IdProducto)
                                .ToList();

                            foreach (var detalleReceta in recetaProducto)
                            {
                                cantidadDisminuir = detalleReceta.CantInsumo * detalle.CantVendida;

                                var consultaInsumos = _context.Insumos
                                    .SingleOrDefault(i => i.IdInsumo == detalleReceta.IdInsumo);

                                int? cantidadInsumo = consultaInsumos.CantInsumo;

                                if (cantidadDisminuir < cantidadInsumo)
                                {
                                    int? insumoDisminuido = cantidadInsumo - cantidadDisminuir;

                                    consultaInsumos.CantInsumo = insumoDisminuido;

                                    _context.Update(consultaInsumos);
                                }
                                else
                                {
                                    ViewBag.MensajeAlerta = "No hay suficientes insumos";
                                }


                            }
                        }
                        await _context.SaveChangesAsync();
                    }
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

        //Funcion para cancelar la venta en caso de que ya no se vaya a registrar
        public IActionResult CancelarVenta(int IdVenta)
        {
            var ventaCancelar = _context.Ventas.SingleOrDefault(v => v.IdVenta == IdVenta);
            if (ventaCancelar != null)
            {
                _context.Ventas.Remove(ventaCancelar);
                _context.SaveChanges();
                Console.WriteLine("Venta cancelada");
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Ventas");
        }

        //Funcion para conultar los detalles de las ventas en el index de ventas
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

            ViewBag.listaDetalles = detallesLista;

            if (detallesVentas == null)
            {
                return NotFound();
            }
            return View(detallesVentas);

        }

        //Funcion para cambiar de estado de la venta a pagado o a pendiente
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
