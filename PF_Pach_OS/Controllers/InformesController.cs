using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PF_Pach_OS.Models;
using static iTextSharp.text.pdf.AcroFields;
using System.Globalization;

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

            decimal diferencia;
            string aumentoODisminucion;
            string graficaMostrar;
            string colorClase;
            int DiferenciaMensual;
            string iconoDiferenciaMensual;
            string colorClaseMensual;

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

            //Grafica 

            DateTime primerDiaDelMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime ultimoDiaDelMesAnterior = primerDiaDelMesActual.AddDays(-1);

            DateTime primerDiaDelMesAnterior = new DateTime(ultimoDiaDelMesAnterior.Year, ultimoDiaDelMesAnterior.Month, 1);

            decimal totalVentasMesActual = _context.Ventas
                .Where(v => v.FechaVenta >= primerDiaDelMesActual && v.FechaVenta <= DateTime.Now)
                .Sum(v => v.TotalVenta.GetValueOrDefault());

            decimal totalVentasMesAnterior = _context.Ventas
                .Where(v => v.FechaVenta >= primerDiaDelMesAnterior && v.FechaVenta <= ultimoDiaDelMesAnterior)
                .Sum(v => v.TotalVenta.GetValueOrDefault());

            if (totalVentasMesActual > totalVentasMesAnterior)
            {
                diferencia = ventasMensuales - totalVentasMesAnterior;
                aumentoODisminucion = "Aumento de ventas";
                graficaMostrar = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"16\" height=\"16\" fill=\"green\" class=\"bi bi-arrow-up\" viewBox=\"0 0 16 16\">\r\n  <path fill-rule=\"evenodd\" d=\"M8 15a.5.5 0 0 0 .5-.5V2.707l3.146 3.147a.5.5 0 0 0 .708-.708l-4-4a.5.5 0 0 0-.708 0l-4 4a.5.5 0 1 0 .708.708L7.5 2.707V14.5a.5.5 0 0 0 .5.5\"/>\r\n</svg>";
                colorClase = "text-success";
            }
            else if (totalVentasMesAnterior > totalVentasMesActual)
            {
                diferencia = totalVentasMesAnterior - ventasMensuales;
                aumentoODisminucion = "Diminución de ventas";
                graficaMostrar = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"16\" height=\"16\" fill=\"red\" class=\"bi bi-arrow-down\" viewBox=\"0 0 16 16\">\r\n  <path fill-rule=\"evenodd\" d=\"M8 1a.5.5 0 0 1 .5.5v11.793l3.146-3.147a.5.5 0 0 1 .708.708l-4 4a.5.5 0 0 1-.708 0l-4-4a.5.5 0 0 1 .708-.708L7.5 13.293V1.5A.5.5 0 0 1 8 1\"/>\r\n</svg>";
                colorClase = "text-danger";
            }
            else
            {
                diferencia = 0;
                aumentoODisminucion = "Sin cambios";
                graficaMostrar = "";
                colorClase = "text-danger";
            }
            string diferenciaFormateada = (diferencia).ToString("C0", new CultureInfo("es-CO")).Replace(",", ".");
            string mensaje = $"{diferenciaFormateada} ({aumentoODisminucion})";

            if (ventasMensuales > comprasMensuales)
            {
                DiferenciaMensual = ventasMensuales - comprasMensuales;
                iconoDiferenciaMensual = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" fill =\"green\" class=\"bi bi-graph-up-arrow mr-2\" viewBox =\"0 0 16 16\" ><path fill-rule=\"evenodd\" d =\"M0 0h1v15h15v1H0zm10 3.5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 .5.5v4a.5.5 0 0 1-1 0V4.9l-3.613 4.417a.5.5 0 0 1-.74.037L7.06 6.767l-3.656 5.027a.5.5 0 0 1-.808-.588l4-5.5a.5.5 0 0 1 .758-.06l2.609 2.61L13.445 4H10.5a.5.5 0 0 1-.5-.5\" /></svg>";
                colorClaseMensual = "text-success";
            }
            else
            {
                DiferenciaMensual = comprasMensuales - ventasMensuales;
                iconoDiferenciaMensual = "<svg xmlns=\"http://www.w3.org/2000/svg\" width =\"24\" height =\"24\" fill =\"red\" class=\"bi bi-graph-down-arrow\" viewBox =\"0 0 16 16\" ><path fill-rule=\"evenodd\" d =\"M0 0h1v15h15v1H0zm10 11.5a.5.5 0 0 0 .5.5h4a.5.5 0 0 0 .5-.5v-4a.5.5 0 0 0-1 0v2.6l-3.613-4.417a.5.5 0 0 0-.74-.037L7.06 8.233 3.404 3.206a.5.5 0 0 0-.808.588l4 5.5a.5.5 0 0 0 .758.06l2.609-2.61L13.445 11H10.5a.5.5 0 0 0-.5.5\"/></svg>";
                colorClaseMensual = "text-danger";
            }

            ViewBag.ventasMensuales = ventasMensuales;
            ViewBag.comprasMensuales = comprasMensuales;
            ViewBag.transferencias = dineroTransferencias;
            ViewBag.efectivo = dineroEfectivo;
            ViewBag.diferencia = diferencia;
            ViewBag.aumentoDisminucion = mensaje;
            ViewBag.graficaMostrar = graficaMostrar;
            ViewBag.color = colorClase;
            ViewBag.DiferenciaMensual = DiferenciaMensual;
            ViewBag.iconoDiferenciaMensual = iconoDiferenciaMensual;
            ViewBag.colorClaseMensual = colorClaseMensual;
            return View();
        }
    }
}
