using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PF_Pach_OS.Models;

namespace PF_Pach_OS.Controllers
{
    public class InsumosController : Controller
    {
        private readonly Pach_OSContext _context;

        public InsumosController(Pach_OSContext context)
        {
            _context = context;
        }





        // GET: Insumos
        public async Task<IActionResult> Index()
        {
              return _context.Insumos != null ? 
                          View(await _context.Insumos.ToListAsync()) :
                          Problem("Entity set 'Pach_OSContext.Insumos'  is null.");
            
        }




        // GET: Insumos/Create
        public IActionResult Create()
        {
            return View();
        }





        // POST: Insumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumo,NomInsumo,CantInsumo,Medida,Estado")] Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                insumo.CantInsumo = 0;
                insumo.Estado = 1;
                insumo.NomInsumo = Ortografia(insumo.NomInsumo);

                if (insumo.Medida == "Gramo")
                {
                    insumo.CantInsumo = insumo.CantInsumo;
                    insumo.Medida = "Gramo";
                }
                else if (insumo.Medida == "Kilogramo")
                {
                    var convercion = insumo.CantInsumo * 1000;
                    insumo.CantInsumo = convercion;
                    insumo.Medida = "Gramo";
                }
                else if (insumo.Medida == "Mililitro")
                {
                    insumo.CantInsumo += insumo.CantInsumo;
                    insumo.Medida = "Mililitro";
                }
                else if (insumo.Medida == "Onza")
                {
                    var convercion = insumo.CantInsumo * 30;
                    insumo.CantInsumo = convercion;
                    insumo.Medida = "Mililitro";
                }
                else if (insumo.Medida == "Litro")
                {
                    var convercion = insumo.CantInsumo * 1000;
                    insumo.CantInsumo = convercion;
                    insumo.Medida = "Mililitro";
                }
                else if (insumo.Medida == "Unidad")
                {
                    insumo.CantInsumo = insumo.CantInsumo;
                    insumo.Medida = "Unidad";
                }

                _context.Add(insumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public string Ortografia(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada))
            {
                return entrada;
            }
            // Divide la cadena en palabras y aplica la transformación a cada una
            string[] palabra = entrada.Split(' ');
            for (int i = 0; i < palabra.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(palabra[i]))
                {
                    palabra[i] = char.ToUpper(palabra[i][0]) + palabra[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", palabra);
        }



        public IActionResult NombreDuplicado(string Nombre)
        {
            var EsDuplicado = _context.Insumos.Any(x => x.NomInsumo == Nombre);
            return Json(EsDuplicado);
        }



        
        public IActionResult HabilitarDeshabilitar(int? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = _context.Insumos.Find(id);
            if (insumo == null)
            {
                return NotFound();
            }

            // Verificar si el insumo está asociado a un producto
            var buscarAsociacion = _context.Recetas.Any(p => p.IdInsumo == id);
            if (buscarAsociacion)
            {
                // Mostrar una alerta de SweetAlert indicando que no se puede deshabilitar
                var resultado = new
                {
                    success = false,
                    message = "No se puede deshabilitar este insumo porque está asociado a una receta."
                };

                return Json(resultado);
            }

            if (insumo.Estado == 1)
            {
                insumo.Estado = 0;
            }else{
                insumo.Estado = 1;
            }

            _context.SaveChanges();

            var successMessage = insumo.Estado == 1 ? "Insumo habilitado." : "Insumo deshabilitado.";

            var response = new
            {
                success = true,
                message = successMessage
            };

            return Json(response);
        }


        public IActionResult EditarInsumo(int id)
        {
            var insumo = _context.Insumos.FirstOrDefault(x => x.IdInsumo == id);
            if (insumo == null)
            {
                return NotFound();
            }
            return PartialView("_EditarInsumo", insumo);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarEdicionInsumo(int id, [Bind("IdInsumo,NomInsumo,CantInsumo,Medida,Estado")] Insumo insumo)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar el insumo en la base de datos
                    insumo.NomInsumo = Ortografia(insumo.NomInsumo);
                    _context.Update(insumo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsumoExists(insumo.IdInsumo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Si llegamos aquí, hubo un error de validación, regresa a la vista de edición
            return PartialView("_EditarInsumo", insumo);
        }



        private bool InsumoExists(int id)
        {
          return (_context.Insumos?.Any(e => e.IdInsumo == id)).GetValueOrDefault();
        }
    }
}
