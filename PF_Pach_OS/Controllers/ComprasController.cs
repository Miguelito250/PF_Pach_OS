using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;
using SendGrid.Helpers.Mail;


namespace PF_Pach_OS.Controllers
{
    public class ComprasController : Controller
    {
        private Pach_OSContext context = new Pach_OSContext();
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly PermisosController _permisosController;
        public ComprasController(Pach_OSContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _permisosController = new PermisosController(context, _userManager, _signInManager);
        }


        public IActionResult Index()
        {
            var compras = context.Compras
            .Join(context.Proveedores,
                c => c.IdProveedor,
                p => p.IdProveedor,
                (c, p) => new { Compra = c, Proveedor = p })
            .Join(context.DetallesCompras,
                cp => cp.Compra.IdCompra,
                oc => oc.IdCompra,
                (cp, oc) => new { CompraProveedor = cp, DetalleCompra = oc })
            .GroupBy(result => new
            {
                result.CompraProveedor.Compra.FechaCompra,
                result.CompraProveedor.Compra.Total,
                result.CompraProveedor.Proveedor.NomLocal,
                result.CompraProveedor.Compra.IdCompra,
                result.CompraProveedor.Compra.NumeroFactura,
                result.CompraProveedor.Compra.IdEmpleado
            })
            .Select(result => new
            {
                result.Key.FechaCompra,
                result.Key.Total,
                result.Key.NomLocal,
                result.Key.IdCompra,
                result.Key.NumeroFactura,
                result.Key.IdEmpleado,
                CantidadDetalles = result.Count()
            })
            .OrderByDescending(result => result.FechaCompra)
            .ToList();

            Console.WriteLine(compras.Count);



            ViewBag.Compras = compras;
            return View();
        }


        public async Task<IActionResult> Create([Bind("NumeroFactura")] Compra compra)
        {

            bool tine_permiso = _permisosController.tinto(5, User);
            Console.WriteLine("======================");
            Console.WriteLine(tine_permiso);
            Console.WriteLine("======================");


            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("Aqui esta el error");
                var proveedor = await context.Proveedores.ToListAsync();
                var nombreUsuario = _userManager.GetUserAsync(User).Result.FirstName;
                ViewBag.NombreUsuario = nombreUsuario;
                if (proveedor.Count() > 0)
                {
                    Console.WriteLine("Aqui esta el error 2");
                    compra.IdProveedor = proveedor[0].IdProveedor;
                    compra.FechaCompra = DateTime.Parse(DateTime.Today.ToString("D"));
                    compra.IdEmpleado = nombreUsuario;
                    compra.Total = 0;
                    compra.NumeroFactura = compra.NumeroFactura;
                    context.Add(compra);
                    context.SaveChanges();
                    return Redirect($"/DetalleCompra/Create/{compra.IdCompra}");
                }
                else
                {
                    Console.WriteLine("Aqui esta el error 3");
                    return RedirectToAction("Index");
                }

            }
            else
            {
                Console.WriteLine("Aqui esta el error 4");
                return RedirectToAction("Index");
            }
        }


        public async Task<IActionResult> DetalleCompra(int? IdCompra)
        {
            if (IdCompra == null)
            {
                return NotFound();
            }

            var detallesCompra = await context.DetallesCompras
                .Include(d => d.IdCompraNavigation)
                    .ThenInclude(c => c.IdProveedorNavigation)
                .FirstOrDefaultAsync(d => d.IdCompra == IdCompra);
            var listaDetalles = context.DetallesCompras
                .Where(d => d.IdCompra == IdCompra)
                .Include(d => d.IdInsumoNavigation)
                .ToList();

            List<DetallesCompra> detallesLista = listaDetalles.ToList();


            ViewBag.listaDetalles = detallesLista;
            ViewBag.IdEmpleado = detallesCompra.IdCompraNavigation.IdEmpleado;

            if (detallesCompra == null)
            {
                return NotFound();
            }

            return View(detallesCompra);

        }

        public IActionResult NumeroFacturaDuplicado(string NumeroFactura)
        {
            var EsDuplicado = context.Compras.Any(x => x.NumeroFactura == NumeroFactura);
            return Json(EsDuplicado);
        }


        public async Task<bool> DetallesSinConfirmar(int IdCompra)
        {
            var compradetalle = await context.Compras
                .Include(v => v.DetallesCompras)
                .FirstOrDefaultAsync(v => v.IdCompra == IdCompra);

            var ordenes = compradetalle.DetallesCompras.ToList();
            if (compradetalle.DetallesCompras.Count() > 0)
            {
                foreach (var orden in ordenes)
                {
                    // Deshacer los cambios en el stock para este detalle de compra
                    var insumo = context.Insumos.FirstOrDefault(i => i.IdInsumo == orden.IdInsumo);
                    var medida = orden.Medida;
                    if (insumo != null)
                    {
                        if (medida == "Gramos" || medida == "Unidad" || medida == "Mililitro")
                        {
                            insumo.CantInsumo -= orden.Cantidad;
                        }
                        else if (medida == "Kilogramos")
                        {
                            var conversion = orden.Cantidad * 1000;
                            insumo.CantInsumo -= conversion;
                        }
                        else if (medida == "Libras")
                        {
                            var conversion = orden.Cantidad * 454;
                            insumo.CantInsumo -= conversion;
                        }
                        else if (medida == "Litros")
                        {
                            var conversion = orden.Cantidad * 1000;
                            insumo.CantInsumo -= conversion;
                        }
                        else if (medida == "Onza")
                        {
                            var conversion = orden.Cantidad * 30;
                            insumo.CantInsumo -= conversion;
                        }

                        context.Update(insumo);
                    }

                    context.DetallesCompras.Remove(orden);
                }

                // Guardar los cambios en el stock y los detalles de compra
                context.SaveChanges();
            }

            // Finalmente, eliminar la compra
            context.Compras.Remove(compradetalle);
            context.SaveChanges();
            return true;

        }


        public async Task<IActionResult> Delete(string id)
        {

            bool tine_permiso = _permisosController.tinto(5, User);
            Console.WriteLine("======================");
            Console.WriteLine(tine_permiso);
            Console.WriteLine("======================");


            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            var compradetalle = await context.Compras
                .Include(v => v.DetallesCompras)
                .FirstOrDefaultAsync(v => v.IdCompra == long.Parse(id));

            if (compradetalle == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var ordenes = compradetalle.DetallesCompras.ToList();
                if (compradetalle.DetallesCompras.Count() > 0)
                {
                    foreach (var orden in ordenes)
                    {
                        // Deshacer los cambios en el stock para este detalle de compra
                        var insumo = context.Insumos.FirstOrDefault(i => i.IdInsumo == orden.IdInsumo);
                        var medida = orden.Medida;
                        if (insumo != null)
                        {
                            if (medida == "Gramos" || medida == "Unidad" || medida == "Mililitro")
                            {
                                insumo.CantInsumo -= orden.Cantidad;
                            }
                            else if (medida == "Kilogramos")
                            {
                                var conversion = orden.Cantidad * 1000;
                                insumo.CantInsumo -= conversion;
                            }
                            else if (medida == "Libras")
                            {
                                var conversion = orden.Cantidad * 454;
                                insumo.CantInsumo -= conversion;
                            }
                            else if (medida == "Litros")
                            {
                                var conversion = orden.Cantidad * 1000;
                                insumo.CantInsumo -= conversion;
                            }
                            else if (medida == "Onza")
                            {
                                var conversion = orden.Cantidad * 30;
                                insumo.CantInsumo -= conversion;
                            }

                            context.Update(insumo);
                        }

                        context.DetallesCompras.Remove(orden);
                    }

                    // Guardar los cambios en el stock y los detalles de compra
                    context.SaveChanges();
                }

                // Finalmente, eliminar la compra
                context.Compras.Remove(compradetalle);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

    }
}
