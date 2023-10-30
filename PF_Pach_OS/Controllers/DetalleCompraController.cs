using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    [Authorize]
    public class DetalleCompraController : Controller
    {

        private Pach_OSContext context = new Pach_OSContext();
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly PermisosController _permisosController;

        public DetalleCompraController(Pach_OSContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _permisosController = new PermisosController(context, _userManager, _signInManager);
        }

        public IActionResult Index()
        {
            bool tine_permiso = _permisosController.tinto(5, User);
            Console.WriteLine("======================");
            Console.WriteLine(tine_permiso);
            Console.WriteLine("======================");


            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            return View();
        }

        public async Task<IActionResult> Create(int id)
        {
            bool tine_permiso = _permisosController.tinto(5, User);
            Console.WriteLine("======================");
            Console.WriteLine(tine_permiso);
            Console.WriteLine("======================");


            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            
            var IdCompra = id;

            ViewBag.IdCompra = IdCompra;



            var detallescompras = context.DetallesCompras
                .Where(o => o.IdCompra == IdCompra)
                .Join(
                    context.Insumos,
                    Detalle => Detalle.IdInsumo,
                    insumo => insumo.IdInsumo,
                    (Detalle, insumo) => new
                    {
                        insumo.NomInsumo,
                        insumo.IdInsumo,
                        Detalle.Cantidad,
                        Detalle.PrecioInsumo,
                        Detalle.Medida,
                        Detalle.IdDetallesCompra,
                        Detalle.IdCompra,
                        Total =Detalle.PrecioInsumo
                    })
                .ToList();

            var nombreUsuario = _userManager.GetUserAsync(User).Result.FirstName;
            ViewBag.NombreUsuario = nombreUsuario;
            ViewBag.Detalles = detallescompras;
            ViewBag.Insumos = await context.Insumos.Select(x => new { x.IdInsumo, x.NomInsumo,x.Estado,x.Medida }).ToListAsync();
            ViewBag.Proveedores = await context.Proveedores.Select(x => new { x.IdProveedor, x.NomLocal }).ToListAsync();
            ViewBag.Empleados = await context.Empleados.Select(x => new { x.IdEmpleado, x.Nombre }).ToListAsync();
            Tuple<DetallesCompra, Compra, Insumo> models = new Tuple<DetallesCompra, Compra, Insumo>(new DetallesCompra(), new Compra(), new Insumo());
            return View(models);
        }

        [HttpPost]
        public IActionResult Create([Bind(Prefix = "Item1")] DetallesCompra detallecompra, [Bind(Prefix = "Item2")] Compra compra, Insumo insumo)
        {
            bool tine_permiso = _permisosController.tinto(5, User);
            Console.WriteLine("======================");
            Console.WriteLine(tine_permiso);
            Console.WriteLine("======================");


            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            if (detallecompra.IdInsumo != null && detallecompra.Cantidad != null)
            {

               
                // Obtiene todos los detalles de compra para la misma compra
                var detallesCompra = context.DetallesCompras
                    .Where(d => d.IdCompra == detallecompra.IdCompra)
                    .ToList();

                bool insumoExistente = false;

                // Recorre todos los detalles existentes y verifica si el insumo ya está en la compra
                foreach (var existingDetalle in detallesCompra)
                {
                    if (existingDetalle.IdInsumo == detallecompra.IdInsumo && existingDetalle.Medida == detallecompra.Medida)
                    {
                        // Si el insumo ya existe con la misma medida, actualiza el detalle existente
                        existingDetalle.Cantidad += detallecompra.Cantidad;
                        existingDetalle.PrecioInsumo += detallecompra.PrecioInsumo;
                        insumoExistente = true;
                        break; // Sale del ciclo ya que encontró el insumo existente
                    }
                }

                if (!insumoExistente)
                {
                    // Si el insumo no existe con la misma medida, crea un nuevo detalle
                    context.Add(detallecompra);
                }

                // Actualiza el total de la compra
                compra.Total += detallecompra.PrecioInsumo;

                // Actualiza la cantidad de insumo si es necesario
                var insumoo = context.Insumos.FirstOrDefault(i => i.IdInsumo == detallecompra.IdInsumo);
                var medida = detallecompra.Medida;
                if (insumoo != null)
                {
                    // Actualiza la cantidad de insumo según la medida
                    if (medida == "Gramos" || medida == "Unidad" || medida == "Mililitro")
                    {
                        insumoo.CantInsumo += detallecompra.Cantidad;
                    }
                    else if (medida == "Kilogramos")
                    {
                        var conversion = detallecompra.Cantidad * 1000;
                        insumoo.CantInsumo += conversion;
                    }
                    else if (medida == "Libras")
                    {
                        var conversion = detallecompra.Cantidad * 454;
                        insumoo.CantInsumo += conversion;
                    }
                    else if (medida == "Litros")
                    {
                        var conversion = detallecompra.Cantidad * 1000;
                        insumoo.CantInsumo += conversion;
                    }
                    else if (medida == "Onza")
                    {
                        var conversion = detallecompra.Cantidad * 30;
                        insumoo.CantInsumo += conversion;
                    }

                    context.Update(insumoo);
                }

                context.SaveChanges();

                return Redirect($"/DetalleCompra/Create/{detallecompra.IdCompra}");
            }
            else
            {
                ViewData["MessageInsumo"] = "Debe ingresar un insumo";
                ViewData["MessageCantidad"] = "Debe ingresar la cantidad";
                return Redirect($"/DetalleCompra/Create/{detallecompra.IdCompra}");
            }
        }



        // GET: Insumos/Create
        public IActionResult CrearInsumo()
        {
            return View();
        }

        // POST: Insumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearInsumo([Bind("IdInsumo,NomInsumo,CantInsumo,Medida")] Insumo insumo, [Bind(Prefix = "Item1")] DetallesCompra detallecompra, [Bind(Prefix = "Item2")] Compra compra)
        {
            bool tine_permiso = _permisosController.tinto(5, User);
            Console.WriteLine("======================");
            Console.WriteLine(tine_permiso);
            Console.WriteLine("======================");


            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            insumo.CantInsumo = 0;
            insumo.Estado = 1;
            insumo.NomInsumo = OrtografiaInsumo(insumo.NomInsumo);


            if (insumo.Medida == "Gramo")
            {
                insumo.CantInsumo = insumo.CantInsumo;
                insumo.Medida = "Gramo";
            }
            else if (insumo.Medida == "Kilogramo")
            {
                var convercion = insumo.CantInsumo * 1000;
                insumo.CantInsumo = convercion;
                insumo.Medida = "Gramo";
            }
            else if (insumo.Medida == "Mililitro")
            {
                insumo.CantInsumo += insumo.CantInsumo;
                insumo.Medida = "Mililitro";
            }
            else if (insumo.Medida == "Onza")
            {
                var convercion = insumo.CantInsumo * 30;
                insumo.CantInsumo = convercion;
                insumo.Medida = "Mililitro";
            }
            else if (insumo.Medida == "Litro")
            {
                var convercion = insumo.CantInsumo * 1000;
                insumo.CantInsumo = convercion;
                insumo.Medida = "Mililitro";
            }
            else if (insumo.Medida == "Unidad")
            {
                insumo.CantInsumo = insumo.CantInsumo;
                insumo.Medida = "Unidad";
            }

            context.Add(insumo);
            await context.SaveChangesAsync();

            return Redirect($"/DetalleCompra/Create/{detallecompra.IdCompra}");
            

        }

        private string OrtografiaInsumo(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada))
            {
                return entrada;
            }
            // Divide la cadena en palabras y aplica la transformación a cada una
            string[] palabra = entrada.Split(' ');
            for (int i = 0; i < palabra.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(palabra[i]))
                {
                    palabra[i] = char.ToUpper(palabra[i][0]) + palabra[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", palabra);
        }

        private string OrtografiaFactura(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada))
            {
                return entrada;
            }
            // Divide la cadena en palabras y aplica la transformación a cada una
            string[] palabra = entrada.Split(' ');
            for (int i = 0; i < palabra.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(palabra[i]))
                {
                    palabra[i] = palabra[i].ToUpper();
                }
            }
            return string.Join(" ", palabra);
        }


        public IActionResult ComfirmarCompra([Bind(Prefix = "Item2")] Compra compra)
        {
            bool tine_permiso = _permisosController.tinto(5, User);
            Console.WriteLine("======================");
            Console.WriteLine(tine_permiso);
            Console.WriteLine("======================");


            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            var Compra = context.Compras.Where(x => x.IdCompra == compra.IdCompra).FirstOrDefault()!;
            if (ModelState.IsValid)
            {
                if (compra.Total == 0)
                {
                    return Redirect($"/DetalleCompra/Create/{compra.IdCompra}");
                }
                Compra.IdProveedor = compra.IdProveedor;
                Compra.Total = compra.Total;
                Compra.NumeroFactura = OrtografiaFactura(compra.NumeroFactura);
                context.Update(Compra);
                context.SaveChanges();
                return Redirect("/Compras");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Details()
        {
            string? _urlData = HttpContext.Request.Path.Value;
            string[] splitData = _urlData.Split('/');
            var IdCompra = int.Parse(splitData[splitData.Length - 1]);

            var Ordenes = context.DetallesCompras
                .Where(o => o.IdCompra == IdCompra)
                .Join(
                    context.Insumos,
                    orden => orden.IdInsumo,
                    insumo => insumo.IdInsumo,
                    (orden, insumo) => new
                    {
                        insumo.NomInsumo,
                        orden.Cantidad,
                        orden.PrecioInsumo,
                        orden.Medida,
                        Total =orden.PrecioInsumo,
                        orden.IdDetallesCompra,
                        orden.IdCompra
                    }).ToList();
            var compra = context.Compras.Where(o => o.IdCompra == IdCompra).Select(x => new { x.IdCompra, x.IdProveedor,x.IdEmpleado, x.FechaCompra, x.Total }).ToList();
            ViewBag.Detalles = Ordenes;
            ViewBag.IdCompra = IdCompra;
            foreach (var item in compra)
            {
                ViewBag.TotalCompra = item.Total;
            }

            return View();
            
        }

        public async Task<IActionResult> Delete(String id, int otroId, int cantidad, int idinsumo, string medida)
        {
            bool tine_permiso = _permisosController.tinto(5, User);
            Console.WriteLine("======================");
            Console.WriteLine(tine_permiso);
            Console.WriteLine("======================");


            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            var orden = await context.DetallesCompras.FindAsync(int.Parse(id));
            if (orden == null)
            {
                return Redirect($"/DetalleCompra/Create/{otroId}");
            }
            else
            {
                var detalle = context.DetallesCompras.FirstOrDefault(i => i.IdDetallesCompra == int.Parse(id));
                if (detalle != null)
                {
                    var insumo = context.Insumos.FirstOrDefault(i => i.IdInsumo == idinsumo);
                    var medidas = medida;
                    if (medida == "Gramos" || medida == "Unidad" || medida == "Mililitro")
                    {
                        insumo.CantInsumo -= cantidad;
                    }
                    else if (medida == "Kilogramos")
                    {
                        var convercion = cantidad * 1000;
                        insumo.CantInsumo -= convercion;
                    }else if (medida == "Libras")
                    {
                        var convercion = cantidad * 454;
                        insumo.CantInsumo -= convercion;
                    }else if (medida == "Litros")
                    {
                        var convercion = cantidad * 1000;
                        insumo.CantInsumo -= convercion;
                    }
                    else if (medida == "Onza")
                    {
                        var convercion = cantidad * 30;
                        insumo.CantInsumo -= convercion;
                    }

                    context.Update(insumo);
                    context.SaveChanges();
                }
                context.DetallesCompras.Remove(orden);
                context.SaveChanges();
                return Redirect($"/DetalleCompra/Create/{otroId}");
            }
        }
    }
}
