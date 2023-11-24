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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using iTextSharp.text.html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace PF_Pach_OS.Controllers
{

    [Authorize]
    public class EstadisticasController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly Pach_OSContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly PermisosController _permisosController;

        public EstadisticasController(Pach_OSContext context, IWebHostEnvironment hostingEnvironment, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
            _permisosController = new PermisosController(_context, _userManager, _signInManager);
        }
        [HttpGet]
        public IActionResult ObtenerVentasMensuales()
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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


            string aumentoODisminucion = diferencia > 0 ? "aumento Ventas" : (diferencia < 0 ? "disminución Ventas" : "sin cambios");

            return Json(new { diferencia, aumentoODisminucion });
        }

        public IActionResult ObtenerVentasPorDia(string fecha)
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            if (DateTime.TryParse(fecha, out DateTime fechaSeleccionada))
            {

                decimal ventasDelDia = _context.Ventas
    .Where(v => v.FechaVenta.Date == fechaSeleccionada.Date && v.TotalVenta != null)
    .Sum(v => v.TotalVenta.GetValueOrDefault());

                return Json(ventasDelDia);
            }

            return Json(0);
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
        //Descargamos el pdf con los datos de las ventas
        [HttpGet]
        public ActionResult ObtenerVentas(string fechaSeleccionada, string tipoInforme)
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            DateTime fecha = DateTime.Now;

            if (tipoInforme == "mensual")
            {
                if (DateTime.TryParseExact(fechaSeleccionada, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    CultureInfo myCulture = new CultureInfo("es-ES");
                    myCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

                    var fechaInicio = fecha;
                    var fechaFin = fecha.AddMonths(1).AddDays(-1);

                    var ventasEnRango = _context.Ventas
                        .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin)
                        .ToList();

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
                        var weekNumber = myCulture.Calendar.GetWeekOfYear((DateTime)venta.FechaVenta, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
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
                            Document doc = new Document(PageSize.A4.Rotate());
                            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                            doc.Open();
                            Image logo = Image.GetInstance(logoPath);
                            logo.Alignment = Element.ALIGN_CENTER;
                            logo.ScaleToFit(150, 150);
                            doc.Add(logo);
                            BaseFont fuente = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
                            iTextSharp.text.Font titulo = new iTextSharp.text.Font(fuente, 16f, iTextSharp.text.Font.ITALIC, new BaseColor(89, 78, 75));
                            BaseFont fuente2 = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
                            iTextSharp.text.Font titulo_tablas = new iTextSharp.text.Font(fuente, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
                            doc.Add(new Paragraph($"Informe de Ventas Mensuales - {fecha.ToString("MMMM yyyy")}", titulo));
                            doc.Add(new Paragraph(" "));

                            var fechaActual = fechaInicio;
                            var ultimoDiaDelMes = fechaFin;
                            string[] nombresDias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };
                            int diaSemanaInicio = (int)fechaActual.DayOfWeek;

                            if (myCulture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday)
                            {
                                diaSemanaInicio = (diaSemanaInicio + 6) % 7;
                            }

                            int diaNumero = 1;
                            int semanaActual = 1;

                            while (fechaActual <= ultimoDiaDelMes)
                            {
                                PdfPTable diasTable = new PdfPTable(7);
                                PdfPTable ventasTable = new PdfPTable(7);

                                diasTable.WidthPercentage = 100;
                                ventasTable.WidthPercentage = 100;
                                BaseColor colorGris = new BaseColor(0xFF, 0xFC, 0xC4);
                                BaseColor colorBlanco = BaseColor.WHITE;


                                PdfPCell semanaCell = new PdfPCell(new Phrase("Semana " + semanaActual, titulo_tablas));
                                semanaCell.Colspan = 7;
                                semanaCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                diasTable.AddCell(semanaCell);

                                for (int i = 0; i < 7; i++)
                                {
                                    if (diaNumero <= DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month))
                                    {
                                        int diaSemana = (diaSemanaInicio + i) % 7;
                                        PdfPCell cell = new PdfPCell(new Phrase(nombresDias[diaSemana] + " " + diaNumero, titulo_tablas));
                                        if (i % 1 == 0)
                                        {
                                            cell.BackgroundColor = colorGris;
                                        }
                                        else
                                        {
                                            cell.BackgroundColor = colorBlanco;
                                        }
                                        diasTable.AddCell(cell);

                                        int totalDiario = ventasEnRango
    .Where(v => v.FechaVenta.Date == fechaActual.Date)
    .Sum(v => v.TotalVenta.GetValueOrDefault());

                                        PdfPCell totalCell = new PdfPCell(new Phrase(totalDiario.ToString("C"), titulo_tablas));
                                        ventasTable.AddCell(totalCell);

                                        fechaActual = fechaActual.AddDays(1);
                                        diaNumero++;
                                    }
                                    else
                                    {
                                        diasTable.AddCell(new PdfPCell());
                                        ventasTable.AddCell(new PdfPCell());
                                    }
                                }

                                doc.Add(diasTable);
                                doc.Add(ventasTable);

                                // Total de ventas de la semana
                                int totalVentasSemana = ventasEnRango
    .Where(v => v.FechaVenta.Date >= fechaActual.AddDays(-7) && v.FechaVenta.Date < fechaActual)
    .Sum(v => v.TotalVenta.GetValueOrDefault());

                                doc.Add(new Paragraph("Total Semana " + semanaActual + ": " + totalVentasSemana.ToString("C"), titulo_tablas));
                                doc.Add(new Paragraph(" "));

                                semanaActual++;
                            }



                            Paragraph total = new Paragraph($"Total de Ventas: ${totalVentas}", titulo_tablas);
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

                    if (ventasEnRango.Count == 0)
                    {
                        return BadRequest("No se encontraron ventas en el rango seleccionado");
                    }

                    int? totalVentas = ventasEnRango.Sum(v => v.TotalVenta);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        string logoPath = Path.Combine(_hostingEnvironment.WebRootPath, "img", "Logo_Empresa.jpg");

                        if (System.IO.File.Exists(logoPath))
                        {
                            Document doc = new Document(PageSize.A4.Rotate());
                            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                            doc.Open();
                            Image logo = Image.GetInstance(logoPath);
                            logo.Alignment = Element.ALIGN_CENTER;
                            logo.ScaleToFit(150, 150);
                            doc.Add(logo);

                            BaseFont fuente = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
                            iTextSharp.text.Font titulo = new iTextSharp.text.Font(fuente, 16f, iTextSharp.text.Font.ITALIC, new BaseColor(89, 78, 75));
                            iTextSharp.text.Font titulo_tablas = new iTextSharp.text.Font(fuente, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

                            doc.Add(new Paragraph($"Informe de Ventas Anuales - {fecha.Year}", titulo));
                            doc.Add(new Paragraph(" "));

                            PdfPTable table = new PdfPTable(2);
                            table.WidthPercentage = 100;
                            BaseColor colorAmarillo = new BaseColor(0xFF, 0xFC, 0xC4);
                            BaseColor colorBlanco = BaseColor.WHITE;
                            string hexColor = "#f9f9f9";
                            BaseColor colorGris = WebColors.GetRGBColor(hexColor);
                            BaseColor colortitulomes = colorAmarillo;
                            table.AddCell(new PdfPCell(new Phrase("Mes", new iTextSharp.text.Font(fuente, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK))) { BackgroundColor = colorAmarillo });
                            table.AddCell(new PdfPCell(new Phrase("Ventas Totales", new iTextSharp.text.Font(fuente, 12f, iTextSharp.text.Font.BOLD, BaseColor.BLACK))) { BackgroundColor = colorAmarillo });

                            bool alternarColorFila = false;

                            for (int mes = 1; mes <= 12; mes++)
                            {
                                var ventasMes = ventasEnRango.Where(v => v.FechaVenta.Month == mes);
                                var totalVentasMes = ventasMes.Sum(v => v.TotalVenta.GetValueOrDefault());
                                BaseColor colorFila = alternarColorFila ? colorGris : colorBlanco;
                                BaseColor colorTexto = colorFila == colorGris ? BaseColor.BLACK : BaseColor.BLACK;
                                table.AddCell(new PdfPCell(new Phrase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes), new iTextSharp.text.Font(fuente, 12f, iTextSharp.text.Font.BOLD, colorTexto))) { BackgroundColor = colorFila });
                                table.AddCell(new PdfPCell(new Phrase($"{totalVentasMes:C}", new iTextSharp.text.Font(fuente, 12f, iTextSharp.text.Font.BOLD, colorTexto))) { BackgroundColor = colorFila });
                                alternarColorFila = !alternarColorFila;
                            }


                            doc.Add(table);

                            Paragraph total = new Paragraph($"Total de Ventas: {totalVentas:C}", titulo_tablas);
                            total.Alignment = Element.ALIGN_RIGHT;
                            doc.Add(total);
                            doc.Close();

                            byte[] pdfBytes = memoryStream.ToArray();
                            return File(pdfBytes, "application/pdf", "InformeVentasAnuales.pdf");
                        }
                    }
                }
            }
            else
            {
            }

            return RedirectToAction("Index");
        }


        //obtenemos los productos mas vendidos
        [HttpGet]
        public IActionResult ObtenerProductosMasVendidos()
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            var fechaActual = DateTime.Now;
            var mesActual = new DateTime(fechaActual.Year, fechaActual.Month, 1);

            var productosMasVendidos = _context.DetalleVentas
              .Where(dv => dv.IdVentaNavigation.FechaVenta >= mesActual)
              .GroupBy(dv => dv.IdProductoNavigation.NomProducto)
              .Select(g => new
              {
                  Producto = g.Key,
                  CantidadVendida = g.Sum(x => x.CantVendida),
                  TotalVendido = g.Sum(x => x.CantVendida * x.Precio)
              })
              .OrderByDescending(x => x.TotalVendido)
              .Take(4)
              .ToList();




            return Json(productosMasVendidos);
        }
        //obtenemos los productos menos vendidos

        [HttpGet]
        public IActionResult ObtenerProductosMenosVendidos()
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            var productosMenosVendidos = _context.DetalleVentas
                .OrderBy(p => p.CantVendida)
                .Take(2)
                .Select(dv => new
                {
                    Producto = dv.IdProductoNavigation.NomProducto,
                    CantidadVendida = dv.CantVendida
                })
                .ToList();

            return Json(productosMenosVendidos);
        }
        //Eliminamos las ventas 
        [HttpPost]
        public IActionResult EliminarTodasLasVentas()
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            try
            {
                var ventas = _context.Ventas.ToList();
                var compras = _context.Compras.ToList();

                if (ventas.Any() || compras.Any())
                {
                    _context.Ventas.RemoveRange(ventas);
                    //_context.Compras.RemoveRange(compras);
                    _context.SaveChanges();

                    return Json(new { success = true, message = "Todas las ventas y compras han sido eliminadas correctamente." });
                }
                else
                {
                    return BadRequest(new { Message = "No hay ventas ni compras para eliminar." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al intentar eliminar las ventas y compras." });

            }
        }
        //obtenemos las transferencias para insertarlas en las tablas
        [HttpGet]
        public IActionResult ObtenerDatosTransferencias(int mes)
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            var dineroTransferencias = _context.Ventas
                .Where(v => v.TipoPago == "Transferencia" && v.FechaVenta.Month == mes)
                .Sum(v => v.TotalVenta);
            return Json(new
            {
                totalTransferencias = dineroTransferencias
            });
        }
        //obtenemos los pagos en efectivo para insertarlas en las tablas
        [HttpGet]
        public IActionResult ObtenerDatosEfectivo(int mes)
        {
            bool tine_permiso = _permisosController.tinto(1, User);

            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

            var dineroEfectivo = _context.Ventas
                .Where(v => v.TipoPago != "Transferencia" && v.FechaVenta.Month == mes)
                .Sum(v => v.TotalVenta);

            return Json(new
            {
                totalEfectivo = dineroEfectivo
            });
        }

        // Movil

        [AllowAnonymous]
        public async Task<IActionResult> VentasdelMes()
        {
            DateTime fecha = DateTime.Today;

            var pach_OSContext = await _context.Ventas
               .Where(v => v.FechaVenta.Month == fecha.Month)
               .ToListAsync();
            return Json(pach_OSContext);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ComprasdelMes()
        {
            DateTime fecha = DateTime.Today;

            var pach_OSContext = await _context.Compras
                .Where(v => v.FechaCompra.HasValue && v.FechaCompra.Value.Month == fecha.Month)
                .ToListAsync();
            return Json(pach_OSContext);
        }

        [AllowAnonymous]
        public async Task<IActionResult> VentasAño()
        {

            DateTime fecha = DateTime.Today;

            var pach_OSContext = await _context.Ventas
               .Where(v => v.FechaVenta.Year == fecha.Year)
               .ToListAsync();
            return Json(pach_OSContext);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ComprasAño()
        {

            DateTime fecha = DateTime.Today;

            var pach_OSContext = await _context.Compras
             .Where(v => v.FechaCompra.HasValue && v.FechaCompra.Value.Year == fecha.Year)
            .ToListAsync();

            return Json(pach_OSContext);
        }

        [AllowAnonymous]
        public IActionResult ProductosMasVendidosMes()
        {
            var fechaActual = DateTime.Now;
            var mesActual = new DateTime(fechaActual.Year, fechaActual.Month, 1);

            var productosMasVendidos = _context.DetalleVentas
              .Where(dv => dv.IdVentaNavigation.FechaVenta >= mesActual)
              .GroupBy(dv => dv.IdProductoNavigation.NomProducto)
              .Select(g => new
              {
                  Producto = g.Key,
                  CantidadVendida = g.Sum(x => x.CantVendida),
                  TotalVendido = g.Sum(x => x.CantVendida * x.Precio)
              })
              .OrderByDescending(x => x.TotalVendido)
              .Take(4)
              .ToList();

            return Json(productosMasVendidos);
        }

    }




}


