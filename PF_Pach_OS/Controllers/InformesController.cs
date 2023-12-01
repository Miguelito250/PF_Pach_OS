using Microsoft.AspNetCore.Mvc;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class InformesController : Controller
    {
        private readonly Pach_OSContext _context;
        public InformesController(Pach_OSContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
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
