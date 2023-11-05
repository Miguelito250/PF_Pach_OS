using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    [Authorize]
    public class DetalleVentasController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        public readonly PermisosController _permisosController;
        public DetalleVentasController(Pach_OSContext context, UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignInManager)
        {
            _context = context;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
            _permisosController = new PermisosController(_context, _UserManager, _SignInManager);
        }

        //Miguel 22/10/2023: Funcion para ir agregando los detalles de venta a la factura
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AgregarDetalle([Bind("CantVendida,Precio,IdVenta,IdProducto,Estado")] DetalleVenta detalleVenta)
        {
            if (!ModelState.IsValid || detalleVenta.CantVendida <= 0 || detalleVenta.IdProducto <= 4)
            {
                return RedirectToAction("Crear", "Ventas", new { detalleVenta.IdVenta });
            }

            bool insumosSuficientes = InsumosSuficientes(detalleVenta.IdProducto, detalleVenta.CantVendida);

            if (!insumosSuficientes)
            {
                return RedirectToAction("Crear", "Ventas", new { detalleVenta.IdVenta });
            }

            var producto = _context.Productos
                .Where(detalle => detalle.IdProducto == detalleVenta.IdProducto)
                .Include(p => p.Receta)
                .FirstOrDefault();

            foreach (var receta in producto.Receta)
            {
                var insumoDescontar = await _context.Insumos
                    .FirstOrDefaultAsync(r => r.IdInsumo == receta.IdInsumo);

                int? cantidadGastar = receta.CantInsumo * detalleVenta.CantVendida;
                insumoDescontar.CantInsumo = insumoDescontar.CantInsumo - cantidadGastar;

                _context.Update(insumoDescontar);
                await _context.SaveChangesAsync();
            }

            detalleVenta.Precio = producto.PrecioVenta;
            detalleVenta.Estado = "Sin Descontar";

            var productoExistente = _context.DetalleVentas
                .FirstOrDefault(detalle => detalle.IdVenta == detalleVenta.IdVenta && detalle.IdProducto == detalleVenta.IdProducto && detalle.Estado != "Descontado");

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarDetalle(int? id)
        {
            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.IdProductoNavigation.Receta)
                .FirstOrDefaultAsync(d => d.IdDetalleVenta == id);
            if (detalleVenta.IdProducto <= 4)
            {
                List<Insumo> insumosDisminuir = new List<Insumo>();
                List<SaborSeleccionado> saboresEliminar = new List<SaborSeleccionado>();

                var detallesVenta = _context.DetalleVentas
                    .FirstOrDefault(d => d.IdDetalleVenta == id);

                var saboresSeleccionados = await _context.SaboresSeleccionados
                    .Where(s => s.IdDetalleVenta == id)
                    .Include(s => s.IdDetalleVentaNavigation)
                    .Include(s => s.IdProductoNavigation)
                        .ThenInclude(p => p.Receta)
                        .ThenInclude(r => r.IdInsumoNavigation)
                    .ToListAsync();

                foreach (var sabor in saboresSeleccionados)
                {
                    SaborSeleccionado saborSeleccionado = new SaborSeleccionado();
                    var producto = sabor.IdProductoNavigation;
                    var receta = producto.Receta;



                    var recetaSaborPizza = receta;

                    foreach (var recetaPizza in recetaSaborPizza)
                    {
                        int cantidadDisminuir = 0;
                        var insumoGastar = recetaPizza.IdInsumoNavigation;
                        cantidadDisminuir = (int)recetaPizza.CantInsumo / saboresSeleccionados.Count();

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
                    saborSeleccionado.IdProducto = sabor.IdProducto;
                    saborSeleccionado.IdDetalleVenta = id;

                    saboresEliminar.Add(saborSeleccionado);
                }

                foreach (var disminuirInsumo in insumosDisminuir)
                {
                    var insumoDescontar = await _context.Insumos
                       .FirstOrDefaultAsync(i => i.IdInsumo == disminuirInsumo.IdInsumo);
                    int? cantidadGastar = disminuirInsumo.CantInsumo * detallesVenta.CantVendida;

                    insumoDescontar.CantInsumo = insumoDescontar.CantInsumo + cantidadGastar;
                    _context.Update(insumoDescontar);
                    await _context.SaveChangesAsync();
                }

            }

            var recetaProducto = detalleVenta.IdProductoNavigation.Receta;

            foreach (var receta in recetaProducto)
            {
                var insumoAnadir = await _context.Insumos
                    .FirstOrDefaultAsync(i => i.IdInsumo == receta.IdInsumo);

                int? cantidadGastar = receta.CantInsumo * detalleVenta.CantVendida;
                insumoAnadir.CantInsumo = insumoAnadir.CantInsumo + cantidadGastar;
            }

            _context.DetalleVentas.Remove(detalleVenta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Crear", "Ventas", new { detalleVenta.IdVenta });
        }

        //Miguel 22/10/2023: Función para consultar si hay ingredientes suficientes para agregar un detalle 
        public bool InsumosSuficientes(int? idProducto, int? cantidadVender)
        {
            bool insumosSuficientes = true;
            var producto = _context.Productos
                .FirstOrDefault(r => r.IdProducto == idProducto);

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

        public async Task<bool> InsumosSuficientesPizzas(List<int> sabores, DetalleVenta detalleVenta)
        {
            List<Insumo> insumosDisminuir = new List<Insumo>();

            foreach (var sabor in sabores)
            {
                var producto = _context.Productos
                    .Include(p => p.Receta)
                    .ThenInclude(r => r.IdInsumoNavigation)
                    .FirstOrDefault(p => p.IdProducto == sabor);

                if (producto == null)
                {
                    return false;
                }

                var recetaPizza = producto.Receta;

                foreach (var receta in recetaPizza)
                {
                    int cantidadDisminuir = (int)receta.CantInsumo / sabores.Count();
                    var insumoGastar = receta.IdInsumoNavigation;

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
            }

            foreach (var disminuirInsumo in insumosDisminuir)
            {
                var insumoDescontar = await _context.Insumos
                    .FirstOrDefaultAsync(i => i.IdInsumo == disminuirInsumo.IdInsumo);

                int? cantidadGastar = disminuirInsumo.CantInsumo * detalleVenta.CantVendida;

                if (cantidadGastar > insumoDescontar.CantInsumo)
                {
                    return false;
                }
            }

            return true;
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
            bool tine_permiso = _permisosController.tinto(2, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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

        //Function 24/10/2023: Función para eliminar los detalles de venta no confirmados
        [HttpPost]
        public async Task<bool> DetallesSinConfirmar(int IdVenta)
        {
            var detalleVentas = await _context.DetalleVentas
                .Where(d => d.IdVenta == IdVenta && d.Estado != "Descontado")
                .ToListAsync();

            if (detalleVentas == null)
            {
                NotFound();
            }

            foreach (var detalle in detalleVentas)
            {
                _context.Remove(detalle);
                await _context.SaveChangesAsync();
            }


            return true;
        }
    }
}
