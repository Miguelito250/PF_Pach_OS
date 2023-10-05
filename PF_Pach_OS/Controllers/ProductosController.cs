//Autor: Juan Andres Navarro Herrera
//Fecha se reación: 10 de agosto del 2023
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class ProductosController : Controller
    {
        private readonly Pach_OSContext _context;

        public ProductosController(Pach_OSContext context)
        {
            _context = context;
        }

        // Esta Función Permite eliminar los insumos asosiados a un producto 
        public void Eliminar_Receta(int id_Productos)
        {
            var reseta = _context.Recetas;

            if (reseta != null)
            {
                foreach (var rec in reseta)
                {
                    if (rec.IdProducto == id_Productos)
                    {
                        _context.Recetas.Remove(rec);

                    }

                }
            }

        }
        // Elimina los regritros que poseean algun campo nulo para que no sea listado, envia una lista ordenada del mas reciente al mas antiguo
        public async Task<IActionResult> Index()
        {
            var pach_OSContext = await _context.Productos.ToListAsync();

            foreach (var pach in pach_OSContext)
            {

                if (pach.NomProducto == null || pach.PrecioVenta == null || pach.IdCategoria == null || pach.Estado == null)
                {
                    Eliminar_Receta(pach.IdProducto);
                    _context.Productos.Remove(pach);
                    _context.SaveChanges();

                }
                if (pach.IdCategoria == 1)
                {
                    if (pach.IdTamano == null)
                    {
                        Eliminar_Receta(pach.IdProducto);
                        _context.Productos.Remove(pach);
                        _context.SaveChanges();

                    }
                }



            }


            return View(Enumerable.Reverse(pach_OSContext).ToList());
        }




        //Envia la informacion del producto que se esta creando o actualizando es ese momento
        public void ProductoActivo(int id)
        {
            var productoActivo = _context.Productos.FirstOrDefault(p => p.IdProducto == id);

            if (productoActivo != null)
            {
                var categoriaActiva = _context.Categorias.FirstOrDefault(p => p.IdCategoria == productoActivo.IdCategoria);
                var tamanoActivo = _context.Tamanos.FirstOrDefault(p => p.IdTamano == productoActivo.IdTamano);

                ViewBag.ProductoActivo = productoActivo;
                if (categoriaActiva != null)
                {


                    ViewBag.SelectCategoria = categoriaActiva.NomCategoria;
                    ViewBag.SelectCategoriaID = categoriaActiva.IdCategoria;
                }
                if (tamanoActivo != null)
                {
                    ViewBag.SelectTamano = tamanoActivo.NombreTamano;
                    ViewBag.SelectTamanoID = tamanoActivo.IdTamano;
                }

            }
            else
            {
                ViewBag.ProductoActivo = null;
            }

        }

        // Crea la informacion nesesaria para trabajar un Producto, las resetas ya relacionadas a este, se asosia los id's de resetas con los de insumos
        //  Tambien se enivia la informacion de las categorias y tamaños en dos selects list 
        public IActionResult CrearInformacionFormulario(int IdProducto)
        {

            ProductoActivo(IdProducto);
            var recetas = _context.Recetas.ToList();
            var insumos = _context.Insumos.ToList();

            var recetasConInsumos = recetas.Select(receta => new
            {
                IdReceta = receta.IdReceta,
                CantInsumo = receta.CantInsumo,
                NomInsumo = insumos.FirstOrDefault(i => i.IdInsumo == receta.IdInsumo)?.NomInsumo,
                IdProducto = receta.IdProducto,
                Medida = insumos.FirstOrDefault(i => i.IdInsumo == receta.IdInsumo)?.Medida,
            }).ToList();

            ViewBag.RecetasConInsumos = recetasConInsumos.Cast<object>().ToList();
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NomCategoria");
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "NombreTamano");
            ViewBag.NombreTamano = new SelectList(_context.Tamanos, "IdTamano", "NombreTamano");
            ViewBag.Insumo = _context.Insumos;
            ViewBag.IdProducto = IdProducto;
            return View("Crear");
        }


        // Crear un producto "vasio" solo con su Id para posteriormente actualizar lo con la informacion nueva 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("IdProducto")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();

                // Redirige a la acción "Actualizar" con el IdProducto como parámetro en la URL
                ViewBag.IdProducto = producto.IdProducto;
                return RedirectToAction("CrearInformacionFormulario", "Productos", new { producto.IdProducto });
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NomCategoria");
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "NombreTamano");



            ViewBag.Insumo = new SelectList(_context.Insumos, "IdInsumo", "NomInsumo");
            return NotFound();
        }

       
        // Acutualiza la informacion del Producto asi como la de su reseta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar([Bind("IdProducto, NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto)
        {
            int idPizza = 1;
            var insumo = _context.Recetas;
            bool existe = true;
            foreach (var insu in insumo)
            {
                if (insu.IdProducto == producto.IdProducto)
                {
                    existe = false;
                    break;
                }
            }
            if (producto.IdCategoria != idPizza)
            {
                producto.IdTamano = null;
            }


            if (ModelState.IsValid)
            {



                producto.Estado = 1;



                _context.Update(producto);
                await _context.SaveChangesAsync();
                ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", producto.IdCategoria);
                ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "nombre_tamano", producto.IdTamano);

                if (existe)
                {
                    return RedirectToAction("CrearInformacionFormulario", "Productos", new { producto.IdProducto });

                }

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("CrearInformacionFormulario", "Productos", new { producto.IdProducto, accion = "Crear" });
        }

        //Toma el producto que se desea editar y lo despliega en un formulario 
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "IdTamano", producto.IdTamano);
            return View(producto);
        }

        //Actualiza la informacion de un proeducto 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdProducto,NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteElProducto(producto.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", producto.IdCategoria);
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "IdTamano", producto.IdTamano);
            return View(producto);
        }

        //Elimina un producto en caso que que este no se este utilizando en ninguna venta 
        public async Task<IActionResult> Eliminar(int id)
        {
            bool exsite= ExisteEnVentas(id);
            if (exsite)
            {
                Deshabilitar(id);
            }
            else
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto != null)
                {
                    Eliminar_Receta(id);
                    _context.Productos.Remove(producto);
                    _context.SaveChanges();
                    return RedirectToAction("Index");

                }
               

            }
            return RedirectToAction("Index");
        }
        //Habilita un producto que este deshabilitado 
        public IActionResult Habilitar(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                producto.Estado = 1; 
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        //Deshabilita un producto que este habilitado 
        public IActionResult Deshabilitar(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                producto.Estado = 0; 
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Verifica si el producto que se nesecita existe
        private bool ExisteElProducto(int id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }

        //Verifica si el producto que se quiere consultar se ha usado en una venta(Detalleventa)
        private bool ExisteEnVentas(int id)
        {
            bool existe = false;
            var detallesVentas = _context.DetalleVentas;
            foreach (var detalleVenta in detallesVentas)
            {
                if(detalleVenta.IdProducto == id)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
    }
}
