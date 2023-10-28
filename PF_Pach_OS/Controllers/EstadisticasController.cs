using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;
using System.Diagnostics.Metrics;

namespace PF_Pach_OS.Controllers { 

    public class EstadisticasController : Controller
{
    private readonly Pach_OSContext _context;
    public EstadisticasController(Pach_OSContext context)
    {
        _context = context;
    }
        [HttpGet]
        public IActionResult ObtenerVentasMensuales()
        {
            // Obtener el mes actual y el año actual
            int mesActual = DateTime.Now.Month;
            int añoActual = DateTime.Now.Year;

            // Crear una lista para almacenar los datos de ventas mensuales
            List<decimal> ventasMensuales = new List<decimal>();

            for (int mes = 1; mes <= mesActual; mes++)
            {
                // Calcular el primer día del mes actual
                DateTime primerDiaDelMes = new DateTime(añoActual, mes, 1);

                // Calcular el último día del mes actual
                DateTime ultimoDiaDelMes = new DateTime(añoActual, mes, DateTime.DaysInMonth(añoActual, mes));

                // Calcular el total de ventas para el mes actual
                decimal totalVentasMes = _context.Ventas
                    .Where(v => v.FechaVenta >= primerDiaDelMes && v.FechaVenta <= ultimoDiaDelMes)
                    .Sum(v => v.TotalVenta.GetValueOrDefault());

                ventasMensuales.Add(totalVentasMes);
            }

            return Json(ventasMensuales);
        }

        public IActionResult ObtenerTotalVentasAnuales()
        {
            // Obtener el año actual
            int añoActual = DateTime.Now.Year;

            // Calcular el primer día del año actual
            DateTime primerDiaDelAño = new DateTime(añoActual, 1, 1);

            // Calcular el último día del año actual
            DateTime ultimoDiaDelAño = new DateTime(añoActual, 12, 31);

            // Calcular el total de ventas para todo el año
            decimal totalVentasAnuales = _context.Ventas
                .Where(v => v.FechaVenta >= primerDiaDelAño && v.FechaVenta <= ultimoDiaDelAño)
                .Sum(v => v.TotalVenta.GetValueOrDefault());

            return Json(totalVentasAnuales);
        }
        public IActionResult ObtenerDiferenciaVentasMesAnterior()
        {
            // Obtener el primer día del mes actual
            DateTime primerDiaDelMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Calcular el último día del mes anterior
            DateTime ultimoDiaDelMesAnterior = primerDiaDelMesActual.AddDays(-1);

            // Calcular el primer día del mes anterior
            DateTime primerDiaDelMesAnterior = new DateTime(ultimoDiaDelMesAnterior.Year, ultimoDiaDelMesAnterior.Month, 1);

            // Calcular el total de ventas para el mes actual
            decimal totalVentasMesActual = _context.Ventas
                .Where(v => v.FechaVenta >= primerDiaDelMesActual && v.FechaVenta <= DateTime.Now)
                .Sum(v => v.TotalVenta.GetValueOrDefault());

            // Calcular el total de ventas para el mes anterior
            decimal totalVentasMesAnterior = _context.Ventas
                .Where(v => v.FechaVenta >= primerDiaDelMesAnterior && v.FechaVenta <= ultimoDiaDelMesAnterior)
                .Sum(v => v.TotalVenta.GetValueOrDefault());

            // Calcular la diferencia
            decimal diferencia = totalVentasMesActual - totalVentasMesAnterior;

            // Determinar si es un aumento o una disminución
            string aumentoODisminucion = diferencia > 0 ? "aumento" : (diferencia < 0 ? "disminución" : "sin cambios");

            return Json(new { diferencia, aumentoODisminucion });
        }

        public IActionResult ObtenerVentasPorDia(string fecha)
        {
            // Convierte la fecha seleccionada a DateTime si es válido
            if (DateTime.TryParse(fecha, out DateTime fechaSeleccionada))
            {
                // Consulta para obtener las ventas del día seleccionado
                decimal ventasDelDia = _context.Ventas
                    .Where(v => v.FechaVenta.HasValue && v.FechaVenta.Value.Date == fechaSeleccionada.Date)
                    .Sum(v => v.TotalVenta.GetValueOrDefault());

                return Json(ventasDelDia);
            }

            // Maneja el caso en el que la conversión de la fecha falla
            return Json(0); // Puedes elegir cómo manejar este caso de acuerdo a tus necesidades
        }
        public IActionResult ObtenerProductosMasYMenosVendidos()
        {
            // Realiza la consulta para obtener los productos más vendidos y menos vendidos
            var productosMasVendidos = _context.DetalleVentas
                .GroupBy(dv => dv.IdProducto)
                .OrderByDescending(g => g.Sum(dv => dv.CantVendida))
                .Select(g => new { ProductoId = g.Key, TotalVendido = g.Sum(dv => dv.CantVendida) })
                .FirstOrDefault();

            var productosMenosVendidos = _context.DetalleVentas
                .GroupBy(dv => dv.IdProducto)
                .OrderBy(g => g.Sum(dv => dv.CantVendida))
                .Select(g => new { ProductoId = g.Key, TotalVendido = g.Sum(dv => dv.CantVendida) })
                .FirstOrDefault();

            // Puedes adaptar este código según la estructura de tu base de datos y tus modelos.

            return Json(new { MasVendido = productosMasVendidos, MenosVendido = productosMenosVendidos });
        }


        private List<Venta> ObtenerDatosVentas()
        {

            DateTime fechaInicio = DateTime.Now.AddMonths(-1); 
            DateTime fechaFin = DateTime.Now; 

            List<Venta> datosVentas = _context.Ventas

                .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                .Select(v => new Venta
                {
                    IdVenta = v.IdVenta,
                    FechaVenta = v.FechaVenta,
                    TotalVenta = v.TotalVenta
                })
                .ToList();

            return datosVentas;
        }


    }




}


