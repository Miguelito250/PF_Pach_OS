using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Versioning;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly DetalleVentasController _detalleVentasController;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public VentasController(Pach_OSContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _detalleVentasController = new DetalleVentasController(_context);
            _signInManager = signInManager;
            _userManager = userManager;
        }   

        //Miguel 22/10/2023: Función para retornar a la vista de index de ventas
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

        //Miguel 22/10/2023: Función para redirigir con los datos necesarios a la vista de registrar los detalles de venta
        public IActionResult Crear(int IdVenta)
        {
            var nombreUsuario = _userManager.GetUserAsync(User).Result.FirstName;
            ViewBag.NombreUsuario = nombreUsuario;

            float? total = 0;
            var ventaNula = _context.Ventas
                .FirstOrDefault(v => v.IdVenta == IdVenta);

            if (ventaNula == null || ventaNula.Estado == "Pagada")
            {
                return RedirectToAction("Index");
            }
            var DetallesVentas = _context.DetalleVentas
                .Where(d => d.IdVenta == IdVenta)
                .Include(d => d.IdProductoNavigation)
                .ToList();

            foreach (var detalle in DetallesVentas)
            {
                total += detalle.Precio * detalle.CantVendida;
            }

            ViewBag.IdVenta = IdVenta;
            ViewBag.Total = total;
            ViewData["DetallesVentas"] = DetallesVentas;
            ViewData["IdProducto"] = new SelectList(_context.Productos.
                Where(p => (p.IdProducto > 4 || p.IdTamano == null) && p.Estado == 1),
                "IdProducto", "NomProducto");

            if (ventaNula.Estado != null)
            {
                DetalleVenta detalleExistente = new DetalleVenta();
                Venta ventaExistente = ventaNula;

                Tuple<DetalleVenta, Venta> Venta_DetalleExistente = new Tuple<DetalleVenta, Venta>(detalleExistente, ventaExistente);
                return View(Venta_DetalleExistente);
            }

            Tuple<DetalleVenta, Venta> Venta_Detalle = new(new DetalleVenta(), new Venta());


            return View(Venta_Detalle);
        }

        //Miguel 22/10/2023: Función para crear la venta vacia para poder asignarle su detalle de venta
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

        //Miguel 22/10/2023: Función para confirmar la venta 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarVenta([Bind("IdVenta,FechaVenta,TotalVenta,TipoPago,Pago,PagoDomicilio,IdEmpleado,Estado,Mesa")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                if (venta.Pago < venta.TotalVenta || venta.Pago == null)
                {
                    return RedirectToAction("Crear", "Ventas", new { venta.IdVenta });
                }
                else
                {
                     venta.Estado = venta.Mesa == "General"
                        ? venta.Estado = "Pagada"
                        : venta.Estado = "Pendiente";


                    var ventaActualizar = await _context.Ventas
                        .FirstOrDefaultAsync(v => v.IdVenta == venta.IdVenta);

                    if (ventaActualizar != null)
                    {
                        ventaActualizar.FechaVenta = venta.FechaVenta;
                        ventaActualizar.TotalVenta = venta.TotalVenta;
                        ventaActualizar.TipoPago = venta.TipoPago;
                        ventaActualizar.Pago = venta.Pago;
                        ventaActualizar.PagoDomicilio = venta.PagoDomicilio;
                        ventaActualizar.IdEmpleado = venta.IdEmpleado;
                        ventaActualizar.Estado = venta.Estado;
                        ventaActualizar.Mesa= venta.Mesa;

                        _context.Ventas.Update(ventaActualizar); 
                        await _context.SaveChangesAsync();
                    }

                    await DescontarInsumos(venta);
                }
                return RedirectToAction("Index", "Ventas");
            }

            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", venta.IdEmpleado);
            return RedirectToAction("Crear", "Ventas", new { venta.IdVenta });
        }

        //Miguel 22/10/2023: Función para cancelar la venta en caso de que ya no se vaya a registrar o se encuentre una nula en el index
        public IActionResult CancelarVenta(int IdVenta)
        {
            var ventaCancelar = _context.Ventas.SingleOrDefault(v => v.IdVenta == IdVenta);
            if (ventaCancelar != null)
            {
                _context.Ventas.Remove(ventaCancelar);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Ventas");
        }

        //Miguel 22/10/2023: Función para descontar insumos de los productos vendidos
        public async Task<IActionResult> DescontarInsumos(Venta venta)
        {
            int? cantidadDisminuir = 0;

            var detalleVenta = _context.DetalleVentas
                .Where(d => d.IdVenta == venta.IdVenta && d.Estado != "Descontado")
                .Include(d => d.IdProductoNavigation.IdTamanoNavigation)
                .ToList();

            foreach (var detalle in detalleVenta)
            {
                var producto = _context.Productos
                    .FirstOrDefault(p => p.IdProducto == detalle.IdProducto);

                _detalleVentasController.OrganizarDetalles(venta.IdVenta, detalle.IdProducto);

                if (producto.IdProducto is <= 4 and > 1)
                {

                    var tamanos = _context.Tamanos
                                .Select(t => t.Tamano1)
                                .ToList();

                    float tamanoMasPequeno = (float)tamanos.Min();
                    float tamanoMasGrande = (float)tamanos.Max();

                    var saboresEscogidos = _context.SaboresSeleccionados
                        .Where(s => s.IdDetalleVenta == detalle.IdDetalleVenta)
                        .ToList();

                    foreach (var sabor in saboresEscogidos)
                    {
                        int? porcentajeInsumo = 0;
                        var recetaPizza = _context.Recetas
                            .Where(r => r.IdProducto == sabor.IdProducto)
                            .Include(r => r.IdProductoNavigation.IdTamanoNavigation)
                            .ToList();

                        foreach (var receta in recetaPizza)
                        {
                            float tamanoActual = (float)detalle.IdProductoNavigation.IdTamanoNavigation.Tamano1;


                            float porcentajeGastar = (tamanoActual - tamanoMasPequeno) / (tamanoMasGrande - tamanoMasPequeno) * 100;
                            var consultaInsumos = _context.Insumos
                               .SingleOrDefault(i => i.IdInsumo == receta.IdInsumo);

                            int cantidadGastar = (int)(receta.CantInsumo * porcentajeGastar) / 100;
                            cantidadDisminuir = (receta.CantInsumo + cantidadGastar) * detalle.CantVendida;


                            if (cantidadDisminuir < consultaInsumos.CantInsumo)
                            {
                                int? insumoDisminuido = consultaInsumos.CantInsumo - cantidadDisminuir;

                                consultaInsumos.CantInsumo = insumoDisminuido;

                                _context.Update(consultaInsumos);
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                ViewBag.MensajeAlerta = "No hay suficientes insumos";
                                return RedirectToAction("Crear", "Ventas", new { detalle.IdVenta });

                            }
                        }
                    }
                }

                else
                {
                    var recetaProducto = _context.Recetas
                                       .Where(r => r.IdProducto == detalle.IdProducto)
                                       .ToList();
                    foreach (var detalleReceta in recetaProducto)
                    {
                        var consultaInsumos = _context.Insumos
                            .SingleOrDefault(i => i.IdInsumo == detalleReceta.IdInsumo);

                        int? cantidadInsumo = consultaInsumos.CantInsumo;
                        cantidadDisminuir = detalle.CantVendida * detalleReceta.CantInsumo;

                        if (cantidadDisminuir < cantidadInsumo)
                        {
                            int? insumoDisminuido = cantidadInsumo - cantidadDisminuir;

                            consultaInsumos.CantInsumo = insumoDisminuido;

                            _context.Update(consultaInsumos);
                            _context.SaveChanges();
                        }
                        else
                        {
                            ViewBag.MensajeAlerta = "No hay suficientes insumos";
                            return RedirectToAction("Crear", "Ventas", new { detalle.IdVenta });
                        }
                    }
                }
                detalle.Estado = "Descontado";
                _context.DetalleVentas.Update(detalle);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Ventas");
        }

        //Miguel 22/10/2023: Función para cambiar de estado de la venta a pagado o a pendiente
        public async Task<IActionResult> CambiarEstado(int IdVenta)
        {
            var cambioEstado = "";

            var estadoVenta = _context.Ventas
                .FirstOrDefault(d => d.IdVenta == IdVenta);


            if (estadoVenta.Estado == "Pendiente")
            {
                cambioEstado = "Entregado";
                estadoVenta.Estado = cambioEstado;
                _context.Update(estadoVenta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Ventas");
        }

        //Miguel 22/10/2023: Función para escoger los sabores de las pizzas a vender en la venta modal
        public async Task<IActionResult> SaboresPizza()
        {
            var saboresPizza = _context.Productos
                .Where(p => p.IdTamano == 1 && p.IdCategoria == 1 && p.IdProducto > 4);

            ViewData["IdProducto"] = new SelectList(_context.Tamanos
                .Where(t => t.IdTamano != 1),
                "IdTamano", "NombreTamano");

            return View(await saboresPizza.ToListAsync());
        }

        //Miguel 22/10/2023: Función para confirmar los sabores de las pizzas escogidos y agregarlos al detalle
        [HttpPost]
        public async Task<bool> ConfirmarSabores(List<int> sabores, DetalleVenta detalleVenta)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(d => d.IdProducto == detalleVenta.IdProducto);

            if (producto == null)
            {
                NotFound();
            }

            detalleVenta.Precio = producto.PrecioVenta;

            bool resultado = _detalleVentasController.InsumosSuficientesPizzas(sabores, detalleVenta);
            if (!resultado)
            {
                return false;
            }
            bool insumosSufucientes = true;
            await _context.DetalleVentas.AddAsync(detalleVenta);
            await _context.SaveChangesAsync();

            foreach (var sabor in sabores)
            {
                SaborSeleccionado saborSeleccionado = new SaborSeleccionado();
                var saborPizza = await _context.Productos
                    .FirstOrDefaultAsync(p => p.IdProducto == sabor);
                var recetaSaborPizza = await _context.Recetas
                    .Where(r => r.IdProducto == sabor)
                    .ToListAsync();

                saborSeleccionado.IdSaborSeleccionado = null;
                saborSeleccionado.IdProducto = sabor;
                saborSeleccionado.IdDetalleVenta = detalleVenta.IdDetalleVenta;

                await _context.SaboresSeleccionados.AddAsync(saborSeleccionado);
                await _context.SaveChangesAsync();
            }
            return insumosSufucientes;
        }

    }
}