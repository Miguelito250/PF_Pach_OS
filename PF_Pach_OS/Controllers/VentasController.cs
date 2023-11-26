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
using System.Linq.Expressions;
using Microsoft.AspNetCore.Antiforgery;
using System.Globalization;
using System.Data.SqlTypes;

namespace PF_Pach_OS.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly DetalleVentasController _detalleVentasController;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly PermisosController _permisosController;

        public VentasController(Pach_OSContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _permisosController = new PermisosController(_context, _userManager, _signInManager);
            _detalleVentasController = new DetalleVentasController(_context, _userManager, _signInManager);
        }

        //Miguel 22/10/2023: Función para retornar a la vista de index de ventas
        public async Task<IActionResult> Index()
        {
            bool tine_permiso = _permisosController.tinto(2, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto");
            var ventasNulas = _context.Ventas.FirstOrDefault(v => v.TipoPago == null);

            if (ventasNulas != null)
            {
                await CancelarVenta(ventasNulas.IdVenta);
            }

            var usuarioActual = await _userManager.GetUserAsync(User);
            var rolUsuario = usuarioActual.Id_Rol;

            if (rolUsuario != 1)
            {
                DateTime fechaActual = DateTime.Now.Date;

                var listadoVentasCajero = await _context.Ventas
                .Where(v => v.FechaVenta.Date == fechaActual)
                .OrderByDescending(v => v.FechaVenta.Year)
                .ThenByDescending(v => v.FechaVenta.Month)
                .ThenByDescending(v => v.FechaVenta.Day)
                .ThenByDescending(v => v.FechaVenta.Hour)
                .ThenByDescending(v => v.FechaVenta.Minute)
                .ThenByDescending(v => v.FechaVenta.Second)
                .ToListAsync();


                return View(listadoVentasCajero);
            }

            var pach_OSContext = await _context.Ventas
                .OrderByDescending(v => v.FechaVenta.Year)
                .ThenByDescending(v => v.FechaVenta.Month)
                .ThenByDescending(v => v.FechaVenta.Day)
                .ThenByDescending(v => v.FechaVenta.Hour)
                .ThenByDescending(v => v.FechaVenta.Minute)
                .ThenByDescending(v => v.FechaVenta.Second)
                .ToListAsync();

            return View(pach_OSContext);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ListarVentasAPI()
        {
            var pach_OSContext = await _context.Ventas
               .OrderByDescending(v => v.FechaVenta.Year)
               .ThenByDescending(v => v.FechaVenta.Month)
               .ThenByDescending(v => v.FechaVenta.Day)
               .ThenByDescending(v => v.FechaVenta.Hour)
               .ThenByDescending(v => v.FechaVenta.Minute)
               .ThenByDescending(v => v.FechaVenta.Second)
               .ToListAsync();
            return Json(pach_OSContext);
        }

        [AllowAnonymous]
        public JsonResult GetDetallesVenta(int id)
        {
            var detallesVenta = _context.DetalleVentas
                .Join(_context.Ventas,
                    dv => dv.IdVenta,
                    v => v.IdVenta,
                    (dv, v) => new { DetalleVenta = dv, Venta = v })
                .Join(_context.Productos,
                    dvv => dvv.DetalleVenta.IdProducto,
                    p => p.IdProducto,
                    (dvv, p) => new { DetalleVentaVenta = dvv, Producto = p })
                .Where(result => result.DetalleVentaVenta.Venta.IdVenta == id)
                .Select(result => new
                {
                    result.DetalleVentaVenta.DetalleVenta.IdDetalleVenta,
                    result.DetalleVentaVenta.DetalleVenta.CantVendida,
                    result.DetalleVentaVenta.DetalleVenta.Precio,
                    result.DetalleVentaVenta.DetalleVenta.IdProducto,
                    result.Producto.NomProducto
                })
                .ToList();

            if (detallesVenta == null || detallesVenta.Count == 0)
            {
                return Json(new { message = "No se encontraron detalles de venta para este ID" });
            }

            return Json(detallesVenta);
        }

        [AllowAnonymous]
        public JsonResult VentaApi(int id)
        {
            var venta = _context.Ventas
                .FirstOrDefault(v => v.IdVenta == id);
            return Json(venta);
        }


        //Miguel 22/10/2023: Función para redirigir con los datos necesarios a la vista de registrar los detalles de venta
        public IActionResult Crear(int IdVenta)
        {
            bool tine_permiso = _permisosController.tinto(2, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

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

            ViewData["IdProducto"] = new SelectList(_context.Productos
    .Where(p => p.Estado == 1 && (p.IdTamano == null || p.IdTamano == 1) && p.IdProducto != 1)
            , "IdProducto", "NomProducto");



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
                DateTime fechaColombia = DateTime.Now;

                venta.FechaVenta = fechaColombia;

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
            bool tine_permiso = _permisosController.tinto(2, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            venta.Estado = venta.Mesa == "General"
               ? venta.Estado = "Pagada"
               : venta.Estado = "Pendiente";

            var ventaActualizar = await _context.Ventas
                .FirstOrDefaultAsync(v => v.IdVenta == venta.IdVenta);

            if (ventaActualizar != null)
            {
                TimeZoneInfo bogotaZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                DateTime currentDateTime = DateTime.Now;
                DateTime fechaVenta = TimeZoneInfo.ConvertTimeFromUtc(currentDateTime.ToUniversalTime(), bogotaZone);

                if (fechaVenta >= SqlDateTime.MinValue.Value && fechaVenta <= SqlDateTime.MaxValue.Value)
                {
                    ventaActualizar.FechaVenta = fechaVenta;
                }

                ventaActualizar.TipoPago = venta.TipoPago;
                ventaActualizar.Pago = venta.Pago;
                venta.PagoDomicilio = venta.PagoDomicilio == null
                    ? 0
                    : venta.PagoDomicilio;
                ventaActualizar.PagoDomicilio = venta.PagoDomicilio;
                ventaActualizar.IdEmpleado = venta.IdEmpleado;
                ventaActualizar.Estado = venta.Estado;
                ventaActualizar.Mesa = venta.Mesa;
                ventaActualizar.TotalVenta = venta.TotalVenta;

                _context.Ventas.Update(ventaActualizar);
                _context.SaveChanges();
            }

            var detallesVenta = await _context.DetalleVentas
                .Where(d => d.IdVenta == venta.IdVenta)
                .ToListAsync();

            foreach (var detalle in detallesVenta)
            {
                detalle.Estado = "Descontado";
                _context.Update(detalle);
            }

            await _context.SaveChangesAsync();
            await _detalleVentasController.OrganizarDetalles(venta.IdVenta);
            return RedirectToAction("Index", "Ventas");
        }

        //Miguel 22/10/2023: Función para cancelar la venta en caso de que ya no se vaya a registrar o se encuentre una nula en el index
        public async Task<IActionResult> CancelarVenta(int IdVenta)
        {
            bool tine_permiso = _permisosController.tinto(2, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            var ventaCancelar = _context.Ventas.SingleOrDefault(v => v.IdVenta == IdVenta);
            var detallesVenta = await _context.DetalleVentas
                .Where(d => d.IdVenta == ventaCancelar.IdVenta)
                .ToListAsync();

            foreach (var detalle in detallesVenta)
            {
                await _detalleVentasController.EliminarDetalle(detalle.IdDetalleVenta);
            }

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

        //Miguel 4/11/2023: Función para descontar los insumos de los productos vendidos
        public async Task<bool> DescontarInsumos(List<Insumo> insumosDisminuir, List<bool> insumosCompletos, int? cantidadVender, int numeroSaboresPizza)
        {
            bool desocontarInsumos = insumosCompletos.Find(i => i == false);

            if (desocontarInsumos == null)
            {
                return false;
            }

            foreach (var insumo in insumosDisminuir)
            {
                var insumoDescontar = await _context.Insumos
                    .FirstOrDefaultAsync(i => i.IdInsumo == insumo.IdInsumo);

                var insumoGastar = insumo.CantInsumo * cantidadVender;
                insumoDescontar.CantInsumo = insumoGastar;

                _context.Update(insumoDescontar);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        //Miguel 22/10/2023: Función para cambiar de estado de la venta a pagado o a pendiente
        public async Task<IActionResult> CambiarEstado(int IdVenta)
        {
            bool tine_permiso = _permisosController.tinto(2, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            var cambioEstado = "";

            var estadoVenta = _context.Ventas
                .FirstOrDefault(d => d.IdVenta == IdVenta);


            if (estadoVenta.Estado == "Pendiente")
            {
                cambioEstado = "Pagada";
                estadoVenta.Estado = cambioEstado;
                _context.Update(estadoVenta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Ventas");
        }

        //Miguel 22/10/2023: Función para escoger los sabores de las pizzas a vender en la venta modal
        public async Task<IActionResult> SaboresPizza(int? tamanoPizza)
        {
            bool tine_permiso = _permisosController.tinto(2, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            var tamanoVender = await _context.Tamanos
                .FirstOrDefaultAsync(t => t.IdTamano == tamanoPizza);
            var idProducto = await _context.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == tamanoPizza);




            if (tamanoVender == null || idProducto == null)
            {
                return NotFound();
            }

            var saboresPizza = _context.Productos
                .Where(p => p.IdTamano == tamanoVender.IdTamano && p.IdProducto > 4 && p.Estado == 1);

            ViewBag.IdProducto = idProducto.IdProducto;
            ViewBag.TamanoVender = tamanoVender.NombreTamano;
            ViewBag.MaximoSabores = tamanoVender.MaximoSabores;

            return View(await saboresPizza.ToListAsync());
        }

        //Miguel 22/10/2023: Función para confirmar los sabores de las pizzas escogidos y agregarlos al detalle
        [HttpPost]
        public async Task<bool> ConfirmarSabores(List<int> sabores, DetalleVenta detalleVenta)
        {
            List<int> PreciosProductos = new List<int>();
            List<Insumo> insumosDisminuir = new List<Insumo>();

            var producto = await _context.Productos
                .FirstOrDefaultAsync(d => d.IdProducto == detalleVenta.IdProducto);

            if (producto == null)
            {
                NotFound();
            }

            foreach (var sabor in sabores)
            {
                var saborEscogido = await _context.Productos
                    .FirstOrDefaultAsync(p => p.IdProducto == sabor);

                int precio = (int)saborEscogido.PrecioVenta;
                PreciosProductos.Add(precio);
            }

            detalleVenta.Precio = PreciosProductos.Max();

            bool resultado = await _detalleVentasController.InsumosSuficientesPizzas(sabores, detalleVenta);

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
                    .Include(p => p.Receta)
                        .ThenInclude(r => r.IdInsumoNavigation)
                    .FirstOrDefaultAsync(p => p.IdProducto == sabor);

                var recetaSaborPizza = saborPizza.Receta;

                foreach (var recetaPizza in recetaSaborPizza)
                {
                    int cantidadDisminuir = 0;
                    var insumoGastar = recetaPizza.IdInsumoNavigation;
                    cantidadDisminuir = (int)recetaPizza.CantInsumo / sabores.Count();

                    Insumo insumo = new Insumo
                    {
                        IdInsumo = insumoGastar.IdInsumo,
                        NomInsumo = insumoGastar.NomInsumo,
                        CantInsumo = cantidadDisminuir
                    };

                    var insumoActualizar = insumosDisminuir
                           .FirstOrDefault(i => i.IdInsumo == insumo.IdInsumo);

                    if (insumoActualizar != null)
                    {
                        insumoActualizar.CantInsumo += cantidadDisminuir;
                    }
                    else
                    {
                        insumosDisminuir.Add(insumo);
                    }

                }

                saborSeleccionado.IdSaborSeleccionado = null;
                saborSeleccionado.IdProducto = sabor;
                saborSeleccionado.IdDetalleVenta = detalleVenta.IdDetalleVenta;

                await _context.SaboresSeleccionados.AddAsync(saborSeleccionado);
                await _context.SaveChangesAsync();
            }
            foreach (var disminuirInsumo in insumosDisminuir)
            {
                var insumoDescontar = await _context.Insumos
                   .FirstOrDefaultAsync(i => i.IdInsumo == disminuirInsumo.IdInsumo);
                int? cantidadGastar = disminuirInsumo.CantInsumo * detalleVenta.CantVendida;

                if (cantidadGastar < insumoDescontar.CantInsumo)
                {
                    insumoDescontar.CantInsumo = insumoDescontar.CantInsumo - cantidadGastar;
                    _context.Update(insumoDescontar);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    return false;
                }


            }
            return insumosSufucientes;
        }

        //Miguel 06/11/2023: Función para hacer el reporte diario y mostrar la información en el cierre de caja
        public async Task<int> ReporteDiario()
        {
            DateTime fechaActual = DateTime.Now.Date;

            var reporteVentas = await _context.Ventas
                .Where(v => v.FechaVenta.Date == fechaActual && v.TotalVenta != null)
                .ToListAsync();

            int ventasDiarias = 0;
            foreach (var venta in reporteVentas)
            {
                ventasDiarias += (int)venta.TotalVenta;
            }

            return ventasDiarias;
        }

        //Miguel 10/11/2023: Funciónn para mostrar los sabores seleccionados de las pizzas
        public async Task<IActionResult> DetallesSabores(int? idDetalleVenta)
        {
            var detalleVenta = await _context.DetalleVentas
               .FirstOrDefaultAsync(d => d.IdDetalleVenta == idDetalleVenta);

            if (detalleVenta == null)
            {
                return NotFound();
            }

            var saboresSeleccionados = await _context.SaboresSeleccionados
                .Where(d => d.IdDetalleVenta == idDetalleVenta)
                .Include(s => s.IdProductoNavigation)
                .ToListAsync();

            return View(saboresSeleccionados);
        }

    }
}