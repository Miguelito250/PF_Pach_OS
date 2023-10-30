//Autor: Juan Andres Navarro Herrera
//Fecha se reación: 10 de agosto del 2023
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;
using PF_Pach_OS.Services;

namespace PF_Pach_OS.Controllers
{


    [Authorize]
    public class ProductosController : Controller
    {
        private readonly Pach_OSContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        public readonly PermisosController _permisosController;




        public ProductosController(Pach_OSContext context, UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignInManager)
        {
            _context = context;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
            _permisosController = new PermisosController(_context,_UserManager, _SignInManager) ;
        }

        
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
            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }

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
        private void ProductoActivo(int id)
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
            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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
            ViewBag.Productos = new SelectList(_context.Productos.Where(p => p.Estado == 1 && p.IdProducto > 4), "IdProducto", "NomProducto");
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
            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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
            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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

            return RedirectToAction("CrearInformacionFormulario", "Productos", new { producto.IdProducto });
        }

        
        //Habilita un producto que este deshabilitado 
        public IActionResult Habilitar(int id)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
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
            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                producto.Estado = 0;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Se llama la vista para Editar 
        public IActionResult Informacin_Editar(int IdProducto)
        {

            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            var producto_Original = _context.Productos.FirstOrDefault(p => p.IdProducto == IdProducto);

            if (producto_Original != null)
            {

                ViewBag.IdProducto = producto_Original.IdProducto;
                ViewBag.ProductoActivo = producto_Original;
                var categoriaActiva = _context.Categorias.FirstOrDefault(p => p.IdCategoria == producto_Original.IdCategoria);
                var tamanoActivo = _context.Tamanos.FirstOrDefault(p => p.IdTamano == producto_Original.IdTamano);
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
            var recetas = _context.Recetas.Where(p => p.IdProducto == IdProducto).ToList();


            var insumos = _context.Insumos.ToList();

            var recetasConInsumos = recetas.Select(receta => new
            {
                IdReceta = receta.IdReceta,
                CantInsumo = receta.CantInsumo,
                IdInsumo = receta.IdInsumo,
                NomInsumo = insumos.FirstOrDefault(i => i.IdInsumo == receta.IdInsumo)?.NomInsumo,
                IdProducto = receta.IdProducto,
                Medida = insumos.FirstOrDefault(i => i.IdInsumo == receta.IdInsumo)?.Medida,
            }).ToList();

            ViewBag.RecetasConInsumos = recetasConInsumos;
            ViewBag.Productos = _context.Productos.Where(p => p.Estado == 1 && p.IdProducto > 4).ToList();
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NomCategoria");
            ViewData["IdTamano"] = new SelectList(_context.Tamanos, "IdTamano", "NombreTamano");
            ViewBag.NombreTamano = new SelectList(_context.Tamanos, "IdTamano", "NombreTamano");
            ViewBag.Insumo = _context.Insumos;
            ViewBag.IdProducto = IdProducto;
            var producto_editar = _context.Productos.FirstOrDefault(p => p.IdProducto == IdProducto);

            return View("Editar");
        }
        public JsonResult ConsultarNomInsumo(int Idinsumo)
        {
            var insumo = _context.Insumos.Where(p => p.IdInsumo == Idinsumo).ToList();

            return Json(insumo);
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
                if (detalleVenta.IdProducto == id)
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

        [HttpPost]
        public IActionResult Interfaz(List<int> Actualizar_Id, List<int> Actualizar_Cantidad, List<int> Crear_id, List<int> Crear_Cantidad, int id_producto, List<int> Eliminar)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            if (Actualizar_Id != null && Actualizar_Cantidad != null)
            {
                Actualizar_recetas(Actualizar_Id, Actualizar_Cantidad);
            }

            if (Crear_id != null && Crear_Cantidad != null)
            {

                Crear_recetas(Crear_id, Crear_Cantidad, id_producto);
            }
            if (Eliminar != null)
            {
                Eliminar_recetas(Eliminar);
            }
            var datos = new { Nombre = "Ejemplo", Edad = 30 };
            return Json(datos);
        }

        private void Actualizar_recetas(List<int> Actualizar_Id, List<int> Actualizar_Cantidad)
        {

            for (int i = 0; i < Actualizar_Id.Count; i++)
            {
                int idReceta = Actualizar_Id[i];
                int cantidad = Actualizar_Cantidad[i];

                Receta receta = _context.Recetas.FirstOrDefault(p => p.IdReceta == idReceta);
                if (receta != null)
                {

                    receta.CantInsumo = cantidad;
                    _context.Update(receta);
                    _context.SaveChanges();
                }


            }
        }
        private void Crear_recetas(List<int> Crear_id, List<int> Crear_Cantidad, int id_producto)
        {


            for (int i = 0; i < Crear_id.Count; i++)
            {

                Receta receta = new Receta();
                receta.IdProducto = id_producto;
                receta.IdInsumo = Crear_id[i];
                receta.CantInsumo = Crear_Cantidad[i];
                _context.Add(receta);
                _context.SaveChanges();

            }
        }
        private void Eliminar_recetas(List<int> Eliminar)
        {
            foreach (int i in Eliminar)
            {
                Receta receta = _context.Recetas.Where(p => p.IdReceta == i).FirstOrDefault();
                _context.Remove(receta);
                _context.SaveChanges();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Actualizar_Producto([Bind("IdProducto, NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto)
        {
            var user = User;

            bool tine_permiso = _permisosController.tinto(3, User);
            if (!tine_permiso)
            {
                return RedirectToAction("AccesoDenegado", "Acceso");
            }
            producto.Estado = 1;
            if (producto != null)
            {
                _context.Update(producto);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Exportar_Receta([Bind("IdProducto,NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto, int IdSeleccionado)
        {
            _context.Update(producto);
            _context.SaveChanges();
            var recetas_Existente = _context.Recetas.Where(p => p.IdProducto == producto.IdProducto).ToList();
            var recetas = _context.Recetas.Where(p => p.IdProducto == IdSeleccionado).ToList();
            

            
            foreach (var receta in recetas)
            {
                
                var receta_Existente = recetas_Existente.FirstOrDefault(re => re.IdInsumo == receta.IdInsumo);

                if (receta_Existente != null)
                {
                    
                    receta_Existente.CantInsumo = receta.CantInsumo;
                    _context.Recetas.Update(receta_Existente);
                }
                else
                {
                   
                    var nuevaReceta = new Receta
                    {
                        IdProducto = producto.IdProducto,
                        IdInsumo = receta.IdInsumo,
                        CantInsumo = receta.CantInsumo
                    };

                    _context.Recetas.Add(nuevaReceta);
                }
            }


            _context.SaveChanges();

            return RedirectToAction("CrearInformacionFormulario", "Productos", new { producto.IdProducto });
        }
        public JsonResult ConsultarRecetas(int Id_PRoducto)
        {
            var Recetas = _context.Recetas.Where(p => p.IdProducto == Id_PRoducto).ToList();
            var insumos = _context.Insumos.ToList();
            var Recetas_Insumos = Recetas.Select(receta => new
            {
                CantInsumo = receta.CantInsumo,
                IdInsumo = receta.IdInsumo,
                NomInsumo = insumos.FirstOrDefault(i => i.IdInsumo == receta.IdInsumo)?.NomInsumo,
                Medida = insumos.FirstOrDefault(i => i.IdInsumo == receta.IdInsumo)?.Medida,
            }).ToList();
            return Json(Recetas_Insumos);
        }

    }
}
