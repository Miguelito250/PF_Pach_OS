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
using ServiceStack.Text;
using Microsoft.AspNetCore.Hosting;
using PdfSharp.Charting;

namespace PF_Pach_OS.Controllers { 

    public class EstadisticasController : Controller
{
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly Pach_OSContext _context;

        public EstadisticasController(Pach_OSContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult ObtenerVentasMensuales()
        {
            int mesActual = DateTime.Now.Month;
            int añoActual = DateTime.Now.Year;

            List<decimal> ventasMensuales = new List<decimal>();

            for (int mes = 1; mes <= mesActual; mes++)
            {
                DateTime primerDiaDelMes = new DateTime(añoActual, mes, 1);

                DateTime ultimoDiaDelMes = new DateTime(añoActual, mes, DateTime.DaysInMonth(añoActual, mes));

                decimal totalVentasMes = _context.Ventas
                    .Where(v => v.FechaVenta >= primerDiaDelMes && v.FechaVenta <= ultimoDiaDelMes)
                    .Sum(v => v.TotalVenta.GetValueOrDefault());

                ventasMensuales.Add(totalVentasMes);
            }

            return Json(ventasMensuales);
        }
        [HttpGet]
        public IActionResult ObtenerComprasMensuales()
        {
            int mesActual = DateTime.Now.Month;
            int añoActual = DateTime.Now.Year;

            List<decimal> comprasMensuales = new List<decimal>();

            for (int mes = 1; mes <= mesActual; mes++)
            {
                DateTime primerDiaDelMes = new DateTime(añoActual, mes, 1);

                DateTime ultimoDiaDelMes = new DateTime(añoActual, mes, DateTime.DaysInMonth(añoActual, mes));

                decimal totalComprasMes = _context.Compras
                    .Where(c => c.FechaCompra >= primerDiaDelMes && c.FechaCompra <= ultimoDiaDelMes)
                    .Sum(c => c.Total.GetValueOrDefault());

                comprasMensuales.Add(totalComprasMes);
            }

            return Json(comprasMensuales);
        }

        public IActionResult ObtenerTotalVentasAnuales()
        {
            int añoActual = DateTime.Now.Year;

            DateTime primerDiaDelAño = new DateTime(añoActual, 1, 1);

            DateTime ultimoDiaDelAño = new DateTime(añoActual, 12, 31);

            decimal totalVentasAnuales = _context.Ventas
                .Where(v => v.FechaVenta >= primerDiaDelAño && v.FechaVenta <= ultimoDiaDelAño)
                .Sum(v => v.TotalVenta.GetValueOrDefault());

            return Json(totalVentasAnuales);
        }
        [HttpGet]
        public IActionResult ObtenerTotalComprasAnuales()
        {
            int añoActual = DateTime.Now.Year;

            DateTime primerDiaDelAño = new DateTime(añoActual, 1, 1);

            DateTime ultimoDiaDelAño = new DateTime(añoActual, 12, 31);

            decimal totalComprasAnuales = _context.Compras
                .Where(v => v.FechaCompra >= primerDiaDelAño && v.FechaCompra <= ultimoDiaDelAño)
                .Sum(v => v.Total.GetValueOrDefault());

            return Json(totalComprasAnuales);
        }
        public IActionResult ObtenerDiferenciaVentasMesAnterior()
        {
            DateTime primerDiaDelMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime ultimoDiaDelMesAnterior = primerDiaDelMesActual.AddDays(-1);

            DateTime primerDiaDelMesAnterior = new DateTime(ultimoDiaDelMesAnterior.Year, ultimoDiaDelMesAnterior.Month, 1);

            decimal totalVentasMesActual = _context.Ventas
                .Where(v => v.FechaVenta >= primerDiaDelMesActual && v.FechaVenta <= DateTime.Now)
                .Sum(v => v.TotalVenta.GetValueOrDefault());


            decimal totalVentasMesAnterior = _context.Ventas
                .Where(v => v.FechaVenta >= primerDiaDelMesAnterior && v.FechaVenta <= ultimoDiaDelMesAnterior)
                .Sum(v => v.TotalVenta.GetValueOrDefault());


            decimal diferencia = totalVentasMesActual - totalVentasMesAnterior;


            string aumentoODisminucion = diferencia > 0 ? "aumento" : (diferencia < 0 ? "disminución" : "sin cambios");

            return Json(new { diferencia, aumentoODisminucion });
        }

        public IActionResult ObtenerVentasPorDia(string fecha)
        {

            if (DateTime.TryParse(fecha, out DateTime fechaSeleccionada))
            {

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
                if (DateTime.TryParseExact(fechaSeleccionada, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    var fechaInicio = fecha;
                    var fechaFin = fecha.AddMonths(1).AddDays(-1);

                    var ventasEnRango = _context.Ventas
                        .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                        .ToList();
                    Console.WriteLine(ventasEnRango);
                    if (ventasEnRango.Count == 0)
                    {
                        return BadRequest("No se encontraron ventas en el rango seleccionado");
                    }

                    int? totalVentas = ventasEnRango.Sum(v => v.TotalVenta);
                    if (ventasEnRango.Count == 0)
                    {
                        return BadRequest("No se encontraron ventas en el rango seleccionado");
                    }
                    var ventasPorSemana = new Dictionary<int, List<Venta>>();

                    foreach (var venta in ventasEnRango)
                    {
                        var weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear((DateTime)venta.FechaVenta, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
                        if (!ventasPorSemana.ContainsKey(weekNumber))
                        {
                            ventasPorSemana[weekNumber] = new List<Venta>();
                        }
                        ventasPorSemana[weekNumber].Add(venta);
                    }


                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        string logoPath = Path.Combine(_hostingEnvironment.WebRootPath, "img", "Logo_Empresa.jpg");
                        if (System.IO.File.Exists(logoPath))
                        {
                        Document doc = new Document();
                        PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                        doc.Open();
                        Image logo = Image.GetInstance(logoPath);
                        logo.Alignment = Element.ALIGN_CENTER;
                        logo.ScaleToFit(150, 150); 
                        doc.Add(logo);

                            doc.Add(new Paragraph("Informe de Ventas Mensuales"));
                            doc.Add(new Paragraph(" "));

                            var fechaActual = fechaInicio;
                            var ultimoDiaDelMes = fechaFin;
                            string[] nombresDias = { "lunes", "martes", "miércoles", "jueves", "viernes", "sábado" ,"domingo" };
                            int diaNumero = 1;

                            while (fechaActual <= ultimoDiaDelMes)
                            {
                                PdfPTable diasTable = new PdfPTable(7);
                                PdfPTable ventasTable = new PdfPTable(7);

                                // Configura el ancho de las celdas
                                diasTable.WidthPercentage = 100;
                                ventasTable.WidthPercentage = 100;

                                // Llena las tablas con los datos de la semana
                                int diaSemanaInicio = (int)fechaActual.DayOfWeek;
                                for (int i = 0; i < 7; i++)
                                {
                                    int diaSemana = (diaSemanaInicio + i) % 7;
                                    PdfPCell cell = new PdfPCell(new Phrase(nombresDias[diaSemana] + " " + diaNumero));
                                    diasTable.AddCell(cell);
                                    if (diaNumero <= DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month))
                                    {
                                        var totalDiario = ventasEnRango
                                            .Where(v => v.FechaVenta.HasValue && v.FechaVenta.Value.Date == fechaActual.Date)
                                            .Sum(v => v.TotalVenta) ?? 0;

                                        PdfPCell totalCell = new PdfPCell(new Phrase(totalDiario.ToString("C")));

                                        ventasTable.AddCell(totalCell);

                                        fechaActual = fechaActual.AddDays(1);
                                        diaNumero++;
                                    }
                                    else
                                    {
                                        // Si se agotan los días del mes, agrega celdas vacías
                                        diasTable.AddCell(new PdfPCell());
                                        ventasTable.AddCell(new PdfPCell());
                                    }
                                }

                                doc.Add(diasTable);
                                doc.Add(ventasTable);

                                doc.Add(new Paragraph(" ")); // Agrega un espacio entre las semanas
                            }


                            Paragraph total = new Paragraph($"Total de Ventas: ${totalVentas}");
                        total.Alignment = Element.ALIGN_RIGHT; 
                        doc.Add(total);
                        doc.Close();

                        byte[] pdfBytes = memoryStream.ToArray();
                        return File(pdfBytes, "application/pdf", "InformeVentas.pdf");
                    }
                    }
                }
                else
                {
                }
            }
            else if (tipoInforme == "anual")
            {

                if (DateTime.TryParseExact(fechaSeleccionada, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {

                    var fechaInicio = new DateTime(fecha.Year, 1, 1);
                    var fechaFin = fechaInicio.AddYears(1).AddDays(-1);


                    var ventasEnRango = _context.Ventas
                        .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                        .ToList();

                    int? totalVentas = ventasEnRango.Sum(v => v.TotalVenta);
                    if (ventasEnRango.Count == 0)
                    {
                        return BadRequest("No se encontraron ventas en el rango seleccionado");
                    }
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        string logoPath = Path.Combine(_hostingEnvironment.WebRootPath, "img", "Logo_Empresa.jpg");
                        if (System.IO.File.Exists(logoPath))
                        {
                            Document doc = new Document();
                            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                            doc.Open();
                            Image logo = Image.GetInstance(logoPath);
                            logo.Alignment = Element.ALIGN_CENTER;
                            logo.ScaleToFit(150, 150);
                            doc.Add(logo);

                            doc.Add(new Paragraph("Informe de Ventas Anuales"));
                            doc.Add(new Paragraph(" "));
                            PdfPTable table = new PdfPTable(3);
                            table.WidthPercentage = 100;

                            table.AddCell("Venta ID");
                            table.AddCell("Fecha");
                            table.AddCell("Monto Total");

                            foreach (var venta in ventasEnRango)
                            {
                                table.AddCell(venta.IdVenta.ToString());
                                table.AddCell(venta.FechaVenta.HasValue ? venta.FechaVenta.Value.ToShortDateString() : "Fecha no disponible");
                                table.AddCell("$" + venta.TotalVenta.ToString());
                            }

                            doc.Add(table);
                            Paragraph total = new Paragraph($"Total de Ventas: ${totalVentas}");
                            total.Alignment = Element.ALIGN_RIGHT; 
                            doc.Add(total);
                            doc.Close();

                            byte[] pdfBytes = memoryStream.ToArray();
                            return File(pdfBytes, "application/pdf", "InformeVentas.pdf");
                        }
                    }
                }
                else
                {
                }
            }
            else
            {
            }

            return RedirectToAction("Index");
        }




    }




}


