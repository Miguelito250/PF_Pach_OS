using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    [Authorize]
    public class InformesController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly PermisosController _permisosController;
        public InformesController(Pach_OSContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _permisosController = new PermisosController(_context, _userManager, _signInManager);
        }
        public async Task<IActionResult> Index()
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            var fechaActual = DateTime.Now;
            int mesActual = DateTime.Now.Month;
            int añoActual = DateTime.Now.Year;
            var mesActualProductosMasVendidos = new DateTime(fechaActual.Year, fechaActual.Month, 1);

            var ventasMensuales = _context.Ventas
                .Where(v => v.FechaVenta.Month == mesActual)
                 .Sum(v => v.TotalVenta.GetValueOrDefault());

            var comprasMensuales = _context.Compras
                .Where(v => v.FechaCompra.Value.Month == mesActual)
                 .Sum(v => v.Total.GetValueOrDefault());

            var dineroTransferencias = _context.Ventas
                .Where(v => v.TipoPago == "Transferencia" && v.FechaVenta.Month == mesActual)
                .Sum(v => v.TotalVenta);

            var dineroEfectivo = _context.Ventas
               .Where(v => v.TipoPago != "Transferencia" && v.FechaVenta.Month == mesActual)
               .Sum(v => v.TotalVenta);

            ViewBag.ventasMensuales = ventasMensuales;
            ViewBag.comprasMensuales = comprasMensuales;
            ViewBag.transferencias = dineroTransferencias;
            ViewBag.efectivo = dineroEfectivo;

            return View();
        }
    }
}
