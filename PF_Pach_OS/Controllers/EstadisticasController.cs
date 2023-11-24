using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;

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

            return Json(0); 
        }
        [HttpGet]
        public IActionResult ObtenerProductosMasYMenosVendidos()
        {
  
            var productosMasVendidos = _context.DetalleVentas
                .GroupBy(dv => dv.IdProducto)
                .OrderByDescending(g => g.Sum(dv => dv.CantVendida))
                .Select(g => g.First())
                .FirstOrDefault();


            var productosMenosVendidos = _context.DetalleVentas
                .GroupBy(dv => dv.IdProducto)
                .OrderBy(g => g.Sum(dv => dv.CantVendida))
                .Select(g => g.First())
                .FirstOrDefault();

            var model = new ProductosMasMenosVendidosModel
            {
                ProductoMasVendido = productosMasVendidos,
                ProductoMenosVendido = productosMenosVendidos
            };

            return View(model);
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
        [HttpGet]
        public ActionResult ObtenerVentas(string fechaSeleccionada, string tipoInforme)
        {
            DateTime fecha=DateTime.Now;

            if (tipoInforme == "mensual")
            {
                // Parsea la fecha seleccionada en formato "yyyy-MM" (por ejemplo, "2023-10")
                if (DateTime.TryParseExact(fechaSeleccionada, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    // Implementa la lógica para el informe mensual aquí
                    var fechaInicio = fecha;
                    var fechaFin = fecha.AddMonths(1).AddDays(-1);

                    // Consulta tus ventas dentro del rango de fechas seleccionado
                    var ventasEnRango = _context.Ventas
                        .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                        .ToList();
                    Console.WriteLine(ventasEnRango);
                    if (ventasEnRango.Count == 0)
                    {
                        // No hay ventas en el mes seleccionado, muestra una notificación SweetAlert
                        return BadRequest("No se encontraron ventas en el rango seleccionado");
                    }
                    foreach (PF_Pach_OS.Models.Venta venta in ventasEnRango)
                    {
                        Console.WriteLine($"Propiedad 1: {venta.IdVenta}");
                        // Sustituye 'Propiedad1', 'Propiedad2', etc., por los nombres de las propiedades de tu modelo 'Venta'
                    }
                    // Calcula el total de ventas en el rango
                    int? totalVentas = ventasEnRango.Sum(v => v.TotalVenta);
                    if (ventasEnRango.Count == 0)
                    {
                        return BadRequest("No se encontraron ventas en el rango seleccionado");
                    }

                    // Crear el documento PDF
                    Document doc = new Document();
                    MemoryStream memoryStream = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                    doc.Open();

                    // Agregar contenido al PDF basado en las ventas dentro del rango
                    doc.Add(new Paragraph("Informe de Ventas"));

                    // Agregar información de ventas
                    doc.Add(new Paragraph("Fecha de inicio: " + fechaInicio.ToShortDateString()));
                    doc.Add(new Paragraph("Fecha de fin: " + fechaFin.ToShortDateString()));
                    doc.Add(new Paragraph("Total de Ventas: $" + totalVentas));
                    doc.Add(Chunk.NEWLINE);

                    foreach (var venta in ventasEnRango)
                    {
                        doc.Add(new Paragraph($"Venta ID: {venta.IdVenta}"));
                        doc.Add(new Paragraph($"Fecha: {venta.FechaVenta}"));
                        doc.Add(new Paragraph($"Monto Total: ${venta.TotalVenta}"));
                        doc.Add(Chunk.NEWLINE);
                    }

                    doc.Close();

                    byte[] pdfBytes = memoryStream.ToArray();
                    return File(pdfBytes, "application/pdf", "InformeVentas.pdf");
                }
                else
                {
                    // Manejar error de fecha no válida
                }
            }
            else if (tipoInforme == "anual")
            {
                // Parsea la fecha seleccionada en formato "yyyy" (por ejemplo, "2023")
                if (DateTime.TryParseExact(fechaSeleccionada, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    // Implementa la lógica para el informe anual aquí
                    var fechaInicio = new DateTime(fecha.Year, 1, 1);
                    var fechaFin = fechaInicio.AddYears(1).AddDays(-1);

                    // Consulta tus ventas dentro del rango de fechas seleccionado
                    var ventasEnRango = _context.Ventas
                        .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                        .ToList();

                    // Calcula el total de ventas en el rango
                    int? totalVentas = ventasEnRango.Sum(v => v.TotalVenta);
                    if (ventasEnRango.Count == 0)
                    {
                        return BadRequest("No se encontraron ventas en el rango seleccionado");
                    }
                    // Crear el documento PDF
                    Document doc = new Document();
                    MemoryStream memoryStream = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                    doc.Open();

                    // Agregar contenido al PDF basado en las ventas dentro del rango
                    doc.Add(new Paragraph("Informe de Ventas"));

                    // Agregar información de ventas
                    doc.Add(new Paragraph("Tipo de Informe: " + tipoInforme));
                    doc.Add(new Paragraph("Fecha seleccionada: " + fechaSeleccionada));
                    doc.Add(new Paragraph("Ventas del informe:"+ totalVentas));
                    doc.Add(Chunk.NEWLINE);

                    foreach (var venta in ventasEnRango)
                    {
                        doc.Add(new Paragraph($"Venta ID: {venta.IdVenta}"));
                        doc.Add(new Paragraph($"Fecha: {venta.FechaVenta}"));
                        doc.Add(new Paragraph($"Monto Total: ${venta.TotalVenta}"));
                        doc.Add(new Paragraph("------------------------------------------------------------------------"));
                        doc.Add(Chunk.NEWLINE);
                    }

                    doc.Close();
                    byte[] pdfBytes = memoryStream.ToArray();
                    // Devolver el PDF como un archivo descargable
                    return File(pdfBytes, "application/pdf", "InformeVentas.pdf");
                }
                else
                {
                    // Manejar error de fecha no válida
                }
            }
            else
            {
                // Manejar error de tipo de informe no válido
            }

            // Manejar otro tipo de error o redirigir a la página de inicio
            return RedirectToAction("Index");
        }


    }




}


