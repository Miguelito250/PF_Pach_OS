//Autor: Juan Andres Navarro Herrera
//Fecha se reación: 10 de agosto del 2023
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;


namespace PF_Pach_OS.Controllers
{
    public class RecetasController : ProductosController
    {
        private readonly Pach_OSContext _context;

        
        public RecetasController(Pach_OSContext context) : base(context)
        {
            _context = context;
        }

        

        //Crea un insumo asosiado(Reseta) a un producto tambien guarda la informacion del producto para mostrarla en el formulario 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("IdReceta,CantInsumo,IdProducto,IdInsumo")] Receta receta, [Bind("IdProducto,NomProducto,PrecioVenta,Estado,IdTamano,IdCategoria")] Producto producto)
        {
            bool exite = false;
            var recetaActiva = new Receta();
            _context.Update(producto);
            await _context.SaveChangesAsync();

            if (!ExisteElProducto(producto.IdProducto))
            {
                return NotFound();
            }
           
            var recetasExistente = _context.Recetas.ToList();
            foreach (var rec in recetasExistente)
            {
                if (rec.IdProducto == receta.IdProducto && rec.IdInsumo == receta.IdInsumo)
                {
                    recetaActiva = rec;
                    exite = true;
                    break;
                }
            }
            if (exite)
            {
                recetaActiva.CantInsumo += receta.CantInsumo;
                _context.Update(recetaActiva);
                await _context.SaveChangesAsync();
                return RedirectToAction("CrearInformacionFormulario", "Productos", new { producto.IdProducto });
            }
            else
            {
                _context.Add(receta);
                await _context.SaveChangesAsync();
                return RedirectToAction("CrearInformacionFormulario", "Productos", new { producto.IdProducto });

            }
        }

        //Elimina un insumo asosiado(Reseta) a un producto 
        public async Task<IActionResult> Eliminar(int? id, int IdProducto)
        {
            if (_context.Recetas == null)
            {
                return Problem("Entity set 'Pach_OSContext.Recetas'  is null.");
            }
            var receta = await _context.Recetas.FindAsync(id);
            if (receta != null)
            {
                _context.Recetas.Remove(receta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("CrearInformacionFormulario", "Productos", new { IdProducto });
        }


        //Verifica si el producto que se nesecita existe
        private bool ExisteElProducto(int id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }

    }
}
